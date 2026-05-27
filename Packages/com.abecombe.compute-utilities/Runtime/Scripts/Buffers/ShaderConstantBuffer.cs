using UnityEngine;

namespace Abecombe.ComputeUtilities
{
    // You should layout the struct correctly to match the hlsl constant buffer layout.
    // See https://maraneshi.github.io/HLSL-ConstantBufferLayoutVisualizer/
    public class ShaderConstantBuffer<T> : GraphicsBufferBase<T>
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
}