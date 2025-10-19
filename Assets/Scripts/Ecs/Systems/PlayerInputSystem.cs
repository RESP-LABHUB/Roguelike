using Assets.Scripts;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    [Inject] private PlayerInputManager _playerInputManager;

    private readonly EcsFilterInject<Inc<PlayerComponent, MoveComponent, RotationComponent, PositionComponent>> _playerFilter;
    private readonly EcsFilterInject<Inc<CameraComponent>> _cameraFilter;

    private readonly EcsWorldInject _world;
    private Plane _plane = new Plane(Vector3.up, Vector3.zero);

    public void Run(IEcsSystems systems)
    {
        Camera camera = _cameraFilter.Pools.Inc1.Get(0).Camera;

        foreach(var id in _playerFilter.Value)
        {
            ref var moveComponent = ref _playerFilter.Pools.Inc2.Get(id);
            ref var rotationComponent = ref _playerFilter.Pools.Inc3.Get(id);
            ref var positionComponent = ref _playerFilter.Pools.Inc4.Get(id);

            var moveDirection = _playerInputManager.GetMoveDirection();
            var ray = camera.ScreenPointToRay(_playerInputManager.GetMousePosition());

            if(_plane.Raycast(ray, out var enter))
            {
                var hitPoint = ray.GetPoint(enter);
                var playerPostionOnPlane = _plane.ClosestPointOnPlane(hitPoint);
                var rotation = Quaternion.LookRotation(playerPostionOnPlane);
                rotationComponent.Rotation = rotation;
            }

            moveComponent.Direction = moveDirection;
        }
    }
}
