using System;
using System.Collections.Generic;

namespace AvaloniaReactorUI
{
    public sealed class RxContext
    {
        public RxContext(RxComponent owner)
        {
            Owner = owner;
        }

        public Dictionary<string, object> Properties { get; } = new();

        public ParameterContext Parameters { get; } = new ParameterContext();

        public RxComponent Owner { get; }

        internal void MigrateTo(RxContext context)
        {
            foreach (var propertEntry in Properties)
            {
                context.Properties[propertEntry.Key] = propertEntry.Value;
            }

            Parameters.MigrateTo(context.Parameters);
        }
    }

    public static class RxContextExtensions
    {
        public static T? GetProperty<T>(this RxContext context, string key, T? defaultValue = default)
        {
            if (context.Properties.TryGetValue(key, out var value))
                return (T)value;

            return defaultValue;
        }
    }
}