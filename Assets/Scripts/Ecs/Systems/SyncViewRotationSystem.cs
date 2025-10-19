using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class SyncViewRotationSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<TransformComponent>> _filter;
    private readonly EcsWorldInject _world;

    public void Run(IEcsSystems systems)
    {
        foreach(var id in _filter.Value)
        {
            ref var transformComponent = ref _filter.Pools.Inc1.Get(id);
            ref var rotationComponent = ref _world.Value.GetComponent<RotationComponent>(id);

            transformComponent.Transform.rotation = rotationComponent.Rotation;
        }
    }
}
