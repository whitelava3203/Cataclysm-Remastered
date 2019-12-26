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
        //Debug.Log(System.Environment.CurrentDirectory);
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
            DirectoryInfo[] CInfo = di.GetDirectories("*", System.IO.SearchOption.AllDirectories);
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
        Debug.Log("(Dataloader.LoadFirstScript)로그/실행성공");
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
        proxy.SafeCall("Initialize");

        DataLoadScript loader = proxy.SafeFields["data"] as DataLoadScript;
        DataLoadScriptLoader.Load(loader,ref maindata,this);
    }

    public Sprite 이미지로드(Drawable drawobj, DataStructure maindata)
    {
        string str;
        string str2;
        if (drawobj.IsShapedImage == true)
        {
            str = drawobj.코드명 + "_" + ((int)drawobj.모양).ToString();
            str2 = drawobj.이미지경로 + "_" + ((int)drawobj.모양).ToString();
        }
        else
        {
            str = drawobj.코드명;
            str2 = drawobj.이미지경로;
        }
        str2 = Path.Combine(this.ModPath, str2);

        if (maindata.data.이미지저장소.ContainsKey(str))
        {
            return maindata.data.이미지저장소[str];
        }
        else
        {
            Sprite spr = File.ReadAllBytes(str2) as object as Sprite;
            if (spr == null)
            {
                Debug.Log(@"오류/파일" + str2 + " 을 찿을수 없습니다.");
                return null;
            }
            else
            {
                maindata.data.이미지저장소.Add(str, spr);
                return spr;
            }
        }

    }
}
public static class DataLoadScriptLoader
{
    public static void Load(DataLoadScript dls,ref DataStructure maindata, DataLoader dataloader)
    {
        foreach (Func<DataStructure.맵.타일> 타일로드 in dls.타일리스트)
        {
            DataStructure.맵.타일 타일1 = 타일로드();
            maindata.data.타일저장소.Add(타일1);
            dataloader.이미지로드(타일1, maindata);
        }


    }
}
