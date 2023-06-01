using UnityEngine;

public class Factor : MonoBehaviour
{
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private int _factor;
    [SerializeField] private PlayerController _playerCont;
    private void OnTriggerStay(Collider other)
    {
        if (_playerCont._isFinish && other.CompareTag("Player"))
        {
            _saveDatas.EarnedMoney *= _factor;
            BoxCollider boxCollider = transform.GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }
    }
}