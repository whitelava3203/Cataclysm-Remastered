using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void 오브젝트생성(object obj)
    {
        Type T = obj.GetType();
        if(T == typeof(DataStructure.맵.청크Container))
        {
            DataStructure.맵.청크Container 청크C1 = (DataStructure.맵.청크Container)obj;
            foreach (DataStructure.맵.타일Container 타일C1 in 청크C1.Data.타일리스트)
            {

            }
        }
    }
}
