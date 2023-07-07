using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class PickCollectable : MonoBehaviour
{
    private int _surplus = 0;
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerController _playerCont;
    [SerializeField] private GameObject[] _numberMeshes;
    [SerializeField] private ParticleSystem _upParticle;
    [SerializeField] private ParticleSystem _downParticle;
    [SerializeField] private GameObject _gameOverParticle;
    [SerializeField] private CinemachineVirtualCamera _gameOverCamera;
    [SerializeField] private Material _grey;
    [SerializeField] private int _playerThousDigit;
    [SerializeField] private int _playerFirstDigit;
    [SerializeField] private int _playerSecondDigit;
    [SerializeField] private int _playerThirdDigit;

    public int PlayerThousDigit
    {
        get { return _playerThousDigit; }
        set { _playerThousDigit = value; }
    }

    public int PlayerFirstDigit
    {
        get { return _playerFirstDigit; }
        set { _playerFirstDigit = value; }
    }
    public int PlayerSecondDigit
    {
        get { return _playerSecondDigit; }
        set { _playerSecondDigit = value; }
    }
    public int PlayerThirdDigit
    {
        get { return _playerThirdDigit; }
        set { _playerThirdDigit = value; }
    }
    private void Start()
    {
        _playerThirdDigit = _saveDatas.StartCount;
        transform.GetChild(3).GetComponent<MeshFilter>().mesh = _numberMeshes[_playerThirdDigit].GetComponent<MeshFilter>().mesh;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            //game over icin gerekliydi
            int first = _playerFirstDigit;
            int second = _playerSecondDigit;
            int third = _playerThirdDigit;
            int thous = _playerThousDigit;

            if (other.GetComponent<CollectableManager>().State=="Positive")//pozitif powerup toplama islemleri
            {
                _playerThirdDigit += other.GetComponent<CollectableManager>().ThirdDigit;
                if (_playerThirdDigit >= 10)
                {
                    _playerThirdDigit = _playerThirdDigit % 10;
                    _surplus = 1;
                }

                _playerSecondDigit += other.GetComponent<CollectableManager>().SecondDigit + _surplus;
                _surplus = 0;
                if (_playerSecondDigit >= 10)
                {
                    _playerSecondDigit = _playerSecondDigit % 10;
                    _surplus = 1;
                }

                _playerFirstDigit += other.GetComponent<CollectableManager>().FirstDigit + _surplus;
                _surplus = 0;
                if (_playerFirstDigit >= 10)
                {
                    _playerFirstDigit = _playerFirstDigit % 10;
                    _surplus = 1;
                }

                _playerThousDigit += _surplus;
                _surplus = 0;
                if (_playerThousDigit >= 10)
                {
                    _playerThousDigit = 9;
                }

                transform.GetComponent<Animator>().enabled = false;
                other.gameObject.SetActive(false);
                _upParticle.Play();
                transform.DORotate(new Vector3(0, 360, 0), .2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(()=> {
                    transform.GetComponent<Animator>().enabled = true;
                });
                MeshChange();

                MeshRendererActiveOrDeactive();
            }
            else if(other.GetComponent<CollectableManager>().State == "Negative")
            {
                if (_playerThirdDigit < other.GetComponent<CollectableManager>().ThirdDigit)
                {
                    _playerThirdDigit += 10 - other.GetComponent<CollectableManager>().ThirdDigit;
                    _surplus = -1;
                }
                else
                {
                    _playerThirdDigit -= other.GetComponent<CollectableManager>().ThirdDigit;
                    _surplus = 0;
                }

                if (_playerSecondDigit + _surplus < other.GetComponent<CollectableManager>().SecondDigit)
                {
                    _playerSecondDigit += _surplus + 10 - other.GetComponent<CollectableManager>().SecondDigit;
                    _surplus = -1;
                }
                else
                {
                    _playerSecondDigit += _surplus - other.GetComponent<CollectableManager>().SecondDigit;
                    _surplus = 0;
                }

                if (_playerFirstDigit + _surplus < other.GetComponent<CollectableManager>().FirstDigit)
                {
                    if (_playerThousDigit > 0)
                    {
                        _playerFirstDigit += _surplus + 10 - other.GetComponent<CollectableManager>().FirstDigit;
                        _playerThousDigit--;
                    }
                    else
                    {
                        _gameOverCamera.Priority = 15;
                        _gameOverParticle.SetActive(true);
                        _playerCont._isFinish = true;
                        transform.DORotate(new Vector3(-90f, 0f, 0f), 1f);
                        transform.DOMove(transform.position - new Vector3(0f, .17f, 0f), 1f).OnComplete(() => { _uiManager._gameOverPanel.SetActive(true); }); ;
                        _playerFirstDigit = first;
                        _playerSecondDigit = second;
                        _playerThirdDigit = third;
                    }
                }
                else
                {
                    _playerFirstDigit = PlayerFirstDigit + _surplus - other.GetComponent<CollectableManager>().FirstDigit;
                    _surplus = 0;
                }

                _downParticle.Play();
                MeshChange();

                MeshRendererActiveOrDeactive();
                other.gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("FinishLine"))
        {
            InvokeRepeating(nameof(AutomaticDecrease), .3f, .1f);
        }
    }
    void MeshChange()
    {
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = _numberMeshes[_playerThousDigit].GetComponent<MeshFilter>().mesh;
        transform.GetChild(1).GetComponent<MeshFilter>().mesh = _numberMeshes[_playerFirstDigit].GetComponent<MeshFilter>().mesh;//bu 3 satir mesh degisimi icin
        transform.GetChild(2).GetComponent<MeshFilter>().mesh = _numberMeshes[_playerSecondDigit].GetComponent<MeshFilter>().mesh;
        transform.GetChild(3).GetComponent<MeshFilter>().mesh = _numberMeshes[_playerThirdDigit].GetComponent<MeshFilter>().mesh;
    }

    void MeshRendererActiveOrDeactive()
    {
        var disableThous = _playerThousDigit == 0;
        var disableHundred = disableThous && _playerFirstDigit == 0;
        var disableTens = disableHundred && _playerSecondDigit == 0;
        var activationArray = new bool[]
        {
            !disableThous, !disableHundred, !disableTens
        };

        for (var i = 0; i < 3; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = activationArray[i];
            transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = activationArray[i];
        }

    }

    void AutomaticDecrease()
    {
        _playerThirdDigit--;
        if (_playerThirdDigit<0)
        {
            _playerThirdDigit = 9;
            _playerSecondDigit--;
        }
        if (_playerSecondDigit < 0)
        {
            _playerSecondDigit = 9;
            _playerFirstDigit--;
        }

        if (_playerFirstDigit==0 && _playerSecondDigit==0 && _playerThirdDigit==0)
        {
            if (_playerThousDigit > 0)
            {
                _playerThousDigit--;
                _playerThirdDigit = 9;
                _playerSecondDigit = 9;
                _playerFirstDigit = 9;
                MeshChange();
                MeshRendererActiveOrDeactive();
            }
            else
            {
                transform.GetChild(3).GetComponent<MeshFilter>().sharedMesh = _numberMeshes[0].GetComponent<MeshFilter>().sharedMesh;
                _gameOverCamera.Priority = 15;
                _playerCont._isFinish = true;
                _gameOverParticle.SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = _grey;
                    transform.GetChild(i).GetComponent<Animator>().enabled = false;
                }
                transform.DOMove(transform.position - new Vector3(0f, .17f, .5f), .75f);
                transform.DORotate(new Vector3(-90f, 0f, 0f), .75f).OnComplete(() =>
                {
                    _uiManager._earnedMoneyText.text = _saveDatas.EarnedMoney.ToString() + "$";
                    _uiManager._finishPanel.SetActive(true);
                });
                CancelInvoke();
            }
        }
        else
        {
            MeshChange();
            MeshRendererActiveOrDeactive();
        }   
    }

    public void ToCancelInvoke()
    {
        CancelInvoke(nameof(AutomaticDecrease));
    }



}