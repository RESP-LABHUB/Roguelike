using Assets.Scripts;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class EcsManager : MonoBehaviour
{
    [Inject] private EcsFactory _ecsFactory;

    private EcsWorld _world;
    private EcsSystems _modelSystems;
    private EcsSystems _modelFixedSystems;
    private EcsSystems _viewSystem;
    private EcsSystems _viewLateUpdateSystem;

    public EcsWorld World => _world;
    
    private void Start()
    {
        _world = new();
        _modelSystems = new(_world);
        _modelFixedSystems = new(_world);
        _viewSystem = new(_world);
        _viewLateUpdateSystem = new(_world);

        _modelSystems
            .Add(_ecsFactory.Create<CreateGameEntitySystem>())
            .Add(_ecsFactory.Create<CreatePlayerSystem>())
            .Add(_ecsFactory.Create<PlayerInputSystem>())
            .Inject()
            .Init();

        _modelFixedSystems
            .Add(_ecsFactory.Create<MoveSystem>())
            .Inject()
            .Init();

        _viewSystem
            .Add(_ecsFactory.Create<CreatePlayerVierwsystem>())
            .Add(_ecsFactory.Create<SyncViewRotationSystem>())
            .Inject()
            .Init();

        _viewLateUpdateSystem
            .Add(_ecsFactory.Create<SyncCameraSystem>())
            .Inject()
            .Init();
    }

    private void Update()
    {
        _modelSystems?.Run();
        _viewSystem?.Run();
    }

    private void FixedUpdate()
    {
        _modelFixedSystems?.Run();
    }

    private void LateUpdate()
    {
        _viewLateUpdateSystem?.Run();
    }

    private void OnDestroy()
    {
        _modelSystems?.Destroy();
        _modelSystems = null;

        _modelFixedSystems?.Destroy();
        _modelFixedSystems = null;

        _viewSystem?.Destroy();
        _viewSystem = null;

        _viewLateUpdateSystem?.Destroy();
        _viewLateUpdateSystem = null;

        _world?.Destroy();
        _world = null;
    }
}
