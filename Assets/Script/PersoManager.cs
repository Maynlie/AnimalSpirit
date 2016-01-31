using UnityEngine;

public class PersoManager : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public float timeJmpStart;
    public int mode;

    
    public GameObject drawLine;
    DrawRitual drawRitual;

    bool jumping;
    bool ballmode;
    float rushTime;
    float Jumptime;
    Rigidbody2D rigid;
    int escalade;
    int direction;
    int Left;
    float fall;

    Animator animRenard;
    Animator animGecko;
    Animator animTatou;
    public GameObject spriteAnimRenard;
    public GameObject spriteAnimGecko;
    public GameObject spriteAnimTatou;
    int jumpHash = Animator.StringToHash("onJump");
    int returnHash = Animator.StringToHash("Return");
    int walkStateHash = Animator.StringToHash("Base Layer.Walk");
    int startMoveHash = Animator.StringToHash("startMove");
    public bool onRight;
    public bool onLeft;
    float lastTranslateX;
    bool tatooRolling;

    public GameObject Renard;
    public GameObject Gecko;
    public GameObject Tatou;
    public BoxCollider2D colliderPerso;
    public Vector2 sizeColRenard;
    public Vector2 sizeColGecko;
    public Vector2 sizeColTatou;


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
        mode = 1;

        drawRitual = drawLine.GetComponent<DrawRitual>();
        animRenard = spriteAnimRenard.GetComponent<Animator>();
        animGecko = spriteAnimGecko.GetComponent<Animator>();
        animTatou = spriteAnimTatou.GetComponent<Animator>();

        onRight = true;
        onLeft = false;
        tatooRolling = false;


    }
	
	// Update is called once per frame
	void Update () {

        if (drawRitual._checkMode == 1)
        {
            mode = 1;
            Renard.SetActive(true);
            Gecko.SetActive(false);
            Tatou.SetActive(false);
            colliderPerso.size = sizeColRenard;

        }
        if (drawRitual._checkMode == 2)
        {
            mode = 2;
            Renard.SetActive(false);
            Gecko.SetActive(true);
            Tatou.SetActive(false);
            colliderPerso.size = sizeColGecko;
        }
        if (drawRitual._checkMode == 3)
        {
            mode = 3;
            Renard.SetActive(false);
            Gecko.SetActive(false);
            Tatou.SetActive(true);
            colliderPerso.size = sizeColTatou;
        }

        float translateX = Input.GetAxis("Horizontal")*speed;
        if (translateX < 0) direction = 1;
        else direction = -1;
        //Debug.Log(translateX);
        

        float translateY = 0.0f;
        if (direction ==1 && onRight)
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
        if (direction ==-1 && onLeft)
        {
            onLeft = false;
            onRight = true;
            if(mode == 1)
                spriteAnimRenard.transform.localScale = new Vector3(-spriteAnimRenard.transform.localScale.x, spriteAnimRenard.transform.localScale.y, spriteAnimRenard.transform.localScale.z);
            if (mode == 2)
                spriteAnimGecko.transform.localScale = new Vector3(-spriteAnimGecko.transform.localScale.x, spriteAnimGecko.transform.localScale.y, spriteAnimGecko.transform.localScale.z);
            if (mode == 3)
                spriteAnimTatou.transform.localScale = new Vector3(-spriteAnimTatou.transform.localScale.x, spriteAnimTatou.transform.localScale.y, spriteAnimTatou.transform.localScale.z);
            //anim.SetTrigger(returnHash);
        }
        //if(Input.getKey)
        
        if(translateX != 0)
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
        lastTranslateX = translateX;
        
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
            
            animRenard.SetTrigger(jumpHash);
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
            //Debug.Log("Au sol");
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
       
        
    }

}
