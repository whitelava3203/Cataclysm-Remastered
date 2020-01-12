using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Dynamic;

public class DataStructure : MonoBehaviour
{
    
    public class IntPos
    {
        public bool CheckPos(Vector2Int pos)
        {
            if (pos == Position) return true;
            else return false;
        }
        public Vector2Int Position;
        public Vector3 FixedPosition
        {
            get
            {
                Vector3 v = new Vector3();
                v.x = Position.x + 0.5f;
                v.y = Position.y + 0.5f;
                return v;
            }
            set
            {
                Position.x = (int)(value.x - 0.5f);
                Position.y = (int)(value.y - 0.5f);
            }
        }
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

            public ChunkContainer()
            {

            }       
            public ChunkContainer(Chunk chunk1)
            {
                Data = chunk1;
            }
            public ChunkContainer(Chunk chunk1, Vector2Int pos)
            {
                Data = chunk1;
                Position = pos;
            }
        }
        public class BaseChunk : CodeObject
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

            public bool IsUpdated = false;
            public bool IsDeleted = false;

            public TileContainer()
            {

            }
            public TileContainer(Map.Tile tile1)
            {
                Data = tile1;
            }
            public TileContainer(Map.Tile tile1, Vector2Int pos)
            {
                Data = tile1;
                Position = pos;
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

    public List<Func<DataStructure.Map.BaseChunk>> BaseChunkList = new List<Func<DataStructure.Map.BaseChunk>>();
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
public class CodeDictionary<T> where T : CodeObject
{
    protected Dictionary<string, T> data = new Dictionary<string, T>();
    protected List<string> codedata = new List<string>();
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
    public virtual void Add(T obj)
    {
        this.data.Add(obj.CodeName, obj);
        this.codedata.Add(obj.CodeName);
    }
    public void Clear()
    {
        this.data.Clear();
        this.codedata.Clear();
    }


}
public class DrawDictionary<T> : CodeDictionary<T> where T : Drawable
{

    public void Add(T drawobj,DataLoader dataloader, ref DataStorage datastorage)
    {
        this.data.Add(drawobj.CodeName, drawobj);
        this.codedata.Add(drawobj.CodeName);
        dataloader.ImageLoad(drawobj, ref datastorage);
    }
}
public class CodeObject
{
    public string CodeName;
}
public class Drawable : CodeObject
{
    public static DataStorage datastorage;

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

    public Sprite Sprite
    {
        get
        {
            string str;
            if (this.IsShapedImage == true)
            {
                str = this.CodeName + "_" + ((int)this.Shape).ToString();
            }
            else
            {
                str = this.CodeName;
            }

            if (datastorage.ImageStorage.ContainsKey(str))
            {
                return datastorage.ImageStorage[str];
            }
            else
            {
                Debug.Log(@"(DataLoader.LoadModList)오류/"+this.CodeName+" 이미지가 로딩되지 않음.");
                return datastorage.ImageStorage[str];//빈 스프라이트 주는걸로 바꿔야함
            }
        }
    }
}
public class DataStorage
{
    public DrawDictionary<DataStructure.Map.Tile> TileStorage = new DrawDictionary<DataStructure.Map.Tile>();
    public CodeDictionary<DataStructure.Map.Item> ItemStorage = new CodeDictionary<DataStructure.Map.Item>();
    public CodeDictionary<DataStructure.Map.Material> MaterialStorage = new CodeDictionary<DataStructure.Map.Material>();
    public Dictionary<string, Sprite> ImageStorage = new Dictionary<string, Sprite>();

    public CodeDictionary<DataStructure.Map.BaseChunk> BaseChunkStorage = new CodeDictionary<DataStructure.Map.BaseChunk>();
}

public static class BaseObject
{
    public static GameObject Tile
    {
        get
        {
            return GameObject.Find("BaseTile");
        }
    }
}