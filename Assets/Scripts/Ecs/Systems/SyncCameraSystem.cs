using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class SyncCameraSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<CameraComponent>> _gameFilter;
    private EcsFilterInject<Inc<PlayerComponent, PositionComponent>> _playerFilter;

    private readonly EcsWorldInject _world;

    public void Run(IEcsSystems systems)
    {
        var playerId = _playerFilter.Value.GetFirstId();
        var gameId = _gameFilter.Value.GetFirstId();

        ref var positionComponent = ref _world.Value.GetComponent<PositionComponent>(playerId);
        ref var cameraComponent = ref _world.Value.GetComponent<CameraComponent>(gameId);

        var position = positionComponent.Position;

        cameraComponent.Transform.position = new Vector3(position.x + 3.88f, position.y + 34.69f, position.z - 35f);
    }
}
