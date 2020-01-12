using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public List<GameObject> ChunkObjectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveChunk(Vector2 position, float radius)
    {
        foreach(GameObject chunkobj in ChunkObjectList)
        {
            bool b = Vector2.Distance(chunkobj.GetComponent<ChunkObjectController>().Data.Position, position) < radius;
            if (b && chunkobj.activeSelf == false)
            {
                chunkobj.SetActive(true);
            }
            else if (!b && chunkobj.activeSelf == true)
            {
                chunkobj.SetActive(false);
            }
            
        }
    }
    public void AddChunk(DataStructure.Map.ChunkContainer chunkcontainer)
    {
        GameObject obj = GameObject.Instantiate(BaseObject.Tile);
        obj.GetComponent<ChunkObjectController>().Data = chunkcontainer;
        obj.GetComponent<ChunkObjectController>().SyncronizeAll();
        ChunkObjectList.Add(obj);
    }
    public void AddChunkList(List<DataStructure.Map.ChunkContainer> chunkcontainerlist)
    {
        chunkcontainerlist.ForEach((chunkcontainer) => { AddChunk(chunkcontainer); });
    }
}
