using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataStructure;

class Main : MonoBehaviour
{
    DataLoadScript Load = new DataLoadScript();
    DataStructure data;//this is ref
    void Initialize()
    {
        Load.MaterialList.Add(() =>
        {
            Map.Material material = new Map.Material();
            material.CodeName = @"main/material/none";
            material.Name["KOR"] = "없음";
            material.Name["ENG"]= "None";
            material.Explanation["KOR"] = "재료 없음";
            material.Explanation["ENG"] = "No Material";
            return material;
        });
        Load.MaterialList.Add(() =>
        {
            Map.Material material = new Map.Material();
            material.CodeName = @"main/material/wood";
            material.Name["KOR"] = "나무";
            material.Name["ENG"] = "Wood";
            material.Explanation["KOR"] = "불에 잘타는 나무";
            return material;
        });

        //Material Load end

        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/floor/empty";
            tile.Name["KOR"] = "빈칸";
            tile.Name["ENG"] = "Empty";
            tile.Explanation["KOR"] = "설명";
            tile.Explanation["ENG"] = "explain";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.DeathHelp["ENG"] = "help when died by this tile";
            tile.ImagePath = @"main\graphic\tile\floor\empty.png";
            tile.Priority = Drawable.EPriority.Floor;
            tile.Attribute.Add("PlayerPassable", true);
            tile.Attribute.Add("LightPassable", true);
            tile.Event.Add("Update", () =>
            {

            });
            tile.Event.Add("PlayerOnTile", () =>
            {

            });
            return tile;
        });
        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/floor/grass";
            tile.Name["KOR"] = "잔디";
            tile.Explanation["KOR"] = "설명";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\floor\grass.png";
            tile.Priority = Drawable.EPriority.Floor;
            tile.Attribute.Add("PlayerPassable", true);
            tile.Attribute.Add("LightPassable", true);
            tile.Attribute.Add("MovingResistance", 0);
            return tile;
        });
        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/floor/concrete";
            tile.Name["KOR"] = "콘크리트 바닥";
            tile.Explanation["KOR"] = "설명";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\floor\concrete.png";
            tile.Priority = Drawable.EPriority.Floor;
            tile.Attribute.Add("PlayerPassable", true);
            tile.Attribute.Add("LightPassable", true);
            tile.Attribute.Add("MovingResistance", 0);
            return tile;
        });
        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/wall/concrete";
            tile.Name["KOR"] = "콘크리트 벽";
            tile.Explanation["KOR"] = "설명";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\wall\concrete.png";
            tile.Priority = Drawable.EPriority.Wall;
            tile.IsShapedImage = true;
            tile.Attribute.Add("PlayerPassable", false);
            tile.Attribute.Add("LightPassable", false);
            return tile;
        });
        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/window/windowstatic";
            tile.Name["KOR"] = "고정 창문";
            tile.Explanation["KOR"] = "설명";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\window\windowclosed.png";
            tile.Priority = Drawable.EPriority.Window;
            tile.Attribute.Add("PlayerPassable", false);
            tile.Attribute.Add("LightPassable", true);
            return tile;
        });
        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/window/windowclosed";
            tile.Name["KOR"] = "닫힌 창문";
            tile.Explanation["KOR"] = "설명";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\window\windowclosed.png";
            tile.Priority = Drawable.EPriority.Window;
            tile.Attribute.Add("PlayerPassable", false);
            tile.Attribute.Add("LightPassable", true);
            return tile;
        });
        Load.TileList.Add(() =>
        {
            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/window/windowopened";
            tile.Name["KOR"] = "열린 창문";
            tile.Explanation["KOR"] = "설명";
            tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\window\windowopened.png";
            tile.Priority = Drawable.EPriority.Window;
            tile.Attribute.Add("PlayerPassable", true);
            tile.Attribute.Add("LightPassable", true);
            tile.Attribute.Add("MovingResistance", 10);
            return tile;
        });


        //Tile Load end




    }
}