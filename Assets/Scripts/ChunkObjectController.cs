using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkObjectController : MonoBehaviour
{
    public List<GameObject> TileObjectList = new List<GameObject>();
    public DataStructure.Map.ChunkContainer data;

    public void SyncronizeAll()
    {
        foreach(GameObject tileobj in TileObjectList)
        {
            tileobj.GetComponent<TileObjectController>().Syncronize();
        }
    }
    public void SyncronizeAllPos(Vector2Int pos)
    {
        GetAllTilePos(pos).ForEach((tileobj) => { tileobj.GetComponent<TileObjectController>().Syncronize(); });
    }
    public List<GameObject> GetAllTile()
    {
        return TileObjectList;
    }
    public List<GameObject> GetAllTilePos(Vector2Int pos)
    {
        List<GameObject> tileobjlist = new List<GameObject>();
        foreach(GameObject tileobj in TileObjectList)
        {
            if(tileobj.GetComponent<TileObjectController>().Data.CheckPos(pos))
            {
                tileobjlist.Add(tileobj);
            }
        }
        return tileobjlist;
    }
    public GameObject GetHighestTilePos(Vector2Int pos)
    {
        List<GameObject> tileobjlist = new List<GameObject>();
        GameObject highesttile = new GameObject();
        foreach (GameObject tileobj in TileObjectList)
        {
            if (tileobj.GetComponent<TileObjectController>().Data.CheckPos(pos))
            {
                tileobjlist.Add(tileobj);
                highesttile = tileobj;
            }
        }

        foreach(GameObject tileobj in tileobjlist)
        {
            if(highesttile.GetComponent<TileObjectController>().Data.Data.Priority < tileobj.GetComponent<TileObjectController>().Data.Data.Priority)
            {
                highesttile = tileobj;
            }
        }
        return highesttile;

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
