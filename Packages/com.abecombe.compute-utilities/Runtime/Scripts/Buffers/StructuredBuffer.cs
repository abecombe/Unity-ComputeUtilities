using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.ComputeUtilities
{
    public interface IStructuredBuffer : IGraphicsBuffer
    {
        public int3 Size { get; }
        public int3 StartIndex { get; }
        public int3 EndIndex { get; }
        public float3 PositionOffset { get; }

        public void Init(int size);
        public void Init(int2 size);
        public void Init(int3 size);

        public void SetStartIndex(int startIndex);
        public void SetStartIndex(int2 startIndex);
        public void SetStartIndex(int3 startIndex);

        public void SetPositionOffset(float offset);
        public void SetPositionOffset(float2 offset);
        public void SetPositionOffset(float3 offset);
    }
    public interface IPingPongStructuredBuffer : IDisposable
    {
        public IStructuredBuffer Read { get; }
        public IStructuredBuffer Write { get; }
        public IStructuredBuffer SimulationBuffer { get; }
        public IStructuredBuffer RenderingBuffer { get; }

        public int Length { get; }
        public int Stride { get; }
        public int Bytes { get; }
        public int3 Size { get; }
        public int3 StartIndex { get; }
        public int3 EndIndex { get; }
        public float3 PositionOffset { get; }

        public bool IsInitialized { get; }

        public void Init(int size);
        public void Init(int2 size);
        public void Init(int3 size);

        public void SetStartIndex(int startIndex);
        public void SetStartIndex(int2 startIndex);
        public void SetStartIndex(int3 startIndex);

        public void SetPositionOffset(float offset);
        public void SetPositionOffset(float2 offset);
        public void SetPositionOffset(float3 offset);

        public void Swap();

        public void CopyFromReadToWrite();
        public void CopyFromReadToWrite(CommandBuffer cb);
        public void CopyFromReadToWrite(IComputeCommandBuffer cb);
    }

    public class StructuredBuffer<T> : GraphicsBufferBase<T>, IStructuredBuffer
        where T : struct
    {
        public override GraphicsBuffer.Target BufferTarget => GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.Raw;

        public override int Length => Size.x * Size.y * Size.z;
        public int3 Size { get; protected set; }
        public int3 StartIndex { get; protected set; }
        public int3 EndIndex => StartIndex + Size - 1;
        public float3 PositionOffset { get; protected set; }

        internal static bool ValidateSize(int3 size)
        {
            if (size.x <= 0 || size.y <= 0 || size.z <= 0)
            {
                Debug.LogError("StructuredBuffer size must be greater than zero in every dimension.");
                return false;
            }

            if ((long)size.x * size.y * size.z > int.MaxValue)
            {
                Debug.LogError("StructuredBuffer length exceeds the supported maximum size.");
                return false;
            }

            return true;
        }

        public void Init(int size)
        {
            Init(new int3(size, 1, 1));
        }
        public void Init(int2 size)
        {
            Init(new int3(size, 1));
        }
        public void Init(int3 size)
        {
            Dispose();
            if (!ValidateSize(size))
            {
                Size = int3.zero;
                return;
            }

            Size = size;
            InitBufferProgram();
            Clear();
            SetStartIndex(int3.zero);
            SetPositionOffset(new float3(0.5f, 0.5f, 0.5f));
            IsInitialized = true;
        }

        public void SetStartIndex(int startIndex)
        {
            SetStartIndex(new int3(startIndex, 0, 0));
        }
        public void SetStartIndex(int2 startIndex)
        {
            SetStartIndex(new int3(startIndex, 0));
        }
        public void SetStartIndex(int3 startIndex)
        {
            StartIndex = startIndex;
        }

        public void SetPositionOffset(float offset)
        {
            SetPositionOffset(new float3(offset, 0.5f, 0.5f));
        }
        public void SetPositionOffset(float2 offset)
        {
            SetPositionOffset(new float3(offset, 0.5f));
        }
        public void SetPositionOffset(float3 offset)
        {
            PositionOffset = offset;
        }
    }

    public class PingPongStructuredBuffer<T> : IPingPongStructuredBuffer
        where T : struct
    {
        public StructuredBuffer<T> Buffer1 { get; protected set; } = new();
        public StructuredBuffer<T> Buffer2 { get; protected set; } = new();

        public IStructuredBuffer Read => Buffer1;
        public IStructuredBuffer Write => Buffer2;
        // for AsyncCompute
        public IStructuredBuffer SimulationBuffer => ComputeShaderUtility.SimulationUseBuffer1 ? Buffer1 : Buffer2;
        public IStructuredBuffer RenderingBuffer => ComputeShaderUtility.RenderingUseBuffer1 ? Buffer1 : Buffer2;

        public int Length => Read.Length;
        public int Stride => Read.Stride;
        public int Bytes => Read.Bytes;
        public int3 Size => Read.Size;
        public int3 StartIndex => Read.StartIndex;
        public int3 EndIndex => Read.EndIndex;
        public float3 PositionOffset => Read.PositionOffset;

        public bool IsInitialized { get; private set; } = false;

        public void Init(int size)
        {
            Init(new int3(size, 1, 1));
        }
        public void Init(int2 size)
        {
            Init(new int3(size, 1));
        }
        public void Init(int3 size)
        {
            Dispose();
            if (!StructuredBuffer<T>.ValidateSize(size))
                return;

            Buffer1.Init(size);
            Buffer2.Init(size);
            IsInitialized = true;
        }

        public void Dispose()
        {
            if (IsInitialized)
            {
                Buffer1.Dispose();
                Buffer2.Dispose();
            }
            IsInitialized = false;
        }

        public void SetStartIndex(int startIndex)
        {
            SetStartIndex(new int3(startIndex, 0, 0));
        }
        public void SetStartIndex(int2 startIndex)
        {
            SetStartIndex(new int3(startIndex, 0));
        }
        public void SetStartIndex(int3 startIndex)
        {
            Buffer1.SetStartIndex(startIndex);
            Buffer2.SetStartIndex(startIndex);
        }

        public void SetPositionOffset(float offset)
        {
            SetPositionOffset(new float3(offset, 0.5f, 0.5f));
        }
        public void SetPositionOffset(float2 offset)
        {
            SetPositionOffset(new float3(offset, 0.5f));
        }
        public void SetPositionOffset(float3 offset)
        {
            Buffer1.SetPositionOffset(offset);
            Buffer2.SetPositionOffset(offset);
        }

        public void Swap()
        {
            (Buffer1, Buffer2) = (Buffer2, Buffer1);
        }

        public void CopyFromReadToWrite()
        {
            Buffer1.CopyTo(Buffer2);
        }
        public void CopyFromReadToWrite(CommandBuffer cb)
        {
            Buffer1.CopyTo(cb, Buffer2);
        }
        public void CopyFromReadToWrite(IComputeCommandBuffer cb)
        {
            Buffer1.CopyTo(cb, Buffer2);
        }
    }

    public static class StructuredBufferExtensions
    {
        public static void SetStructuredBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, IStructuredBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.StructuredBufferConcatNames);
            int count = 0;
            cs.SetBuffer(kernel, propertyIDs[count++], buffer.Data);
            cs.SetInt(propertyIDs[count++], buffer.Length);
            cs.SetInts(propertyIDs[count++], buffer.Size);
            cs.SetInts(propertyIDs[count++], buffer.StartIndex);
            cs.SetInts(propertyIDs[count++], buffer.EndIndex);
            cs.SetVector(propertyIDs[count++], buffer.PositionOffset);
            cs.SetVector(propertyIDs[count++], (float3)0.5f - buffer.PositionOffset);
            cs.SetVector(propertyIDs[count++], -buffer.PositionOffset);
        }
        public static void SetStructuredBuffer(this ComputeKernel kernel, string name, IStructuredBuffer buffer)
        {
            kernel.Program.SetStructuredBuffer(kernel, name, buffer);
        }

        public static void SetStructuredBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, IStructuredBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.StructuredBufferConcatNames);
            int count = 0;
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer.Data);
            cs.SetInt(cb, propertyIDs[count++], buffer.Length);
            cs.SetInts(cb, propertyIDs[count++], buffer.Size);
            cs.SetInts(cb, propertyIDs[count++], buffer.StartIndex);
            cs.SetInts(cb, propertyIDs[count++], buffer.EndIndex);
            cs.SetVector(cb, propertyIDs[count++], buffer.PositionOffset);
            cs.SetVector(cb, propertyIDs[count++], (float3)0.5f - buffer.PositionOffset);
            cs.SetVector(cb, propertyIDs[count++], -buffer.PositionOffset);
        }
        public static void SetStructuredBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, IStructuredBuffer buffer)
        {
            kernel.Program.SetStructuredBuffer(cb, kernel, name, buffer);
        }

        public static void SetStructuredBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, IStructuredBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.StructuredBufferConcatNames);
            int count = 0;
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer.Data);
            cs.SetInt(cb, propertyIDs[count++], buffer.Length);
            cs.SetInts(cb, propertyIDs[count++], buffer.Size);
            cs.SetInts(cb, propertyIDs[count++], buffer.StartIndex);
            cs.SetInts(cb, propertyIDs[count++], buffer.EndIndex);
            cs.SetVector(cb, propertyIDs[count++], buffer.PositionOffset);
            cs.SetVector(cb, propertyIDs[count++], (float3)0.5f - buffer.PositionOffset);
            cs.SetVector(cb, propertyIDs[count++], -buffer.PositionOffset);
        }
        public static void SetStructuredBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, IStructuredBuffer buffer)
        {
            kernel.Program.SetStructuredBuffer(cb, kernel, name, buffer);
        }
    }
}
