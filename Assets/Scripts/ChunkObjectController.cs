using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkObjectController : MonoBehaviour
{
    public List<GameObject> TileObjectList = new List<GameObject>();
    public DataStructure.Map.ChunkContainer Data;

    public void ReloadAllObjectWith(List<DataStructure.Map.TileContainer> tilecontainerlist)
    {
        TileObjectList = new List<GameObject>();
        foreach(DataStructure.Map.TileContainer tilecontainer in tilecontainerlist)
        {
            GameObject obj = GameObject.Instantiate(BaseObject.Tile);
            obj.GetComponent<TileObjectController>().Data = tilecontainer;
            obj.GetComponent<TileObjectController>().Syncronize();
            TileObjectList.Add(obj);
        }
        SyncronizeAll();
    }
    public void ReloadAllObject()
    {
        TileObjectList = new List<GameObject>();
        foreach (DataStructure.Map.TileContainer tilecontainer in Data.Data.TileContainerList)
        {
            GameObject obj = GameObject.Instantiate(BaseObject.Tile);
            obj.GetComponent<TileObjectController>().Data = tilecontainer;
            obj.GetComponent<TileObjectController>().Syncronize();
            TileObjectList.Add(obj);
        }
        SyncronizeAll();
    }
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
