using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ClickChecker : MonoBehaviour
{
    public static GameObject ClickedObject;
    public bool IsSameClicked = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null)
            {

                if (Input.GetMouseButtonDown(1))
                {
                    if (Mod_ChunkMaker.상태 == Mod_ChunkMaker.E상태.타일선택됨)
                    {
                        if (ClickedObject.transform.position == hit.transform.gameObject.transform.position)
                        {
                            IsSameClicked = true;
                        }
                        else
                        {
                            IsSameClicked = false;
                        }
                    }
                    else if(Mod_ChunkMaker.상태 == Mod_ChunkMaker.E상태.선택안되있음)
                    {
                        IsSameClicked = false;
                    }

                    ClickedObject = hit.transform.gameObject;
                    OnMouseEvent();
                }
                else if(Input.GetMouseButtonDown(0))
                {
                    if (Mod_ChunkMaker.상태 == Mod_ChunkMaker.E상태.선택안되있음)
                    {
                        hit.transform.gameObject.GetComponent<FloorController>().Data.Data = Mod_ChunkMaker.현재타일;
                        hit.transform.gameObject.GetComponent<FloorController>().Data.IsUpdated = true;
                    }
                }
            }
        }
    }
    void OnMouseEvent()
    {
        Debug.Log(ClickedObject.transform.position);
        if (IsSameClicked == true)
        {
            Debug.Log("Canceled");
            ClickedObject = null;
            GameObject.Find("ClickedObjectHighlight").transform.position = new Vector2(-1000f,-1000f);
            GameObject.Find("SelectedTile").GetComponent<FloorController>().Data.Data = MainData.전역설정.바닥타일저장소["main/tile/floor/empty"];
            GameObject.Find("SelectedTile").GetComponent<FloorController>().Data.IsUpdated = true;
            Mod_ChunkMaker.상태 = Mod_ChunkMaker.E상태.선택안되있음;
        }
        else
        {
            Debug.Log("Moved");
            GameObject.Find("ClickedObjectHighlight").transform.position = ClickedObject.transform.position;
            GameObject.Find("SelectedTile").GetComponent<FloorController>().Data.Data = ClickedObject.GetComponent<FloorController>().Data.Data;
            GameObject.Find("SelectedTile").GetComponent<FloorController>().Data.IsUpdated = true;
            Mod_ChunkMaker.상태 = Mod_ChunkMaker.E상태.타일선택됨;
        }
    }
}
*/