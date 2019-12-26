using UnityEngine;

public class PlayerController : MonoBehaviour
{





    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Debug.Log(Data.달리기최고속도);
        Debug.Log(Data.가속도);
    }

    double nowspeed = 0;
    Rigidbody2D body;

    // Update is called once per frame
    void FixedUpdate()
    {

        /*

        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(transform.right * (float)Data.가속도);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(transform.right * (float)Data.가속도 * -1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.AddForce(transform.up * (float)Data.가속도);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(transform.up * (float)Data.가속도 * -1);
        }



        if(body.velocity.magnitude>=Data.달리기최고속도)
        {
            body.velocity *= (float)Data.달리기최고속도 / body.velocity.magnitude;
        }


        */

        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = (transform.right * (float)Data.달리기최고속도);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.velocity = (transform.right * (float)Data.달리기최고속도 * -1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            body.velocity = (transform.up * (float)Data.달리기최고속도);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            body.velocity = (transform.up * (float)Data.달리기최고속도 * -1);
        }
        else
        {
            body.velocity = new Vector2();
        }
    }


    public MainData.플레이어 Data = new MainData.플레이어();

}
