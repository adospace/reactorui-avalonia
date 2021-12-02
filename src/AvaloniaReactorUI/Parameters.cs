﻿using AvaloniaReactorUI.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI
{
    public interface IParameter
    {
        string Name { get; }

        object GetValue();
    }

    public interface IParameter<T> : IParameter where T : new()
    {
        T Value { get; }

        void Set(Action<T> setAction);

        ParameterContext Context { get; }
    }

    internal interface IParameterWithReferences<T> : IParameter<T> where T : new()
    {
        void RegisterReference(ParameterReference<T> reference);
    }

    internal class Parameters<T> : IParameterWithReferences<T> where T : new()
    {
        private readonly HashSet<WeakReference<ParameterReference<T>>> _parameterReferences = new();

        public Parameters(ParameterContext context, string name)
        {
            Context = context;
            Name = name;
        }

        public ParameterContext Context { get; }
        public string Name { get; }

        private readonly T _value = new();

        public T Value => _value;

        object IParameter.GetValue() => Value!;

        public void Set(Action<T> setAction)
        {
            setAction(_value);
            Context.Owner.Owner.InvalidateComponent();

            foreach (var componetOfReferencedParameter in _parameterReferences.ToList())
            {
                if (componetOfReferencedParameter.TryGetTarget(out var componetOfReferencedParameterValue))
                {
                    componetOfReferencedParameterValue.Context.Owner.Owner.InvalidateComponent();
                }
                else
                {
                    _parameterReferences.Remove(componetOfReferencedParameter);
                }                
            }
        }

        public void RegisterReference(ParameterReference<T> reference)
        {
            _parameterReferences.Add(new WeakReference<ParameterReference<T>>(reference));
        }
    }

    internal class ParameterReference<T> : IParameter<T> where T : new()
    {
        private readonly IParameter<T> _actualParameter;
        
        public ParameterReference(ParameterContext context, IParameterWithReferences<T> actualParameter)
        {
            Context = context;
            _actualParameter = actualParameter;
            actualParameter.RegisterReference(this);
        }

        public string Name => _actualParameter.Name;

        public ParameterContext Context { get; }

        public T Value => _actualParameter.Value;

        object IParameter.GetValue() => Value!;

        public void Set(Action<T> setAction)
        {
            _actualParameter.Set(setAction);
        }
    }

    public sealed class ParameterContext
    {
        private readonly Dictionary<string, IParameter> _parameters = new();

        public RxContext Owner { get; }

        internal ParameterContext(RxContext owner)
        {
            Owner = owner;
        }

        internal void MigrateTo(ParameterContext destinationContext)
        {
            foreach (var parameterEntry in _parameters)
            {
                if (destinationContext._parameters.TryGetValue(parameterEntry.Key, out var destinationParameter))
                {
                    CopyObjectExtensions.CopyPropertiesTo(
                        parameterEntry.Value.GetValue(),
                        destinationParameter.GetValue());
                }
            }
        }

        public IParameter<T> Create<T>(string? name = null) where T : new()
        {
            name ??= typeof(T).FullName ?? throw new InvalidOperationException();
            _parameters.TryGetValue(name, out var parameter);

            if (parameter == null)
            {
                _parameters[name] = parameter = new Parameters<T>(this, name);
            }

            return (IParameter<T>)parameter;
        }

        public IParameter<T>? Get<T>(string? name = null) where T : new()
        {
            name ??= typeof(T).FullName ?? throw new InvalidOperationException();
            _parameters.TryGetValue(name, out var parameter);

            return parameter as IParameter<T>;
        }

        public void Register<T>(IParameter<T> parameter, string? name = null) where T : new()
        {
            name ??= typeof(T).FullName ?? throw new InvalidOperationException();
            _parameters[name] = parameter;
        }
    }
    
    //public sealed class LayoutContext
    //{
    //    public ParameterContext Parameters { get; } = new ParameterContext();

    //    internal void MigrateTo(LayoutContext destinationContext)
    //    {
    //        Parameters.MigrateTo(destinationContext.Parameters);
    //    }
    //}
}
