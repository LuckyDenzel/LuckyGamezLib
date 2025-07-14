using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace LuckyGamezLib {
    public class EnableIfAttribute : PropertyAttribute {

        public string boolName;


        public EnableIfAttribute(string boolName) {
            this.boolName = boolName;
        }

    }


#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EnableIfAttribute enableIfAttribute = (EnableIfAttribute)attribute;
            bool enabled = true;

            // Try to find the property first
            SerializedProperty boolProp = property.serializedObject.FindProperty(enableIfAttribute.boolName);

            if (boolProp != null && boolProp.propertyType == SerializedPropertyType.Boolean) {
                enabled = boolProp.boolValue;
            }
            else {
                // Try method fallback
                var target = property.serializedObject.targetObject;
                var method = target.GetType().GetMethod(enableIfAttribute.boolName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (method != null && method.ReturnType == typeof(bool) && method.GetParameters().Length == 0) {
                    enabled = (bool)method.Invoke(target, null);
                }
                else {
                    Debug.LogWarning($"EnableIf: Can't find boolean field or method named '{enableIfAttribute.boolName}' on {target}");
                }
            }

            bool previousGUIState = GUI.enabled;
            GUI.enabled = enabled;

            EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = previousGUIState;
        }
    }

# endif
}