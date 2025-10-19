using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public Vector3 GetMoveDirection()
    {
        return _playerInput.Player.Move.ReadValue<Vector3>();
    }

    public Vector2 GetMousePosition()
    {
        return _playerInput.Player.MousePosition.ReadValue<Vector2>();
    }
}
