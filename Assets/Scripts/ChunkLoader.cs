﻿using System.Collections;
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
            bool b = Vector2.Distance(chunkobj.GetComponent<ChunkObjectController>().data.Position, position) < radius;
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
}
