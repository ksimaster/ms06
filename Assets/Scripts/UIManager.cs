using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private SaveDatas _saveDatas;
    [SerializeField] private PlayerController _playerCont;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _inputPanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _startCountLv;
    [SerializeField] private TextMeshProUGUI _bulletCountLv;
    [SerializeField] private TextMeshProUGUI _totalMoneyText;
    public GameObject _finishPanel;
    public GameObject _gameOverPanel;
    public TextMeshProUGUI _earnedMoneyText;
   // private static int _startCount = 0;
   // private static int _bulletCount = 1;
    private bool _isStart;
    private Scene _scene;
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
        _saveDatas.Money += _saveDatas.EarnedMoney;
        _totalMoneyText.text = _saveDatas.Money.ToString();
        _saveDatas.EarnedMoney = 0;
        _startCountLv.text = "Lv." + _saveDatas.StartCount.ToString();
        _bulletCountLv.text = "Lv." + _saveDatas.BulletCount.ToString();
        _scene = SceneManager.GetActiveScene();
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
        if (_saveDatas.Money > 99)
        {
            _saveDatas.StartCount++;
            _startCountLv.text = "Lv." + _saveDatas.StartCount.ToString();
            _saveDatas.Money -= 1000;
            _totalMoneyText.text = _saveDatas.Money.ToString();
        }
    }
    public void BulletLevelCount()
    {
        if (_saveDatas.Money > 99)
        {
            _saveDatas.BulletCount++;
            _bulletCountLv.text = "Lv." + _saveDatas.BulletCount.ToString();
            _saveDatas.Money -= 1000;
            _totalMoneyText.text = _saveDatas.Money.ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(_scene.buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_scene.buildIndex + 1);
    }
}