using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataStructure;

class Main : MonoBehaviour
{
    DataLoadScript data = new DataLoadScript();
    
    void Initialize()
    {

        data.TileList.Add(() =>
        {

            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/floor/empty";
            tile.Name = "빈칸";
            tile.Explanation.SetString("누르면 뜨는 설명",LangString.Language.Kr);
            tile.DeathHelp = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\floor\empty.png";
            tile.Priority = Drawable.EPriority.Floor;
            tile.Attribute.PlayerPassable = true;
            tile.Attribute.LightPassable = true;


            tile.TileEvent.Update = () =>
            {

            };
            tile.TileEvent.PlayerOnTile = () =>
            {

            };
            return tile;
        });
		data.TileList.Add(() =>
        {

            Map.Tile tile = new Map.Tile();
            tile.CodeName = @"main/tile/floor/grass";
            tile.Name = "잔디";
            tile.Explanation.SetString("누르면 뜨는 설명",LangString.Language.Kr);
            tile.DeathHelp = "이타일효과로 뒤졌을때 뜨는 도움말";
            tile.ImagePath = @"main\graphic\tile\floor\grass.png";
            tile.Priority = Drawable.EPriority.Floor;
            tile.Attribute.PlayerPassable = true;
            tile.Attribute.LightPassable = true;


            tile.TileEvent.Update = () =>
            {

            };
            tile.TileEvent.PlayerOnTile = () =>
            {

            };
            return tile;
        });
    }
}