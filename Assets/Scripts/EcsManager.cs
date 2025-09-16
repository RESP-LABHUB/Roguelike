using Assets.Scripts;
using Leopotam.EcsLite;
using UnityEngine;

public class EcsManager : MonoBehaviour
{
    [Inject] private EcsFactory _ecsFactory;

    private EcsWorld _world;
    private EcsSystems _updateSystems;

    public EcsWorld World => _world;
    
    private void Start()
    {
        _world = new();
        _updateSystems = new(_world);

        _updateSystems
            .Add(_ecsFactory.Create<PlayerInputSystem>())
            ?.Init();
    }

    private void Update()
    {
        _updateSystems?.Run();
    }

    private void OnDestroy()
    {
        _updateSystems?.Destroy();
        _updateSystems = null;

        _world?.Destroy();
        _world = null;
    }
}
