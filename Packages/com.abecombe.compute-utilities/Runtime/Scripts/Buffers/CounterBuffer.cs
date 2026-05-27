using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.ComputeUtilities
{
    public class CounterBuffer : GraphicsBufferBase<uint>
    {
        public override GraphicsBuffer.Target BufferTarget => GraphicsBuffer.Target.Counter;

        public override int Length => 1;

        public IndirectArgumentsBuffer CountBuffer { get; } = new();

        public void Init()
        {
            Dispose();
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

    public static class CounterBufferExtensions
    {
        public static void SetCounterBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, CounterBuffer buffer, bool resetCounter = false)
        {
            if (resetCounter) buffer.ResetCounter();
            cs.SetBuffer(kernel, name, buffer.Data);
        }
        public static void SetCounterBuffer(this ComputeKernel kernel, string name, CounterBuffer buffer, bool resetCounter = false)
        {
            kernel.Program.SetCounterBuffer(kernel, name, buffer, resetCounter);
        }

        public static void SetCounterCountBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, CounterBuffer buffer)
        {
            buffer.UpdateCountBuffer();
            cs.SetBuffer(kernel, name, buffer.CountBuffer);
        }
        public static void SetCounterCountBuffer(this ComputeKernel kernel, string name, CounterBuffer buffer)
        {
            kernel.Program.SetCounterCountBuffer(kernel, name, buffer);
        }

        public static void SetCounterBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, CounterBuffer buffer, bool resetCounter = false)
        {
            if (resetCounter) buffer.ResetCounter(cb);
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetCounterBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, CounterBuffer buffer, bool resetCounter = false)
        {
            kernel.Program.SetCounterBuffer(cb, kernel, name, buffer, resetCounter);
        }

        public static void SetCounterCountBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, CounterBuffer buffer)
        {
            buffer.UpdateCountBuffer(cb);
            cs.SetBuffer(cb, kernel, name, buffer.CountBuffer);
        }
        public static void SetCounterCountBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, CounterBuffer buffer)
        {
            kernel.Program.SetCounterCountBuffer(cb, kernel, name, buffer);
        }

        public static void SetCounterBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, CounterBuffer buffer, bool resetCounter = false)
        {
            if (resetCounter) buffer.ResetCounter(cb);
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetCounterBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, CounterBuffer buffer, bool resetCounter = false)
        {
            kernel.Program.SetCounterBuffer(cb, kernel, name, buffer, resetCounter);
        }

        public static void SetCounterCountBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, CounterBuffer buffer)
        {
            buffer.UpdateCountBuffer(cb);
            cs.SetBuffer(cb, kernel, name, buffer.CountBuffer);
        }
        public static void SetCounterCountBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, CounterBuffer buffer)
        {
            kernel.Program.SetCounterCountBuffer(cb, kernel, name, buffer);
        }
    }
}
