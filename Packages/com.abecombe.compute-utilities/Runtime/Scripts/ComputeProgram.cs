using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace Abecombe.ComputeUtilities
{
    [Serializable]
    public class ComputeProgram
    {
        [SerializeField]
        private ComputeShader _shader;
        public ComputeShader Shader => _shader;
        public bool IsInitialized => _shader != null;

        private Dictionary<string, ComputeKernel> _kernels = new();

        private Dictionary<string, int> _propertyIdByName = new();
        private Dictionary<(string, string[]), int[]> _propertyIdsByName = new();

        private int[] _intArr = new int[4];

        public void Init()
        {
            _kernels.Clear();
            _propertyIdByName.Clear();
            _propertyIdsByName.Clear();
            if (_shader == null)
            {
                Debug.LogError("Compute Shader is Null. Please set a Compute Shader to dispatch kernels.");
            }
        }
        public void Init(ComputeShader shader)
        {
            _shader = shader;
            Init();
        }
        public void Init(string shaderResourcePath)
        {
            _shader = string.IsNullOrEmpty(shaderResourcePath) ? null : Resources.Load<ComputeShader>(shaderResourcePath);
            Init();
        }

        public ComputeKernel FindKernel(string name)
        {
            if (_shader == null)
            {
                Debug.LogError($"Cannot find kernel '{name}' because Compute Shader is null.");
                return null;
            }

            if (_kernels.TryGetValue(name, out var kernel))
                return kernel;

            kernel = new ComputeKernel(this, name);
            _kernels.Add(name, kernel);
            return kernel;
        }

        public int GetPropertyID(string name)
        {
            if (_propertyIdByName.TryGetValue(name, out var id))
                return id;

            id = UnityEngine.Shader.PropertyToID(name);
            _propertyIdByName.Add(name, id);
            return id;
        }

        public int[] GetPropertyIDs(string name, string[] concatNames)
        {
            var key = (name, concatNames);

            if (_propertyIdsByName.TryGetValue(key, out var ids))
                return ids;

            ids = new int[concatNames.Length];
            for (int i = 0; i < concatNames.Length; i++)
                ids[i] = UnityEngine.Shader.PropertyToID(name + concatNames[i]);
            _propertyIdsByName.Add(key, ids);
            return ids;
        }

        #region SetBool
        public void SetBool(int id, bool value)
        {
            Shader.SetBool(id, value);
        }
        public void SetBool(string name, bool value)
        {
            Shader.SetBool(name, value);
        }

        public void SetBool(CommandBuffer cb, int id, bool value)
        {
            cb.SetComputeIntParam(Shader, id, value ? 1 : 0);
        }
        public void SetBool(CommandBuffer cb, string name, bool value)
        {
            cb.SetComputeIntParam(Shader, name, value ? 1 : 0);
        }

        public void SetBool(IComputeCommandBuffer cb, int id, bool value)
        {
            cb.SetComputeIntParam(Shader, id, value ? 1 : 0);
        }
        public void SetBool(IComputeCommandBuffer cb, string name, bool value)
        {
            cb.SetComputeIntParam(Shader, name, value ? 1 : 0);
        }
        #endregion

        #region SetInt
        public void SetInt(int id, int value)
        {
            Shader.SetInt(id, value);
        }
        public void SetInt(int id, uint value)
        {
            Shader.SetInt(id, (int)value);
        }
        public void SetInt(string name, int value)
        {
            Shader.SetInt(name, value);
        }
        public void SetInt(string name, uint value)
        {
            Shader.SetInt(name, (int)value);
        }

        public void SetInt(CommandBuffer cb, int id, int value)
        {
            cb.SetComputeIntParam(Shader, id, value);
        }
        public void SetInt(CommandBuffer cb, int id, uint value)
        {
            cb.SetComputeIntParam(Shader, id, (int)value);
        }
        public void SetInt(CommandBuffer cb, string name, int value)
        {
            cb.SetComputeIntParam(Shader, name, value);
        }
        public void SetInt(CommandBuffer cb, string name, uint value)
        {
            cb.SetComputeIntParam(Shader, name, (int)value);
        }

        public void SetInt(IComputeCommandBuffer cb, int id, int value)
        {
            cb.SetComputeIntParam(Shader, id, value);
        }
        public void SetInt(IComputeCommandBuffer cb, int id, uint value)
        {
            cb.SetComputeIntParam(Shader, id, (int)value);
        }
        public void SetInt(IComputeCommandBuffer cb, string name, int value)
        {
            cb.SetComputeIntParam(Shader, name, value);
        }
        public void SetInt(IComputeCommandBuffer cb, string name, uint value)
        {
            cb.SetComputeIntParam(Shader, name, (int)value);
        }
        #endregion

        #region SetInts
        private void SetInts(int id)
        {
            Shader.SetInts(id, _intArr);
        }
        public void SetInts(int id, int x, int y)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            SetInts(id);
        }
        public void SetInts(int id, int x, int y, int z)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            SetInts(id);
        }
        public void SetInts(int id, int x, int y, int z, int w)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            _intArr[3] = w;
            SetInts(id);
        }
        public void SetInts(int id, int2 value)
        {
            SetInts(id, value.x, value.y);
        }
        public void SetInts(int id, int3 value)
        {
            SetInts(id, value.x, value.y, value.z);
        }
        public void SetInts(int id, int4 value)
        {
            SetInts(id, value.x, value.y, value.z, value.w);
        }
        public void SetInts(int id, uint x, uint y)
        {
            SetInts(id, (int)x, (int)y);
        }
        public void SetInts(int id, uint x, uint y, uint z)
        {
            SetInts(id, (int)x, (int)y, (int)z);
        }
        public void SetInts(int id, uint x, uint y, uint z, uint w)
        {
            SetInts(id, (int)x, (int)y, (int)z, (int)w);
        }
        public void SetInts(int id, uint2 value)
        {
            SetInts(id, (int)value.x, (int)value.y);
        }
        public void SetInts(int id, uint3 value)
        {
            SetInts(id, (int)value.x, (int)value.y, (int)value.z);
        }
        public void SetInts(int id, uint4 value)
        {
            SetInts(id, (int)value.x, (int)value.y, (int)value.z, (int)value.w);
        }
        private void SetInts(string name)
        {
            Shader.SetInts(name, _intArr);
        }
        public void SetInts(string name, int x, int y)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            SetInts(name);
        }
        public void SetInts(string name, int x, int y, int z)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            SetInts(name);
        }
        public void SetInts(string name, int x, int y, int z, int w)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            _intArr[3] = w;
            SetInts(name);
        }
        public void SetInts(string name, int2 value)
        {
            SetInts(name, value.x, value.y);
        }
        public void SetInts(string name, int3 value)
        {
            SetInts(name, value.x, value.y, value.z);
        }
        public void SetInts(string name, int4 value)
        {
            SetInts(name, value.x, value.y, value.z, value.w);
        }
        public void SetInts(string name, uint x, uint y)
        {
            SetInts(name, (int)x, (int)y);
        }
        public void SetInts(string name, uint x, uint y, uint z)
        {
            SetInts(name, (int)x, (int)y, (int)z);
        }
        public void SetInts(string name, uint x, uint y, uint z, uint w)
        {
            SetInts(name, (int)x, (int)y, (int)z, (int)w);
        }
        public void SetInts(string name, uint2 value)
        {
            SetInts(name, (int)value.x, (int)value.y);
        }
        public void SetInts(string name, uint3 value)
        {
            SetInts(name, (int)value.x, (int)value.y, (int)value.z);
        }
        public void SetInts(string name, uint4 value)
        {
            SetInts(name, (int)value.x, (int)value.y, (int)value.z, (int)value.w);
        }

        private void SetInts(CommandBuffer cb, int id)
        {
            cb.SetComputeIntParams(Shader, id, _intArr);
        }
        public void SetInts(CommandBuffer cb, int id, int x, int y)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            SetInts(cb, id);
        }
        public void SetInts(CommandBuffer cb, int id, int x, int y, int z)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            SetInts(cb, id);
        }
        public void SetInts(CommandBuffer cb, int id, int x, int y, int z, int w)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            _intArr[3] = w;
            SetInts(cb, id);
        }
        public void SetInts(CommandBuffer cb, int id, int2 value)
        {
            SetInts(cb, id, value.x, value.y);
        }
        public void SetInts(CommandBuffer cb, int id, int3 value)
        {
            SetInts(cb, id, value.x, value.y, value.z);
        }
        public void SetInts(CommandBuffer cb, int id, int4 value)
        {
            SetInts(cb, id, value.x, value.y, value.z, value.w);
        }
        public void SetInts(CommandBuffer cb, int id, uint x, uint y)
        {
            SetInts(cb, id, (int)x, (int)y);
        }
        public void SetInts(CommandBuffer cb, int id, uint x, uint y, uint z)
        {
            SetInts(cb, id, (int)x, (int)y, (int)z);
        }
        public void SetInts(CommandBuffer cb, int id, uint x, uint y, uint z, uint w)
        {
            SetInts(cb, id, (int)x, (int)y, (int)z, (int)w);
        }
        public void SetInts(CommandBuffer cb, int id, uint2 value)
        {
            SetInts(cb, id, (int)value.x, (int)value.y);
        }
        public void SetInts(CommandBuffer cb, int id, uint3 value)
        {
            SetInts(cb, id, (int)value.x, (int)value.y, (int)value.z);
        }
        public void SetInts(CommandBuffer cb, int id, uint4 value)
        {
            SetInts(cb, id, (int)value.x, (int)value.y, (int)value.z, (int)value.w);
        }
        private void SetInts(CommandBuffer cb, string name)
        {
            cb.SetComputeIntParams(Shader, name, _intArr);
        }
        public void SetInts(CommandBuffer cb, string name, int x, int y)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            SetInts(cb, name);
        }
        public void SetInts(CommandBuffer cb, string name, int x, int y, int z)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            SetInts(cb, name);
        }
        public void SetInts(CommandBuffer cb, string name, int x, int y, int z, int w)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            _intArr[3] = w;
            SetInts(cb, name);
        }
        public void SetInts(CommandBuffer cb, string name, int2 value)
        {
            SetInts(cb, name, value.x, value.y);
        }
        public void SetInts(CommandBuffer cb, string name, int3 value)
        {
            SetInts(cb, name, value.x, value.y, value.z);
        }
        public void SetInts(CommandBuffer cb, string name, int4 value)
        {
            SetInts(cb, name, value.x, value.y, value.z, value.w);
        }
        public void SetInts(CommandBuffer cb, string name, uint x, uint y)
        {
            SetInts(cb, name, (int)x, (int)y);
        }
        public void SetInts(CommandBuffer cb, string name, uint x, uint y, uint z)
        {
            SetInts(cb, name, (int)x, (int)y, (int)z);
        }
        public void SetInts(CommandBuffer cb, string name, uint x, uint y, uint z, uint w)
        {
            SetInts(cb, name, (int)x, (int)y, (int)z, (int)w);
        }
        public void SetInts(CommandBuffer cb, string name, uint2 value)
        {
            SetInts(cb, name, (int)value.x, (int)value.y);
        }
        public void SetInts(CommandBuffer cb, string name, uint3 value)
        {
            SetInts(cb, name, (int)value.x, (int)value.y, (int)value.z);
        }
        public void SetInts(CommandBuffer cb, string name, uint4 value)
        {
            SetInts(cb, name, (int)value.x, (int)value.y, (int)value.z, (int)value.w);
        }

        private void SetInts(IComputeCommandBuffer cb, int id)
        {
            cb.SetComputeIntParams(Shader, id, _intArr);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int x, int y)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            SetInts(cb, id);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int x, int y, int z)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            SetInts(cb, id);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int x, int y, int z, int w)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            _intArr[3] = w;
            SetInts(cb, id);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int2 value)
        {
            SetInts(cb, id, value.x, value.y);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int3 value)
        {
            SetInts(cb, id, value.x, value.y, value.z);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, int4 value)
        {
            SetInts(cb, id, value.x, value.y, value.z, value.w);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint x, uint y)
        {
            SetInts(cb, id, (int)x, (int)y);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint x, uint y, uint z)
        {
            SetInts(cb, id, (int)x, (int)y, (int)z);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint x, uint y, uint z, uint w)
        {
            SetInts(cb, id, (int)x, (int)y, (int)z, (int)w);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint2 value)
        {
            SetInts(cb, id, (int)value.x, (int)value.y);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint3 value)
        {
            SetInts(cb, id, (int)value.x, (int)value.y, (int)value.z);
        }
        public void SetInts(IComputeCommandBuffer cb, int id, uint4 value)
        {
            SetInts(cb, id, (int)value.x, (int)value.y, (int)value.z, (int)value.w);
        }
        private void SetInts(IComputeCommandBuffer cb, string name)
        {
            cb.SetComputeIntParams(Shader, name, _intArr);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int x, int y)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            SetInts(cb, name);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int x, int y, int z)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            SetInts(cb, name);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int x, int y, int z, int w)
        {
            _intArr[0] = x;
            _intArr[1] = y;
            _intArr[2] = z;
            _intArr[3] = w;
            SetInts(cb, name);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int2 value)
        {
            SetInts(cb, name, value.x, value.y);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int3 value)
        {
            SetInts(cb, name, value.x, value.y, value.z);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, int4 value)
        {
            SetInts(cb, name, value.x, value.y, value.z, value.w);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint x, uint y)
        {
            SetInts(cb, name, (int)x, (int)y);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint x, uint y, uint z)
        {
            SetInts(cb, name, (int)x, (int)y, (int)z);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint x, uint y, uint z, uint w)
        {
            SetInts(cb, name, (int)x, (int)y, (int)z, (int)w);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint2 value)
        {
            SetInts(cb, name, (int)value.x, (int)value.y);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint3 value)
        {
            SetInts(cb, name, (int)value.x, (int)value.y, (int)value.z);
        }
        public void SetInts(IComputeCommandBuffer cb, string name, uint4 value)
        {
            SetInts(cb, name, (int)value.x, (int)value.y, (int)value.z, (int)value.w);
        }
        #endregion

        #region SetFloat
        public void SetFloat(int id, float value)
        {
            Shader.SetFloat(id, value);
        }
        public void SetFloat(string name, float value)
        {
            Shader.SetFloat(name, value);
        }

        public void SetFloat(CommandBuffer cb, int id, float value)
        {
            cb.SetComputeFloatParam(Shader, id, value);
        }
        public void SetFloat(CommandBuffer cb, string name, float value)
        {
            cb.SetComputeFloatParam(Shader, name, value);
        }

        public void SetFloat(IComputeCommandBuffer cb, int id, float value)
        {
            cb.SetComputeFloatParam(Shader, id, value);
        }
        public void SetFloat(IComputeCommandBuffer cb, string name, float value)
        {
            cb.SetComputeFloatParam(Shader, name, value);
        }
        #endregion

        #region SetVector
        public void SetVector(int id, float x, float y)
        {
            Shader.SetVector(id, new Vector4(x, y));
        }
        public void SetVector(int id, float x, float y, float z)
        {
            Shader.SetVector(id, new Vector4(x, y, z));
        }
        public void SetVector(int id, float x, float y, float z, float w)
        {
            Shader.SetVector(id, new Vector4(x, y, z, w));
        }
        public void SetVector(int id, float2 value)
        {
            Shader.SetVector(id, new Vector4(value.x, value.y));
        }
        public void SetVector(int id, float3 value)
        {
            Shader.SetVector(id, new Vector4(value.x, value.y, value.z));
        }
        public void SetVector(int id, float4 value)
        {
            Shader.SetVector(id, new Vector4(value.x, value.y, value.z, value.w));
        }
        public void SetVector(int id, Vector2 value)
        {
            Shader.SetVector(id, value);
        }
        public void SetVector(int id, Vector3 value)
        {
            Shader.SetVector(id, value);
        }
        public void SetVector(int id, Vector4 value)
        {
            Shader.SetVector(id, value);
        }
        public void SetVector(string name, float x, float y)
        {
            Shader.SetVector(name, new Vector4(x, y));
        }
        public void SetVector(string name, float x, float y, float z)
        {
            Shader.SetVector(name, new Vector4(x, y, z));
        }
        public void SetVector(string name, float x, float y, float z, float w)
        {
            Shader.SetVector(name, new Vector4(x, y, z, w));
        }
        public void SetVector(string name, float2 value)
        {
            Shader.SetVector(name, new Vector4(value.x, value.y));
        }
        public void SetVector(string name, float3 value)
        {
            Shader.SetVector(name, new Vector4(value.x, value.y, value.z));
        }
        public void SetVector(string name, float4 value)
        {
            Shader.SetVector(name, new Vector4(value.x, value.y, value.z, value.w));
        }
        public void SetVector(string name, Vector2 value)
        {
            Shader.SetVector(name, value);
        }
        public void SetVector(string name, Vector3 value)
        {
            Shader.SetVector(name, value);
        }
        public void SetVector(string name, Vector4 value)
        {
            Shader.SetVector(name, value);
        }

        public void SetVector(CommandBuffer cb, int id, float x, float y)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(x, y));
        }
        public void SetVector(CommandBuffer cb, int id, float x, float y, float z)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(x, y, z));
        }
        public void SetVector(CommandBuffer cb, int id, float x, float y, float z, float w)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(x, y, z, w));
        }
        public void SetVector(CommandBuffer cb, int id, float2 value)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(value.x, value.y));
        }
        public void SetVector(CommandBuffer cb, int id, float3 value)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(value.x, value.y, value.z));
        }
        public void SetVector(CommandBuffer cb, int id, float4 value)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(value.x, value.y, value.z, value.w));
        }
        public void SetVector(CommandBuffer cb, int id, Vector2 value)
        {
            cb.SetComputeVectorParam(Shader, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, Vector3 value)
        {
            cb.SetComputeVectorParam(Shader, id, value);
        }
        public void SetVector(CommandBuffer cb, int id, Vector4 value)
        {
            cb.SetComputeVectorParam(Shader, id, value);
        }
        public void SetVector(CommandBuffer cb, string name, float x, float y)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(x, y));
        }
        public void SetVector(CommandBuffer cb, string name, float x, float y, float z)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(x, y, z));
        }
        public void SetVector(CommandBuffer cb, string name, float x, float y, float z, float w)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(x, y, z, w));
        }
        public void SetVector(CommandBuffer cb, string name, float2 value)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(value.x, value.y));
        }
        public void SetVector(CommandBuffer cb, string name, float3 value)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(value.x, value.y, value.z));
        }
        public void SetVector(CommandBuffer cb, string name, float4 value)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(value.x, value.y, value.z, value.w));
        }
        public void SetVector(CommandBuffer cb, string name, Vector2 value)
        {
            cb.SetComputeVectorParam(Shader, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, Vector3 value)
        {
            cb.SetComputeVectorParam(Shader, name, value);
        }
        public void SetVector(CommandBuffer cb, string name, Vector4 value)
        {
            cb.SetComputeVectorParam(Shader, name, value);
        }

        public void SetVector(IComputeCommandBuffer cb, int id, float x, float y)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(x, y));
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float x, float y, float z)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(x, y, z));
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float x, float y, float z, float w)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(x, y, z, w));
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float2 value)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(value.x, value.y));
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float3 value)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(value.x, value.y, value.z));
        }
        public void SetVector(IComputeCommandBuffer cb, int id, float4 value)
        {
            cb.SetComputeVectorParam(Shader, id, new Vector4(value.x, value.y, value.z, value.w));
        }
        public void SetVector(IComputeCommandBuffer cb, int id, Vector2 value)
        {
            cb.SetComputeVectorParam(Shader, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, Vector3 value)
        {
            cb.SetComputeVectorParam(Shader, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, int id, Vector4 value)
        {
            cb.SetComputeVectorParam(Shader, id, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float x, float y)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(x, y));
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float x, float y, float z)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(x, y, z));
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float x, float y, float z, float w)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(x, y, z, w));
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float2 value)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(value.x, value.y));
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float3 value)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(value.x, value.y, value.z));
        }
        public void SetVector(IComputeCommandBuffer cb, string name, float4 value)
        {
            cb.SetComputeVectorParam(Shader, name, new Vector4(value.x, value.y, value.z, value.w));
        }
        public void SetVector(IComputeCommandBuffer cb, string name, Vector2 value)
        {
            cb.SetComputeVectorParam(Shader, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, Vector3 value)
        {
            cb.SetComputeVectorParam(Shader, name, value);
        }
        public void SetVector(IComputeCommandBuffer cb, string name, Vector4 value)
        {
            cb.SetComputeVectorParam(Shader, name, value);
        }
        #endregion

        #region SetMatrix
        public void SetMatrix(int id, Matrix4x4 matrix)
        {
            Shader.SetMatrix(id, matrix);
        }
        public void SetMatrix(int id, float4x4 matrix)
        {
            Shader.SetMatrix(id, matrix);
        }
        public void SetMatrix(string name, Matrix4x4 matrix)
        {
            Shader.SetMatrix(name, matrix);
        }
        public void SetMatrix(string name, float4x4 matrix)
        {
            Shader.SetMatrix(name, matrix);
        }

        public void SetMatrix(CommandBuffer cb, int id, Matrix4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, id, matrix);
        }
        public void SetMatrix(CommandBuffer cb, int id, float4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, id, matrix);
        }
        public void SetMatrix(CommandBuffer cb, string name, Matrix4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, name, matrix);
        }
        public void SetMatrix(CommandBuffer cb, string name, float4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, name, matrix);
        }

        public void SetMatrix(IComputeCommandBuffer cb, int id, Matrix4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, id, matrix);
        }
        public void SetMatrix(IComputeCommandBuffer cb, int id, float4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, id, matrix);
        }
        public void SetMatrix(IComputeCommandBuffer cb, string name, Matrix4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, name, matrix);
        }
        public void SetMatrix(IComputeCommandBuffer cb, string name, float4x4 matrix)
        {
            cb.SetComputeMatrixParam(Shader, name, matrix);
        }
        #endregion

        #region SetBuffer
        public void SetBuffer(int kernelIndex, int id, GraphicsBuffer buffer)
        {
            Shader.SetBuffer(kernelIndex, id, buffer);
        }
        public void SetBuffer(ComputeKernel kernel, int id, GraphicsBuffer buffer)
        {
            SetBuffer(kernel.Index, id, buffer);
        }
        public void SetBuffer(int kernelIndex, string name, GraphicsBuffer buffer)
        {
            Shader.SetBuffer(kernelIndex, name, buffer);
        }
        public void SetBuffer(ComputeKernel kernel, string name, GraphicsBuffer buffer)
        {
            SetBuffer(kernel.Index, name, buffer);
        }

        public void SetBuffer(CommandBuffer cb, int kernelIndex, int id, GraphicsBuffer buffer)
        {
            cb.SetComputeBufferParam(Shader, kernelIndex, id, buffer);
        }
        public void SetBuffer(CommandBuffer cb, ComputeKernel kernel, int id, GraphicsBuffer buffer)
        {
            SetBuffer(cb, kernel.Index, id, buffer);
        }
        public void SetBuffer(CommandBuffer cb, int kernelIndex, string name, GraphicsBuffer buffer)
        {
            cb.SetComputeBufferParam(Shader, kernelIndex, name, buffer);
        }
        public void SetBuffer(CommandBuffer cb, ComputeKernel kernel, string name, GraphicsBuffer buffer)
        {
            SetBuffer(cb, kernel.Index, name, buffer);
        }

        public void SetBuffer(IComputeCommandBuffer cb, int kernelIndex, int id, GraphicsBuffer buffer)
        {
            cb.SetComputeBufferParam(Shader, kernelIndex, id, buffer);
        }
        public void SetBuffer(IComputeCommandBuffer cb, ComputeKernel kernel, int id, GraphicsBuffer buffer)
        {
            SetBuffer(cb, kernel.Index, id, buffer);
        }
        public void SetBuffer(IComputeCommandBuffer cb, int kernelIndex, string name, GraphicsBuffer buffer)
        {
            cb.SetComputeBufferParam(Shader, kernelIndex, name, buffer);
        }
        public void SetBuffer(IComputeCommandBuffer cb, ComputeKernel kernel, string name, GraphicsBuffer buffer)
        {
            SetBuffer(cb, kernel.Index, name, buffer);
        }
        #endregion

        #region SetConstantBuffer
        public void SetConstantBuffer(int id, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            if (buffer.target != GraphicsBuffer.Target.Constant)
            {
                Debug.LogError("Buffer is not a constant buffer");
                return;
            }
            if (buffer.count != 1)
            {
                Debug.LogError("Constant Buffer should be a single element");
                return;
            }
            size = size == -1 ? buffer.stride - offset : size;
            Shader.SetConstantBuffer(id, buffer, offset, size);
        }
        public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            if (buffer.target != GraphicsBuffer.Target.Constant)
            {
                Debug.LogError("Buffer is not a constant buffer");
                return;
            }
            if (buffer.count != 1)
            {
                Debug.LogError("Constant Buffer should be a single element");
                return;
            }
            size = size == -1 ? buffer.stride - offset : size;
            Shader.SetConstantBuffer(name, buffer, offset, size);
        }

        public void SetConstantBuffer(CommandBuffer cb, int id, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            if (buffer.target != GraphicsBuffer.Target.Constant)
            {
                Debug.LogError("Buffer is not a constant buffer");
                return;
            }
            if (buffer.count != 1)
            {
                Debug.LogError("Constant Buffer should be a single element");
                return;
            }
            size = size == -1 ? buffer.stride - offset : size;
            cb.SetComputeConstantBufferParam(Shader, id, buffer, offset, size);
        }
        public void SetConstantBuffer(CommandBuffer cb, string name, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            if (buffer.target != GraphicsBuffer.Target.Constant)
            {
                Debug.LogError("Buffer is not a constant buffer");
                return;
            }
            if (buffer.count != 1)
            {
                Debug.LogError("Constant Buffer should be a single element");
                return;
            }
            size = size == -1 ? buffer.stride - offset : size;
            cb.SetComputeConstantBufferParam(Shader, name, buffer, offset, size);
        }

        public void SetConstantBuffer(IComputeCommandBuffer cb, int id, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            if (buffer.target != GraphicsBuffer.Target.Constant)
            {
                Debug.LogError("Buffer is not a constant buffer");
                return;
            }
            if (buffer.count != 1)
            {
                Debug.LogError("Constant Buffer should be a single element");
                return;
            }
            size = size == -1 ? buffer.stride - offset : size;
            cb.SetComputeConstantBufferParam(Shader, id, buffer, offset, size);
        }
        public void SetConstantBuffer(IComputeCommandBuffer cb, string name, GraphicsBuffer buffer, int offset = 0, int size = -1)
        {
            if (buffer.target != GraphicsBuffer.Target.Constant)
            {
                Debug.LogError("Buffer is not a constant buffer");
                return;
            }
            if (buffer.count != 1)
            {
                Debug.LogError("Constant Buffer should be a single element");
                return;
            }
            size = size == -1 ? buffer.stride - offset : size;
            cb.SetComputeConstantBufferParam(Shader, name, buffer, offset, size);
        }
        #endregion

        #region SetTexture
        public void SetTexture(int kernelIndex, int id, Texture tex)
        {
            Shader.SetTexture(kernelIndex, id, tex);
        }
        public void SetTexture(ComputeKernel kernel, int id, Texture tex)
        {
            SetTexture(kernel.Index, id, tex);
        }
        public void SetTexture(int kernelIndex, string name, Texture tex)
        {
            Shader.SetTexture(kernelIndex, name, tex);
        }
        public void SetTexture(ComputeKernel kernel, string name, Texture tex)
        {
            SetTexture(kernel.Index, name, tex);
        }

        public void SetTexture(CommandBuffer cb, int kernelIndex, int id, Texture tex)
        {
            cb.SetComputeTextureParam(Shader, kernelIndex, id, tex);
        }
        public void SetTexture(CommandBuffer cb, ComputeKernel kernel, int id, Texture tex)
        {
            SetTexture(cb, kernel.Index, id, tex);
        }
        public void SetTexture(CommandBuffer cb, int kernelIndex, string name, Texture tex)
        {
            cb.SetComputeTextureParam(Shader, kernelIndex, name, tex);
        }
        public void SetTexture(CommandBuffer cb, ComputeKernel kernel, string name, Texture tex)
        {
            SetTexture(cb, kernel.Index, name, tex);
        }

        public void SetTexture(IComputeCommandBuffer cb, int kernelIndex, int id, TextureHandle tex)
        {
            cb.SetComputeTextureParam(Shader, kernelIndex, id, tex);
        }
        public void SetTexture(IComputeCommandBuffer cb, ComputeKernel kernel, int id, TextureHandle tex)
        {
            SetTexture(cb, kernel.Index, id, tex);
        }
        public void SetTexture(IComputeCommandBuffer cb, int kernelIndex, string name, TextureHandle tex)
        {
            cb.SetComputeTextureParam(Shader, kernelIndex, name, tex);
        }
        public void SetTexture(IComputeCommandBuffer cb, ComputeKernel kernel, string name, TextureHandle tex)
        {
            SetTexture(cb, kernel.Index, name, tex);
        }
        #endregion

        #region SetRayTracingAccelerationStructure
        public void SetRayTracingAccelerationStructure(int kernelIndex, int id, RayTracingAccelerationStructure rtas)
        {
            Shader.SetRayTracingAccelerationStructure(kernelIndex, id, rtas);
        }
        public void SetRayTracingAccelerationStructure(ComputeKernel kernel, int id, RayTracingAccelerationStructure rtas)
        {
            SetRayTracingAccelerationStructure(kernel.Index, id, rtas);
        }
        public void SetRayTracingAccelerationStructure(int kernelIndex, string name, RayTracingAccelerationStructure rtas)
        {
            Shader.SetRayTracingAccelerationStructure(kernelIndex, name, rtas);
        }
        public void SetRayTracingAccelerationStructure(ComputeKernel kernel, string name, RayTracingAccelerationStructure rtas)
        {
            SetRayTracingAccelerationStructure(kernel.Index, name, rtas);
        }

        public void SetRayTracingAccelerationStructure(CommandBuffer cb, int kernelIndex, int id, RayTracingAccelerationStructure rtas)
        {
            cb.SetRayTracingAccelerationStructure(Shader, kernelIndex, id, rtas);
        }
        public void SetRayTracingAccelerationStructure(CommandBuffer cb, ComputeKernel kernel, int id, RayTracingAccelerationStructure rtas)
        {
            SetRayTracingAccelerationStructure(cb, kernel.Index, id, rtas);
        }
        public void SetRayTracingAccelerationStructure(CommandBuffer cb, int kernelIndex, string name, RayTracingAccelerationStructure rtas)
        {
            cb.SetRayTracingAccelerationStructure(Shader, kernelIndex, name, rtas);
        }
        public void SetRayTracingAccelerationStructure(CommandBuffer cb, ComputeKernel kernel, string name, RayTracingAccelerationStructure rtas)
        {
            SetRayTracingAccelerationStructure(cb, kernel.Index, name, rtas);
        }

        public void SetRayTracingAccelerationStructure(IComputeCommandBuffer cb, int kernelIndex, int id, RayTracingAccelerationStructure rtas)
        {
            cb.SetRayTracingAccelerationStructure(Shader, kernelIndex, id, rtas);
        }
        public void SetRayTracingAccelerationStructure(IComputeCommandBuffer cb, ComputeKernel kernel, int id, RayTracingAccelerationStructure rtas)
        {
            SetRayTracingAccelerationStructure(cb, kernel.Index, id, rtas);
        }
        public void SetRayTracingAccelerationStructure(IComputeCommandBuffer cb, int kernelIndex, string name, RayTracingAccelerationStructure rtas)
        {
            cb.SetRayTracingAccelerationStructure(Shader, kernelIndex, name, rtas);
        }
        public void SetRayTracingAccelerationStructure(IComputeCommandBuffer cb, ComputeKernel kernel, string name, RayTracingAccelerationStructure rtas)
        {
            SetRayTracingAccelerationStructure(cb, kernel.Index, name, rtas);
        }
        #endregion

        #region SetKeyword
        public void EnableKeyword(string keyword)
        {
            Shader.EnableKeyword(keyword);
        }
        public void DisableKeyword(string keyword)
        {
            Shader.DisableKeyword(keyword);
        }
        public void SetKeyword(string keyword, bool enabled)
        {
            if (enabled)
                EnableKeyword(keyword);
            else
                DisableKeyword(keyword);
        }

        public void EnableKeyword(CommandBuffer cb, string keyword)
        {
            cb.EnableShaderKeyword(keyword);
        }
        public void DisableKeyword(CommandBuffer cb, string keyword)
        {
            cb.DisableShaderKeyword(keyword);
        }
        public void SetKeyword(CommandBuffer cb, string keyword, bool enabled)
        {
            if (enabled)
                EnableKeyword(cb, keyword);
            else
                DisableKeyword(cb, keyword);
        }

        public void EnableKeyword(IComputeCommandBuffer cb, string keyword)
        {
            cb.EnableShaderKeyword(keyword);
        }
        public void DisableKeyword(IComputeCommandBuffer cb, string keyword)
        {
            cb.DisableShaderKeyword(keyword);
        }
        public void SetKeyword(IComputeCommandBuffer cb, string keyword, bool enabled)
        {
            if (enabled)
                EnableKeyword(cb, keyword);
            else
                DisableKeyword(cb, keyword);
        }
        #endregion

        #region DispatchGroups
        public void DispatchGroups(int kernelIndex, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            EnableKeyword(ComputeShaderUtility.DirectDispatch);
            DisableKeyword(ComputeShaderUtility.IndirectDispatch);
            Shader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
        }
        public void DispatchGroups(ComputeKernel kernel, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            DispatchGroups(kernel.Index, threadGroupsX, threadGroupsY, threadGroupsZ);
        }

        public void DispatchGroups(CommandBuffer cb, int kernelIndex, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            EnableKeyword(cb, ComputeShaderUtility.DirectDispatch);
            DisableKeyword(cb, ComputeShaderUtility.IndirectDispatch);
            cb.DispatchCompute(Shader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
        }
        public void DispatchGroups(CommandBuffer cb, ComputeKernel kernel, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            DispatchGroups(cb, kernel.Index, threadGroupsX, threadGroupsY, threadGroupsZ);
        }

        public void DispatchGroups(IComputeCommandBuffer cb, int kernelIndex, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            EnableKeyword(cb, ComputeShaderUtility.DirectDispatch);
            DisableKeyword(cb, ComputeShaderUtility.IndirectDispatch);
            cb.DispatchCompute(Shader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
        }
        public void DispatchGroups(IComputeCommandBuffer cb, ComputeKernel kernel, int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
        {
            DispatchGroups(cb, kernel.Index, threadGroupsX, threadGroupsY, threadGroupsZ);
        }
        #endregion

        #region DispatchIndirect
        public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            DisableKeyword(ComputeShaderUtility.DirectDispatch);
            EnableKeyword(ComputeShaderUtility.IndirectDispatch);
            Shader.DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
        }
        public void DispatchIndirect(ComputeKernel kernel, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            DispatchIndirect(kernel.Index, argsBuffer, argsOffset);
        }

        public void DispatchIndirect(CommandBuffer cb, int kernelIndex, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            DisableKeyword(cb, ComputeShaderUtility.DirectDispatch);
            EnableKeyword(cb, ComputeShaderUtility.IndirectDispatch);
            cb.DispatchCompute(Shader, kernelIndex, argsBuffer, argsOffset);
        }
        public void DispatchIndirect(CommandBuffer cb, ComputeKernel kernel, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            DispatchIndirect(cb, kernel.Index, argsBuffer, argsOffset);
        }

        public void DispatchIndirect(IComputeCommandBuffer cb, int kernelIndex, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            DisableKeyword(cb, ComputeShaderUtility.DirectDispatch);
            EnableKeyword(cb, ComputeShaderUtility.IndirectDispatch);
            cb.DispatchCompute(Shader, kernelIndex, argsBuffer, argsOffset);
        }
        public void DispatchIndirect(IComputeCommandBuffer cb, ComputeKernel kernel, GraphicsBuffer argsBuffer, uint argsOffset = 0)
        {
            DispatchIndirect(cb, kernel.Index, argsBuffer, argsOffset);
        }
        #endregion
    }
}