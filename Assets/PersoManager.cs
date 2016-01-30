using UnityEngine;

public class PersoManager : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public float timeJmpStart;
    public int mode;
    bool jumping;
    bool ballmode;
    float rushTime;
    float Jumptime;
    Rigidbody2D rigid;
    int escalade;
    int direction;
    int Left;
    float fall;
    // Use this for initialization
    void Start () {
        jumping = false;
        Jumptime = 0.0f;
        rushTime = 0.0f;
        rigid = GetComponent<Rigidbody2D>();
        escalade = 0;
        ballmode = false;
        direction = 1;
        fall = rigid.velocity.y;
    }
	
	// Update is called once per frame
	void Update () {
        float translateX = Input.GetAxis("Horizontal")*speed;
        float translateY = 0.0f;
        if (mode == 2) translateY = Input.GetAxis("Vertical") * speed;
        if (mode == 3)
        {
            if (translateX != 0.0f)
            {
                if (translateX < 0) direction = 1;
                else direction = -1;
                ballmode = true;
                if (rushTime == 0.0f) rushTime = Time.time;
            }
            else
            {
                rushTime = 0.0f;
                ballmode = false;
            }
        }
        int jump = 0;
        bool superjump = false;
        bool grounded = false;
        
        if (Time.time - Jumptime >= timeJmpStart && Jumptime != 0.0f)
        {
            Debug.Log("Super jump");
            jump = 5;
            jumping = true;
            superjump = true;
            Jumptime = 0.0f;
            rigid.AddForce(new Vector2(0, jumpHeight * jump));
            
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump * jumpHeight * Time.deltaTime));
        }
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            if (Jumptime == 0.0f && mode == 1 && !jumping) Jumptime = Time.time;
            else if (mode == 2)
            {
                escalade = -1;
                rigid.gravityScale = 10.0f;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (mode == 1 && !jumping)
            {
                Debug.Log("Jump");
                Jumptime = 0.0f;
                jump = 3;
                jumping = true;
                rigid.AddForce(new Vector2(0, jumpHeight * jump));
            }
        }
        
        if (GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("Sol").GetComponent<BoxCollider2D>()) && !superjump)
        {
            Debug.Log("Au sol");
            grounded = true;
            jumping = false;
            if (mode == 2)
            {
                escalade = 0;
                rigid.gravityScale = 5.0f;
            }
            if(mode == 3) rigid.gravityScale = 1.0f;
        }
        if (GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("Mur").GetComponent<BoxCollider2D>()) && mode == 2 && escalade != -1)
        {
            escalade = 1;
            fall = 0.0f;
            rigid.gravityScale = 0.0f;
        }
        if(GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("MurD").GetComponent<BoxCollider2D>()) && ballmode)
        {
            if(Time.time - rushTime > 1.0 && rushTime != 0.0f)
                GameObject.Find("MurD").GetComponent<BoxCollider2D>().enabled = false;
            else
                rushTime = 0.0f;
        }
        if (mode == 2)
        {
            if (escalade == -1) translateY = 0.0f;
            if (rigid.gravityScale != 1.0f && !grounded) translateX = 0.0f;
            else if (grounded) fall = rigid.velocity.y;
            if (fall > 0) fall = -fall;
            rigid.velocity = new Vector2(translateX, translateY * escalade + fall);
        }
        else if(ballmode)
            rigid.velocity = new Vector2(direction*speed*(rushTime-Time.time), rigid.velocity.y);
        else
            rigid.velocity = new Vector2(translateX, rigid.velocity.y);
        //transform.Translate(translate * Time.deltaTime, 0, 0);

    }
}
