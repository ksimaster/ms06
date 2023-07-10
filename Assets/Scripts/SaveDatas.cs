using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Datas",menuName = "Datas")]

public class SaveDatas : ScriptableObject
{
    [SerializeField] private int _money = 250;
    [SerializeField] private int _earnedMoney;
    [SerializeField] private int _startCount = 0;
    [SerializeField] private int _bulletCount = 1;
    public int Money
    {
        get 
        {
            _money = PlayerPrefs.GetInt("Money");
            return _money;
        }
        set 
        {
            PlayerPrefs.SetInt("Money", value);
            _money = value; 
        }
    }
    public int EarnedMoney
    {
        get { return _earnedMoney; }
        set { _earnedMoney = value; }
    }
    public int StartCount
    {
        get { return _startCount; }
        set { _startCount = value; }
    }
    public int BulletCount
    {
        get { return _bulletCount; }
        set { _bulletCount = value; }
    }
}
