using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.ComputeUtilities
{
    public interface IAppendConsumeBuffer : IGraphicsBuffer
    {
        public IndirectArgumentsBuffer CountBuffer { get; }

        public void Init(int size);

        public void UpdateCountBuffer();
        public void CopyCountTo(GraphicsBufferBase<uint> dest, int destOffset = 0);
        public void UpdateCountBuffer(CommandBuffer cb);
        public void CopyCountTo(CommandBuffer cb, GraphicsBufferBase<uint> dest, int destOffset = 0);
        public void UpdateCountBuffer(IComputeCommandBuffer cb);
        public void CopyCountTo(IComputeCommandBuffer cb, GraphicsBufferBase<uint> dest, int destOffset = 0);

        public void SetCounterValue(uint value);
        public void ResetCounter();
        public void SetCounterValue(CommandBuffer cb, uint value);
        public void ResetCounter(CommandBuffer cb);
        public void SetCounterValue(IComputeCommandBuffer cb, uint value);
        public void ResetCounter(IComputeCommandBuffer cb);

        public uint GetCounterValue();
    }

    public class AppendConsumeBuffer<T> : GraphicsBufferBase<T>, IAppendConsumeBuffer
        where T : struct
    {
        public override GraphicsBuffer.Target BufferTarget => GraphicsBuffer.Target.Append;

        public override int Length => _length;
        protected int _length;

        public IndirectArgumentsBuffer CountBuffer { get; } = new();

        public void Init(int size)
        {
            Dispose();
            if (size <= 0)
            {
                _length = 0;
                Debug.LogError("AppendConsumeBuffer size must be greater than zero.");
                return;
            }

            _length = size;
            InitBufferProgram();
            ResetCounter();
            CountBuffer.Init(1);
            IsInitialized = true;
        }

        public override void Dispose()
        {
            if (IsInitialized)
            {
                ReleaseBufferResources();
                CountBuffer.Dispose();
            }
            IsInitialized = false;
        }

        public void UpdateCountBuffer()
        {
            GraphicsBuffer.CopyCount(Data, CountBuffer, 0);
        }
        public void CopyCountTo(GraphicsBufferBase<uint> dest, int destOffset = 0)
        {
            GraphicsBuffer.CopyCount(Data, dest, destOffset * dest.Stride);
        }
        public void UpdateCountBuffer(CommandBuffer cb)
        {
            cb.CopyCounterValue(Data, CountBuffer, 0);
        }
        public void CopyCountTo(CommandBuffer cb, GraphicsBufferBase<uint> dest, int destOffset = 0)
        {
            cb.CopyCounterValue(Data, dest, (uint)(destOffset * dest.Stride));
        }
        public void UpdateCountBuffer(IComputeCommandBuffer cb)
        {
            cb.CopyCounterValue(Data, CountBuffer, 0);
        }
        public void CopyCountTo(IComputeCommandBuffer cb, GraphicsBufferBase<uint> dest, int destOffset = 0)
        {
            cb.CopyCounterValue(Data, dest, (uint)(destOffset * dest.Stride));
        }

        public void SetCounterValue(uint value)
        {
            Data.SetCounterValue(value);
        }
        public void ResetCounter()
        {
            SetCounterValue(0);
        }
        public void SetCounterValue(CommandBuffer cb, uint value)
        {
            cb.SetBufferCounterValue(Data, value);
        }
        public void ResetCounter(CommandBuffer cb)
        {
            SetCounterValue(cb, 0);
        }
        public void SetCounterValue(IComputeCommandBuffer cb, uint value)
        {
            cb.SetBufferCounterValue(Data, value);
        }
        public void ResetCounter(IComputeCommandBuffer cb)
        {
            SetCounterValue(cb, 0);
        }

        public uint GetCounterValue()
        {
            UpdateCountBuffer();

            CountBuffer.GetDataToArgs(0, 0, 1);
            return CountBuffer.Args[0];
        }
    }

    public static class AppendConsumeBufferExtensions
    {
        public static void SetAppendBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, IAppendConsumeBuffer buffer, bool resetBuffer = false)
        {
            if (resetBuffer) buffer.ResetCounter();
            cs.SetBuffer(kernel, name, buffer.Data);
        }
        public static void SetAppendBuffer(this ComputeKernel kernel, string name, IAppendConsumeBuffer buffer, bool resetBuffer = false)
        {
            kernel.Program.SetAppendBuffer(kernel, name, buffer, resetBuffer);
        }

        public static void SetAppendBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, IAppendConsumeBuffer buffer, bool resetBuffer = false)
        {
            if (resetBuffer) buffer.ResetCounter(cb);
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetAppendBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, IAppendConsumeBuffer buffer, bool resetBuffer = false)
        {
            kernel.Program.SetAppendBuffer(cb, kernel, name, buffer, resetBuffer);
        }

        public static void SetAppendBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, IAppendConsumeBuffer buffer, bool resetBuffer = false)
        {
            if (resetBuffer) buffer.ResetCounter(cb);
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetAppendBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, IAppendConsumeBuffer buffer, bool resetBuffer = false)
        {
            kernel.Program.SetAppendBuffer(cb, kernel, name, buffer, resetBuffer);
        }

        public static void SetConsumeBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, IAppendConsumeBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.AppendConsumeBufferConcatNames);
            int count = 0;
            buffer.UpdateCountBuffer();
            cs.SetBuffer(kernel, propertyIDs[count++], buffer.Data);
            cs.SetBuffer(kernel, propertyIDs[count++], buffer.CountBuffer);
        }
        public static void SetConsumeBuffer(this ComputeKernel kernel, string name, IAppendConsumeBuffer buffer)
        {
            kernel.Program.SetConsumeBuffer(kernel, name, buffer);
        }

        public static void SetConsumeBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, IAppendConsumeBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.AppendConsumeBufferConcatNames);
            int count = 0;
            buffer.UpdateCountBuffer(cb);
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer.Data);
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer.CountBuffer);
        }
        public static void SetConsumeBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, IAppendConsumeBuffer buffer)
        {
            kernel.Program.SetConsumeBuffer(cb, kernel, name, buffer);
        }

        public static void SetConsumeBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, IAppendConsumeBuffer buffer)
        {
            var propertyIDs = cs.GetPropertyIDs(name, ComputeShaderUtility.AppendConsumeBufferConcatNames);
            int count = 0;
            buffer.UpdateCountBuffer(cb);
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer.Data);
            cs.SetBuffer(cb, kernel, propertyIDs[count++], buffer.CountBuffer);
        }
        public static void SetConsumeBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, IAppendConsumeBuffer buffer)
        {
            kernel.Program.SetConsumeBuffer(cb, kernel, name, buffer);
        }
    }
}