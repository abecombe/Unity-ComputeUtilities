using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Abecombe.ComputeUtilities
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
            var clampedCountBufferSize = math.clamp(countBufferSize, 1, 3);
            if (countBuffer == null || countBufferOffset < 0 || countBufferOffset + clampedCountBufferSize > countBuffer.Length)
            {
                CountBuffer = null;
                CountBufferOffset = 0;
                CountBufferSize = 0;
                Debug.LogError("DispatchIndirectArgumentsBuffer count range is outside the count buffer.");
                return;
            }

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
            CountBufferSize = clampedCountBufferSize;
            IsInitialized = true;
        }

        public void UpdateBuffer(int3 threadGroupSize)
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
        public void UpdateBuffer(CommandBuffer cb, int3 threadGroupSize)
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
        public void UpdateBuffer(IComputeCommandBuffer cb, int3 threadGroupSize)
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
            if (IsInitialized)
            {
                ReleaseBufferResources();
                DispatchThreadSizeBuffer?.Release();
                DispatchThreadSizeBuffer = null;
            }
            IsInitialized = false;
            CountBuffer = null;
            CountBufferOffset = 0;
            CountBufferSize = 0;
        }
    }

    public static class DispatchIndirectArgumentsBufferExtensions
    {
        public static void DispatchIndirectDesired(this ComputeKernel kernel, DispatchIndirectArgumentsBuffer argsBuffer, bool updateBuffer = true)
        {
            if (updateBuffer) argsBuffer.UpdateBuffer(kernel.ThreadGroupSizes);

            kernel.SetBuffer(ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, argsBuffer.DispatchThreadSizeBuffer);
            kernel.DispatchIndirect(argsBuffer);
        }

        public static void DispatchIndirectDesired(this ComputeKernel kernel, CommandBuffer cb, DispatchIndirectArgumentsBuffer argsBuffer, bool updateBuffer = true)
        {
            if (updateBuffer) argsBuffer.UpdateBuffer(cb, kernel.ThreadGroupSizes);

            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, argsBuffer.DispatchThreadSizeBuffer);
            kernel.DispatchIndirect(cb, argsBuffer);
        }

        public static void DispatchIndirectDesired(this ComputeKernel kernel, IComputeCommandBuffer cb, DispatchIndirectArgumentsBuffer argsBuffer, bool updateBuffer = true)
        {
            if (updateBuffer) argsBuffer.UpdateBuffer(cb, kernel.ThreadGroupSizes);

            kernel.SetBuffer(cb, ComputeShaderUtility.DispatchThreadSizeBufferShaderPropertyID, argsBuffer.DispatchThreadSizeBuffer);
            kernel.DispatchIndirect(cb, argsBuffer);
        }
    }
}