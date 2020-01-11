using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectController : MonoBehaviour
{
    public DataStructure.Map.TileContainer Data;

    public Sprite sprite
    {
        get
        {
            return GetComponent<SpriteRenderer>().sprite;
        }
        set
        {
            GetComponent<SpriteRenderer>().sprite = value;
        }
    }
    public void Remove()
    {
        Destroy(this);
    }
    public void Syncronize()
    {
        this.sprite = Data.Data.Sprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
