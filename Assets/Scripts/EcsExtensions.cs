using Leopotam.EcsLite;
using UnityEngine;

public static class EcsExtensions
{
    public static ref T GetComponent<T>(this EcsWorld world, int entity) where T : struct
    {
        var pool = world.GetPool<T>();
        ref var component = ref pool.Get(entity);

        return ref component;
    }

    public static ref T AddComponent<T>(this EcsWorld world, int entity) where T : struct
    {
        var pool = world.GetPool<T>();
        ref var component = ref pool.Add(entity);

        return ref component;
    }

    public static bool HasComponent<T>(this EcsWorld world, int entity) where T: struct
    {
        var pool = world.GetPool<T>();

        return pool.Has(entity);
    }

    public static ref T ResolveComponent<T>(this EcsWorld world, int entity) where T : struct
    {
        if(HasComponent<T>(world, entity))
        {
            var pool = world.GetPool<T>();

            return ref pool.Get(entity);
        }
        else
        {
            return ref AddComponent<T>(world, entity);
        }
    }
}
