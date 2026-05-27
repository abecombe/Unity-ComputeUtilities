using UnityEngine;

namespace Abecombe.GpuTools
{
    /// <summary>
    /// Configuration for GpuTools compute shaders.
    /// </summary>
    /// <remarks>
    /// This class holds references to the compute shaders used in the GpuTools Package.
    /// </remarks>
    [CreateAssetMenu(fileName = "ComputeShaderConfig", menuName = "GpuTools/ComputeConfig")]
    public class ComputeShaderConfig : ScriptableObject
    {
        public ComputeShader UtilityShader;
    }
}