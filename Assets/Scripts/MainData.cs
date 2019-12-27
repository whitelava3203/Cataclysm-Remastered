//moved to DataStructure.cs

using System.Collections.Generic;
using UnityEngine;
using System;

//using Microsoft.CodeAnalysis.CSharp.Scripting;

public class MainData : MonoBehaviour
{   

    // Use this for initialization
    void Start()
    {

        
        //맵시작();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayer()
    {
        플레이어.정보.GetComponent<PlayerController>().Data = new 플레이어();
    }
    public void LoadResources()
    {
        new 맵.바닥타일.빈칸().Load();
        new 맵.바닥타일.잔디().Load();
        new 맵.바닥타일.콘크리트().Load();
        new 맵.바닥타일.콘크리트벽().Load();
        new 맵.바닥타일.고정창문().Load();
        new 맵.바닥타일.닫힌창문().Load();
        new 맵.바닥타일.열린창문().Load();
    }
    public class PosCalc
    {
        public float X;
        public float Y;
        public PosCalc(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public static implicit operator Vector3(PosCalc pos)
        {
            Vector3 v = new Vector3();
            v.x = pos.X + 0.5f;
            v.y = pos.Y + 0.5f;
            return v;
        }
    }

    public class 아이템
    {

    }

    public interface IntPos
    {
        bool CheckPos(int x, int y);
    }





    public class 플레이어
    {
        public static GameObject 정보
        {
            get
            {
                return GameObject.Find("Player");
            }
        }

        public float X;
        public float Y;
        public int 코드 = 0;
        public string 이름 = "whitelava";
        public C욕구 욕구 = new C욕구();
        public C체력 체력 = new C체력();
        public C스탯 스탯 = new C스탯();
        public C수치 수치 = new C수치();
        public class C욕구
        {
            public double 허기 = 0;
            public double 목마름 = 0;
            public double 피로 = 0;
        }
        public class C체력
        {
            public int 머리 = 75;
            public int 몸 = 75;
            public int 왼팔 = 75;
            public int 오른팔 = 75;
            public int 왼다리 = 75;
            public int 오른다리 = 75;
        }
        public class C스탯
        {
            public double 힘 = 10;
            public double 체력 = 10;
            public double 지능 = 10;
            public double 민첩 = 10;
            public double 지각 = 10;
        }
        public class C수치
        {
            public double 집중 = 100;
            public double 지구력 = 100;
            public double 기분 = 50;
            public double 고통 = 0;
        }

        public double 최대지구력
        {
            get
            {
                return (스탯.체력 - 10) * 9;
            }
        }

        public double 달리기최고속도
        {
            get
            {
                return ((스탯.체력 - 10) * 0.4 + (스탯.민첩 - 10) * 0.1 + 5.5) * System.Math.Min(System.Math.Pow(수치.지구력, 0.5), 10) / 10;
            }
        }
        public double 걷기최고속도
        {
            get
            {
                return (스탯.체력 - 10) * 0.1 + (스탯.민첩 - 10) * 0.1 + 2;
            }
        }
        public double 가속도
        {
            get
            {
                return (((스탯.민첩 - 1) * (스탯.민첩 - 1)) / 20 + 9.45) * System.Math.Min(System.Math.Pow(수치.지구력, 0.5), 10) / 10;
            }
        }
    }






    public static class 전역설정
    {
        public static 플레이어 플레이어 = new 플레이어();
        public static 맵.C월드 월드 = new 맵.C월드();

        public static Dictionary<string, Sprite> 이미지저장소 = new Dictionary<string, Sprite>();
        public static Dictionary<string, 맵.바닥타일> 바닥타일저장소 = new Dictionary<string, 맵.바닥타일>();
        public static List<string> 바닥타일코드저장소 = new List<string>();
        public static Dictionary<string, 맵오브젝트.유닛> 유닛저장소 = new Dictionary<string, 맵오브젝트.유닛>();
        public static List<string> 유닛코드저장소 = new List<string>();
    }

    public static void 맵시작()
    {

        맵.청크Container 청크1 = new 맵.청크Container();
        청크1.Data = 맵.잔디청크생성();
        청크1.X = 0;
        청크1.Y = 0;

        전역설정.월드.청크리스트.Add(청크1);

    }

    public interface Objectable
    {
        void 오브젝트생성();
    }
    public abstract class CodeObject
    {
        public string 코드명;
    }
    public abstract class Drawable : CodeObject
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
        public Sprite 이미지로드()
        {
            string str;
            string str2;
            if (this.IsShapedImage == true)
            {
                str = this.코드명 + "_"+((int)this.모양).ToString();
                str2 = this.이미지경로 + "_" + ((int)this.모양).ToString(); 
            }
            else
            { 
                str = this.코드명;
                str2 = this.이미지경로;
            }
            if (전역설정.이미지저장소.ContainsKey(str))
            {
                return 전역설정.이미지저장소[str];
            }
            else
            {
                Sprite spr = Resources.Load(str2, typeof(Sprite)) as Sprite; ;
                if (spr == null)
                {
                    Debug.Log(@"오류/파일" + str2 + " 을 찿을수 없습니다.");
                    return null;
                }
                else
                {
                    전역설정.이미지저장소.Add(str, spr);
                    return spr;
                }
            }
        
        }
    }
    





    public class 맵오브젝트 : Drawable
    {
        public abstract class 유닛 : 맵오브젝트
        {
            
            public float X;
            public float Y;

            public void Load()
            {
                전역설정.유닛저장소.Add(코드명, this);
                전역설정.유닛코드저장소.Add(코드명);
                이미지로드();
            }


            public enum 인공지능
            {
                착함,
                비선공,
                선공
            }


            public string 이름;
            public string 설명;
            public string 사망도움말;

            public int 체력;
            public double 이동속도;
            public int 최소피해량;
            public int 최대피해량;
            public 아이템 주무장;
            public 아이템 부무장;

            public 인공지능 공격인공지능;



            public double 공격사거리;
            public double 공격속도;


            public class 좀비 : 유닛
            {
                public 좀비()
                {


                    this.코드명 = @"main/unit/zombie";
                    this.이름 = "좀비";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이새끼한테 뒤졌을때 뜨는 도움말";

                    this.체력 = 30;
                    this.이동속도 = 1;
                    this.최소피해량 = 3;
                    this.최대피해량 = 7;
                    this.주무장 = new 아이템();
                    this.부무장 = new 아이템();
                    this.공격사거리 = 0.3;
                    this.공격속도 = 0.45;

                    this.공격인공지능 = 인공지능.선공;

                    this.이미지경로 = @"main/unit/zombie.png";
                    this.이미지크기 = 1;


                    Load();
                }
            }

        }
        public class 차량 : 맵오브젝트
        {

        }
        public class NPC : 맵오브젝트
        {

        }
    }

    public class 맵
    {
        public List<C월드> 월드리스트 = new List<C월드>();



        public class C월드
        {

            public List<청크Container> 청크리스트 = new List<청크Container>();
        }
        public class C맵구성
        {
            public double 도시밀도 = 4;
            public double 도시크기 = 4;
            public int 가로크기 = 180;
            public int 세로크기 = 180;
            public int 길종착지밀도 = 4;
            public int 시드 = 3203;
        }
        private enum E청크종류
        {
            평야,
            강,
            강기슭,
            도로
        }
        public void 맵생성(C맵구성 맵구성)
        {

            E청크종류[,] Base = new E청크종류[맵구성.가로크기, 맵구성.세로크기];
            int i;
            int[] v = new int[20];

            UnityEngine.Random.InitState(맵구성.시드);//시드로 랜덤함수 초기화

            if (맵구성.세로크기 > 50)//강 생성 조건
            {
                v[1] = UnityEngine.Random.Range(20, 맵구성.세로크기 - 20);
                v[2] = UnityEngine.Random.Range(v[1] - 15, v[1] + 15);


                v[3] = UnityEngine.Random.Range(4, 7);
                v[4] = UnityEngine.Random.Range(-2, 2);

                for (i=0;i<맵구성.가로크기;i++)//여기서 강생성
                {
                    사각형배치(Base,E청크종류.강,i,i,v[3],v[3]+v[4]);

                }


            }




            //강 생성
        }

        private E청크종류[,] 사각형배치(E청크종류[,] Base, E청크종류 Tile,int x1,int x2,int y1,int y2)
        {
            for(int i=x1;i<=x2;i++)
            {
                for(int j=y1;j<=y2;j++)
                {
                    Base[i, j] = Tile;
                }
            }
            return Base;
        }

        public static 청크 잔디청크생성()
        {
            청크 청크1 = new 청크();
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    바닥타일Container 바닥1 = new 바닥타일Container(new 바닥타일.잔디());
                    바닥1.X = i;
                    바닥1.Y = j;
                    바닥1.타일추가(청크1);
                }

            }
            return 청크1;
        }
        public class 청크Container : IntPos
        {
            public 청크 Data;
            public int X;
            public int Y;
            public bool CheckPos(int x, int y)
            {
                if (x == X && y == Y) return true;
                else return false;
            }
            public 청크Container()
            {

            }
            public 청크Container(청크 청크1)
            {
                Data =청크1;
            }
            public 청크Container(청크 청크1, int x, int y)
            {
                Data = 청크1;
                X = x;
                Y = y;
            }
            public void 오브젝트생성()
            {
                foreach (바닥타일Container 타일 in this.Data.바닥)
                {
                    타일.오브젝트생성(this);
                }
            }
            public void 삭제()
            {
                foreach(바닥타일Container 타일 in this.Data.바닥)
                {
                    타일.IsDeleted = true;
                }
            }

        }
        public class 청크
        {

            
            public List<바닥타일Container> 바닥 = new List<바닥타일Container>();
            public List<맵오브젝트> 맵오브젝트 = new List<맵오브젝트>();
            public bool IsLoaded = false;
        }

        public class 기본청크 : CodeObject
        {
            public List<바닥타일> 바닥 = new List<바닥타일>();
            public List<맵오브젝트> 맵오브젝트 = new List<맵오브젝트>();
            public bool 회전가능 = true;
            public bool 좌우대칭가능 = true;
            public bool 상하대칭가능 = true;
            public int 입구위치 = 0;

            public string 파일명;
            public class 주택 : 기본청크
            {
                public 주택()
                {
                    this.코드명 = @"main/chunktype/house";
                    this.파일명 = @"main/chunktype/house.xml";
                }
            }
        }

        public class 바닥타일Container :  IntPos
        {
            public 바닥타일 Data;
            public int X;
            public int Y;

            public bool IsUpdated = false;
            public bool IsDeleted = false;
            public 바닥타일Container()
            {

            }
            public 바닥타일Container(바닥타일 타일)
            {
                Data = 타일;
            }
            public 바닥타일Container(바닥타일 타일, int x, int y)
            {
                Data = 타일;
                X = x;
                Y = y;
            }
            public void 타일추가(청크 청크1)
            {
                청크1.바닥.Add(this);
                //오브젝트생성();
            }
            public void 오브젝트생성(청크Container 청크1)
            {
                GameObject obj = Instantiate(바닥타일.FloorTile, new PosCalc(청크1.X*24 + this.X, 청크1.Y*24 + this.Y), Quaternion.identity);
                //obj.GetComponent<FloorController>().Data = this;


            }

            public bool CheckPos(int x, int y)
            {
                if (x == X && y == Y) return true;
                else return false;
            }

            
        }
        public class 바닥타일 : Drawable
        {






            public static GameObject Container
            {
                get
                {
                    return GameObject.Find("FloorContainer");
                }
            }
            public static GameObject FloorTile
            {
                get
                {
                    return GameObject.Find("UnnamedFloor");
                }
            }
            public static Transform[] 바닥리스트
            {
                get
                {
                    return 바닥타일.Container.GetComponentsInChildren<Transform>();
                }
            }

            public string 이름;
            public string 설명;
            public string 사망도움말;
            public class C특성
            {
                public bool PlayerPassable = true;
                public bool LightPassable = true;

            }
            public C특성 특성 = new C특성();

            List<Func<바닥타일>> 바닥타일로드 = new List<Func<바닥타일>>();

            public class C타일이벤트
            {
                public Action PlayerOnTile;
                public Action Update;
            }
            public C타일이벤트 타일이벤트 = new C타일이벤트();

            


            public void Load()
            {
                전역설정.바닥타일저장소.Add(코드명, this);
                전역설정.바닥타일코드저장소.Add(코드명);
                이미지로드();
            }

            
            


            public class 빈칸 : 바닥타일
            {
                public 빈칸()
                {
                    this.코드명 = @"main/tile/floor/empty";
                    this.이름 = "빈칸";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/floor/empty";
                    this.우선순위 = E우선순위.바닥;
                    this.특성.PlayerPassable = true;
                    this.특성.LightPassable = true;
                }
            }
            public class 잔디 : 바닥타일
            {
                public 잔디()
                {
                    this.코드명 = @"main/tile/floor/grass";
                    this.이름 = "잔디";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/floor/grass";
                    this.우선순위 = E우선순위.바닥;
                    this.특성.PlayerPassable = true;
                    this.특성.LightPassable = true;
                }
            }
            public class 콘크리트 : 바닥타일
            {
                public 콘크리트()
                {
                    this.코드명 = @"main/tile/floor/concrete";
                    this.이름 = "콘크리트";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/floor/concrete";
                    this.우선순위 = E우선순위.바닥;
                    this.특성.PlayerPassable = true;
                    this.특성.LightPassable = true;
                }



            }
            public class 콘크리트벽 : 바닥타일
            {
                public 콘크리트벽()
                {
                    this.코드명 = @"main/tile/wall/concrete";
                    this.이름 = "콘크리트벽";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/wall/concrete";
                    this.우선순위 = E우선순위.벽;
                    this.IsShapedImage = true;
                    this.모양 = E모양.단칸;
                    this.특성.PlayerPassable = false;
                    this.특성.LightPassable = false;
                }



            }
            public class 고정창문 : 바닥타일
            {
                public 고정창문()
                {
                    this.코드명 = @"main/tile/window/windowstatic";
                    this.이름 = "닫힌 창문";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/window/windowclosed";
                    this.우선순위 = E우선순위.창문;
                    this.특성.PlayerPassable = false;
                    this.특성.LightPassable = true;
                }



            }
            public class 닫힌창문 : 바닥타일
            {
                public 닫힌창문()
                {
                    this.코드명 = @"main/tile/window/windowclosed";
                    this.이름 = "닫힌 창문";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/window/windowclosed";
                    this.우선순위 = E우선순위.창문;
                    this.특성.PlayerPassable = false;
                    this.특성.LightPassable = true;
                }
            }
            public class 열린창문 : 바닥타일
            {
                public 열린창문()
                {
                    this.코드명 = @"main/tile/window/windowopened";
                    this.이름 = "열린 창문";
                    this.설명 = "누르면 뜨는 설명";
                    this.사망도움말 = "이타일효과로 뒤졌을때 뜨는 도움말";
                    this.이미지경로 = @"main/tile/window/windowopened";
                    this.우선순위 = E우선순위.창문;
                    this.특성.PlayerPassable = false;
                    this.특성.LightPassable = true;
                }
            }
        }
    }






}
