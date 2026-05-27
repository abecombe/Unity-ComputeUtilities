using UnityEngine;

namespace Abecombe.GpuTools
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
            Inited = true;
        }

        public override void Dispose()
        {
            if (Inited)
            {
                Data.Release();
                Data = null;
            }
            Inited = false;
        }
    }
}