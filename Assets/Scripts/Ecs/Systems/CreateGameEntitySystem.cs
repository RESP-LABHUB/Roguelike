using Assets.Scripts;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class CreateGameEntitySystem : IEcsInitSystem
{
    private readonly EcsWorldInject _world;

    public void Init(IEcsSystems systems)
    {
        var gameEntity = _world.Value.NewEntity();
        ref var cameraComponent = ref _world.Value.AddComponent<CameraComponent>(gameEntity);
        var camera = Camera.main;

        cameraComponent.Camera = camera;
        cameraComponent.Transform = camera.transform;
    }
}
