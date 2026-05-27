using UnityEngine;

namespace Abecombe.ComputeUtilities
{
    /// <summary>
    /// Configuration for the package's utility compute shaders.
    /// </summary>
    /// <remarks>
    /// Holds references to compute shaders used internally by the package.
    /// </remarks>
    [CreateAssetMenu(fileName = "ComputeShaderConfig", menuName = "Abecombe/Compute Utilities/Compute Config")]
    public class ComputeShaderConfig : ScriptableObject
    {
        public ComputeShader UtilityShader;
    }
}
