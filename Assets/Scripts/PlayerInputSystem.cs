using Assets.Scripts;
using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    [Inject] private PlayerInputManager _playerInputManager;

    public void Run(IEcsSystems systems)
    {
    }
}
