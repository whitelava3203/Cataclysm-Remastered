using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public static class FileHandler
{

    public static void ExportAsXML(object data, string str)
    {
        System.Type T = data.GetType();
        System.Type[] ET = new System.Type[20];
        if (T == typeof(MainData.맵.청크Container))
        {
            for (int i = 0; i < MainData.전역설정.바닥타일코드저장소.Count; i++)
            {
                ET[i] = MainData.전역설정.바닥타일저장소[MainData.전역설정.바닥타일코드저장소[i]].GetType();
            }
        }

        using (StreamWriter wr = new StreamWriter(str))
        {
            XmlSerializer xs = new XmlSerializer(T, ET);
            xs.Serialize(wr, data);
        }
    }
}