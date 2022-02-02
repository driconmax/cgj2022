using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void RegisterServices<T>(T service)
    {
        var type = typeof(T);
        if(_services.ContainsKey(type)) 
            throw new Exception($"Service {type} is already registered");
        _services.Add(type, service);
    }

    public static T GetServices<T>()
    {
        var type = typeof(T);
        if (!_services.TryGetValue(type, out var service))
            throw new Exception($"Service {type} not found");
        return (T)service;
    }

}