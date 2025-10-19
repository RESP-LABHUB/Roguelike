using UnityEngine;

public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody _rb;

    public Vector3 Move(Vector3 pos)
    {
        _rb.linearVelocity = pos;

        return _rb.position;
    }
}
