using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using RoslynCSharp;
using System.Text;

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

    public void LoadMods(ref DataStorage datastorage)
    {
        Debug.Log(System.Environment.CurrentDirectory);
        LoadMod(ref datastorage);
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

    private void LoadMod(ref DataStorage maindata)
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
    private void LoadSingleMod(ref DataStorage mainstorage, string ModFolderPath)
    {
        if (initCompiler == false)
        {
            LoadFirstScript();
        }
        string path1 = Path.Combine(ModFolderPath, "Script.cs");
        string path2 = Path.Combine(ModFolderPath, "Script.zip");
        string str="";
        if (!File.Exists(path2))
        {
            str = UnZip(File.ReadAllText(path2));
        }
        else if(!File.Exists(path1))
        {
            str = File.ReadAllText(path1);
        }
        else
        {
            Debug.Log(@"(DataLoader.LoadSingleMod)오류/"+path1+ " 스크립트 파일을 찾을수 없습니다.");
        }
        
        
        ScriptType type = domain.CompileAndLoadMainSource(str,ScriptSecurityMode.EnsureSecurity);
        ScriptProxy proxy = type.CreateInstance(new GameObject());
        proxy.SafeFields["data"] = mainstorage;
        proxy.SafeCall("Initialize");
        DataLoadScript loader = proxy.SafeFields["load"] as DataLoadScript;
        DataLoadScriptLoader.Load(loader,ref mainstorage,this);
    }
    private string Zip(string str)
    {
        var rowData = Encoding.UTF8.GetBytes(str);
        byte[] compressed = null;
        using (var outStream = new MemoryStream())
        {
            using (var hgs = new GZipStream(outStream, CompressionMode.Compress))
            {
                //outStream에 압축을 시킨다.
                hgs.Write(rowData, 0, rowData.Length);
            }
            compressed = outStream.ToArray();
        }

        return Convert.ToBase64String(compressed);
    }
    private string UnZip(string str)
    {
        string output = null;
        byte[] cmpData = Convert.FromBase64String(str);
        using (var decomStream = new MemoryStream(cmpData))
        {
            using (var hgs = new GZipStream(decomStream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(hgs))
                {
                    output = reader.ReadToEnd();
                }
            }
        }
        return output;
    }
    public Sprite ImageLoad(Drawable drawobj, ref DataStorage mainstorage)
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

        if (mainstorage.ImageStorage.ContainsKey(str))
        {
            return mainstorage.ImageStorage[str];
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
                mainstorage.ImageStorage.Add(str, spr);
                return spr;
            }
        }

    }
}
public static class DataLoadScriptLoader
{
    public static void Load(DataLoadScript loader,ref DataStorage mainstorage, DataLoader dataloader)
    {

        foreach (Func<DataStructure.Map.Tile> tileload in loader.TileList)
        {
            mainstorage.TileStorage.Add(tileload(),dataloader,ref mainstorage);
        }
        foreach(Func<DataStructure.Map.Material> materialload in loader.MaterialList)
        {
            mainstorage.MaterialStorage.Add(materialload());
        }
        foreach(Func<DataStructure.Map.BaseChunk> basechunkload in loader.BaseChunkList)
        {
            mainstorage.BaseChunkStorage.Add(basechunkload());
        }

    }
}
