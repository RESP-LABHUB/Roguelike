using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;

    void Start()
    {
        Object.Instantiate(_player, transform.position, Quaternion.identity);
    }
}
