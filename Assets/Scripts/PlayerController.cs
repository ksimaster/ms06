using System.Collections;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PickCollectable _pickCollectableScript;
    [SerializeField] private GameObject _prefabShootingObject;
    [SerializeField] private Transform _bonusRoad;
    [SerializeField] private float _range;
    [SerializeField] private float _fireRate;
    // private static int _money=250;
    // private static int _earnedMoney;
    public bool _isFinish;
    [SerializeField] private ParticleSystem _moneyParticle;
    [SerializeField] private GameObject[] _bullets;
    private int _index;
   /* public int Money
    {
        get { return _money; }
        set { _money = value; }
    }
    public int EarnedMoney
    {
        get { return _earnedMoney; }
        set { _earnedMoney = value; }
    }*/
    public float Range 
    {
        get {return _range; }
        set {_range=value; }
    }
    public float FireRate
    {
        get { return _fireRate; }
        set { _fireRate = value; }
    }
    void Awake()
    {
        CheckFirstDigitZeroOrNot();
    }
    void Start()
    {
        InvokeRepeating(nameof(ValueControl), 5f, .2f);
        StartCoroutine(Shooting());
    }
    public IEnumerator Shooting()
    {
        while(!_isFinish)
        {
            yield return new WaitForSeconds(_fireRate);
            if (_index>199)
            {
                _index = 0;
            }
            _bullets[_index].transform.position = transform.position-new Vector3(0,1.2f,-1f);//
            _bullets[_index].SetActive(true);
            _index++;
        } 
    }
    public void CheckFirstDigitZeroOrNot()
    {
        if (_pickCollectableScript.PlayerFirstDigit == 0 && _pickCollectableScript.PlayerSecondDigit == 0)
        {
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (_pickCollectableScript.PlayerFirstDigit == 0)
        {
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate") && other.transform.GetChild(0).GetComponent<GateManager>().TypePowerup == "Firerate")
        {
            other.gameObject.SetActive(false);
            if (_fireRate>0.085f)
            {
                _fireRate += (_fireRate - other.transform.GetChild(0).GetComponent<GateManager>().Number) / 200f;  
            }
            if (_fireRate < 0.085)
            {
                _fireRate = 0.085f;
            }
        }
        else if (other.CompareTag("Gate") && other.transform.GetChild(0).GetComponent<GateManager>().TypePowerup == "Range")
        {
            other.gameObject.SetActive(false);
            if (_range<3)
            {
                _range += (_range + other.transform.GetChild(0).GetComponent<GateManager>().Number) / 50f;
            }
            if (_range > 3) {
                _range = 3f;
            }
        }
        else if (other.CompareTag("Bonus"))
        {
            _bonusRoad.gameObject.SetActive(true);
            transform.DOJump(_bonusRoad.position - new Vector3(0f,-.35f,6f), 5f, 1, 1.5f).OnComplete(()=>
            {
                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = false;
                }
            });
        }
        else if (other.CompareTag("BonusEnd"))
        {
            transform.DOJump(_bonusRoad.position - new Vector3(0f,14f,-5f), 4f, 1, 1.2f).OnComplete(() =>
            {
                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
                }
                _bonusRoad.gameObject.SetActive(false);
            });
        }

        else if (other.CompareTag("Money"))
        {
            other.gameObject.SetActive(false);
            _moneyParticle.gameObject.SetActive(true);
            _moneyParticle.transform.position = transform.position;
            _moneyParticle.Play();
            _saveDatas.EarnedMoney += 10;
        }
    }

    void ValueControl()
    {
        if (_fireRate<0.085f)
        {
            _fireRate = 0.085f;
        }

        if (_range>3)
        {
            _range = 3;
        }
    }
}