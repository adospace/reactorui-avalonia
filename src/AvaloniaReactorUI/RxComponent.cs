﻿using Avalonia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AvaloniaReactorUI.Internals;
using Avalonia.Controls;
using Avalonia.Threading;

namespace AvaloniaReactorUI
{
    public interface IRxComponent
    {
        RxContext Context { get; }
    }

    public abstract class RxComponent : VisualNode, IEnumerable<VisualNode>
    {
        public abstract VisualNode Render();

        private readonly List<VisualNode> _children = new List<VisualNode>();

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }   

        public void Add(params VisualNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            _children.AddRange(nodes);
        }

        protected new IReadOnlyList<VisualNode> Children()
            => _children;

        private IRxHostElement GetPageHost()
        {
            var current = Parent;
            while (current != null && !(current is IRxHostElement))
                current = current.Parent;

            return current as IRxHostElement;
        }

        protected Window ContainerWindow
        {
            get
            {
                return GetPageHost()?.ContainerWindow;
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, AvaloniaObject nativeControl)
        {
            foreach (var attachedProperty in _attachedProperties)
            {
                nativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
            }

            Parent.AddChild(this, nativeControl);
        }

        protected sealed override void OnRemoveChild(VisualNode widget, AvaloniaObject nativeControl)
        {
            Parent.RemoveChild(this, nativeControl);
            
            foreach (var attachedProperty in _attachedProperties)
            {
                if (attachedProperty.Key.GetMetadata(nativeControl.GetType()) is IDirectPropertyMetadata directPropertyMetadata)
                {
                    nativeControl.SetValue(attachedProperty.Key, directPropertyMetadata.UnsetValue);
                }
                else if (attachedProperty.Key.GetMetadata(nativeControl.GetType()) is IStyledPropertyMetadata styledPropertyMetadata)
                {
                    nativeControl.SetValue(attachedProperty.Key, styledPropertyMetadata.DefaultValue);
                }
            }           
        }

        protected sealed override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Render();
        }

        protected sealed override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected sealed override void OnAnimate()
        {
            base.OnAnimate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType().FullName == GetType().FullName)
            {
                ((RxComponent)newNode)._isMounted = true;
                ((RxComponent)newNode).OnUpdated();
                base.MergeWith(newNode);
            }
            else
            {
                Unmount();
            }
        }

        protected sealed override void OnMount()
        {
            //System.Diagnostics.Debug.WriteLine($"Mounting {Key ?? GetType()} under {Parent.Key ?? Parent.GetType()} at index {ChildIndex}");

            base.OnMount();

            OnMounted();
        }

        protected virtual void OnMounted()
        {
        }

        protected sealed override void OnUnmount()
        {
            OnWillUnmount();

            foreach (var child in base.Children)
            {
                child.Unmount();
            }

            base.OnUnmount();
        }

        protected virtual void OnWillUnmount()
        {
        }

        protected virtual void OnUpdated()
        { }

        //public INavigation Navigation
        //    => RxApplication.Instance.Navigation;

        public RxContext Context
            => RxApplication.Instance.Context;

    }

    public static class RxComponentExtensions
    {
        public static T WithContext<T>(this T node, string key, object value) where T : IRxComponent
        {
            node.Context[key] = value;
            return node;
        }
    }

    internal interface IRxComponentWithState
    {
        object State { get; }

        PropertyInfo[] StateProperties { get; }

        void ForwardState(object stateFromOldComponent);
    }

    internal interface IRxComponentWithProps
    {
        object Props { get; }

        PropertyInfo[] PropsProperties { get; }
    }

    public interface IState
    {
    }

    public interface IProps
    {
    }

    public abstract class RxComponentWithProps<P> : RxComponent, IRxComponentWithProps where P : class, IProps, new()
    {
        public RxComponentWithProps(P props = null)
        {
            Props = props ?? new P();
        }

        public P Props { get; private set; }
        object IRxComponentWithProps.Props => Props;
        public PropertyInfo[] PropsProperties => typeof(P).GetProperties().Where(_ => _.CanWrite).ToArray();
    }

    public abstract class RxComponent<S, P> : RxComponentWithProps<P>, IRxComponentWithState where S : class, IState, new() where P : class, IProps, new()
    {
        private IRxComponentWithState _newComponent;
        
        protected RxComponent(S state = null, P props = null)
            : base(props)
        {
            State = state ?? new S();
        }

        public S State { get; private set; }

        public PropertyInfo[] StateProperties => typeof(S).GetProperties().Where(_ => _.CanWrite).ToArray();

        object IRxComponentWithState.State => State;

        void IRxComponentWithState.ForwardState(object stateFromOldComponent)
        {
            stateFromOldComponent.CopyPropertiesTo(State, StateProperties);

            if (!Dispatcher.UIThread.CheckAccess())
                Dispatcher.UIThread.Post(Invalidate);
            else
                Invalidate();
        }

        protected virtual void SetState(Action<S> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(State);

            if (_newComponent != null)
            {
                _newComponent.ForwardState(State);
                return;
            }

            if (!Dispatcher.UIThread.CheckAccess())
                Dispatcher.UIThread.Post(Invalidate);
            else
                Invalidate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode is IRxComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;

                State.CopyPropertiesTo(newComponentWithState.State, newComponentWithState.StateProperties);
            }

            if (newNode is IRxComponentWithProps newComponentWithProps)
            {
                Props.CopyPropertiesTo(newComponentWithProps.Props, newComponentWithProps.PropsProperties);
            }

            base.MergeWith(newNode);
        }
    }

    public class EmptyProps : IProps
    { }

    public abstract class RxComponent<S> : RxComponent<S, EmptyProps> where S : class, IState, new()
    {
        protected RxComponent(S state = null, EmptyProps props = null)
            : base(state, props)
        {
        }
    }

}
