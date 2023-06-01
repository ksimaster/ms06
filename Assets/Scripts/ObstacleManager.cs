using UnityEngine;
using DG.Tweening;
using TMPro;
using Cinemachine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerController _playerCont;
    [SerializeField] private TextMeshPro _countText;
    [SerializeField] private Transform _money;
    [SerializeField] private GameObject _gameOverParticle;
    [SerializeField] private CinemachineVirtualCamera _gameOverCamera;
    [SerializeField] private Material _grey;
    [SerializeField] private int _count;
    [SerializeField] private PickCollectable _pickCollectable;
    private void Start()
    {
        _money = transform.parent;
        _countText.text = _count.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pickCollectable.ToCancelInvoke();
            _gameOverCamera.Priority = 15;
            _playerCont._isFinish = true;
            _gameOverParticle.SetActive(true);
            other.transform.parent.GetComponent<Animator>().enabled = false;
            for (int i = 0; i < 4; i++)
            {
                other.transform.parent.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = _grey;
                other.transform.parent.GetChild(i).GetComponent<Animator>().enabled = false;
            }
            other.transform.parent.DOMove(other.transform.position - new Vector3(0f, .17f, .5f), .75f);
            other.transform.parent.DORotate(new Vector3(-90f, 0f, 0f), .75f).OnComplete(() => 
            {
                _uiManager._earnedMoneyText.text = _saveDatas.EarnedMoney.ToString() + "$";
                _uiManager._finishPanel.SetActive(true);
            });       
        }

        else if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.localPosition = other.gameObject.GetComponent<Bullet>()._firstPos;
            _count -= other.gameObject.GetComponent<Bullet>().Value;
            _countText.text = _count.ToString();
            if (_count<=0)
            {
                gameObject.SetActive(false);
                _money.transform.DOMoveY(_money.transform.position.y - 2.5f, .7f);
            }
        }
    }
}