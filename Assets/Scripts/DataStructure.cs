using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Dynamic;

public class DataStructure : MonoBehaviour
{
    public class CodeStructure<T> where T : CodeObject
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        private List<string> codedata = new List<string>();
        public T this[int index]
        {
            get
            {
                return data[codedata[index]];
            }
            set
            {
                data[codedata[index]] = value;
            }
        }
        public T this[string key]
        {
            get
            {
                return data[key];
            }
            set
            {
                data[key] = value;
            }
        }
        public void Add(T obj)
        {
            this.data.Add(obj.CodeName,obj);
            this.codedata.Add(obj.CodeName);
        }
       public void Clear()
       {
            this.data.Clear();
            this.codedata.Clear();
       }


    }
    public interface IntPos
    {
        bool CheckPos(int x, int y);
    }







    public CStorage data = new CStorage();



    public class CStorage
    {
        public CodeStructure<Map.Tile> TileStorage = new CodeStructure<Map.Tile>();
        public CodeStructure<Map.Item> ItemStorage = new CodeStructure<Map.Item>();
        public Dictionary<string, Sprite> ImageStorage = new Dictionary<string, Sprite>(); 
    }
    public class CodeObject
    {
        public string CodeName;
    }
    public class Drawable : CodeObject
    {
        public enum EPriority
        {
            Floor = 0,
            Wall = 1000,
            Window = 2000,
            장식물 = 3000,
            Unit = 4000
        }
        public enum EDirection
        {
            Straight,
            Clockwise90,
            Clockwise180,
            Clockwise270
        }
        public enum EShape
        {
            Single,
            Full,
            Degree90,
            Straight,
            One,
            Three
        }
        public bool IsShapedImage = false;
        public string ImagePath;
        public double ImageSize;
        public EPriority Priority;
        public EShape Shape;
        public EDirection Direction;
    }
    public class Map
    {
        public class CWorld
        {

            public List<List<ChunkContainer>> ChunkList = new List<List<ChunkContainer>>();
        }

        public class Chunk
        {
            public List<TileContainer> TileList = new List<TileContainer>();

        }
        public class ChunkContainer : IntPos
        {
            public Chunk Data;
            public int X;
            public int Y;

            public ChunkContainer()
            {

            }
            public ChunkContainer(Chunk chunk1)
            {
                Data = chunk1;
            }
            public ChunkContainer(Chunk chunk1, int x, int y)
            {
                Data = chunk1;
                X = x;
                Y = y;
            }

            public bool CheckPos(int x, int y)
            {
                if (x == X && y == Y) return true;
                else return false;
            }
        }
        public class 기본청크
        {

        }
        public class Tile : Drawable
        {
            public LangString Name = new LangString();
            public LangString Explanation = new LangString();
            public LangString DeathHelp = new LangString();
            public Dictionary<string, Action> TileEvent = new Dictionary<string, Action>();
            public Dictionary<string,object> Attribute = new Dictionary<string, object>();


            public class CTileAttribute
            {
                public bool PlayerPassable = true;
                public bool LightPassable = true;
            }



        }
        public class TileContainer : IntPos
        {
            public Map.Tile Data;
            public int X;
            public int Y;

            public bool IsUpdated = false;
            public bool IsDeleted = false;

            public TileContainer()
            {

            }
            public TileContainer(Map.Tile tile1)
            {
                Data = tile1;
            }
            public TileContainer(Map.Tile tile1, int x, int y)
            {
                Data = tile1;
                X = x;
                Y = y;
            }
            public bool CheckPos(int x, int y)
            {
                if (x == X && y == Y) return true;
                else return false;
            }
        }

        public class Item : Drawable
        {
            public string Name;
            public LangString Explanation = new LangString();
            public string DeathHelp;
            public CItemAttribute Attribute = new CItemAttribute();
            public CMaterial Material = new CMaterial();
            public CItemEvent ItemEvent = new CItemEvent();

            public class CItemAttribute
            {
                public bool IsFireAble;
                public int BurnTime;
                public int BurnStrength;
            }
            public class CMaterial
            {
                public string Name;
                public CItemAttribute Attribute = new CItemAttribute();
                public CItemEvent ItemEvent = new CItemEvent();
            }

            public class CItemEvent
            {

            }
        }
        public class ItemContainer
        {

        }
    }
}
public class DataLoadScript
{
    public List<Func<DataStructure.Map.Tile>> TileList = new List<Func<DataStructure.Map.Tile>>();
}


public class LangString
{
    private Dictionary<string, string> data = new Dictionary<string, string>();

    public static List<string> CurrentLanguage = new List<string>();



    public string this[string lang]
    {
        get
        {
            return data[lang];
        }
        set
        {
            string str;
            if(data.TryGetValue(lang, out str))
                data[lang] = value;
            else
                data.Add(lang, value);
            
        }
    }

    public static implicit operator string(LangString langstr)
    {
        foreach(string lang in CurrentLanguage)
        {
            if(langstr.data[lang] != null)
            {
                return langstr.data[lang];
            }
        }
        return "EMPTY";
    }
}