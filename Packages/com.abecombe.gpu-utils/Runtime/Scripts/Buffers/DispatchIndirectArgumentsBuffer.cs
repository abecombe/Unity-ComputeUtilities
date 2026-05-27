using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.GpuTools
{
    public class DispatchIndirectArgumentsBuffer : GraphicsBufferBase<uint>
    {
        public override GraphicsBuffer.Target BufferTarget => GraphicsBuffer.Target.IndirectArguments;

        public override int Length => 3;

        public GraphicsBufferBase<uint> CountBuffer { get; private set; }
        public int CountBufferOffset { get; private set; }
        public int CountBufferSize { get; private set; }

        public GraphicsBuffer DispatchThreadSizeBuffer { get; private set; }

        public void Init(GraphicsBufferBase<uint> countBuffer, int countBufferOffset, int countBufferSize)
        {
            Dispose();
            InitBufferProgram();
            using (var array = new NativeArray<uint>(new[] { 1u, 1u, 1u }, Allocator.Temp))
            {
                SetData(array);
            }
            DispatchThreadSizeBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, 3, Marshal.SizeOf(typeof(uint)));
            using (var array = new NativeArray<uint>(new[] { 0u, 0u, 0u }, Allocator.Temp))
            {
                DispatchThreadSizeBuffer.SetData(array);
            }
            CountBuffer = countBuffer;
            CountBufferOffset = countBufferOffset;
            CountBufferSize = math.clamp(countBufferSize, 1, 3);
            Inited = true;
        }

        public void UpdateBuffer(uint3 threadGroupSize)
        {
            var cs = _utilityProgram;
            var kernel = cs.FindKernel(ComputeShaderUtility.BuildDispatchIndirectKernelName);

            kernel.SetBuffer(ComputeShaderUtility.CountBufferShaderPropertyID, CountBuffer);
            kernel.SetBuffer(ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, DispatchThreadSizeBuffer);
            kernel.SetBuffer(ComputeShaderUtility.DispatchIndirectArgumentsBufferShaderPropertyID, Data);
            cs.SetInt(ComputeShaderUtility.CountBufferOffsetShaderPropertyID, CountBufferOffset);
            cs.SetInt(ComputeShaderUtility.CountBufferSizeShaderPropertyID, CountBufferSize);
            cs.SetInts(ComputeShaderUtility.ThreadGroupSizeShaderPropertyID, threadGroupSize);

            kernel.DispatchDesired(3);
        }
        public void UpdateBuffer(CommandBuffer cb, uint3 threadGroupSize)
        {
            var cs = _utilityProgram;
            var kernel = cs.FindKernel(ComputeShaderUtility.BuildDispatchIndirectKernelName);

            kernel.SetBuffer(cb, ComputeShaderUtility.CountBufferShaderPropertyID, CountBuffer);
            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, DispatchThreadSizeBuffer);
            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchIndirectArgumentsBufferShaderPropertyID, Data);
            cs.SetInt(cb, ComputeShaderUtility.CountBufferOffsetShaderPropertyID, CountBufferOffset);
            cs.SetInt(cb, ComputeShaderUtility.CountBufferSizeShaderPropertyID, CountBufferSize);
            cs.SetInts(cb, ComputeShaderUtility.ThreadGroupSizeShaderPropertyID, threadGroupSize);

            kernel.DispatchDesired(cb, 3);
        }
        public void UpdateBuffer(IComputeCommandBuffer cb, uint3 threadGroupSize)
        {
            var cs = _utilityProgram;
            var kernel = cs.FindKernel(ComputeShaderUtility.BuildDispatchIndirectKernelName);

            kernel.SetBuffer(cb, ComputeShaderUtility.CountBufferShaderPropertyID, CountBuffer);
            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, DispatchThreadSizeBuffer);
            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchIndirectArgumentsBufferShaderPropertyID, Data);
            cs.SetInt(cb, ComputeShaderUtility.CountBufferOffsetShaderPropertyID, CountBufferOffset);
            cs.SetInt(cb, ComputeShaderUtility.CountBufferSizeShaderPropertyID, CountBufferSize);
            cs.SetInts(cb, ComputeShaderUtility.ThreadGroupSizeShaderPropertyID, threadGroupSize);

            kernel.DispatchDesired(cb, 3);
        }

        public override void Dispose()
        {
            if (Inited)
            {
                Data.Release();
                Data = null;
                DispatchThreadSizeBuffer.Release();
                DispatchThreadSizeBuffer = null;
            }
            Inited = false;
        }
    }

    public static class DispatchIndirectArgumentsBufferExtensions
    {
        public static void DispatchIndirectDesired(this ComputeKernel kernel, DispatchIndirectArgumentsBuffer argsBuffer, bool updateBuffer = true)
        {
            if (updateBuffer) argsBuffer.UpdateBuffer(kernel.ThreadGroupSizes);

            kernel.SetBuffer(ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, argsBuffer.DispatchThreadSizeBuffer);
            kernel.Program.DispatchIndirect(kernel, argsBuffer);
        }

        public static void DispatchIndirectDesired(this ComputeKernel kernel, CommandBuffer cb, DispatchIndirectArgumentsBuffer argsBuffer, bool updateBuffer = true)
        {
            if (updateBuffer) argsBuffer.UpdateBuffer(cb, kernel.ThreadGroupSizes);

            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, argsBuffer.DispatchThreadSizeBuffer);
            kernel.Program.DispatchIndirect(cb, kernel, argsBuffer);
        }

        public static void DispatchIndirectDesired(this ComputeKernel kernel, IComputeCommandBuffer cb, DispatchIndirectArgumentsBuffer argsBuffer, bool updateBuffer = true)
        {
            if (updateBuffer) argsBuffer.UpdateBuffer(cb, kernel.ThreadGroupSizes);

            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, argsBuffer.DispatchThreadSizeBuffer);
            kernel.Program.DispatchIndirect(cb, kernel, argsBuffer);
        }
    }
}