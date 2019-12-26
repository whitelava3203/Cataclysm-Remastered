using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataStructure;

class Main
{
    
    
    DataLoadScript Initialize()
    {
        DataLoadScript data = new DataLoadScript();


        data.타일리스트.Add(() =>
        {

            맵.타일 tile = new 맵.타일();
            tile.코드명 = @"main/tile/floor/empty";
            tile.이름 = "빈칸";
            tile.설명 = "누르면 뜨는 설명";
            tile.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.이미지경로 = @"main/tile/floor/empty";
            tile.우선순위 = Drawable.E우선순위.바닥;
            tile.특성.PlayerPassable = true;
            tile.특성.LightPassable = true;


            tile.타일이벤트.Update = () =>
            {

            };
            tile.타일이벤트.PlayerOnTile = () =>
            {

            };
            return tile;
        });
        data.타일리스트.Add(() =>
        {
            맵.타일 tile = new 맵.타일();
            tile.코드명 = @"main/tile/floor/grass";
            tile.이름 = "잔디";
            tile.설명 = "누르면 뜨는 설명";
            tile.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.이미지경로 = @"main/tile/floor/grass";
            tile.우선순위 = Drawable.E우선순위.바닥;
            tile.특성.PlayerPassable = true;
            tile.특성.LightPassable = true;
            return tile;
        });


        return data;
    }
}