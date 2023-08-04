using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LittleFlighter.Custom.Attributes;

namespace LittleFlighter.Custom.Drawers
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ListToPopupAttribute))]
    public class ListToPopupDrawer: PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ListToPopupAttribute atb = attribute as ListToPopupAttribute;
            List<string> list = null;

            if (atb.Type.GetField(atb.PropertyName) != null)
                list = atb.Type.GetField(atb.PropertyName).GetValue(atb.Type) as List<string>;

            if (list != null && list.Count > 0)
            {
                int selectedIndex = Mathf.Max(list.IndexOf(property.stringValue), 0);
                selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, list.ToArray());
                property.stringValue = list[selectedIndex];
            }
            else 
                EditorGUI.PropertyField(position, property, label);
        }
    }
    #endif
}