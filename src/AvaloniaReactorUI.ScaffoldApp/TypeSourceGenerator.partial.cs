﻿using Avalonia;
using Avalonia.Controls.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AvaloniaReactorUI.ScaffoldApp
{
    public partial class TypeSourceGenerator
    {
        private readonly Type _typeToScaffold;

        public TypeSourceGenerator(Type typeToScaffold)
        {
            _typeToScaffold = typeToScaffold;

            var propertiesMap = _typeToScaffold.GetProperties()
                .Where(_ => !_.PropertyType.IsGenericType)
                .Where(_ => _.PropertyType != typeof(IControlTemplate))
                .Distinct(new PropertyInfoEqualityComparer())
                .ToDictionary(_ => _.Name, _ => _);

            Properties = _typeToScaffold.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(_ => typeof(AvaloniaProperty).IsAssignableFrom(_.FieldType))
                .Where(_ => _.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Select(_ => _.Name.Substring(0, _.Name.Length - "Property".Length))
                .Where(_ => propertiesMap.ContainsKey(_))
                .Select(_ => propertiesMap[_])
                .Where(_ => _.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Where(_ => !_.PropertyType.IsGenericType)
                .Where(_ => (_.GetSetMethod()?.IsPublic).GetValueOrDefault())
                .ToArray();

            
        }

        public string TypeName => _typeToScaffold.Name;

        public string BaseTypeName => _typeToScaffold.BaseType.Name == "AvaloniaObject" ? "VisualNode" : $"Rx{_typeToScaffold.BaseType.Name}";

        public bool IsTypeNotAbstractWithEmptyConstructur => !_typeToScaffold.IsAbstract && _typeToScaffold.GetConstructor(new Type[] { }) != null;

        public PropertyInfo[] Properties { get; }

    }

    internal class PropertyInfoEqualityComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }

    internal static class StringExtensions
    {
        public static string CamelCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;
            return Char.ToLowerInvariant(s[0]) + s.Substring(1, s.Length - 1);
        }

        public static string ToResevedWordTypeName(this string typename)
        {
            return typename switch
            {
                "SByte" => "sbyte",
                "Byte" => "byte",
                "Int16" => "short",
                "UInt16" => "ushort",
                "Int32" => "int",
                "UInt32" => "uint",
                "Int64" => "long",
                "UInt64" => "ulong",
                "Char" => "char",
                "Single" => "float",
                "Double" => "double",
                "Boolean" => "bool",
                "Decimal" => "decimal",
                "String" => "string",
                "Object" => "object",
                _ => typename,
            };
        }
    }

}