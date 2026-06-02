using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.ComputeUtilities
{
    public interface IConstantBuffer : IGraphicsBuffer
    {
        public void Init();
    }

    // You should layout the struct correctly to match the hlsl constant buffer layout.
    // See https://maraneshi.github.io/HLSL-ConstantBufferLayoutVisualizer/
    public class ConstantBuffer<T> : GraphicsBufferBase<T>, IConstantBuffer
        where T : struct
    {
        public override GraphicsBuffer.Target BufferTarget => GraphicsBuffer.Target.Constant;

        public override int Length => 1;

        public void Init()
        {
            Dispose();
            InitBufferProgram();
            IsInitialized = true;
        }

        public override void Dispose()
        {
            if (IsInitialized)
            {
                ReleaseBufferResources();
            }
            IsInitialized = false;
        }
    }

    public static class ConstantBufferExtensions
    {
        public static void SetConstantBuffer(this ComputeProgram cs, ComputeKernel kernel, string name, IConstantBuffer buffer)
        {
            cs.SetBuffer(kernel, name, buffer.Data);
        }
        public static void SetConstantBuffer(this ComputeKernel kernel, string name, IConstantBuffer buffer)
        {
            kernel.Program.SetConstantBuffer(kernel, name, buffer);
        }

        public static void SetConstantBuffer(this ComputeProgram cs, CommandBuffer cb, ComputeKernel kernel, string name, IConstantBuffer buffer)
        {
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetConstantBuffer(this ComputeKernel kernel, CommandBuffer cb, string name, IConstantBuffer buffer)
        {
            kernel.Program.SetConstantBuffer(cb, kernel, name, buffer);
        }

        public static void SetConstantBuffer(this ComputeProgram cs, IComputeCommandBuffer cb, ComputeKernel kernel, string name, IConstantBuffer buffer)
        {
            cs.SetBuffer(cb, kernel, name, buffer.Data);
        }
        public static void SetConstantBuffer(this ComputeKernel kernel, IComputeCommandBuffer cb, string name, IConstantBuffer buffer)
        {
            kernel.Program.SetConstantBuffer(cb, kernel, name, buffer);
        }
    }
}