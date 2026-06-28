using UnityEditor;

namespace Abecombe.ComputeUtilities
{
    [CustomPropertyDrawer(typeof(ComputeProgram))]
    internal class ComputeProgramPropertyDrawer : PropertyDrawer
    {
        private const string ShaderPropertyName = "_shader";

        public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
        {
            var shaderProperty = property.FindPropertyRelative(ShaderPropertyName);
            if (shaderProperty == null)
            {
                EditorGUI.LabelField(position, label.text, "Missing _shader property");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, shaderProperty, label);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, UnityEngine.GUIContent label)
        {
            var shaderProperty = property.FindPropertyRelative(ShaderPropertyName);
            return shaderProperty == null
                ? EditorGUIUtility.singleLineHeight
                : EditorGUI.GetPropertyHeight(shaderProperty, label);
        }
    }
}
