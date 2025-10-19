using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class CreatePlayerSystem : IEcsInitSystem
{
    private readonly EcsWorldInject _world;

    public void Init(IEcsSystems systems)
    {
        var playerEntity = _world.Value.NewEntity();
        _world.Value.AddComponent<PlayerComponent>(playerEntity);
        _world.Value.AddComponent<RotationComponent>(playerEntity);
        ref var moveComponent = ref _world.Value.AddComponent<MoveComponent>(playerEntity);
        ref var positionComponent = ref _world.Value.AddComponent<PositionComponent>(playerEntity);

        moveComponent.Speed = 10f;
        positionComponent.Position = new Vector3(0, -1.5f, 0);
    }
}
