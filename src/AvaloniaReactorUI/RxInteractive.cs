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
using Avalonia.Interactivity;

using AvaloniaReactorUI.Internals;

namespace AvaloniaReactorUI
{
    public interface IRxInteractive : IRxLayoutable
    {
    }

    public class RxInteractive<T> : RxLayoutable<T>, IRxInteractive where T : Interactive, new()
    {
        public RxInteractive()
        {

        }

        public RxInteractive(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }


        protected override void OnUpdate()
        {

            base.OnUpdate();
        }

    }

    public class RxInteractive : RxInteractive<Interactive>
    {
        public RxInteractive()
        {

        }

        public RxInteractive(Action<Interactive> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class RxInteractiveExtensions
    {
    }
}