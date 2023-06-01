using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] private int _firstDigit;
    [SerializeField] private int _secondDigit;
    [SerializeField] private int _thirdDigit;
    [SerializeField] private string _state;
    public string State 
    {
        get {return _state; }
        set {_state=value; }
    }
    public int FirstDigit 
    {
        get {return _firstDigit; }
        set { _firstDigit = value; }
    }
    public int SecondDigit
    {
        get { return _secondDigit; }
        set { _secondDigit = value; }
    }
    public int ThirdDigit
    {
        get { return _thirdDigit; }
        set { _thirdDigit = value; }
    }
    void Start()
    {
        CheckFirstDigitsZeroOrNot();
        InvokeRepeating(nameof(BoxColliderScaleCheck), .5f, .3f);
    }

    public void CheckFirstDigitsZeroOrNot()
    {
        if (_firstDigit == 0 && _secondDigit == 0)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetChild(4).transform.position = gameObject.transform.GetChild(1).transform.position;
        }
        else if (_firstDigit == 0)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetChild(4).transform.position = gameObject.transform.GetChild(0).transform.position;
        }
    }
    void BoxColliderScaleCheck()
    {
        if (transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
        {
            BoxCollider boxCol = transform.GetComponent<BoxCollider>();
            boxCol.center = new Vector3(-0.473673f, boxCol.center.y, boxCol.center.z);
            boxCol.size = new Vector3(2.270566f, boxCol.size.y, boxCol.size.z);
        }
        else if (transform.GetChild(1).GetComponent<MeshRenderer>().enabled)
        {
            BoxCollider boxCol = transform.GetComponent<BoxCollider>();
            boxCol.center = new Vector3(-0.2005937f, boxCol.center.y, boxCol.center.z);
            boxCol.size = new Vector3(1.724408f, boxCol.size.y, boxCol.size.z);
        }
        else
        {
            BoxCollider boxCol = transform.GetComponent<BoxCollider>();
            boxCol.center = new Vector3(0.02114105f, boxCol.center.y, boxCol.center.z);
            boxCol.size = new Vector3(1.280938f, boxCol.size.y, boxCol.size.z);
        }
    }
}