﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainData;

public class Mod_ChunkMaker : MonoBehaviour
{
    public enum EState
    {
        Tile,
        Chunk
    }
}
/*
public class Mod_ChunkMaker : MonoBehaviour
{
    public enum E상태
    {
        선택안되있음,
        타일선택됨
    }
    public static E상태 상태;
    맵.청크Container 만드는청크;
    맵.청크Container 팔레트청크;
    // Start is called before the first frame update
    void Start()
    {
        DataLoader dataloader = new DataLoader();
        //dataloader.AStart();
        //MainData maindata = new MainData();
        //maindata.LoadResources();
        현재타일 = 전역설정.바닥타일저장소[@"main/tile/floor/empty"];

        만드는청크 = new 맵.청크Container();
        만드는청크.Data = WorldGen.청크관련.잔디청크생성();
        만드는청크.X = 0;
        만드는청크.Y = 0;
        만드는청크.오브젝트생성();

        팔레트청크 = new 맵.청크Container();
        팔레트청크.Data = WorldGen.청크관련.팔레트청크생성();
        팔레트청크.X = -100;
        팔레트청크.Y = -100;

        팔레트청크.오브젝트생성();


        //전역설정.월드.청크리스트.Add(팔레트청크);
    }
    float speed = 0.06f;
    맵.바닥타일[] 단축키 = new 맵.바닥타일[10];
    맵.바닥타일Container 선택된타일
    {
        get
        {
            return ClickChecker.ClickedObject.GetComponent<FloorController>().Data;
        }
        set
        {
            ClickChecker.ClickedObject.GetComponent<FloorController>().Data = value;
        }
    }
    public static 맵.바닥타일 현재타일;
    bool 팔레트작업중 = false;
    Vector2 저장된위치 = new Vector2(-2400f,-2400f);
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed,0,0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-1* speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -1* speed, 0);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            Vector2 temp;
            if (팔레트작업중)
            {
                팔레트작업중 = !팔레트작업중;
                temp = 저장된위치;
                저장된위치 = transform.position;
                transform.position = temp;
            }
            else
            {
                팔레트작업중 = !팔레트작업중;
                temp = 저장된위치;
                저장된위치 = transform.position;
                transform.position = temp;
            }
        }
        if(Input.GetKey(KeyCode.W))
        {
            if (상태 == E상태.타일선택됨)
            {
                선택된타일.Data = 전역설정.바닥타일저장소[@"main/tile/floor/empty"];
                선택된타일.IsUpdated = true;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            FileHandler.ExportAsXML(만드는청크, @"C:\Users\whitelava3203\Documents\Cataclysm\Assets\Scripts\data.xml");
            
        }

        if (상태 == E상태.선택안되있음)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                현재타일 = 단축키[0];
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                현재타일 = 단축키[1];
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                현재타일 = 단축키[2];
            }
        }
        else if(상태 == E상태.타일선택됨)
        {
            맵.바닥타일 타일1 = ClickChecker.ClickedObject.GetComponent<FloorController>().Data.Data;
            if (Input.GetKey(KeyCode.Alpha1))
            {
                단축키[0] = 타일1;
                GameObject.Find("HotBar1").GetComponent<FloorController>().Data.Data = 타일1;
                GameObject.Find("HotBar1").GetComponent<FloorController>().Data.IsUpdated = true;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                단축키[1] = 타일1;
                GameObject.Find("HotBar2").GetComponent<FloorController>().Data.Data = 타일1;
                GameObject.Find("HotBar2").GetComponent<FloorController>().Data.IsUpdated = true;
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                단축키[2] = 타일1;
                GameObject.Find("HotBar3").GetComponent<FloorController>().Data.Data = 타일1;
                GameObject.Find("HotBar3").GetComponent<FloorController>().Data.IsUpdated = true;
            }
        }






    }
}

    */
