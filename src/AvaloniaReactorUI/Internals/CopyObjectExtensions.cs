﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AvaloniaReactorUI.Internals
{
    internal static class CopyObjectExtensions
    {
        public static void CopyPropertiesTo<T>(this T source, object dest, PropertyInfo[] destProps)
        {
            var sourceProps = typeof(T).GetProperties()
                .Where(x => x.CanRead)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                var targetProperty = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProperty != null)
                {
                    var sourceValue = sourceProp.GetValue(source, null);
                    if (sourceValue != null && sourceValue.GetType().IsEnum)
                    {
                        sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
                    }

                    try
                    {
                        targetProperty.SetValue(dest, sourceValue, null);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Unable to copy property '{targetProperty.Name}' of state ({source?.GetType()}) to new state after hot reload (Exception: '{ex.Message}')");
                    }
                }
            }
        }

        public static void CopyPropertiesTo(object source, object dest)
        {
            var sourceProps = source.GetType()
                .GetProperties()
                .Where(x => x.CanRead)
                .ToList();

            var destProps = dest.GetType()
                .GetProperties()
                .Where(_ => _.CanWrite)
                .ToArray();

            foreach (var sourceProp in sourceProps)
            {
                var targetProperty = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProperty != null)
                {
                    var sourceValue = sourceProp.GetValue(source, null);
                    if (sourceValue != null && sourceValue.GetType().IsEnum)
                    {
                        sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
                    }

                    try
                    {
                        targetProperty.SetValue(dest, sourceValue, null);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Unable to copy property '{targetProperty.Name}' of state ({source?.GetType()}) to new state after hot reload (Exception: '{ex.Message}')");
                    }
                }
            }
        }
    }
}
