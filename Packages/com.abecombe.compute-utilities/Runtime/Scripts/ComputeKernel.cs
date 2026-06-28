using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace Abecombe.ComputeUtilities
{
    public class ComputeKernel
    {
        public ComputeProgram Program { get; }
        public string Name { get; }
        public int Index { get; }
        public int3 ThreadGroupSizes { get; private set; }
        public int ThreadGroupSizeX => ThreadGroupSizes.x;
        public int ThreadGroupSizeY => ThreadGroupSizes.y;
        public int ThreadGroupSizeZ => ThreadGroupSizes.z;

        public ComputeKernel(ComputeProgram program, string name)
        {
            Program = program;
            Name = name;
            Index = program.Shader.FindKernel(name);
            program.Shader.GetKernelThreadGroupSizes(Index, out var threadGroupSizeX, out var threadGroupSizeY, out var threadGroupSizeZ);
            ThreadGroupSizes = new int3((int)threadGroupSizeX, (int)threadGroupSizeY, (int)threadGroupSizeZ);
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
        public void SetInts(int id, int x, int y)
        {
            Program.SetInts(id, x, y);
        }
        public void SetInts(int id, int x, int y, int z)
        {
            Program.SetInts(id, x, y, z);
        }
        public void SetInts(int id, int x, int y, int z, int w)
        {
            Program.SetInts(id, x, y, z, w);
        }
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
        public void SetInts(string name, int x, int y)
        {
            Program.SetInts(name, x, y);
        }
        public void SetInts(string name, int x, int y, int z)
        {
            Program.SetInts(name, x, y, z);
        }
        public void SetInts(string name, int x, int y, int z, int w)
        {
            Program.SetInts(name, x, y, z, w);
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

        public void SetInts(CommandBuffer cb, int id, int x, int y)
        {
            Program.SetInts(cb, id, x, y);
        }
        public void SetInts(CommandBuffer cb, int id, int x, int y, int z)
        {
            Program.SetInts(cb, id, x, y, z);
        }
        public void SetInts(CommandBuffer cb, int id, int x, int y, int z, int w)
        {
            Program.SetInts(cb, id, x, y, z, w);
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
        public void SetInts(CommandBuffer cb, string name, int x, int y)
        {
            Program.SetInts(cb, name, x, y);
        }
        public void SetInts(CommandBuffer cb, string name, int x, int y, int z)
        {
            Program.SetInts(cb, name, x, y, z);
        }
        public void SetInts(CommandBuffer cb, string name, int x, int y, int z, int w)
        {
            Program.SetInts(cb, name, x, y, z, w);
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

        public void SetInts(IComputeCommandBuffer cb, int id, int x, int y)
        {
            Program.SetInts(cb, id, x, y);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int x, int y, int z)
        {
            Program.SetInts(cb, id, x, y, z);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int x, int y, int z, int w)
        {
            Program.SetInts(cb, id, x, y, z, w);
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
        public void SetInts(IComputeCommandBuffer cb, string name, int x, int y)
        {
            Program.SetInts(cb, name, x, y);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int x, int y, int z)
        {
            Program.SetInts(cb, name, x, y, z);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int x, int y, int z, int w)
        {
            Program.SetInts(cb, name, x, y, z, w);
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

        #region DispatchGroups
        public void DispatchGroups(int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            Program.DispatchGroups(this, threadGroupsX, threadGroupsY, threadGroupsZ);
        }

        public void DispatchGroups(CommandBuffer cb, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            Program.DispatchGroups(cb, this, threadGroupsX, threadGroupsY, threadGroupsZ);
        }

        public void DispatchGroups(IComputeCommandBuffer cb, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            Program.DispatchGroups(cb, this, threadGroupsX, threadGroupsY, threadGroupsZ);
        }
        #endregion

        #region DispatchIndirect
        public void DispatchIndirect(GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            Program.DispatchIndirect(this, argsBuffer, argsOffset);
        }

        public void DispatchIndirect(CommandBuffer cb, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            Program.DispatchIndirect(cb, this, argsBuffer, argsOffset);
        }

        public void DispatchIndirect(IComputeCommandBuffer cb, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            Program.DispatchIndirect(cb, this, argsBuffer, argsOffset);
        }
        #endregion

        #region DispatchThreads
        private static int GetThreadGroupCount(int size, int threadGroupSize)
        {
            var safeThreadGroupSize = Mathf.Max(1, threadGroupSize);
            return (int)(((long)size + safeThreadGroupSize - 1) / safeThreadGroupSize);
        }

        private bool TryGetDispatchGroupCounts(int sizeX, int sizeY, int sizeZ, out int groupCountX, out int groupCountY, out int groupCountZ)
        {
            groupCountX = 0;
            groupCountY = 0;
            groupCountZ = 0;

            if (sizeX < 0 || sizeY < 0 || sizeZ < 0)
            {
                Debug.LogError("Dispatch thread size must be zero or greater.");
                return false;
            }

            if (sizeX == 0 || sizeY == 0 || sizeZ == 0)
                return false;

            groupCountX = GetThreadGroupCount(sizeX, ThreadGroupSizeX);
            groupCountY = GetThreadGroupCount(sizeY, ThreadGroupSizeY);
            groupCountZ = GetThreadGroupCount(sizeZ, ThreadGroupSizeZ);

            if (groupCountX > ComputeLimits.MaxDispatchSize || groupCountY > ComputeLimits.MaxDispatchSize || groupCountZ > ComputeLimits.MaxDispatchSize)
            {
                Debug.LogError("Dispatch group count exceeds maximum dispatch size.");
                return false;
            }

            return true;
        }

        public void DispatchThreads(int sizeX, int sizeY = 1, int sizeZ = 1)
        {
            if (!TryGetDispatchGroupCounts(sizeX, sizeY, sizeZ, out var groupCountX, out var groupCountY, out var groupCountZ))
                return;

            SetInts(ComputeShaderUtility.DispatchThreadSizeShaderPropertyID, sizeX, sizeY, sizeZ);
            DispatchGroups(groupCountX, groupCountY, groupCountZ);
        }
        public void DispatchThreads(int2 size)
        {
            DispatchThreads(size.x, size.y);
        }
        public void DispatchThreads(int3 size)
        {
            DispatchThreads(size.x, size.y, size.z);
        }

        public void DispatchThreads(CommandBuffer cb, int sizeX, int sizeY = 1, int sizeZ = 1)
        {
            if (!TryGetDispatchGroupCounts(sizeX, sizeY, sizeZ, out var groupCountX, out var groupCountY, out var groupCountZ))
                return;

            SetInts(cb, ComputeShaderUtility.DispatchThreadSizeShaderPropertyID, sizeX, sizeY, sizeZ);
            DispatchGroups(cb, groupCountX, groupCountY, groupCountZ);
        }
        public void DispatchThreads(CommandBuffer cb, int2 size)
        {
            DispatchThreads(cb, size.x, size.y);
        }
        public void DispatchThreads(CommandBuffer cb, int3 size)
        {
            DispatchThreads(cb, size.x, size.y, size.z);
        }

        public void DispatchThreads(IComputeCommandBuffer cb, int sizeX, int sizeY = 1, int sizeZ = 1)
        {
            if (!TryGetDispatchGroupCounts(sizeX, sizeY, sizeZ, out var groupCountX, out var groupCountY, out var groupCountZ))
                return;

            SetInts(cb, ComputeShaderUtility.DispatchThreadSizeShaderPropertyID, sizeX, sizeY, sizeZ);
            DispatchGroups(cb, groupCountX, groupCountY, groupCountZ);
        }
        public void DispatchThreads(IComputeCommandBuffer cb, int2 size)
        {
            DispatchThreads(cb, size.x, size.y);
        }
        public void DispatchThreads(IComputeCommandBuffer cb, int3 size)
        {
            DispatchThreads(cb, size.x, size.y, size.z);
        }
        #endregion
    }
}