using UnityEngine;

public class FloorController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = Data.Data.이미지로드(_maindata);
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.IsDeleted)
        {
            System.GC.SuppressFinalize(_Data);
            System.GC.SuppressFinalize(_maindata);
            Destroy(gameObject);
            Destroy(this);
        }
        else if (Data.IsUpdated) Syncronize();
    }

    void Syncronize()
    {
        //GetComponent<SpriteRenderer>().sprite = Data.Data.이미지로드(_maindata);
        Data.IsUpdated = false;
    }

    private DataStructure _maindata;
    public void SetData(ref DataStructure maindata)
    {
        _maindata = maindata;
    }
    private DataStructure.Map.TileContainer _Data;
    public DataStructure.Map.TileContainer Data
    {
        get
        {
               return _Data;
        }
        set
        {
            _Data = value;
        }
    }
}
