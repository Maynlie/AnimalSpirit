using UnityEngine;

public class PersoManager : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public float timeJmpStart;
    public int mode;
    bool jumping;
    float Jumptime;
    Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
        jumping = false;
        Jumptime = 0.0f;
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        float translate = Input.GetAxis("Horizontal")*speed;
        int jump = 0;
        if (Time.time - Jumptime >= timeJmpStart && Jumptime != 0.0f)
        {
            jump = 5;
            jumping = true;
            Jumptime = 0.0f;
            rigid.AddForce(new Vector2(0, jumpHeight * jump));
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump * jumpHeight * Time.deltaTime));
        }
        if ((Input.GetKeyDown(KeyCode.Space) && !jumping))
        {
            if (Jumptime == 0.0f) Jumptime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !jumping)
        {
            Jumptime = 0.0f;
            jump = 3;
            jumping = true;
            rigid.AddForce(new Vector2(0, jumpHeight * jump));
        }
        else if(GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("Sol").GetComponent<BoxCollider2D>()))
        {
            jumping = false;
        }
        rigid.velocity = new Vector2(translate * speed, rigid.velocity.y);
        //transform.Translate(translate * Time.deltaTime, 0, 0);

    }
}
