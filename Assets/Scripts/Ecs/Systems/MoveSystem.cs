using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class MoveSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<MoveComponent, PositionComponent>> _filter;

    public void Run(IEcsSystems systems)
    {
        foreach(var id in _filter.Value)
        {
            ref var moveComponent = ref _filter.Pools.Inc1.Get(id);
            ref var postionComponent = ref _filter.Pools.Inc2.Get(id);

            var postion = moveComponent.Movable.Move(moveComponent.Direction * moveComponent.Speed);
            postionComponent.Position = postion;
        }
    }
}
