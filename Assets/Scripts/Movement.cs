using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour, IDragHandler
{
    [SerializeField] private PlayerController _playerCont;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Transform _leftClamp;
    [SerializeField] private Transform _rightClamp;
    [SerializeField] private Transform _player;
    [SerializeField] private float _autoSpeed = 4f;
    [SerializeField] private float _sensivity = 0.005f;
    private Vector3 _currentPos;
    public void OnDrag(PointerEventData eventData)
    {
        _currentPos = _player.position;
        _currentPos.x = Mathf.Clamp(_player.position.x + eventData.delta.x * _sensivity, _leftClamp.position.x, _rightClamp.position.x);
        _player.position = _currentPos;
    }
    void Update()
    {
        if (_uiManager.IsStart && !_playerCont._isFinish)
        {
            _player.transform.position+= _autoSpeed * Time.deltaTime * Vector3.forward;
        }
    }
}