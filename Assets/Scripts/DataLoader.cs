using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using RoslynCSharp;
using UnityEditor;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using static DataStructure;

public class DataLoader : MonoBehaviour
{
    public string CurrentPath = System.Environment.CurrentDirectory;
    public string ModPath
    {
        get
        {
            return Path.Combine(CurrentPath, "Data");
        }
    }
        
    public List<DirectoryInfo> ModFolderDiList = new List<DirectoryInfo>();

    void Start()
    {
        Debug.Log(System.Environment.CurrentDirectory);
        DataStructure maindata = new DataStructure();
        LoadMod(ref maindata);
    }
    private List<string> LoadModList()
    {

        ModFolderDiList = new List<DirectoryInfo>();
        DirectoryInfo di = new DirectoryInfo(ModPath);
        List<string> str2 = new List<string>();
        if (di.Exists)
        {
            DirectoryInfo[] CInfo = di.GetDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
            foreach(DirectoryInfo info in CInfo)
            {
                ModFolderDiList.Add(info);
            }


            if(ModFolderDiList.Count == 0)
            {
                Debug.Log(@"(DataLoader.LoadModList)오류/모드가 없습니다.");
            }
            else
            {
                foreach(DirectoryInfo di2 in ModFolderDiList)
                {
                    str2.Add(di2.FullName);
                }
                Debug.Log(@"(DataLoader.LoadModList)로그/로드성공.");
                return str2;
            }
        }
        else
        {
            Debug.Log(@"(DataLoader.LoadModList)오류/"+ModPath+" 모드 폴더를 찿을수 없습니다.");
        }


        Debug.Log(@"(DataLoader.LoadModList)오류/로드실패.");
        return new List<String>();
    }

    public void LoadMod(ref DataStructure maindata)
    {
        List<string> ModFolderPathList = LoadModList();
        foreach(string ModFolderPath in ModFolderPathList)
        {
            LoadSingleMod(ref maindata, ModFolderPath);
        }

    }



    bool initCompiler = false;

    private ScriptDomain domain = null;

    private ScriptAssembly basescript = null;

    private void LoadFirstScript()
    {
        initCompiler = true;

        domain = ScriptDomain.CreateDomain("ScriptLoader", initCompiler);
        //byte[] bytearray = File.ReadAllBytes(@"C:\Users\whitelava3203\Documents\Cataclysm\Library\ScriptAssemblies\Assembly-CSharp.dll");
        //domain.LoadAssembly(bytearray,ScriptSecurityMode.EnsureLoad);
        Debug.Log(@"(Dataloader.LoadFirstScript)로그/실행성공");
    }
    private void LoadSingleMod(ref DataStructure maindata, string ModFolderPath)
    {
        if (initCompiler == false)
        {
            LoadFirstScript();
        }
        string str = File.ReadAllText(Path.Combine(ModFolderPath,"Script.cs"));
        ScriptType type = domain.CompileAndLoadMainSource(str,ScriptSecurityMode.EnsureSecurity);
        ScriptProxy proxy = type.CreateInstance(new GameObject());
        proxy.SafeFields["data"] = maindata;
        proxy.SafeCall("Initialize");
        DataLoadScript loader = proxy.SafeFields["load"] as DataLoadScript;
        DataLoadScriptLoader.Load(loader,ref maindata,this);
    }

    public Sprite ImageLoad(Drawable drawobj, DataStructure maindata)
    {
        string str;
        string str2;
        if (drawobj.IsShapedImage == true)
        {
            str = drawobj.CodeName + "_" + ((int)drawobj.Shape).ToString();
            str2 = drawobj.ImagePath + "_" + ((int)drawobj.Shape).ToString();
        }
        else
        {
            str = drawobj.CodeName;
            str2 = drawobj.ImagePath;
        }
        str2 = Path.Combine(this.ModPath, str2);

        if (maindata.data.ImageStorage.ContainsKey(str))
        {
            return maindata.data.ImageStorage[str];
        }
        else
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            byte[] bytes = File.ReadAllBytes(str2);
            texture.LoadImage(bytes);
            Sprite spr = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            if (spr == null)
            {
                Debug.Log(@"오류/파일" + str2 + " 을 찿을수 없습니다.");
                return null;
            }
            else
            {
                maindata.data.ImageStorage.Add(str, spr);
                return spr;
            }
        }

    }
}
public static class DataLoadScriptLoader
{
    public static void Load(DataLoadScript loader,ref DataStructure maindata, DataLoader dataloader)
    {
        foreach (Func<DataStructure.Map.Tile> tileload in loader.TileList)
        {
            DataStructure.Map.Tile tile1 = tileload();
            maindata.data.TileStorage.Add(tile1);
            dataloader.ImageLoad(tile1, maindata);
        }
        foreach(Func<DataStructure.Map.Material> materialload in loader.MaterialList)
        {
            DataStructure.Map.Material material1 = materialload();
            maindata.data.MaterialStorage.Add(material1);
        }


    }
}
