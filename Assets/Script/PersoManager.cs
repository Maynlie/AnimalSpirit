using UnityEngine;

public class PersoManager : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public int mode;

    
    public GameObject drawLine;
    DrawRitual drawRitual;
    bool jumping;
    bool falling;
    bool ballmode;
    bool Superballmode;
    bool WallLeft, WallRight, Ground, Sky;
    bool destructible;
    float rushTime;
    Rigidbody2D rigid;
    int escalade;
    int direction;
    int Left;
    float fall;

    public enum PositionGecko
    {
        murG,
        murD,
        Sol,
        Ciel
    }
    PositionGecko p;
    Animator animRenard;
    Animator animGecko;
    Animator animTatou;
    public GameObject spriteAnimRenard;
    public GameObject spriteAnimGecko;
    public GameObject spriteAnimTatou;
    int jumpHash = Animator.StringToHash("onJump");
    //int returnHash = Animator.StringToHash("Return");
    //int walkStateHash = Animator.StringToHash("Base Layer.Walk");
    //int startMoveHash = Animator.StringToHash("startMove");
    public bool onRight;
    public bool onLeft;
    //float lastTranslateX;
    bool tatooRolling;
    bool LeftEnabled = false;
    bool RightEnabled = false;
    public GameObject Renard;
    public GameObject Gecko;
    public GameObject Tatou;
    public BoxCollider2D colliderPerso;
    public Vector2 sizeColRenard;
    public Vector2 sizeColGecko;
    public Vector2 sizeColTatou;

    // Use this for initialization
    void Start () {
        p = PositionGecko.Sol;
        jumping = false;
        rushTime = 0.0f;
        rigid = GetComponent<Rigidbody2D>();
        escalade = 0;
        ballmode = false;
        Superballmode = false;
        direction = 1;
        fall = rigid.velocity.y;
        mode = 1;
        direction = 0;
        drawRitual = drawLine.GetComponent<DrawRitual>();
        animRenard = spriteAnimRenard.GetComponent<Animator>();
        animGecko = spriteAnimGecko.GetComponent<Animator>();
        animTatou = spriteAnimTatou.GetComponent<Animator>();

        onRight = true;
        onLeft = false;
        tatooRolling = false;
        WallLeft = false;
        WallRight = false;
        Ground = false;
        Sky = false;
        destructible = false;
        falling = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        for(int i = 0; i < coll.contacts.Length; i++)
        {
            if (coll.contacts[i].normal.x > 0) WallLeft = true;
            if (coll.contacts[i].normal.x < 0) WallRight = true;
            if (coll.contacts[i].normal.y < 0) Sky = true;
            if (coll.contacts[i].normal.y > 0) Ground = true;
        }
        if (coll.gameObject.tag == "destructible")
        {
            destructible = true;
            if ((Time.time - rushTime > 0.5f && rushTime != 0.0f) || Superballmode)
                Destroy(coll.gameObject);
        }
                
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        for (int i = 0; i < coll.contacts.Length; i++)
        {
            if (coll.contacts[i].normal.x > 0) WallLeft = false;
            if (coll.contacts[i].normal.x < 0) WallRight = false;
            if (coll.contacts[i].normal.y < 0) Sky = false;
            if (coll.contacts[i].normal.y > 0) Ground = false;
        }
        if (coll.gameObject.tag == "destructible") destructible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float translateX = Input.GetAxis("Horizontal") * speed;
        float translateY = 0.0f;
        if (translateX < 0) direction = -1;
        else if (translateX > 0) direction = 1;
        else direction = 0;
        /*if (direction == 1)
            GameObject.Find("Background").GetComponent<ScrollScript>().Invoke("ScrollR", 0);
        else if (direction == -1)
            GameObject.Find("Background").GetComponent<ScrollScript>().Invoke("ScrollL", 0);*/
        if (mode == 2)
        {
            translateY = Input.GetAxis("Vertical") * speed;
        }

        if (drawRitual._checkMode == 1 && mode != 1)
        {
            mode = 1;
            Renard.SetActive(true);
            Gecko.SetActive(false);
            Tatou.SetActive(false);
            colliderPerso.size = sizeColRenard;
            rigid.gravityScale = 1.0f;
            if (p == PositionGecko.Ciel)
                transform.Rotate(0, 0, -180);
            if (p == PositionGecko.murG)
                transform.Rotate(0, 0, 90);
            if (p == PositionGecko.murD)
                transform.Rotate(0, 0, -90);
            p = PositionGecko.Sol;

        }
        if (drawRitual._checkMode == 2 && mode != 2)
        {
            mode = 2;
            Renard.SetActive(false);
            Gecko.SetActive(true);
            Tatou.SetActive(false);
            colliderPerso.size = sizeColGecko;
        }
        if (drawRitual._checkMode == 3 && mode != 3)
        {
            mode = 3;
            Renard.SetActive(false);
            Gecko.SetActive(false);
            Tatou.SetActive(true);
            colliderPerso.size = sizeColTatou;
            rigid.gravityScale = 1.0f;
            if (p == PositionGecko.Ciel)
                transform.Rotate(0, 0, -180);
            if (p == PositionGecko.murG)
                transform.Rotate(0, 0, 90);
            if (p == PositionGecko.murD)
                transform.Rotate(0, 0, -90);
            p = PositionGecko.Sol;
        }


        if (direction == 1 && onRight)
        {
            if (mode == 1)
                spriteAnimRenard.transform.localScale = new Vector3(spriteAnimRenard.transform.localScale.x, spriteAnimRenard.transform.localScale.y, spriteAnimRenard.transform.localScale.z);
            if (mode == 2)
                spriteAnimGecko.transform.localScale = new Vector3(spriteAnimGecko.transform.localScale.x, spriteAnimGecko.transform.localScale.y, spriteAnimGecko.transform.localScale.z);
            if (mode == 3)
                spriteAnimTatou.transform.localScale = new Vector3(spriteAnimTatou.transform.localScale.x, spriteAnimTatou.transform.localScale.y, spriteAnimTatou.transform.localScale.z);
            onLeft = true;
            onRight = false;
            //anim.SetTrigger(returnHash);
        }
        if (direction == -1 && onLeft)
        {
            onLeft = false;
            onRight = true;
            if (mode == 1)
                spriteAnimRenard.transform.localScale = new Vector3(-spriteAnimRenard.transform.localScale.x, spriteAnimRenard.transform.localScale.y, spriteAnimRenard.transform.localScale.z);
            if (mode == 2)
                spriteAnimGecko.transform.localScale = new Vector3(-spriteAnimGecko.transform.localScale.x, spriteAnimGecko.transform.localScale.y, spriteAnimGecko.transform.localScale.z);
            if (mode == 3)
                spriteAnimTatou.transform.localScale = new Vector3(-spriteAnimTatou.transform.localScale.x, spriteAnimTatou.transform.localScale.y, spriteAnimTatou.transform.localScale.z);
            //anim.SetTrigger(returnHash);
        }
        //if(Input.getKey)

        if (translateX != 0)
        {
            if (mode == 1)
                animRenard.SetBool("onMove", true);
            if (mode == 2)
            {
                animGecko.SetBool("onMove", true);
            }
            if (mode == 3)
            {
                animTatou.SetBool("startMove", true);
                tatooRolling = true;
            }
        }
        if (translateX == 0)
        {
            if (mode == 1)
                animRenard.SetBool("onMove", false);
            if (mode == 2)
            {
                animGecko.SetBool("onMove", false);
            }
            if (mode == 3)
            {
                animTatou.SetBool("startMove", false);
                tatooRolling = false;
            }

        }

        //Debug.Log(translateX);
        //lastTranslateX = translateX;

        if (mode == 2) translateY = Input.GetAxis("Vertical") * speed;
        if (mode == 3)
        {
            if (translateX != 0.0f)
            {

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
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            if (mode == 1 && !jumping)
            {
                jump = 3;
                jumping = true;
                rigid.AddForce(new Vector2(0, jumpHeight * jump));
                animRenard.SetTrigger(jumpHash);
            }
            if (mode == 2)
            {
                escalade = -1;
                rigid.gravityScale = 10.0f;
                falling = true;
                if(p == PositionGecko.Ciel)
                    transform.Rotate(0, 0, -180);
                if(p == PositionGecko.murG)
                    transform.Rotate(0,0,90);
                if (p == PositionGecko.murD)
                    transform.Rotate(0,0,-90);
                p = PositionGecko.Sol;
            }
            if (mode == 3 && !Ground) Superballmode = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && mode == 3) Superballmode = false;
        if (Sky && mode == 2 && !falling)
        {
            escalade = 0;
            fall = 0.0f;
        }
        if (Ground)
        {
            //Debug.Log("Au sol");
            if (jumping) jumping = false;
            falling = false;
            rigid.gravityScale = 1.0f;
            if (mode == 2)
            {
                escalade = 0;
                rigid.gravityScale = 1.0f;
            }
        }
        if (destructible)
        {
            if ((Time.time - rushTime > 0.5f && rushTime != 0.0f) || Superballmode)
                GameObject.Find("MurD").GetComponent<BoxCollider2D>().enabled = false;
            else
                rushTime = 0.0f;
        }
        if ((WallLeft || WallRight))
        {
            if (mode == 2 && !falling)
            {
                escalade = 1;
                fall = 0.0f;
                rigid.gravityScale = 0.0f;
            }
            else if (mode == 3)
            {
                rushTime = 0.0f;
            }
        }
        //Les test de collision pour savoir s'il faut changer la rotation du collider
        //Si Sol+MurG
        if (WallLeft && Ground && mode == 2)
        {
            if (translateY > 0 && p == PositionGecko.Sol)//Si on monte
            {
                transform.Rotate(0, 0, -90.0f);
                //transform.Translate(1.5f, 0, 0);
                p = PositionGecko.murG;
            }
            else if (translateX > 0 && p == PositionGecko.murG)//Si on va droite
            {
                p = PositionGecko.Sol;
                transform.Rotate(0, 0, 90.0f);
                transform.Translate(0, -1f, 0);
            }
        }
        if(WallRight && Ground && mode == 2)
        {
            Debug.Log("Interface G/R");
            if (translateY > 0 && p == PositionGecko.Sol)//Si on monte
            {
                Debug.Log("G->R");
                transform.Rotate(0, 0, 90.0f);
                transform.Translate(-0.5f, 0, 0);
                p = PositionGecko.murD;
            }
            else if (translateX < 0 && p == PositionGecko.murD)//Si on va droite
            {
                Debug.Log("R->G");
                p = PositionGecko.Sol;
                transform.Rotate(0, 0, -90.0f);
                transform.Translate(0, -1f, 0);
            }
        }
        if (WallRight && Sky && mode == 2)
        {
            Debug.Log("Interface C/R");
            if (translateY < 0 && p == PositionGecko.Ciel)//Si on monte
            {
                Debug.Log("C->R");
                transform.Rotate(0, 0, -90.0f);
                //transform.Translate(-0.5f, 0, 0);
                p = PositionGecko.murD;
            }
            else if (translateX < 0 && p == PositionGecko.murD)//Si on va droite
            {
                Debug.Log("R->C");
                p = PositionGecko.Ciel;
                transform.Rotate(0, 0, 90.0f);
                transform.Translate(0.1f, 0.5f, 0);
            }
        }
        if (WallLeft && Sky && mode == 2)
        {
            Debug.Log("Interface C/L");
            if (translateY < 0 && p == PositionGecko.Ciel)//Si on monte
            {
                Debug.Log("C->L");
                transform.Rotate(0, 0, 90.0f);
                //transform.Translate(-0.5f, 0, 0);
                p = PositionGecko.murG;
            }
            else if (translateX > 0 && p == PositionGecko.murG)//Si on va droite
            {
                Debug.Log("L->C");
                p = PositionGecko.Ciel;
                transform.Rotate(0, 0, -90.0f);
                //transform.Translate(0, 1.0f, 0);
            }
        }
        /*if(p == PositionGecko.murG && !WallLeft)
            LeftEnabled = true;
        if (p == PositionGecko.murD && !WallRight)
            RightEnabled = true;*/
            if (mode == 2)
        {
            if (escalade == -1 || LeftEnabled || RightEnabled) translateY = 0.0f;
            if (rigid.gravityScale != 1.0f && !Ground && !Sky)
            {
                if(LeftEnabled && translateX<0)
                {
                    transform.Rotate(0,0,90);
                }
                else if (LeftEnabled && translateX>0)
                {
                    transform.Rotate(0, -90, 0);
                }
                else
                    translateX = 0.0f;
            }
            else if (Ground) fall = rigid.velocity.y;
            if (fall > 0) fall = -fall;
            rigid.velocity = new Vector2(translateX, translateY * escalade + fall);
        }
        else if (ballmode)
            rigid.velocity = new Vector2(direction * speed * 2, rigid.velocity.y);
        else if (!Superballmode)
            rigid.velocity = new Vector2(translateX, rigid.velocity.y);
    }
}
