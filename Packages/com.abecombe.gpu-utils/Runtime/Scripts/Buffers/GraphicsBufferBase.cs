using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Abecombe.GpuTools
{
    public interface IGraphicsBuffer : IDisposable
    {
        public GraphicsBuffer Data { get; }
        public GraphicsBuffer.Target BufferTarget { get; }

        public int Length { get; }
        public int Stride { get; }
        public int Bytes { get; }

        public bool IsInitialized { get; }

        public void SetData<U>(U[] data) where U : struct;
        public void SetData<U>(U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(List<U> data) where U : struct;
        public void SetData<U>(List<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(NativeArray<U> data) where U : struct;
        public void SetData<U>(NativeArray<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(CommandBuffer cb, U[] data) where U : struct;
        public void SetData<U>(CommandBuffer cb, U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(CommandBuffer cb, List<U> data) where U : struct;
        public void SetData<U>(CommandBuffer cb, List<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(CommandBuffer cb, NativeArray<U> data) where U : struct;
        public void SetData<U>(CommandBuffer cb, NativeArray<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(IComputeCommandBuffer cb, U[] data) where U : struct;
        public void SetData<U>(IComputeCommandBuffer cb, U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(IComputeCommandBuffer cb, List<U> data) where U : struct;
        public void SetData<U>(IComputeCommandBuffer cb, List<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void SetData<U>(IComputeCommandBuffer cb, NativeArray<U> data) where U : struct;
        public void SetData<U>(IComputeCommandBuffer cb, NativeArray<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;
        public void GetData<U>(U[] data) where U : struct;
        public void GetData<U>(U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct;

        public void CopyTo(IGraphicsBuffer toBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1);
        public void CopyFrom(IGraphicsBuffer fromBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1);
        public void CopyTo(CommandBuffer cb, IGraphicsBuffer toBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1);
        public void CopyFrom(CommandBuffer cb, IGraphicsBuffer fromBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1);
        public void CopyTo(IComputeCommandBuffer cb, IGraphicsBuffer toBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1);
        public void CopyFrom(IComputeCommandBuffer cb, IGraphicsBuffer fromBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1);

        public void Clear();
        public void Clear(CommandBuffer cmd);
        public void Clear(IComputeCommandBuffer cmd);
    }

    public abstract class GraphicsBufferBase<T> : IGraphicsBuffer
        where T : struct
    {
        public GraphicsBuffer Data { get; protected set; }
        public abstract GraphicsBuffer.Target BufferTarget { get; }

        public abstract int Length { get; }
        public int Stride => Data.stride;
        public int Bytes => Length * Stride;

        protected ComputeProgram _utilityProgram = new();
        private ComputeShader _utilityShaderInstance;

        public bool IsInitialized { get; protected set; } = false;

        protected void InitBufferProgram()
        {
            Data = new GraphicsBuffer(BufferTarget, Length, Marshal.SizeOf(typeof(T)));
            var utilityShader = ComputeShaderUtility.LoadUtilityShader();
            _utilityShaderInstance = utilityShader == null ? null : Object.Instantiate(utilityShader);
            _utilityProgram.Init(_utilityShaderInstance);
        }

        protected void ReleaseBufferResources()
        {
            Data?.Release();
            Data = null;

            if (_utilityShaderInstance != null)
            {
                Object.Destroy(_utilityShaderInstance);
                _utilityShaderInstance = null;
            }
        }

        public virtual void Dispose()
        {
            if (IsInitialized)
            {
                ReleaseBufferResources();
            }
            IsInitialized = false;
        }

        public void SetData<U>(U[] data) where U : struct
        {
            Data.SetData(data);
        }
        public void SetData<U>(U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            Data.SetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(List<U> data) where U : struct
        {
            Data.SetData(data);
        }
        public void SetData<U>(List<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            Data.SetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(NativeArray<U> data) where U : struct
        {
            Data.SetData(data);
        }
        public void SetData<U>(NativeArray<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            Data.SetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(CommandBuffer cb, U[] data) where U : struct
        {
            cb.SetBufferData(Data, data);
        }
        public void SetData<U>(CommandBuffer cb, U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            cb.SetBufferData(Data, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(CommandBuffer cb, List<U> data) where U : struct
        {
            cb.SetBufferData(Data, data);
        }
        public void SetData<U>(CommandBuffer cb, List<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            cb.SetBufferData(Data, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(CommandBuffer cb, NativeArray<U> data) where U : struct
        {
            cb.SetBufferData(Data, data);
        }
        public void SetData<U>(CommandBuffer cb, NativeArray<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            cb.SetBufferData(Data, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(IComputeCommandBuffer cb, U[] data) where U : struct
        {
            cb.SetBufferData(Data, data);
        }
        public void SetData<U>(IComputeCommandBuffer cb, U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            cb.SetBufferData(Data, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(IComputeCommandBuffer cb, List<U> data) where U : struct
        {
            cb.SetBufferData(Data, data);
        }
        public void SetData<U>(IComputeCommandBuffer cb, List<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            cb.SetBufferData(Data, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void SetData<U>(IComputeCommandBuffer cb, NativeArray<U> data) where U : struct
        {
            cb.SetBufferData(Data, data);
        }
        public void SetData<U>(IComputeCommandBuffer cb, NativeArray<U> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            cb.SetBufferData(Data, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }
        public void GetData<U>(U[] data) where U : struct
        {
            Data.GetData(data);
        }
        public void GetData<U>(U[] data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where U : struct
        {
            Data.GetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count);
        }

        public void CopyTo(IGraphicsBuffer toBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1)
        {
            if (Stride != toBuffer.Stride)
            {
                Debug.LogError($"Stride mismatch, cannot copy from stride {Stride} to {toBuffer.Stride}.");
                return;
            }
            if (!BufferTarget.HasFlag(GraphicsBuffer.Target.Raw) || !toBuffer.BufferTarget.HasFlag(GraphicsBuffer.Target.Raw))
            {
                Debug.LogError("Copy kernel only supports Raw buffers, please use your own copy method");
                return;
            }

            if (count == -1)
            {
                if (Length != toBuffer.Length)
                {
                    Debug.LogError("Buffer length mismatch, please specify count");
                    return;
                }
                count = Length;
            }
            switch (count)
            {
                case <= 0:
                    return;
                case > 1024 * ComputeLimits.MaxDispatchSize:
                    Debug.LogError("Buffer copy count exceeds maximum dispatch size, please use your own copy method");
                    return;
            }

            var cs = _utilityProgram;
            var kernel = cs.FindKernel(count == 1 ? ComputeShaderUtility.CopyBuffer1KernelName : count <= 32 ? ComputeShaderUtility.CopyBuffer32KernelName : count <= 128 * ComputeLimits.MaxDispatchSize ? ComputeShaderUtility.CopyBuffer128KernelName : ComputeShaderUtility.CopyBuffer1024KernelName);

            int uintScaling = Stride / sizeof(uint);

            cs.SetInt(ComputeShaderUtility.BufferCountShaderPropertyID, count);
            cs.SetInt(ComputeShaderUtility.BufferUIntCountShaderPropertyID, count * uintScaling);
            cs.SetInt(ComputeShaderUtility.FromBufferUIntStartIndexShaderPropertyID, fromBufferStartIndex * uintScaling);
            cs.SetInt(ComputeShaderUtility.ToBufferUIntStartIndexShaderPropertyID, toBufferStartIndex * uintScaling);
            kernel.SetBuffer(ComputeShaderUtility.FromBufferShaderPropertyID, Data);
            kernel.SetBuffer(ComputeShaderUtility.ToBufferShaderPropertyID, toBuffer.Data);

            kernel.DispatchDesired(count);
        }
        public void CopyFrom(IGraphicsBuffer fromBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1)
        {
            fromBuffer.CopyTo(this, fromBufferStartIndex, toBufferStartIndex, count);
        }

        public void CopyTo(CommandBuffer cb, IGraphicsBuffer toBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1)
        {
            if (Stride != toBuffer.Stride)
            {
                Debug.LogError($"Stride mismatch, cannot copy from stride {Stride} to {toBuffer.Stride}.");
                return;
            }
            if (!BufferTarget.HasFlag(GraphicsBuffer.Target.Raw) || !toBuffer.BufferTarget.HasFlag(GraphicsBuffer.Target.Raw))
            {
                Debug.LogError("Copy kernel only supports Raw buffers, please use your own copy method");
                return;
            }

            if (count == -1)
            {
                if (Length != toBuffer.Length)
                {
                    Debug.LogError("Buffer length mismatch, please specify count");
                    return;
                }
                count = Length;
            }
            switch (count)
            {
                case <= 0:
                    return;
                case > 1024 * ComputeLimits.MaxDispatchSize:
                    Debug.LogError("Buffer copy count exceeds maximum dispatch size, please use your own copy method");
                    return;
            }

            var cs = _utilityProgram;
            var kernel = cs.FindKernel(count == 1 ? ComputeShaderUtility.CopyBuffer1KernelName : count <= 32 ? ComputeShaderUtility.CopyBuffer32KernelName : count <= 128 * ComputeLimits.MaxDispatchSize ? ComputeShaderUtility.CopyBuffer128KernelName : ComputeShaderUtility.CopyBuffer1024KernelName);

            int uintScaling = Stride / sizeof(uint);

            cs.SetInt(cb, ComputeShaderUtility.BufferCountShaderPropertyID, count);
            cs.SetInt(cb, ComputeShaderUtility.BufferUIntCountShaderPropertyID, count * uintScaling);
            cs.SetInt(cb, ComputeShaderUtility.FromBufferUIntStartIndexShaderPropertyID, fromBufferStartIndex * uintScaling);
            cs.SetInt(cb, ComputeShaderUtility.ToBufferUIntStartIndexShaderPropertyID, toBufferStartIndex * uintScaling);
            kernel.SetBuffer(cb, ComputeShaderUtility.FromBufferShaderPropertyID, Data);
            kernel.SetBuffer(cb, ComputeShaderUtility.ToBufferShaderPropertyID, toBuffer.Data);

            kernel.DispatchDesired(cb, count);
        }
        public void CopyFrom(CommandBuffer cb, IGraphicsBuffer fromBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1)
        {
            fromBuffer.CopyTo(cb, this, fromBufferStartIndex, toBufferStartIndex, count);
        }

        public void CopyTo(IComputeCommandBuffer cb, IGraphicsBuffer toBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1)
        {
            if (Stride != toBuffer.Stride)
            {
                Debug.LogError($"Stride mismatch, cannot copy from stride {Stride} to {toBuffer.Stride}.");
                return;
            }
            if (!BufferTarget.HasFlag(GraphicsBuffer.Target.Raw) || !toBuffer.BufferTarget.HasFlag(GraphicsBuffer.Target.Raw))
            {
                Debug.LogError("Copy kernel only supports Raw buffers, please use your own copy method");
                return;
            }

            if (count == -1)
            {
                if (Length != toBuffer.Length)
                {
                    Debug.LogError("Buffer length mismatch, please specify count");
                    return;
                }
                count = Length;
            }
            switch (count)
            {
                case <= 0:
                    return;
                case > 1024 * ComputeLimits.MaxDispatchSize:
                    Debug.LogError("Buffer copy count exceeds maximum dispatch size, please use your own copy method");
                    return;
            }

            var cs = _utilityProgram;
            var kernel = cs.FindKernel(count == 1 ? ComputeShaderUtility.CopyBuffer1KernelName : count <= 32 ? ComputeShaderUtility.CopyBuffer32KernelName : count <= 128 * ComputeLimits.MaxDispatchSize ? ComputeShaderUtility.CopyBuffer128KernelName : ComputeShaderUtility.CopyBuffer1024KernelName);

            int uintScaling = Stride / sizeof(uint);

            cs.SetInt(cb, ComputeShaderUtility.BufferCountShaderPropertyID, count);
            cs.SetInt(cb, ComputeShaderUtility.BufferUIntCountShaderPropertyID, count * uintScaling);
            cs.SetInt(cb, ComputeShaderUtility.FromBufferUIntStartIndexShaderPropertyID, fromBufferStartIndex * uintScaling);
            cs.SetInt(cb, ComputeShaderUtility.ToBufferUIntStartIndexShaderPropertyID, toBufferStartIndex * uintScaling);
            kernel.SetBuffer(cb, ComputeShaderUtility.FromBufferShaderPropertyID, Data);
            kernel.SetBuffer(cb, ComputeShaderUtility.ToBufferShaderPropertyID, toBuffer.Data);

            kernel.DispatchDesired(cb, count);
        }
        public void CopyFrom(IComputeCommandBuffer cb, IGraphicsBuffer fromBuffer, int fromBufferStartIndex = 0, int toBufferStartIndex = 0, int count = -1)
        {
            fromBuffer.CopyTo(cb, this, fromBufferStartIndex, toBufferStartIndex, count);
        }

        public void Clear()
        {
            if (!BufferTarget.HasFlag(GraphicsBuffer.Target.Raw))
            {
                Debug.LogError("Clear kernel only supports Raw buffers, please use your own clear method");
                return;
            }
            if (Length > 1024 * ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Buffer clear count exceeds maximum dispatch size, please use your own clear method");
            }

            var count = Length;

            var cs = _utilityProgram;
            var kernel = cs.FindKernel(count == 1 ? ComputeShaderUtility.ClearBuffer1KernelName : count <= 32 ? ComputeShaderUtility.ClearBuffer32KernelName : count <= 128 * ComputeLimits.MaxDispatchSize ? ComputeShaderUtility.ClearBuffer128KernelName : ComputeShaderUtility.ClearBuffer1024KernelName);

            int uintScaling = Stride / sizeof(uint);

            cs.SetInt(ComputeShaderUtility.BufferCountShaderPropertyID, count);
            cs.SetInt(ComputeShaderUtility.BufferUIntCountShaderPropertyID, count * uintScaling);
            kernel.SetBuffer(ComputeShaderUtility.BufferShaderPropertyID, Data);

            kernel.DispatchDesired(count);
        }
        public void Clear(CommandBuffer cb)
        {
            if (!BufferTarget.HasFlag(GraphicsBuffer.Target.Raw))
            {
                Debug.LogError("Clear kernel only supports Raw buffers, please use your own clear method");
                return;
            }
            if (Length > 1024 * ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Buffer clear count exceeds maximum dispatch size, please use your own clear method");
            }

            var count = Length;

            var cs = _utilityProgram;
            var kernel = cs.FindKernel(count == 1 ? ComputeShaderUtility.ClearBuffer1KernelName : count <= 32 ? ComputeShaderUtility.ClearBuffer32KernelName : count <= 128 * ComputeLimits.MaxDispatchSize ? ComputeShaderUtility.ClearBuffer128KernelName : ComputeShaderUtility.ClearBuffer1024KernelName);

            int uintScaling = Stride / sizeof(uint);

            cs.SetInt(cb, ComputeShaderUtility.BufferCountShaderPropertyID, count);
            cs.SetInt(cb, ComputeShaderUtility.BufferUIntCountShaderPropertyID, count * uintScaling);
            kernel.SetBuffer(cb, ComputeShaderUtility.BufferShaderPropertyID, Data);

            kernel.DispatchDesired(cb, count);
        }
        public void Clear(IComputeCommandBuffer cb)
        {
            if (!BufferTarget.HasFlag(GraphicsBuffer.Target.Raw))
            {
                Debug.LogError("Clear kernel only supports Raw buffers, please use your own clear method");
                return;
            }
            if (Length > 1024 * ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Buffer clear count exceeds maximum dispatch size, please use your own clear method");
            }

            var count = Length;

            var cs = _utilityProgram;
            var kernel = cs.FindKernel(count == 1 ? ComputeShaderUtility.ClearBuffer1KernelName : count <= 32 ? ComputeShaderUtility.ClearBuffer32KernelName : count <= 128 * ComputeLimits.MaxDispatchSize ? ComputeShaderUtility.ClearBuffer128KernelName : ComputeShaderUtility.ClearBuffer1024KernelName);

            int uintScaling = Stride / sizeof(uint);

            cs.SetInt(cb, ComputeShaderUtility.BufferCountShaderPropertyID, count);
            cs.SetInt(cb, ComputeShaderUtility.BufferUIntCountShaderPropertyID, count * uintScaling);
            kernel.SetBuffer(cb, ComputeShaderUtility.BufferShaderPropertyID, Data);

            kernel.DispatchDesired(cb, count);
        }

        public static implicit operator GraphicsBuffer(GraphicsBufferBase<T> buffer)
        {
            return buffer.Data;
        }
    }

    public static class GraphicsBufferExtensions
    {
        public static void SetGraphicsBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, IGraphicsBuffer buffer)
        {
            cs.SetBuffer(kernel, name, buffer.Data);
        }
        public static void SetGraphicsBuffer(this ComputeKernel kernel, string name, IGraphicsBuffer buffer)
        {
            kernel.Program.SetGraphicsBuffer(kernel, name, buffer);
        }

        public static void SetGraphicsBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, IGraphicsBuffer buffer)
        {
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetGraphicsBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, IGraphicsBuffer buffer)
        {
            kernel.Program.SetGraphicsBuffer(cb, kernel, name, buffer);
        }

        public static void SetGraphicsBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, IGraphicsBuffer buffer)
        {
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetGraphicsBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, IGraphicsBuffer buffer)
        {
            kernel.Program.SetGraphicsBuffer(cb, kernel, name, buffer);
        }
    }
}
