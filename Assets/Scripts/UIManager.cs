using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private const int LvlupPrice = 100;
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private PlayerController _playerCont;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _inputPanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _startCountLv;
    [SerializeField] private TextMeshProUGUI _bulletCountLv;
    [SerializeField] private TextMeshProUGUI _totalMoneyText;
    public GameObject _finishPanel;
    //public TextMeshProUGUI finishMoney;
    public GameObject _gameOverPanel;
    public TextMeshProUGUI _earnedMoneyText;

    public Button startCount;
    public Button bulletCount;
    public Button rewardStartCount;
    public Button rewardBulletCount;

    // private static int _startCount = 0;
    // private static int _bulletCount = 1;
    private bool _isStart;
    private Scene _scene;
    private int countReward = 0;
    private int maxCountReward = 4;
    public bool IsStart
    {
        get { return _isStart; }
        set { _isStart = value; }
    }
   /* public int StartCount 
    {
        get {return _startCount; }
        set { _startCount = value; }
    }
    public int BulletCount
    {
        get { return _bulletCount; }
        set { _bulletCount = value; }
    }*/
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Lewel", 1);
      //  if(int.Parse(SceneManager.GetActiveScene().name) != PlayerPrefs.GetInt("Lewel")) SceneManager.LoadScene(PlayerPrefs.GetInt("Lewel").ToString());
        _saveDatas.Money += _saveDatas.EarnedMoney + 1000;
        _saveDatas.StartCount = 0;
        _saveDatas.BulletCount = 1;
        _totalMoneyText.text = _saveDatas.Money.ToString();
        _saveDatas.EarnedMoney = 0;
        _startCountLv.text = "Lv." + _saveDatas.StartCount.ToString();
        _bulletCountLv.text = "Lv." + _saveDatas.BulletCount.ToString();
        _scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        CheckAds();
        if (countReward >= maxCountReward) rewardStartCount.gameObject.SetActive(false);
        if (countReward >= maxCountReward) rewardBulletCount.gameObject.SetActive(false);
        
        if (_saveDatas.StartCount >= 9)
        {
            startCount.interactable = false;
            rewardStartCount.gameObject.SetActive(false);
        }
        if (_saveDatas.BulletCount >= 9)
        {
            bulletCount.interactable = false;
            rewardBulletCount.gameObject.SetActive(false);
        }
    }
    public void StartGame()
    {
        _isStart = true;
        _player.GetComponent<PlayerController>().enabled = true;
        _player.GetComponent<PickCollectable>().enabled = true;
        _inputPanel.SetActive(true);
        for (int i = 0; i < _startPanel.transform.childCount; i++)
        {
            _startPanel.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < 4; i++)
        {
            _player.transform.GetChild(i).GetComponent<Animator>().SetBool("isMove", true);
        }
    }
    public void StartLevelCount()
    {
        if (_saveDatas.Money >= LvlupPrice && _saveDatas.StartCount < 9)
        {
            _saveDatas.StartCount++;
            _startCountLv.text = "Lv." + _saveDatas.StartCount.ToString();
            _saveDatas.Money -= LvlupPrice;
            _totalMoneyText.text = _saveDatas.Money.ToString();
        }
    }
    public void BulletLevelCount()
    {
        if (_saveDatas.Money >= LvlupPrice && _saveDatas.BulletCount < 9)
        {
            _saveDatas.BulletCount++;
            _bulletCountLv.text = "Lv." + _saveDatas.BulletCount.ToString();
            _saveDatas.Money -= LvlupPrice;
            _totalMoneyText.text = _saveDatas.Money.ToString();
        }
    }
    public void RewardStartLevelCount()
    {
        if (_saveDatas.StartCount < 9 )
        {
            _saveDatas.StartCount++;
            _startCountLv.text = "Lv." + _saveDatas.StartCount.ToString();
            countReward += 1;
        }
        ShowAdReward();
    }
    public void RewardBulletLevelCount()
    {
        if (_saveDatas.BulletCount < 9)
        {
            _saveDatas.BulletCount++;
            _bulletCountLv.text = "Lv." + _saveDatas.BulletCount.ToString();
            countReward += 1;
        }
        ShowAdReward();
    }


    public void RestartGame()
    {
        //SceneManager.LoadScene(_scene.buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        //var nameCurrentScene = SceneManager.GetActiveScene().name;
        // var numberScene = int.Parse(SceneManager.GetActiveScene().name) + 1;
        // 15 заменить на большее число при увеличении сценг
        if (int.Parse(SceneManager.GetActiveScene().name) == 15)
        {
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene("1");
        }
        else
        {
            PlayerPrefs.SetInt("Level", int.Parse(SceneManager.GetActiveScene().name) + 1);
            SceneManager.LoadScene((int.Parse(SceneManager.GetActiveScene().name) + 1).ToString());
        }
        
    }

    public void DoubleFinishMoney()
    {
        var skipLast = _earnedMoneyText.text.TrimEnd('$');
        var countSkipLast = int.Parse(skipLast);
        _earnedMoneyText.text = (countSkipLast * 2).ToString() + "$";
        _saveDatas.EarnedMoney += countSkipLast;
        ShowAdReward();
    }

    public void ShowAdReward()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif

    }

    public void CheckAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        var adsOpen = WebGLPluginJS.GetAdsOpen();
        if (lastIsAdsOpen == null) {
            lastIsAdsOpen = adsOpen;
        }

        if (adsOpen == "yes")
        {
            PlayerPrefs.SetInt("AdsOpen", 1);
            AudioListener.pause = true;
            lastIsAdsOpen = "yes";
        }
        else
        {
            //Коничлась реклама
            PlayerPrefs.SetInt("AdsOpen", 0);
           // if (PlayerPrefs.GetInt("Sound") == 1) AudioListener.pause = false;
            if (lastIsAdsOpen == "yes") {
               // AdsClosed();
                lastIsAdsOpen = "no";
            }
        }
#endif
    }
}