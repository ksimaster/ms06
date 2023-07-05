using System.Collections;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerController _playerCont;
    [SerializeField] private GameObject[] _numberMeshes;
    [SerializeField] private GameObject _plusMesh;
    [SerializeField] private Material _green;
    [SerializeField] private Material _greenFade;
    [SerializeField] private int _valueFirstDigit;
    [SerializeField] private int _valueSecondDigit;
    [SerializeField] private int _valueThirdDigit;
    [SerializeField] private int _valueFourthDigit;
    [SerializeField] private int _valueFifthDigit;
    [SerializeField] private int _value;//bu gatelerdeki text iзin gerekliydi usttekiler ise model isteyen carpismalar icin
    [SerializeField] private CollectableManager _collectableManager;
    private int _surplus;
    private bool _goToEnd;

    public Vector3 _firstPos;
    public int Value 
    {
        get { return _value; }
        set { _value =value; }
    }
    void Start()
    {
        _firstPos = transform.localPosition;
        StartCoroutine(SetDeactiveBullet());
        transform.GetChild(1).GetComponent<MeshFilter>().sharedMesh = _numberMeshes[_saveDatas.BulletCount].GetComponent<MeshFilter>().sharedMesh;
        _valueThirdDigit = _saveDatas.BulletCount;
        _value = _valueThirdDigit;
    }
    IEnumerator SetDeactiveBullet()
    {
        yield return new WaitForSeconds(_playerCont.Range);

        gameObject.SetActive(false);
        transform.localPosition = _firstPos;
    }

    void Update()
    {
        transform.Translate(20f * Time.deltaTime * Vector3.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate")&& other.transform.GetChild(0).GetComponent<GateManager>().TypePowerup=="Firerate")
        {
            ContactWithGate(other.gameObject);
        }
        else if (other.CompareTag("Gate") && other.transform.GetChild(0).GetComponent<GateManager>().TypePowerup == "Range")
        {
            ContactWithGate(other.gameObject);
        }

        else if (other.CompareTag("Collectable"))
        {
            _collectableManager = other.gameObject.GetComponent<CollectableManager>();
            if (_collectableManager.State=="Positive")
            {
                _collectableManager.FifthDigit += _valueFifthDigit;
                if (_collectableManager.FifthDigit >= 10)
                {
                    _collectableManager.FifthDigit = _collectableManager.FifthDigit % 10;
                    _surplus = 1;
                }

                _collectableManager.FourthDigit = _collectableManager.FourthDigit + _valueFourthDigit + _surplus;
                _surplus = 0;
                if (_collectableManager.FourthDigit >= 10)
                {
                    _collectableManager.FourthDigit = _collectableManager.FourthDigit % 10;
                    _surplus = 1;
                }

                _collectableManager.ThirdDigit = _collectableManager.ThirdDigit + _valueThirdDigit + _surplus;
                _surplus = 0;
                if (_collectableManager.ThirdDigit >= 10)
                {
                    _collectableManager.ThirdDigit = _collectableManager.ThirdDigit % 10;
                    _surplus = 1;
                }

                _collectableManager.SecondDigit = _collectableManager.SecondDigit + _valueSecondDigit + _surplus;
                _surplus = 0;
                if (_collectableManager.SecondDigit >= 10)
                {
                    _collectableManager.SecondDigit = _collectableManager.SecondDigit % 10;
                    _surplus = 1;
                }

                _collectableManager.FirstDigit = _collectableManager.FirstDigit + _valueFirstDigit + _surplus;
                _surplus = 0;
                if (_collectableManager.FirstDigit >= 10)
                {
                    _collectableManager.FirstDigit = _collectableManager.FirstDigit % 10;
                }
                MeshChange(other.gameObject);

                MeshRendererActiveOrDeactive(other.gameObject);
                gameObject.SetActive(false);
                transform.localPosition = _firstPos;
            }

            if (_collectableManager.State == "Negative")
            {
                if ((_collectableManager.FifthDigit < _valueFifthDigit) && _collectableManager.FourthDigit == 0 && _collectableManager.ThirdDigit == 0 && _collectableManager.SecondDigit == 0 && _collectableManager.FirstDigit==0)//eksiden 0 gormeyip direkt artэya gecis
                {
                    _collectableManager.FirstDigit = 0;
                    _collectableManager.SecondDigit = 0;
                    _collectableManager.ThirdDigit = 0;
                    _collectableManager.FourthDigit = 0;
                    int newFifthDigit = _value - _collectableManager.FifthDigit;
                    _collectableManager.FifthDigit = newFifthDigit;
                    _goToEnd = true;
                    _collectableManager.State = "Positive";
                    other.transform.GetChild(6).GetComponent<MeshFilter>().mesh = _plusMesh.GetComponent<MeshFilter>().sharedMesh;
                    for (int i = 0; i < other.transform.childCount; i++)
                    {
                        other.transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = _green;
                    }
                }
                else if (_collectableManager.FifthDigit < _valueFifthDigit)
                {
                    _collectableManager.FifthDigit = _collectableManager.FifthDigit + 10 - _valueFifthDigit;
                    _surplus = -1;
                }
                else
                {
                    _collectableManager.FifthDigit -= _valueFifthDigit;
                    _surplus = 0;
                }
                /////////////////////////////////////////////////////////////////////////////////////
                if (!_goToEnd)
                {
                    if (_collectableManager.FourthDigit + _surplus < _valueFourthDigit)
                    {
                        _collectableManager.FourthDigit = _collectableManager.FourthDigit + _surplus + 10 - _valueFourthDigit;
                        _surplus = -1;
                    }
                    else
                    {
                        _collectableManager.FourthDigit = _collectableManager.FourthDigit + _surplus - _valueFourthDigit;
                        _surplus = 0;
                    }

                    if (_collectableManager.ThirdDigit + _surplus < _valueThirdDigit)
                    {
                        _collectableManager.ThirdDigit = _collectableManager.ThirdDigit + _surplus + 10 - _valueThirdDigit;
                        _surplus = -1;
                    }
                    else
                    {
                        _collectableManager.ThirdDigit = _collectableManager.ThirdDigit + _surplus - _valueThirdDigit;
                        _surplus = 0;
                    }

                    if (_collectableManager.SecondDigit + _surplus < _valueSecondDigit)
                    {
                        _collectableManager.SecondDigit = _collectableManager.SecondDigit + _surplus + 10 - _valueSecondDigit;
                        _surplus = -1;
                    }
                    else
                    {
                        _collectableManager.SecondDigit = _collectableManager.SecondDigit + _surplus - _valueSecondDigit;
                        _surplus = 0;
                    }

                    if (_collectableManager.FirstDigit + _surplus < _valueFirstDigit || (_collectableManager.FirstDigit == 0 && _collectableManager.SecondDigit == 0 && _collectableManager.ThirdDigit == 0))
                    {
                        _collectableManager.State = "Positive";
                        other.transform.GetChild(6).GetComponent<MeshFilter>().mesh = _plusMesh.GetComponent<MeshFilter>().sharedMesh;
                        for (int i = 0; i < other.transform.childCount; i++)
                        {
                            other.transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = _green;
                        }
                    }
                    else
                    {
                        _collectableManager.FirstDigit = _collectableManager.FirstDigit + _surplus - _valueFirstDigit;
                        _surplus = 0;
                    }
                }
                MeshChange(other.gameObject);

                MeshRendererActiveOrDeactive(other.gameObject);
                gameObject.SetActive(false);
                transform.localPosition = _firstPos;
            }
        }
    }
    void MeshChange(GameObject other)
    {
        other.transform.GetChild(0).GetComponent<MeshFilter>().mesh = _numberMeshes[_collectableManager.FirstDigit].GetComponent<MeshFilter>().sharedMesh;//bu 3 satir mesh degisimi icin
        other.transform.GetChild(1).GetComponent<MeshFilter>().mesh = _numberMeshes[_collectableManager.SecondDigit].GetComponent<MeshFilter>().sharedMesh;
        other.transform.GetChild(2).GetComponent<MeshFilter>().mesh = _numberMeshes[_collectableManager.ThirdDigit].GetComponent<MeshFilter>().sharedMesh;
        other.transform.GetChild(3).GetComponent<MeshFilter>().mesh = _numberMeshes[_collectableManager.FourthDigit].GetComponent<MeshFilter>().sharedMesh;
        other.transform.GetChild(4).GetComponent<MeshFilter>().mesh = _numberMeshes[_collectableManager.FifthDigit].GetComponent<MeshFilter>().sharedMesh;
    }
    void MeshRendererActiveOrDeactive(GameObject other)
    {
        if (_collectableManager.FirstDigit != 0)//bu uclu 0 olanlarэn meshini kapar, olmayanlarэn acar
        {
            other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(6).transform.localPosition = new Vector3(-3.5f, 0f, 0f); // настраивать
        }
        else if (_collectableManager.FirstDigit == 0 && _collectableManager.SecondDigit == 0 && _collectableManager.ThirdDigit == 0 && _collectableManager.FourthDigit == 0)
        {
            other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(6).transform.localPosition = new Vector3(-.5f, 0f, 0f); // настраивать
        }
        else if (_collectableManager.FirstDigit == 0 && _collectableManager.SecondDigit == 0 && _collectableManager.ThirdDigit == 0)
        {
            other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).transform.localPosition = new Vector3(-.5f, 0f, 0f); // настраивать
        }
        else if (_collectableManager.FirstDigit == 0 && _collectableManager.SecondDigit == 0)
        {
            other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).transform.localPosition = new Vector3(-.5f, 0f, 0f); // настраивать
        }
        else if (_collectableManager.FirstDigit == 0)
        {
            other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = true;
            other.transform.GetChild(4).transform.localPosition = new Vector3(-.85f,0f,0f); // настраивать
        }
    }

    void ContactWithGate(GameObject other)
    {
        other.transform.GetChild(0).GetComponent<GateManager>().Number = other.transform.GetChild(0).GetComponent<GateManager>().Number + _value;
        if (other.transform.GetChild(0).GetComponent<GateManager>().Number < 0)
        {
            other.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = other.transform.GetChild(0).GetComponent<GateManager>().Number.ToString();
        }
        else if (other.transform.GetChild(0).GetComponent<GateManager>().Number > 0)
        {
            other.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + other.transform.GetChild(0).GetComponent<GateManager>().Number.ToString();
            other.transform.GetComponentInParent<MeshRenderer>().sharedMaterial = _greenFade;
        }

        else if (other.transform.GetChild(0).GetComponent<GateManager>().Number == 0)
        {
            other.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = other.transform.GetChild(0).GetComponent<GateManager>().Number.ToString();
            other.transform.GetComponentInParent<MeshRenderer>().sharedMaterial = _greenFade;
        }
        gameObject.SetActive(false);
        transform.localPosition = _firstPos;
    }
}