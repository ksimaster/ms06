using UnityEngine;

public class MovingCollectable : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    void Update()
    {
        if (_uiManager.IsStart)
        {
            transform.Translate(2.2f * Time.deltaTime * Vector3.back);
        }
    }
}