using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

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
            this.data.Add(obj.코드명,obj);
            this.codedata.Add(obj.코드명);
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
        public CodeStructure<맵.타일> 타일저장소 = new CodeStructure<맵.타일>();
        public Dictionary<string, Sprite> 이미지저장소 = new Dictionary<string, Sprite>();
    }
    public class CodeObject
    {
        public string 코드명;
    }
    public class Drawable : CodeObject
    {
        public enum E우선순위
        {
            바닥 = 0,
            벽 = 1000,
            창문 = 2000,
            장식물 = 3000,
            유닛 = 4000
        }
        public enum E방향
        {
            정방향,
            시계방향90도,
            시계방향180도,
            시계방향270도
        }
        public enum E모양
        {
            단칸,
            꽉참,
            직각,
            직선,
            한개,
            세개
        }
        public bool IsShapedImage = false;
        public string 이미지경로;
        public double 이미지크기;
        public E우선순위 우선순위;
        public E모양 모양;
        public E방향 방향;
    }

    public class 맵
    {
        public class C월드
        {

            public List<List<청크Container>> 청크리스트 = new List<List<청크Container>>();
        }

        public class 청크
        {
            public List<타일Container> 타일리스트 = new List<타일Container>();

        }
        public class 청크Container : IntPos
        {
            public 청크 Data;
            public int X;
            public int Y;

            public 청크Container()
            {

            }
            public 청크Container(청크 청크1)
            {
                Data = 청크1;
            }
            public 청크Container(청크 청크1, int x, int y)
            {
                Data = 청크1;
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
        public class 타일 : Drawable
        {
            public string 이름;
            public string 설명;
            public string 사망도움말;
            public C특성 특성 = new C특성();
            public C타일이벤트 타일이벤트 = new C타일이벤트();


            public class C특성
            {
                public bool PlayerPassable = true;
                public bool LightPassable = true;
            }

            public class C타일이벤트
            {
                public Action PlayerOnTile;
                public Action Update;
            }


        }
        public class 타일Container : IntPos
        {
            public 맵.타일 Data;
            public int X;
            public int Y;

            public bool IsUpdated = false;
            public bool IsDeleted = false;

            public 타일Container()
            {

            }
            public 타일Container(맵.타일 타일1)
            {
                Data = 타일1;
            }
            public 타일Container(맵.타일 타일1, int x, int y)
            {
                Data = 타일1;
                X = x;
                Y = y;
            }
            public bool CheckPos(int x, int y)
            {
                if (x == X && y == Y) return true;
                else return false;
            }
        }
    }
}
public class DataLoadScript
{
    public List<Func<DataStructure.맵.타일>> 타일리스트 = new List<Func<DataStructure.맵.타일>>();
}
public class LangString
{
    public enum 언어
    {
        Kr,
        En
    }
    private Dictionary<언어, string> data = new Dictionary<언어, string>();

    public static List<언어> CurrentLanguage = new List<언어>();

    public void SetString(string str, 언어 lang)
    {
        this.data.Add(lang,str);
    }

    public static implicit operator string(LangString langstr)
    {
        foreach(언어 lang in CurrentLanguage)
        {
            if(langstr.data[lang] != null)
            {
                return langstr.data[lang];
            }
        }
        return "EMPTY";
    }
}