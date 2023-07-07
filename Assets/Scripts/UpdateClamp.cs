using UnityEngine;

public class UpdateClamp : MonoBehaviour
{
    [SerializeField] private Transform _player;
    void FixedUpdate()
    {
        UpdateLeftClamp();
    }
    void UpdateLeftClamp()
    {
        if (_player.GetChild(0).GetComponent<MeshRenderer>().enabled)
        {
            transform.localPosition = new Vector3(-1.5f, transform.localPosition.y, transform.localPosition.z);
        }
        if (_player.GetChild(1).GetComponent<MeshRenderer>().enabled)
        {
            transform.localPosition = new Vector3(-2f, transform.localPosition.y, transform.localPosition.z);
        }
        else if (_player.GetChild(2).GetComponent<MeshRenderer>().enabled)
        {
            transform.localPosition = new Vector3(-2.5f, transform.localPosition.y, transform.localPosition.z);
        }
        else if(_player.GetChild(3).GetComponent<MeshRenderer>().enabled)
        {
            transform.localPosition = new Vector3(-3f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}