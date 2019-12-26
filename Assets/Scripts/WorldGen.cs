using UnityEngine;
using static MainData.맵;

public class WorldGen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class 청크관련
    {
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
        public static 청크 팔레트청크생성()
        {
            청크 청크1 = new 청크();
            int n=0;
            foreach(string str in MainData.전역설정.바닥타일코드저장소)
            {
                바닥타일Container 바닥1 = new 바닥타일Container(MainData.전역설정.바닥타일저장소[MainData.전역설정.바닥타일코드저장소[n]]);
                바닥1.X = n%24;
                바닥1.Y = n/24;
                //Debug.Log(바닥1.X + " : " + 바닥1.Y);
                바닥1.타일추가(청크1);
                n++;
            }
            return 청크1;
        }
    }
}
