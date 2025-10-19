using Assets.Scripts;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class CreatePlayerVierwsystem : IEcsInitSystem
{
    [Inject] private readonly Player _playertPrefab;

    private EcsFilterInject<Inc<PlayerComponent, PositionComponent, MoveComponent>> _filter;
    private EcsWorldInject _world;

    public void Init(IEcsSystems systems)
    {
        var id = _filter.Value.GetFirstId();

        ref var positionComponent = ref _world.Value.GetComponent<PositionComponent>(id);
        ref var moveComponent = ref _world.Value.GetComponent<MoveComponent>(id);
        ref var transformComponent = ref _world.Value.AddComponent<TransformComponent>(id);

        var playerView = Object.Instantiate(_playertPrefab, positionComponent.Position, Quaternion.identity);

        moveComponent.Movable = playerView;
        transformComponent.Transform = playerView.transform;
    }
}
