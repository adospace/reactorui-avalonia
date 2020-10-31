using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;

namespace AvaloniaReactorUI.Internals
{
    public static class AvaloniaExtensions
    {
        public static object GetDefaultValue<T>(this AvaloniaProperty property) where T : AvaloniaObject
        {
            if (property.GetMetadata<T>() is IStyledPropertyMetadata styledPropertyMetadata)
            {
                return styledPropertyMetadata.DefaultValue;
            }
            if (property.GetMetadata<T>() is IDirectPropertyMetadata directPropertyMetadata)
            {
                return directPropertyMetadata.UnsetValue;
            }

            throw new InvalidOperationException($"Unable to get default value for '{property.Name}'");
        }
    }
}
