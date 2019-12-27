using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataStructure;

class Main : MonoBehaviour
{
    DataLoadScript load = new DataLoadScript();
    DataStructure data;//this is ref
    void Initialize()
    {
		
        load.TileList.Add(() =>
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
            tile.Attribute.Add("PlayerPassable",true);
            tile.Attribute.Add("LightPassable", true);
            tile.TileEvent.Add("Update", () =>
            {

            });

            tile.TileEvent.Add("PlayerOnTile", () =>
            {

            });
            return tile;
        });
        load.TileList.Add(() =>
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
            return tile;
        });
		refer.da = 6974;
    }
}