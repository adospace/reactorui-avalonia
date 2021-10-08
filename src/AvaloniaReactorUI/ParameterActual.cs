using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReactorUI
{
    public interface IParameter<T> where T : new()
    {
        string Name { get; }

        T Value { get; }

        void Set(Action<T> setAction);
    }

    internal class ParameterActual<T> : IParameter<T> where T : new()
    {
        private readonly HashSet<ParameterReference<T>> _parameterReferences = new();

        public ParameterActual(ParameterContext context, string name)
        {
            Context = context;
            Name = name;
        }

        public ParameterContext Context { get; }
        public string Name { get; }

        private T _value = new();

        public T Value => _value;

        public void Set(Action<T> setAction)
        {
            setAction(_value);
            Context.Owner.Owner.InvalidateComponent();
        }

        public void RegisterReference(ParameterReference<T> reference)
        {
            _parameterReferences.Add(reference);
        }
    }

    internal class ParameterReference<T> : IParameter<T> where T : new()
    {
        public ParameterReference(ParameterActual<T> actualParameter)
        {
            _actualParameter = actualParameter;
        }

        public string Name => _actualParameter.Name;
        private readonly ParameterActual<T> _actualParameter;

        public T Value => _actualParameter.Value;

        public void Set(Action<T> setAction)
        {
            _actualParameter.Set(setAction);
        }
    }

    public sealed class ParameterContext
    {
        private Dictionary<string, object> _parameters = new();

        public RxContext Owner { get; }

        internal ParameterContext(RxContext owner)
        {
            Owner = owner;
        }

        internal void MigrateTo(ParameterContext destinationContext)
        {
            foreach (var parameterEntry in _parameters)
            {
                destinationContext._parameters[parameterEntry.Key] = parameterEntry.Value;
            }
        }

        public IParameter<T> GetOrCreate<T>(string? name = null) where T : new()
        {
            name ??= typeof(T).FullName ?? throw new InvalidOperationException();
            _parameters.TryGetValue(name, out var parameter);

            if (parameter == null)
            {
                _parameters[name] = parameter = new ParameterActual<T>(this, name);
            }
            else
            {
                _parameters[name] = parameter = new ParameterReference<T>((parameter as ParameterActual<T>) ?? throw new InvalidOperationException($"Parameter '{name}' is not of type {typeof(T).FullName}"));
            }

            return (IParameter<T>)parameter;
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
