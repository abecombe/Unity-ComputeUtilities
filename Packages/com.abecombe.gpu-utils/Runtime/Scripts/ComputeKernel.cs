using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace Abecombe.GpuTools
{
    public class ComputeKernel
    {
        public ComputeProgram Program { get; }
        public string Name { get; }
        public int ID { get; }
        public uint3 ThreadGroupSizes;
        public uint ThreadGroupSizeX => ThreadGroupSizes.x;
        public uint ThreadGroupSizeY => ThreadGroupSizes.y;
        public uint ThreadGroupSizeZ => ThreadGroupSizes.z;

        public ComputeKernel(ComputeProgram program, string name)
        {
            Program = program;
            Name = name;
            ID = program.Cs.FindKernel(name);
            program.Cs.GetKernelThreadGroupSizes(ID, out var threadGroupSizeX, out var threadGroupSizeY, out var threadGroupSizeZ);
            ThreadGroupSizes = new uint3(threadGroupSizeX, threadGroupSizeY, threadGroupSizeZ);
        }

        #region SetBool
        public void SetBool(int id, bool value)
        {
            Program.SetBool(id, value);
        }
        public void SetBool(string name, bool value)
        {
            Program.SetBool(name, value);
        }

        public void SetBool(CommandBuffer cb, int id, bool value)
        {
            Program.SetBool(cb, id, value);
        }
        public void SetBool(CommandBuffer cb, string name, bool value)
        {
            Program.SetBool(cb, name, value);
        }

        public void SetBool(IComputeCommandBuffer cb, int id, bool value)
        {
            Program.SetBool(cb, id, value);
        }
        public void SetBool(IComputeCommandBuffer cb, string name, bool value)
        {
            Program.SetBool(cb, name, value);
        }
        #endregion

        #region SetInt
        public void SetInt(int id, int value)
        {
            Program.SetInt(id, value);
        }
        public void SetInt(int id, uint value)
        {
            Program.SetInt(id, value);
        }
        public void SetInt(string name, int value)
        {
            Program.SetInt(name, value);
        }
        public void SetInt(string name, uint value)
        {
            Program.SetInt(name, value);
        }

        public void SetInt(CommandBuffer cb, int id, int value)
        {
            Program.SetInt(cb, id, value);
        }
        public void SetInt(CommandBuffer cb, int id, uint value)
        {
            Program.SetInt(cb, id, value);
        }
        public void SetInt(CommandBuffer cb, string name, int value)
        {
            Program.SetInt(cb, name, value);
        }
        public void SetInt(CommandBuffer cb, string name, uint value)
        {
            Program.SetInt(cb, name, value);
        }

        public void SetInt(IComputeCommandBuffer cb, int id, int value)
        {
            Program.SetInt(cb, id, value);
        }
        public void SetInt(IComputeCommandBuffer cb, int id, uint value)
        {
            Program.SetInt(cb, id, value);
        }
        public void SetInt(IComputeCommandBuffer cb, string name, int value)
        {
            Program.SetInt(cb, name, value);
        }
        public void SetInt(IComputeCommandBuffer cb, string name, uint value)
        {
            Program.SetInt(cb, name, value);
        }
        #endregion

        #region SetInts
        public void SetInts(int id, int2 value)
        {
            Program.SetInts(id, value);
        }
        public void SetInts(int id, int3 value)
        {
            Program.SetInts(id, value);
        }
        public void SetInts(int id, int4 value)
        {
            Program.SetInts(id, value);
        }
        public void SetInts(int id, uint x, uint y)
        {
            Program.SetInts(id, x, y);
        }
        public void SetInts(int id, uint x, uint y, uint z)
        {
            Program.SetInts(id, x, y, z);
        }
        public void SetInts(int id, uint x, uint y, uint z, uint w)
        {
            Program.SetInts(id, x, y, z, w);
        }
        public void SetInts(int id, uint2 value)
        {
            Program.SetInts(id, value);
        }
        public void SetInts(int id, uint3 value)
        {
            Program.SetInts(id, value);
        }
        public void SetInts(int id, uint4 value)
        {
            Program.SetInts(id, value);
        }
        public void SetInts(string name, int2 value)
        {
            Program.SetInts(name, value);
        }
        public void SetInts(string name, int3 value)
        {
            Program.SetInts(name, value);
        }
        public void SetInts(string name, int4 value)
        {
            Program.SetInts(name, value);
        }
        public void SetInts(string name, uint x, uint y)
        {
            Program.SetInts(name, x, y);
        }
        public void SetInts(string name, uint x, uint y, uint z)
        {
            Program.SetInts(name, x, y, z);
        }
        public void SetInts(string name, uint x, uint y, uint z, uint w)
        {
            Program.SetInts(name, x, y, z, w);
        }
        public void SetInts(string name, uint2 value)
        {
            Program.SetInts(name, value);
        }
        public void SetInts(string name, uint3 value)
        {
            Program.SetInts(name, value);
        }
        public void SetInts(string name, uint4 value)
        {
            Program.SetInts(name, value);
        }

        public void SetInts(CommandBuffer cb, int id, int2 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(CommandBuffer cb, int id, int3 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(CommandBuffer cb, int id, int4 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(CommandBuffer cb, int id, uint x, uint y)
        {
            Program.SetInts(cb, id, x, y);
        }
        public void SetInts(CommandBuffer cb, int id, uint x, uint y, uint z)
        {
            Program.SetInts(cb, id, x, y, z);
        }
        public void SetInts(CommandBuffer cb, int id, uint x, uint y, uint z, uint w)
        {
            Program.SetInts(cb, id, x, y, z, w);
        }
        public void SetInts(CommandBuffer cb, int id, uint2 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(CommandBuffer cb, int id, uint3 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(CommandBuffer cb, int id, uint4 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(CommandBuffer cb, string name, int2 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(CommandBuffer cb, string name, int3 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(CommandBuffer cb, string name, int4 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(CommandBuffer cb, string name, uint x, uint y)
        {
            Program.SetInts(cb, name, x, y);
        }
        public void SetInts(CommandBuffer cb, string name, uint x, uint y, uint z)
        {
            Program.SetInts(cb, name, x, y, z);
        }
        public void SetInts(CommandBuffer cb, string name, uint x, uint y, uint z, uint w)
        {
            Program.SetInts(cb, name, x, y, z, w);
        }
        public void SetInts(CommandBuffer cb, string name, uint2 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(CommandBuffer cb, string name, uint3 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(CommandBuffer cb, string name, uint4 value)
        {
            Program.SetInts(cb, name, value);
        }

        public void SetInts(IComputeCommandBuffer cb, int id, int2 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int3 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int4 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint x, uint y)
        {
            Program.SetInts(cb, id, x, y);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint x, uint y, uint z)
        {
            Program.SetInts(cb, id, x, y, z);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint x, uint y, uint z, uint w)
        {
            Program.SetInts(cb, id, x, y, z, w);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint2 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint3 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint4 value)
        {
            Program.SetInts(cb, id, value);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int2 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int3 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int4 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint x, uint y)
        {
            Program.SetInts(cb, name, x, y);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint x, uint y, uint z)
        {
            Program.SetInts(cb, name, x, y, z);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint x, uint y, uint z, uint w)
        {
            Program.SetInts(cb, name, x, y, z, w);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint2 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint3 value)
        {
            Program.SetInts(cb, name, value);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint4 value)
        {
            Program.SetInts(cb, name, value);
        }
        #endregion

        #region SetFloat
        public void SetFloat(int id, float value)
        {
            Program.SetFloat(id, value);
        }
        public void SetFloat(string name, float value)
        {
            Program.SetFloat(name, value);
        }

        public void SetFloat(CommandBuffer cb, int id, float value)
        {
            Program.SetFloat(cb, id, value);
        }
        public void SetFloat(CommandBuffer cb, string name, float value)
        {
            Program.SetFloat(cb, name, value);
        }

        public void SetFloat(IComputeCommandBuffer cb, int id, float value)
        {
            Program.SetFloat(cb, id, value);
        }
        public void SetFloat(IComputeCommandBuffer cb, string name, float value)
        {
            Program.SetFloat(cb, name, value);
        }
        #endregion

        #region SetVector
        public void SetVector(int id, float x, float y)
        {
            Program.SetVector(id, x, y);
        }
        public void SetVector(int id, float x, float y, float z)
        {
            Program.SetVector(id, x, y, z);
        }
        public void SetVector(int id, float x, float y, float z, float w)
        {
            Program.SetVector(id, x, y, z, w);
        }
        public void SetVector(int id, float2 value)
        {
            Program.SetVector(id, value);
        }
        public void SetVector(int id, float3 value)
        {
            Program.SetVector(id, value);
        }
        public void SetVector(int id, float4 value)
        {
            Program.SetVector(id, value);
        }
        public void SetVector(int id, Vector2 value)
        {
            Program.SetVector(id, value);
        }
        public void SetVector(int id, Vector3 value)
        {
            Program.SetVector(id, value);
        }
        public void SetVector(int id, Vector4 value)
        {
            Program.SetVector(id, value);
        }
        public void SetVector(string name, float x, float y)
        {
            Program.SetVector(name, x, y);
        }
        public void SetVector(string name, float x, float y, float z)
        {
            Program.SetVector(name, x, y, z);
        }
        public void SetVector(string name, float x, float y, float z, float w)
        {
            Program.SetVector(name, x, y, z, w);
        }
        public void SetVector(string name, float2 value)
        {
            Program.SetVector(name, value);
        }
        public void SetVector(string name, float3 value)
        {
            Program.SetVector(name, value);
        }
        public void SetVector(string name, float4 value)
        {
            Program.SetVector(name, value);
        }
        public void SetVector(string name, Vector2 value)
        {
            Program.SetVector(name, value);
        }
        public void SetVector(string name, Vector3 value)
        {
            Program.SetVector(name, value);
        }
        public void SetVector(string name, Vector4 value)
        {
            Program.SetVector(name, value);
        }

        public void SetVector(CommandBuffer cb, int id, float x, float y)
        {
            Program.SetVector(cb, id, x, y);
        }
        public void SetVector(CommandBuffer cb, int id, float x, float y, float z)
        {
            Program.SetVector(cb, id, x, y, z);
        }
        public void SetVector(CommandBuffer cb, int id, float x, float y, float z, float w)
        {
            Program.SetVector(cb, id, x, y, z, w);
        }
        public void SetVector(CommandBuffer cb, int id, float2 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, float3 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, float4 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, Vector2 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, Vector3 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, Vector4 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(CommandBuffer cb, string name, float x, float y)
        {
            Program.SetVector(cb, name, x, y);
        }
        public void SetVector(CommandBuffer cb, string name, float x, float y, float z)
        {
            Program.SetVector(cb, name, x, y, z);
        }
        public void SetVector(CommandBuffer cb, string name, float x, float y, float z, float w)
        {
            Program.SetVector(cb, name, x, y, z, w);
        }
        public void SetVector(CommandBuffer cb, string name, float2 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, float3 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, float4 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, Vector2 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, Vector3 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, Vector4 value)
        {
            Program.SetVector(cb, name, value);
        }

        public void SetVector(IComputeCommandBuffer cb, int id, float x, float y)
        {
            Program.SetVector(cb, id, x, y);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float x, float y, float z)
        {
            Program.SetVector(cb, id, x, y, z);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float x, float y, float z, float w)
        {
            Program.SetVector(cb, id, x, y, z, w);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float2 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float3 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float4 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, Vector2 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, Vector3 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, Vector4 value)
        {
            Program.SetVector(cb, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float x, float y)
        {
            Program.SetVector(cb, name, x, y);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float x, float y, float z)
        {
            Program.SetVector(cb, name, x, y, z);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float x, float y, float z, float w)
        {
            Program.SetVector(cb, name, x, y, z, w);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float2 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float3 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float4 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, Vector2 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, Vector3 value)
        {
            Program.SetVector(cb, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, Vector4 value)
        {
            Program.SetVector(cb, name, value);
        }
        #endregion

        #region SetMatrix
        public void SetMatrix(int id, Matrix4x4 matrix)
        {
            Program.SetMatrix(id, matrix);
        }
        public void SetMatrix(int id, float4x4 matrix)
        {
            Program.SetMatrix(id, matrix);
        }
        public void SetMatrix(string name, Matrix4x4 matrix)
        {
            Program.SetMatrix(name, matrix);
        }
        public void SetMatrix(string name, float4x4 matrix)
        {
            Program.SetMatrix(name, matrix);
        }

        public void SetMatrix(CommandBuffer cb, int id, Matrix4x4 matrix)
        {
            Program.SetMatrix(cb, id, matrix);
        }
        public void SetMatrix(CommandBuffer cb, int id, float4x4 matrix)
        {
            Program.SetMatrix(cb, id, matrix);
        }
        public void SetMatrix(CommandBuffer cb, string name, Matrix4x4 matrix)
        {
            Program.SetMatrix(cb, name, matrix);
        }
        public void SetMatrix(CommandBuffer cb, string name, float4x4 matrix)
        {
            Program.SetMatrix(cb, name, matrix);
        }

        public void SetMatrix(IComputeCommandBuffer cb, int id, Matrix4x4 matrix)
        {
            Program.SetMatrix(cb, id, matrix);
        }
        public void SetMatrix(IComputeCommandBuffer cb, int id, float4x4 matrix)
        {
            Program.SetMatrix(cb, id, matrix);
        }
        public void SetMatrix(IComputeCommandBuffer cb, string name, Matrix4x4 matrix)
        {
            Program.SetMatrix(cb, name, matrix);
        }
        public void SetMatrix(IComputeCommandBuffer cb, string name, float4x4 matrix)
        {
            Program.SetMatrix(cb, name, matrix);
        }
        #endregion

        #region SetBuffer
        public void SetBuffer(int id, GraphicsBuffer buffer)
        {
            Program.SetBuffer(this, id, buffer);
        }
        public void SetBuffer(string name, GraphicsBuffer buffer)
        {
            Program.SetBuffer(this, name, buffer);
        }

        public void SetBuffer(CommandBuffer cb, int id, GraphicsBuffer buffer)
        {
            Program.SetBuffer(cb, this, id, buffer);
        }
        public void SetBuffer(CommandBuffer cb, string name, GraphicsBuffer buffer)
        {
            Program.SetBuffer(cb, this, name, buffer);
        }

        public void SetBuffer(IComputeCommandBuffer cb, int id, GraphicsBuffer buffer)
        {
            Program.SetBuffer(cb, this, id, buffer);
        }
        public void SetBuffer(IComputeCommandBuffer cb, string name, GraphicsBuffer buffer)
        {
            Program.SetBuffer(cb, this, name, buffer);
        }
        #endregion

        #region SetConstantBuffer
        public void SetConstantBuffer(int id, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            Program.SetConstantBuffer(id, buffer, offset, size);
        }
        public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            Program.SetConstantBuffer(name, buffer, offset, size);
        }

        public void SetConstantBuffer(CommandBuffer cb, int id, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            Program.SetConstantBuffer(cb, id, buffer, offset, size);
        }
        public void SetConstantBuffer(CommandBuffer cb, string name, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            Program.SetConstantBuffer(cb, name, buffer, offset, size);
        }

        public void SetConstantBuffer(IComputeCommandBuffer cb, int id, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            Program.SetConstantBuffer(cb, id, buffer, offset, size);
        }
        public void SetConstantBuffer(IComputeCommandBuffer cb, string name, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            Program.SetConstantBuffer(cb, name, buffer, offset, size);
        }
        #endregion

        #region SetTexture
        public void SetTexture(int id, Texture tex)
        {
            Program.SetTexture(this, id, tex);
        }
        public void SetTexture(string name, Texture tex)
        {
            Program.SetTexture(this, name, tex);
        }

        public void SetTexture(CommandBuffer cb, int id, Texture tex)
        {
            Program.SetTexture(cb, this, id, tex);
        }
        public void SetTexture(CommandBuffer cb, string name, Texture tex)
        {
            Program.SetTexture(cb, this, name, tex);
        }

        public void SetTexture(IComputeCommandBuffer cb, int id, TextureHandle tex)
        {
            Program.SetTexture(cb, this, id, tex);
        }
        public void SetTexture(IComputeCommandBuffer cb, string name, TextureHandle tex)
        {
            Program.SetTexture(cb, this, name, tex);
        }
        #endregion

        #region SetRayTracingAccelerationStructure
        public void SetRayTracingAccelerationStructure(int id, RayTracingAccelerationStructure rtas)
        {
            Program.SetRayTracingAccelerationStructure(this, id, rtas);
        }

        public void SetRayTracingAccelerationStructure(string name, RayTracingAccelerationStructure rtas)
        {
            Program.SetRayTracingAccelerationStructure(this, name, rtas);
        }

        public void SetRayTracingAccelerationStructure(CommandBuffer cb, int id, RayTracingAccelerationStructure rtas)
        {
            Program.SetRayTracingAccelerationStructure(cb, this, id, rtas);
        }

        public void SetRayTracingAccelerationStructure(CommandBuffer cb, string name, RayTracingAccelerationStructure rtas)
        {
            Program.SetRayTracingAccelerationStructure(cb, this, name, rtas);
        }

        public void SetRayTracingAccelerationStructure(IComputeCommandBuffer cb, int id, RayTracingAccelerationStructure rtas)
        {
            Program.SetRayTracingAccelerationStructure(cb, this, id, rtas);
        }

        public void SetRayTracingAccelerationStructure(IComputeCommandBuffer cb, string name, RayTracingAccelerationStructure rtas)
        {
            Program.SetRayTracingAccelerationStructure(cb, this, name, rtas);
        }
        #endregion

        #region Dispatch
        public void DispatchDesired(int sizeX, int sizeY = 1, int sizeZ = 1)
        {
            int groupSizeX = Mathf.Max(1, (sizeX + (int)ThreadGroupSizeX - 1) / (int)ThreadGroupSizeX);
            int groupSizeY = Mathf.Max(1, (sizeY + (int)ThreadGroupSizeY - 1) / (int)ThreadGroupSizeY);
            int groupSizeZ = Mathf.Max(1, (sizeZ + (int)ThreadGroupSizeZ - 1) / (int)ThreadGroupSizeZ);
            if (groupSizeX > ComputeLimits.MaxDispatchSize || groupSizeY > ComputeLimits.MaxDispatchSize || groupSizeZ > ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Dispatch size exceeds maximum dispatch size");
                return;
            }
            Program.SetInts(ComputeShaderUtility.DispatchThreadSizeShaderPropertyID, sizeX, sizeY, sizeZ);
            Program.Dispatch(this, groupSizeX, groupSizeY, groupSizeZ);
        }
        public void DispatchDesired(int2 size)
        {
            DispatchDesired(size.x, size.y);
        }
        public void DispatchDesired(int3 size)
        {
            DispatchDesired(size.x, size.y, size.z);
        }

        public void DispatchDesired(CommandBuffer cb, int sizeX, int sizeY = 1, int sizeZ = 1)
        {
            int groupSizeX = Mathf.Max(1, (sizeX + (int)ThreadGroupSizeX - 1) / (int)ThreadGroupSizeX);
            int groupSizeY = Mathf.Max(1, (sizeY + (int)ThreadGroupSizeY - 1) / (int)ThreadGroupSizeY);
            int groupSizeZ = Mathf.Max(1, (sizeZ + (int)ThreadGroupSizeZ - 1) / (int)ThreadGroupSizeZ);
            if (groupSizeX > ComputeLimits.MaxDispatchSize || groupSizeY > ComputeLimits.MaxDispatchSize || groupSizeZ > ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Dispatch size exceeds maximum dispatch size");
                return;
            }
            Program.SetInts(cb, ComputeShaderUtility.DispatchThreadSizeShaderPropertyID, sizeX, sizeY, sizeZ);
            Program.Dispatch(cb, this, groupSizeX, groupSizeY, groupSizeZ);
        }
        public void DispatchDesired(CommandBuffer cb, int2 size)
        {
            DispatchDesired(cb, size.x, size.y);
        }
        public void DispatchDesired(CommandBuffer cb, int3 size)
        {
            DispatchDesired(cb, size.x, size.y, size.z);
        }

        public void DispatchDesired(IComputeCommandBuffer cb, int sizeX, int sizeY = 1, int sizeZ = 1)
        {
            int groupSizeX = Mathf.Max(1, (sizeX + (int)ThreadGroupSizeX - 1) / (int)ThreadGroupSizeX);
            int groupSizeY = Mathf.Max(1, (sizeY + (int)ThreadGroupSizeY - 1) / (int)ThreadGroupSizeY);
            int groupSizeZ = Mathf.Max(1, (sizeZ + (int)ThreadGroupSizeZ - 1) / (int)ThreadGroupSizeZ);
            if (groupSizeX > ComputeLimits.MaxDispatchSize || groupSizeY > ComputeLimits.MaxDispatchSize || groupSizeZ > ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Dispatch size exceeds maximum dispatch size");
                return;
            }
            Program.SetInts(cb, ComputeShaderUtility.DispatchThreadSizeShaderPropertyID, sizeX, sizeY, sizeZ);
            Program.Dispatch(cb, this, groupSizeX, groupSizeY, groupSizeZ);
        }
        public void DispatchDesired(IComputeCommandBuffer cb, int2 size)
        {
            DispatchDesired(cb, size.x, size.y);
        }
        public void DispatchDesired(IComputeCommandBuffer cb, int3 size)
        {
            DispatchDesired(cb, size.x, size.y, size.z);
        }
        #endregion
    }
}
