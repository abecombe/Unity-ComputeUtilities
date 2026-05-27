using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.ComputeUtilities
{
    public class IndirectArgumentsBuffer : GraphicsBufferBase<uint>
    {
        public override GraphicsBuffer.Target BufferTarget => GraphicsBuffer.Target.IndirectArguments | GraphicsBuffer.Target.Raw;

        public override int Length => Args?.Length ?? 0;

        public uint[] Args { get; protected set; }

        public DispatchIndirectArgumentsBuffer DispatchIndirectArgumentsBuffer { get; } = new();
        public int CountBufferOffset { get; protected set; }
        public int CountBufferSize { get; protected set; }

        public void Init(int size, int countBufferOffset = 0, int countBufferSize = 1)
        {
            Dispose();
            if (size <= 0)
            {
                Args = Array.Empty<uint>();
                CountBufferOffset = 0;
                CountBufferSize = 0;
                Debug.LogError("IndirectArgumentsBuffer size must be greater than zero.");
                return;
            }

            Args = new uint[size];
            InitPrivate(countBufferOffset, countBufferSize);
        }
        public void Init(uint[] args, int countBufferOffset = 0, int countBufferSize = 1)
        {
            Dispose();
            if (args == null || args.Length == 0)
            {
                Args = Array.Empty<uint>();
                CountBufferOffset = 0;
                CountBufferSize = 0;
                Debug.LogError("IndirectArgumentsBuffer args must contain at least one element.");
                return;
            }

            Args = (uint[])args.Clone();
            InitPrivate(countBufferOffset, countBufferSize);
        }
        protected void InitPrivate(int countBufferOffset, int countBufferSize)
        {
            var clampedCountBufferSize = math.clamp(countBufferSize, 1, 3);
            if (countBufferOffset < 0 || countBufferOffset + clampedCountBufferSize > Args.Length)
            {
                CountBufferOffset = 0;
                CountBufferSize = 0;
                Debug.LogError("IndirectArgumentsBuffer count range is outside the args buffer.");
                return;
            }

            InitBufferProgram();
            SetData(Args);
            CountBufferOffset = countBufferOffset;
            CountBufferSize = clampedCountBufferSize;
            DispatchIndirectArgumentsBuffer.Init(this, CountBufferOffset, CountBufferSize);
            IsInitialized = true;
        }

        public override void Dispose()
        {
            if (IsInitialized)
            {
                ReleaseBufferResources();
                DispatchIndirectArgumentsBuffer.Dispose();
            }
            IsInitialized = false;
            CountBufferOffset = 0;
            CountBufferSize = 0;
        }

        public void SetDataFromArgs()
        {
            SetData(Args);
        }
        public void SetDataFromArgs(int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
        {
            SetData(Args, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetDataFromArgs(CommandBuffer cb)
        {
            SetData(cb, Args);
        }
        public void SetDataFromArgs(CommandBuffer cb, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
        {
            SetData(cb, Args, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetDataFromArgs(IComputeCommandBuffer cb)
        {
            SetData(cb, Args);
        }
        public void SetDataFromArgs(IComputeCommandBuffer cb, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
        {
            SetData(cb, Args, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void GetDataToArgs()
        {
            GetData(Args);
        }
        public void GetDataToArgs(int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
        {
            GetData(Args, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }

        public uint3 GetCount()
        {
            GetDataToArgs(CountBufferOffset, CountBufferOffset, CountBufferSize);

            return CountBufferSize switch
            {
                1 => new uint3(Args[CountBufferOffset], 0, 0),
                2 => new uint3(Args[CountBufferOffset], Args[CountBufferOffset + 1], 0),
                3 => new uint3(Args[CountBufferOffset], Args[CountBufferOffset + 1], Args[CountBufferOffset + 2]),
                _ => new uint3(0, 0, 0)
            };
        }

        public void SetCount(uint count)
        {
            if (CountBufferSize != 1)
            {
                Debug.LogWarning("CountBufferSize is not 1.");
                return;
            }
            Args[CountBufferOffset] = count;
            SetDataFromArgs(CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(uint2 count)
        {
            if (CountBufferSize != 2)
            {
                Debug.LogWarning("CountBufferSize is not 2.");
                return;
            }
            Args[CountBufferOffset] = count.x;
            Args[CountBufferOffset + 1] = count.y;
            SetDataFromArgs(CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(uint3 count)
        {
            if (CountBufferSize != 3)
            {
                Debug.LogWarning("CountBufferSize is not 3.");
                return;
            }
            Args[CountBufferOffset] = count.x;
            Args[CountBufferOffset + 1] = count.y;
            Args[CountBufferOffset + 2] = count.z;
            SetDataFromArgs(CountBufferOffset, CountBufferOffset, CountBufferSize);
        }

        public void SetCount(CommandBuffer cb, uint count)
        {
            if (CountBufferSize != 1)
            {
                Debug.LogWarning("CountBufferSize is not 1.");
                return;
            }
            Args[CountBufferOffset] = count;
            SetDataFromArgs(cb, CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(CommandBuffer cb, uint2 count)
        {
            if (CountBufferSize != 2)
            {
                Debug.LogWarning("CountBufferSize is not 2.");
                return;
            }
            Args[CountBufferOffset] = count.x;
            Args[CountBufferOffset + 1] = count.y;
            SetDataFromArgs(cb, CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(CommandBuffer cb, uint3 count)
        {
            if (CountBufferSize != 3)
            {
                Debug.LogWarning("CountBufferSize is not 3.");
                return;
            }
            Args[CountBufferOffset] = count.x;
            Args[CountBufferOffset + 1] = count.y;
            Args[CountBufferOffset + 2] = count.z;
            SetDataFromArgs(cb, CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(IComputeCommandBuffer cb, uint count)
        {
            if (CountBufferSize != 1)
            {
                Debug.LogWarning("CountBufferSize is not 1.");
                return;
            }
            Args[CountBufferOffset] = count;
            SetDataFromArgs(cb, CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(IComputeCommandBuffer cb, uint2 count)
        {
            if (CountBufferSize != 2)
            {
                Debug.LogWarning("CountBufferSize is not 2.");
                return;
            }
            Args[CountBufferOffset] = count.x;
            Args[CountBufferOffset + 1] = count.y;
            SetDataFromArgs(cb, CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
        public void SetCount(IComputeCommandBuffer cb, uint3 count)
        {
            if (CountBufferSize != 3)
            {
                Debug.LogWarning("CountBufferSize is not 3.");
                return;
            }
            Args[CountBufferOffset] = count.x;
            Args[CountBufferOffset + 1] = count.y;
            Args[CountBufferOffset + 2] = count.z;
            SetDataFromArgs(cb, CountBufferOffset, CountBufferOffset, CountBufferSize);
        }
    }

    public static class IndirectArgumentsBufferExtensions
    {
        public static void SetIndirectArgumentsBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, IndirectArgumentsBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.IndirectArgumentsBufferConcatNames);
            int count = 0;
            cs.SetBuffer(kernel, propertyIDs[count++], buffer);
            cs.SetInt(propertyIDs[count++], buffer.CountBufferOffset);
            cs.SetInt(propertyIDs[count++], buffer.CountBufferSize);
        }
        public static void SetIndirectArgumentsBuffer(this ComputeKernel kernel, string name, IndirectArgumentsBuffer buffer)
        {
            kernel.Program.SetIndirectArgumentsBuffer(kernel, name, buffer);
        }

        public static void SetIndirectArgumentsBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, IndirectArgumentsBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.IndirectArgumentsBufferConcatNames);
            int count = 0;
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer);
            cs.SetInt(cb, propertyIDs[count++], buffer.CountBufferOffset);
            cs.SetInt(cb, propertyIDs[count++], buffer.CountBufferSize);
        }
        public static void SetIndirectArgumentsBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, IndirectArgumentsBuffer buffer)
        {
            kernel.Program.SetIndirectArgumentsBuffer(cb, kernel, name, buffer);
        }

        public static void SetIndirectArgumentsBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, IndirectArgumentsBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.IndirectArgumentsBufferConcatNames);
            int count = 0;
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer);
            cs.SetInt(cb, propertyIDs[count++], buffer.CountBufferOffset);
            cs.SetInt(cb, propertyIDs[count++], buffer.CountBufferSize);
        }
        public static void SetIndirectArgumentsBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, IndirectArgumentsBuffer buffer)
        {
            kernel.Program.SetIndirectArgumentsBuffer(cb, kernel, name, buffer);
        }
    }
}
