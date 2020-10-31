﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     AvaloniaReactorUI.ScaffoldApp Version: 1.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Input;
using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public interface IRxInputElement : IRxInteractive
    {
        bool Focusable { get; set; }
        bool IsEnabled { get; set; }
        Cursor Cursor { get; set; }
        bool IsHitTestVisible { get; set; }
    }

    public class RxInputElement<T> : RxInteractive<T>, IRxInputElement where T : InputElement, new()
    {
        public RxInputElement()
        {

        }

        public RxInputElement(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public bool Focusable { get; set; } = (bool)InputElement.FocusableProperty.GetDefaultValue<T>();
        public bool IsEnabled { get; set; } = (bool)InputElement.IsEnabledProperty.GetDefaultValue<T>();
        public Cursor Cursor { get; set; } = (Cursor)InputElement.CursorProperty.GetDefaultValue<T>();
        public bool IsHitTestVisible { get; set; } = (bool)InputElement.IsHitTestVisibleProperty.GetDefaultValue<T>();

        protected override void OnUpdate()
        {
            NativeControl.Focusable = Focusable;
            NativeControl.IsEnabled = IsEnabled;
            NativeControl.Cursor = Cursor;
            NativeControl.IsHitTestVisible = IsHitTestVisible;

            base.OnUpdate();
        }

    }

    public class RxInputElement : RxInputElement<InputElement>
    {
        public RxInputElement()
        {

        }

        public RxInputElement(Action<InputElement> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class RxInputElementExtensions
    {
        public static T Focusable<T>(this T inputelement, bool focusable) where T : IRxInputElement
        {
            inputelement.Focusable = focusable;
            return inputelement;
        }



        public static T IsEnabled<T>(this T inputelement, bool isEnabled) where T : IRxInputElement
        {
            inputelement.IsEnabled = isEnabled;
            return inputelement;
        }



        public static T Cursor<T>(this T inputelement, Cursor cursor) where T : IRxInputElement
        {
            inputelement.Cursor = cursor;
            return inputelement;
        }



        public static T IsHitTestVisible<T>(this T inputelement, bool isHitTestVisible) where T : IRxInputElement
        {
            inputelement.IsHitTestVisible = isHitTestVisible;
            return inputelement;
        }



    }
}