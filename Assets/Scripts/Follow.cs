using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, _player.position.y, _player.position.z);
    }
}