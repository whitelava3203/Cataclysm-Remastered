using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class WorldGen : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        CreateWorldPreset(new CWorldInformation());
    }

    // Update is called once per frame
    void Update()
    {
    
    }



    public enum EChunkType
    {
        None,
        River,
        StreetBase,
        EndBase,
        CityBase,
        TownBase,
        Street,
        Bridge,
        Building,
        Forest,
        EndStructure
    }
    public List<List<EChunkType>> Map = new List<List<EChunkType>>();
    public class CWorldInformation
    {
        public int RandSeed = 3203;
        public int XSize = 180;
        public int YSize = 180;
        public int CItySize = 9;
        public int TownSize = 4;
        public int StreetDensity = 35;
        public int CityCount = 10;
        public int TownCount = 15;
        public int EndCount = 20;
    }

    void CreateWorldPreset(CWorldInformation info)
    {
        int[] n = new int[20];
        int[] s = new int[20];
        object[] o = new object[20];
        bool[] b = new bool[20];
        List<EChunkType> tl = new List<EChunkType>();
        Random.InitState(info.RandSeed);
        for(int i=0;i<info.XSize;i++)
        {
            Map.Add(new List<EChunkType>());
            for(int j=0;j<info.YSize;j++)
            {
                Map[i].Add(EChunkType.None);
            }
        }

        s[0] = Random.Range(info.YSize+20, info.YSize-20+1);//강 시작위치
        n[1] = s[0];
        n[0] = 6;
        for(int i=0;i<info.XSize;i++)
        {
            n[0] = n[0] + Random.Range(-1, 1+1);//강 폯
            n[0] = (n[0] / 2) * 2;
            if (n[0] > 10) n[0] = 10;
            if (n[0] < 2) n[0] = 2;
            n[1] = n[1] + Random.Range(-2, 2+1);//강 위치
            if(n[1]<0 + 15)
            {
                n[1] = 15;
            }
            else if(n[1] > info.YSize - 15)
            {
                n[1] = info.YSize - 15;
            }
            Fill(i,i,n[1]-n[0]-2,n[1]+n[0]+2,EChunkType.River);
        }
        //강 생성 완료

        tl.Add(EChunkType.River);
        tl.Add(EChunkType.StreetBase);

        for (int i=0;i<info.StreetDensity;i++)
        {

            back1:
            n[0] = Random.Range(0 + 25,info.XSize - 25+1);
            n[1] = Random.Range(0 + 25, info.YSize - 25+1);
            if(Map[n[0]][n[1]] != EChunkType.None || CheckExist(n[0] - 5, n[0] + 5, n[1] - 5, n[1] + 5, tl))
            {
                goto back1;
            }
            Fill(n[0],n[0],n[1],n[1],EChunkType.StreetBase);
        }
        //중심 길목 생성 완료

        tl.Add(EChunkType.CityBase);

        for (int i = 0; i < info.CityCount; i++)
        {
            back1:
            n[0] = Random.Range(0 + 40, info.XSize - 40+1);
            n[1] = Random.Range(0 + 40, info.YSize - 40+1);
            
            
            if (Map[n[0]][n[1]] != EChunkType.None || CheckExist(n[0] - 5, n[0] + 5, n[1] - 5,n[1]+5,tl))
            {
                goto back1;
            }
            Fill(n[0], n[0], n[1], n[1], EChunkType.CityBase);
        }
        //도시 생성 완료

        tl.Add(EChunkType.TownBase);

        for (int i = 0; i < info.TownCount; i++)
        {
            back1:
            n[0] = Random.Range(0 + 33, info.XSize - 33+1);
            n[1] = Random.Range(0 + 33, info.YSize - 33+1);
            if (Map[n[0]][n[1]] != EChunkType.None || CheckExist(n[0] - 5, n[0] + 5, n[1] - 5, n[1] + 5, tl))
            {
                goto back1;
            }
            Fill(n[0], n[0], n[1], n[1], EChunkType.TownBase);
        }
        //마을 생성 완료


        for (int i = 0; i < info.XSize; i++)
        {
            for (int j = 0; j < info.YSize; j++)
            {
                
                if(Map[i][j] == EChunkType.CityBase)
                {
                    n[8] = 0;
                    while (n[8]<70)
                    {
                        n[2] = -99;
                        n[10] = 0;
                        n[0] = i;
                        n[1] = j;
                        for(int k=0;k<10;k++)
                        {
                            n[3] = Random.Range(i + ((int)info.CItySize - 3), i - ((int)info.CItySize + 3)+1);
                            n[4] = Random.Range(i + ((int)info.CItySize - 3), i - ((int)info.CItySize + 3)+1);
                            if(Map[n[3]][n[4]] == EChunkType.Street)
                            {
                                n[0] = n[3];
                                n[1] = n[4];
                                k = 100;
                            }

                        }
                        while (true)
                        {
                           
                            n[3] = Random.Range(0, 3+1);
                            n[9] = Random.Range(0, 1+1);

                            if (n[9] == 1 || (n[3] + 2 == n[2] || n[3] - 2 == n[2])) 
                                n[3] = n[2];
                            if (n[9] == 0 || n[2] == -99)
                                n[2] = n[3];
                            n[4] = 0;
                            n[5] = 0;


                            if (n[3] == 0)
                                n[4] = 1;

                            if (n[3] == 1)
                                n[5] = -1;

                            if (n[3] == 2)
                                n[4] = -1;

                            if (n[3] == 3)
                                n[5] = 1;


                            n[6] = n[0] + n[4];
                            n[7] = n[1] + n[5];

                            b[0] = (Map[n[6]][n[7]]==EChunkType.Street);
                            b[1] = (Map[n[6] + 1][n[7]] == EChunkType.Street) && (Map[n[6]][n[7] + 1] == EChunkType.Street) && (Map[n[6] + 1][n[7] + 1] == EChunkType.Street);
                            b[2] = (Map[n[6] + 1][n[7]] == EChunkType.Street) && (Map[n[6]][n[7] - 1] == EChunkType.Street) && (Map[n[6] + 1][n[7] - 1] == EChunkType.Street);
                            b[3] = (Map[n[6] - 1][n[7]] == EChunkType.Street) && (Map[n[6]][n[7] + 1] == EChunkType.Street) && (Map[n[6] - 1][n[7] + 1] == EChunkType.Street);
                            b[4] = (Map[n[6] - 1][n[7]] == EChunkType.Street) && (Map[n[6]][n[7] - 1] == EChunkType.Street) && (Map[n[6] - 1][n[7] - 1] == EChunkType.Street);
                            if (Map[n[6]][n[7]]==EChunkType.CityBase || n[10]>50 || b[1] || b[2] || b[3] || b[4] || info.CItySize < (Vector2.Distance(new Vector2(n[6], n[7]), new Vector2(i, j))))
                            {
                                goto end1;
                            }
                            Map[n[6]][n[7]] = EChunkType.Street;
                            n[0] = n[6];
                            n[1] = n[7];

                            n[10]++;
                        }
                        end1:;
                        n[8]++;
                    }
                    //도시 길 생성 완료

                    for (int k = i - info.CItySize; k <= i + info.CItySize; k++) 
                    {
                        for (int l = j - info.CItySize; l <= j + info.CItySize; l++)
                        {
                            b[0] = Map[k][l] == EChunkType.Street;
                            b[1] = Map[k + 1][l] == EChunkType.Street;
                            b[2] = Map[k - 1][l] == EChunkType.Street;
                            b[3] = Map[k][l+1] == EChunkType.Street;
                            b[4] = Map[k][l-1] == EChunkType.Street;
                            if (!b[0] && (b[1] || b[2] || b[3] || b[4]))
                            {
                                Map[k][l] = EChunkType.Building;
                            }
                        }
                    }
                    //도시 건물 생성 완료
                    
                }
            }
        }
        //도시 생성 완료



        TestExport(info);
    }
    private void Fill(int x1,int x2,int y1,int y2,EChunkType type)
    {
        DoSwap(ref x1, ref x2);
        DoSwap(ref y1, ref y2);
        for (int i=x1;i<=x2;i++)
        {
            for(int j=y1;j<=y2;j++)
            {
                Map[i][j] = type;
            }
        }
    }
    private void Replace(int x1, int x2, int y1, int y2, EChunkType origin, EChunkType replace)
    {
        DoSwap(ref x1,ref x2);
        DoSwap(ref y1,ref y2);
        for (int i = x1; i <= x2; i++)
        {
            for (int j = y1; j <= y2; j++)
            {
                if(Map[i][j]==origin)
                    Map[i][j] = replace;
            }
        }
    }
    private bool CheckExist(int x1, int x2, int y1, int y2, List<EChunkType> typelist)
    {
        DoSwap(ref x1, ref x2);
        DoSwap(ref y1, ref y2);
        for (int i = x1; i <= x2; i++)
        {
            for (int j = y1; j <= y2; j++)
            {
                if (typelist.Contains(Map[i][j]))
                    return true;
            }
        }
        return false;
    }
    private void DoSwap(ref int val1, ref int val2)
    {
        int temp;
        if (val2 < val1)
        {
            temp = val1;
            val1 = val2;
            val2 = temp;
        }
    }
    private void TestExport(CWorldInformation info)
    {
        StringBuilder sb = new StringBuilder();

        for(int i=0;i<info.XSize;i++)
        {
            for(int j=0;j<info.YSize;j++)
            {
                sb.Append(TestToString(Map[j][i]));
            }
            sb.AppendLine();
        }
        File.Delete(@"C:\Users\whitelava3203\Documents\Cataclysm\Data\main\Test.txt");
        File.WriteAllText(@"C:\Users\whitelava3203\Documents\Cataclysm\Data\main\Test.txt", sb.ToString());
        
    }
    private char TestToString(EChunkType type)
    {
        if (type == EChunkType.None)
            return '.';
        if (type == EChunkType.River)
            return 'O';
        if (type == EChunkType.StreetBase)
            return 'X';
        if (type == EChunkType.CityBase)
            return 'C';
        if (type == EChunkType.TownBase)
            return 'T';
        if (type == EChunkType.Street)
            return '■';
        if (type == EChunkType.Building)
            return '%';
        return 'Q';
    }
}
