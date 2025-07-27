using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
    public enum Lifetime
    {
        Singleton,
        Transient
    }

    public class DiContainer
    {
        private readonly Dictionary<Type, object> _singletons = new();
        private readonly Dictionary<Type, Func<object>> _factories = new();
        private readonly HashSet<object> _injectedObjects = new();

        public void Bind<T>(T instance)
        {
            _singletons[instance.GetType()] = instance;
        }

        public void Bind<T>(Lifetime type = Lifetime.Singleton) where T : class, new()
        {
            if(type == Lifetime.Singleton)
            {
                _singletons[typeof(T)] = Activator.CreateInstance(typeof(T));
            }
            else
            {
                _factories[typeof(T)] = () =>
                {
                    var instance = Activator.CreateInstance(typeof(T));

                    Inject(instance);

                    return instance;
                };
            }
        }

        public void Bind<TInterface, TImplementation>(Lifetime lifetime = Lifetime.Singleton) where TImplementation : TInterface, new()
        {
            if(lifetime == Lifetime.Singleton)
            {
                _singletons[typeof(TInterface)] = Activator.CreateInstance(typeof(TImplementation));
            }
            else
            {
                _factories[typeof(TInterface)] = () =>
                {
                    var instance = Activator.CreateInstance(typeof(TImplementation));

                    Inject(instance);

                    return instance;
                };
            }
        }

        public void Inject(object instance)
        {
            var type = instance.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public
                | BindingFlags.NonPublic)
                .Where(field => field.IsDefined(typeof(InjectAttribute), true));

            foreach (var field in fields)
            {
                if (_singletons.TryGetValue(field.FieldType, out var singleton))
                {
                    field.SetValue(instance, singleton);

                    if(!_injectedObjects.Contains(singleton))
                    {
                        Inject(singleton);
                        _injectedObjects.Add(singleton);
                    }  
                }
                else if (_factories.TryGetValue(field.FieldType, out var factory))
                {
                    field.SetValue(instance, factory?.Invoke());
                }
                else
                {
                    Debug.LogError($"Missing: {field.FieldType.Name} in {type.Name}");
                }
            }

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public
                | BindingFlags.NonPublic)
                .Where(method => method.IsDefined(typeof(InjectAttribute), true));

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                var args = ResolveAll(parameters);

                if(args != null)
                {
                    method.Invoke(instance, args);
                }
            }
        }

        private object[] ResolveAll(ParameterInfo[] parameters)
        {
            var args = new object[parameters.Length];
            
            for(int i = 0; i < parameters.Length; i++)
            {
                if (_singletons.TryGetValue(parameters[i].ParameterType, out var singleton))
                {
                    args[i] = singleton;

                    if(!_injectedObjects.Contains(singleton))
                    {
                        Inject(singleton);
                        _injectedObjects.Add(singleton);
                    }
                }
                else if (_factories.TryGetValue(parameters[i].ParameterType, out var factory))
                {
                    args[i] = factory?.Invoke();
                }
            }

            return args;
        }

        public void InjectAll()
        {
            foreach (var instance in _singletons.Values)
            {
                Inject(instance);
            }
        }
    }
}