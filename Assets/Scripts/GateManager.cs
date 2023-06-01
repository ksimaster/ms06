using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private string _type;
    public int Number 
    {
        get {return _number; }
        set {_number=value; }
    }
    public string TypePowerup
    {
        get { return _type; }
        set { _type = value; }
    }
}
