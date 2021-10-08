using Avalonia;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI
{
    public class PropertyValue<T>
    {
        public PropertyValue(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }

    public static class PropertyValueExtenstions
    {
        public static void SetNullable<T>(this AvaloniaObject avaloniaObject, AvaloniaProperty<T?> property, PropertyValue<T?>? propertyValue)
        {
            if (propertyValue == null)
                avaloniaObject.ClearValue(property);
            else
                avaloniaObject.SetValue(property, propertyValue.Value);
        }

        public static void Set<T>(this AvaloniaObject avaloniaObject, AvaloniaProperty<T> property, PropertyValue<T>? propertyValue)
        {
            if (property == Visual.IsVisibleProperty &&
                avaloniaObject is Avalonia.Controls.Window)
            {
                //NOTE: Setting IsVisible for Window (expecially on startup i.e. when window is not yet visible) breaks Avalonia rendering engine!
                return;
            }

            if (propertyValue == null)
                avaloniaObject.ClearValue(property);
            else
                avaloniaObject.SetValue(property, propertyValue.Value);
        }
    }
}
