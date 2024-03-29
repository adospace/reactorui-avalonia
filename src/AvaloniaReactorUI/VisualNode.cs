﻿using Avalonia;
using AvaloniaReactorUI.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AvaloniaReactorUI
{
    public interface IVisualNode
    {
        void AppendAnimatable<T>(object key, T animation, Action<T> action) where T : RxAnimation;

        Action<object, PropertyChangingEventArgs>? PropertyChangingAction { get; set; }

        Action<object, PropertyChangedEventArgs>? PropertyChangedAction { get; set; }

        IVisualNode? Parent { get; }

        void AddChild(VisualNode widget, AvaloniaObject childNativeControl);

        void RequestAnimationFrame();

        void RequireLayoutCycle();

        void RemoveChild(VisualNode visualNode, AvaloniaObject nativeControl);
    }

    public static class VisualNodeExtensions
    {
        public static T OnPropertyChanged<T>(this T element, Action<object, PropertyChangedEventArgs> action) where T : VisualNode
        {
            element.PropertyChangedAction = action;
            return element;
        }

        public static T OnPropertyChanging<T>(this T element, Action<object, System.ComponentModel.PropertyChangingEventArgs> action) where T : VisualNode
        {
            element.PropertyChangingAction = action;
            return element;
        }

        public static T When<T>(this T node, bool flag, Action<T> actionToApplyWhenFlagIsTrue) where T : VisualNode
        {
            if (flag)
            {
                actionToApplyWhenFlagIsTrue(node);
            }
            return node;
        }

        public static T WithAnimation<T>(this T node, Easing? easing = null, double duration = 600) where T : VisualNode
        {
            node.EnableCurrentAnimatableProperties(easing, duration);
            return node;
        }

        public static T WithKey<T>(this T node, object key) where T : VisualNode
        {
            node.Key = key;
            return node;
        }

        public static T WithMetadata<T>(this T node, string key, object value) where T : VisualNode
        {
            node.SetMetadata(key, value);
            return node;
        }

        public static T WithMetadata<T>(this T node, object value) where T : VisualNode
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            node.SetMetadata(value.GetType().FullName ?? throw new InvalidOperationException(), value);
            return node;
        }

        public static T WithOutAnimation<T>(this T node) where T : VisualNode
        {
            node.DisableCurrentAnimatableProperties();
            return node;
        }
    }

    public abstract class VisualNode : IVisualNode
    {
        protected bool _isMounted = false;

        protected bool _stateChanged = true;

        private readonly Dictionary<object, Animatable> _animatables = new();

        private readonly Dictionary<string, object?> _metadata = new();

        private IReadOnlyList<VisualNode>? _children = null;

        private bool _invalidated = false;

        protected VisualNode()
        {
            //System.Diagnostics.Debug.WriteLine($"{this}->Created()");
        }

        public int ChildIndex { get; private set; }
        public object? Key { get; set; }
        public Action<object, PropertyChangedEventArgs>? PropertyChangedAction { get; set; }
        public Action<object, PropertyChangingEventArgs>? PropertyChangingAction { get; set; }
        protected readonly Dictionary<AvaloniaProperty, object> _attachedProperties = new();
  
        public void SetAttachedProperty(AvaloniaProperty property, object value)
            => _attachedProperties[property] = value;

        //internal event EventHandler LayoutCycleRequest;
        internal IReadOnlyList<VisualNode> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<VisualNode>(RenderChildren().Where(_ => _ != null)!);
                    for (int i = 0; i < _children.Count; i++)
                    {
                        _children[i].ChildIndex = i;
                        _children[i].Parent = this;
                    }
                }
                return _children;
            }
        }

        internal bool IsAnimationFrameRequested { get; private set; } = false;
        internal bool IsLayoutCycleRequired { get; set; } = true;
        internal IVisualNode? Parent { get; private set; }

        IVisualNode? IVisualNode.Parent => Parent;

        public void AppendAnimatable<T>(object key, T animation, Action<T> action) where T : RxAnimation
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (animation is null)
            {
                throw new ArgumentNullException(nameof(animation));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var newAnimatableProperty = new Animatable(key, animation, new Action<RxAnimation>(target => action((T)target)));

            _animatables[key] = newAnimatableProperty;
        }

        public T? GetMetadata<T>(string key, T? defaultValue = default)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("can'be null or empty", nameof(key));
            }

            if (_metadata.TryGetValue(key, out var value))
                return (T?)value;

            return defaultValue;
        }

        public T? GetMetadata<T>(T? defaultValue = default)
            => GetMetadata(typeof(T).FullName ?? throw new InvalidOperationException(), defaultValue);

        public void SetMetadata<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("can'be null or empty", nameof(key));
            }

            _metadata[key] = value;
        }

        public void SetMetadata<T>(T value)
        {
            _metadata[typeof(T).FullName ?? throw new InvalidOperationException()] = value;
        }

        void IVisualNode.AddChild(VisualNode widget, AvaloniaObject childNativeControl)
        {
            OnAddChild(widget, childNativeControl);
        }

        internal virtual bool Animate()
        {
            if (!IsAnimationFrameRequested)
                return false;

            var animated = AnimateThis();

            OnAnimate();

            foreach (var child in Children)
            {
                if (child.Animate())
                    animated = true;
            }

            if (!animated)
            {
                IsAnimationFrameRequested = false;
            }

            return animated;
        }

        internal void DisableCurrentAnimatableProperties()
        {
            foreach (var _ in _animatables.Where(_ => _.Value.IsEnabled == null))
            {
                _.Value.IsEnabled = false;
            };
        }

        internal void EnableCurrentAnimatableProperties(Easing? easing = null, double duration = 600)
        {
            foreach (var _ in _animatables.Where(_ => _.Value.IsEnabled == null).Select(_ => _.Value))
            {
                if (_.Animation is RxTweenAnimation tweenAnimation)
                {
                    tweenAnimation.Easing ??= easing;
                    tweenAnimation.Duration ??= duration;
                    _.IsEnabled = true;
                }
            };
        }

        internal virtual void Layout(IVisualNode parent)
        {
            Parent = parent;

            if (!IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = false;

            if (_invalidated)
            {
                var oldChildren = Children;
                _children = null;
                MergeChildrenFrom(oldChildren);
                _invalidated = false;
            }

            if (!_isMounted && Parent != null)
                OnMount();

            CommitAnimations();

            AnimateThis();

            if (_stateChanged)
                OnUpdate();

            foreach (var child in Children)
                child.Layout(this);
        }

        internal virtual void MergeChildrenFrom(IReadOnlyList<VisualNode> oldChildren)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (oldChildren.Count > i)
                {
                    oldChildren[i].MergeWith(Children[i]);
                }
            }

            for (int i = Children.Count; i < oldChildren.Count; i++)
            {
                oldChildren[i].Unmount();
                oldChildren[i].Parent = null;
            }
        }

        internal virtual void MergeWith(VisualNode newNode)
        {
            if (newNode == this)
                return;

            for (int i = 0; i < Children.Count; i++)
            {
                if (newNode.Children.Count > i)
                {
                    Children[i].MergeWith(newNode.Children[i]);
                }
            }

            for (int i = newNode.Children.Count; i < Children.Count; i++)
            {
                Children[i].Unmount();
                Children[i].Parent = null;
            }

            Parent = null;
        }

        void IVisualNode.RemoveChild(VisualNode widget, AvaloniaObject childNativeControl)
        {
            OnRemoveChild(widget, childNativeControl);
        }

        internal void Unmount()
        {
            OnUnmount();
        }

        protected internal virtual void OnLayoutCycleRequested()
        {
        }

        protected virtual void CommitAnimations()
        {
            if (_animatables.Any(_ => _.Value.IsEnabled.GetValueOrDefault() && !_.Value.Animation.IsCompleted()))
            {
                ((IVisualNode)this).RequestAnimationFrame();
            }
        }

        protected T? GetParent<T>() where T : IVisualNode
        {
            var parent = Parent;
            while (parent != null && !(parent is T))
                parent = parent.Parent;

            return (T?)parent;
        }

        protected void Invalidate()
        {
            _invalidated = true;

            ((IVisualNode)this).RequireLayoutCycle();

            OnInvalidated();
            //System.Diagnostics.Debug.WriteLine($"{this}->Invalidated()");
        }

        protected virtual void OnAddChild(VisualNode widget, AvaloniaObject childNativeControl)
        {
        }

        protected virtual void OnAnimate()
        {
        }

        protected virtual void OnInvalidated()
        {
        }

        protected virtual void OnMigrated(VisualNode newNode)
        {
            OnDetachNativeEvents();

            foreach (var newAnimatableProperty in newNode._animatables)
            {
                if (_animatables.TryGetValue(newAnimatableProperty.Key, out var oldAnimatableProperty))
                {
                    if (oldAnimatableProperty.Animation.GetType() == newAnimatableProperty.Value.Animation.GetType())
                    {
                        newAnimatableProperty.Value.Animation.MigrateFrom(oldAnimatableProperty.Animation);
                    }
                }
            }

            _animatables.Clear();
        }

        protected virtual void OnMount()
        {
            _isMounted = true;
        }

        protected virtual void OnRemoveChild(VisualNode widget, AvaloniaObject childNativeControl)
        {
        }

        protected virtual void OnUnmount()
        {
            OnDetachNativeEvents();

            _isMounted = false;
            Parent = null;
        }

        protected virtual void OnUpdate()
        {
            _stateChanged = false;

            OnAttachNativeEvents();
        }

        protected virtual void OnAttachNativeEvents()
        { 
        
        }

        protected virtual void OnDetachNativeEvents()
        { 
        
        }

        protected virtual IEnumerable<VisualNode?> RenderChildren()
        {
            yield break;
        }

        private bool AnimateThis()
        {
            bool animated = false;
            foreach (var _ in _animatables
                .Where(_ => _.Value.IsEnabled.GetValueOrDefault() && !_.Value.Animation.IsCompleted()))
            {
                _.Value.Animate();
                animated = true;
            };

            return animated;
        }

        void IVisualNode.RequestAnimationFrame()
        {
            IsAnimationFrameRequested = true;
            Parent?.RequestAnimationFrame();
        }

        void IVisualNode.RequireLayoutCycle()
        {
            if (IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = true;
            Parent?.RequireLayoutCycle();
            OnLayoutCycleRequested();
        }
    }

    internal interface IVisualNodeWithNativeControl
    {
        TResult GetNativeControl<TResult>() where TResult : AvaloniaObject;
    }

    // public interface IVisualNodeWithAttachedProperties
    // {
    //     void SetAttachedProperty(AvaloniaProperty property, object value);
    // }

    public abstract class VisualNode<T> : VisualNode, IVisualNodeWithNativeControl where T : AvaloniaObject, new()
    {
        protected AvaloniaObject? _nativeControl;

        private readonly Action<T?>? _componentRefAction;

        protected VisualNode()
        { }

        protected VisualNode(Action<T?> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

        protected T? NativeControl { get => (T?)_nativeControl; }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType() == GetType())
            {
                ((VisualNode<T>)newNode)._nativeControl = this._nativeControl;
                ((VisualNode<T>)newNode)._isMounted = this._nativeControl != null;
                ((VisualNode<T>)newNode)._componentRefAction?.Invoke(NativeControl);
                OnMigrated(newNode);

                base.MergeWith(newNode);
            }
            else
            {
                this.Unmount();
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            //if (NativeControl != null)
            //{
            //    //NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
            //    //NativeControl.PropertyChanging -= NativeControl_PropertyChanging;

            //    foreach (var attachedProperty in _attachedProperties)
            //    {
            //        if (attachedProperty.Key.GetMetadata<T>() is IDirectPropertyMetadata directPropertyMetadata)
            //        {
            //            NativeControl.SetValue(attachedProperty.Key, directPropertyMetadata.UnsetValue);
            //        }
            //        else if (attachedProperty.Key.GetMetadata<T>() is IStyledPropertyMetadata styledPropertyMetadata)
            //        {
            //            NativeControl.SetValue(attachedProperty.Key, styledPropertyMetadata.DefaultValue);
            //        }
            //    }
            //}

            //_attachedProperties.Clear();

            base.OnMigrated(newNode);
        }

        protected override void OnMount()
        {
            _nativeControl ??= new T();
            Parent?.AddChild(this, _nativeControl);
            _componentRefAction?.Invoke(NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            if (_nativeControl != null)
            {
                //_nativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                //_nativeControl.PropertyChanging -= NativeControl_PropertyChanging;

                Parent?.RemoveChild(this, _nativeControl);

                _nativeControl = null;
                _componentRefAction?.Invoke(null);
            }

            base.OnUnmount();
        }

        protected override void OnUpdate()
        {
            foreach (var attachedProperty in _attachedProperties)
            {
                System.Diagnostics.Trace.WriteLine($"{NativeControl} {attachedProperty.Key} = {attachedProperty.Value}");
                NativeControl?.SetValue(attachedProperty.Key, attachedProperty.Value);
            }

            //if (PropertyChangedAction != null)
            //    NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            //if (PropertyChangingAction != null)
            //    NativeControl.PropertyChanging += NativeControl_PropertyChanging;

            base.OnUpdate();
        }

        //private void NativeControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    PropertyChangedAction?.Invoke(sender, e);
        //}

        //private void NativeControl_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        //{
        //    PropertyChangingAction?.Invoke(sender, new System.ComponentModel.PropertyChangingEventArgs(e.PropertyName));
        //}

        TResult IVisualNodeWithNativeControl.GetNativeControl<TResult>()
        {
            return (_nativeControl as TResult) ??
                throw new InvalidOperationException($"Unable to convert from type {typeof(T)} to type {typeof(TResult)} when getting the native control");
        }
    }

}
