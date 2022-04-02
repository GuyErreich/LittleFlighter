using System;
using UnityEngine;

namespace LittleFlighter.Custom.Attributes
{
    public class ListToPopupAttribute : PropertyAttribute
    {
        private Type type;
        private string propertyName;

        public Type Type{ get { return type; } }
        public string PropertyName{ get { return propertyName; } }

        public ListToPopupAttribute(Type type, string propertyName)
        {
            this.type = type;
            this.propertyName = propertyName;
        }
    }
}