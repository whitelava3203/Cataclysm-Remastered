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
        bool CheckPos(Vector2 pos);
    }







    public CStorage data = new CStorage();



    public class CStorage
    {
        public CodeStructure<Map.Tile> TileStorage = new CodeStructure<Map.Tile>();
        public CodeStructure<Map.Item> ItemStorage = new CodeStructure<Map.Item>();
        public CodeStructure<Map.Material> MaterialStorage = new CodeStructure<Map.Material>();
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
            Decoration = 3000,
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
            public Vector2 Position;

            public ChunkContainer()
            {

            }       
            public ChunkContainer(Chunk chunk1)
            {
                Data = chunk1;
            }
            public ChunkContainer(Chunk chunk1, Vector2 pos)
            {
                Data = chunk1;
                Position = pos;
            }

            public bool CheckPos(Vector2 pos)
            {
                if (pos==Position) return true;
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
            public Material BaseMaterial = new Material();
            public Dictionary<string, Action> Event = new Dictionary<string, Action>();
            public Dictionary<string,object> Attribute = new Dictionary<string, object>();

            public class CTileAttribute//not used
            {
                public bool PlayerPassable = true;
                public bool LightPassable = true;
            }



        }
        public class TileContainer : IntPos
        {
            public Map.Tile Data;
            public Vector2 Position;

            public bool IsUpdated = false;
            public bool IsDeleted = false;

            public TileContainer()
            {

            }
            public TileContainer(Map.Tile tile1)
            {
                Data = tile1;
            }
            public TileContainer(Map.Tile tile1, Vector2 pos)
            {
                Data = tile1;
                Position = pos;
            }
            public bool CheckPos(Vector2 pos)
            {
                if (pos == Position) return true;
                else return false;
            }
        }
        public class Material : CodeObject
        {
            public LangString Name = new LangString();
            public LangString Explanation = new LangString();
            public Dictionary<string, Action> Event = new Dictionary<string, Action>();
            public Dictionary<string, object> Attribute = new Dictionary<string, object>();
        }

        public class Item : Drawable
        {
            public string Name;
            public LangString Explanation = new LangString();
            public string DeathHelp;
            
            public Material Material = new Material();
            public Dictionary<string, Action> Event = new Dictionary<string, Action>();
            public Dictionary<string, object> Attribute = new Dictionary<string, object>();

            public class CItemAttribute//not used
            {
                public bool IsFireAble;
                public int BurnTime;
                public int BurnStrength;
            }
            

            public class CItemEvent//not used
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
    public List<Func<DataStructure.Map.Material>> MaterialList = new List<Func<DataStructure.Map.Material>>();
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