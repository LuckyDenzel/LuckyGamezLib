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
            SerializedProperty boolProp = property.serializedObject.FindProperty(enableIfAttribute.boolName);

            bool enabled = true;

            if (boolProp != null && boolProp.propertyType == SerializedPropertyType.Boolean) { 
                enabled = boolProp.boolValue;
            } else {
                Debug.LogWarning("Can't find the bool you referenced in the EnableIf attribute!");
            }

            bool previousGuiState = GUI.enabled;
            GUI.enabled = enabled;

            EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = previousGuiState;
        }
    }

# endif
}