using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{

    public float movePower = 1f;
    public float jumpPower = 5.2f;
    public int maxHealth = 3;

    public GUISkin GUISkin1;
    Rigidbody2D rigid;
    SpriteRenderer Renderer;
    Animator animator;

    Vector3 movement;
    bool isDie = false;
    bool isJumping = false;
    bool isUnBeatTime = false;

    static int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        Renderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            if(!isDie)
                Die();
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (rigid.velocity.y == 0)
                isJumping = true;
        }
           
    }
    public static int getHealth()
    {
        return health;
    }
    public static void setHealth(int value)
    {
        health = value;
    }
    void FixedUpdate(){
        if (health == 0)
            return;
        
        Move();
        Jump();
    }

    void Die()
    {
        isDie = true;

        rigid.velocity = Vector2.zero;

        BoxCollider2D[] colls = gameObject.GetComponents<BoxCollider2D>();
        colls[0].enabled = false;
        colls[1].enabled = false;
        
        Vector2 dieVelocity = new Vector2 (0, 2f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        Invoke("RestartStage",2f);
    }
    void RestartStage()
    {
        GM.RestartStage();
    }

    void Move(){
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if(Input.GetAxisRaw("Horizontal")<0)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.left;
            Renderer.flipX = true;
        }
        else if(Input.GetAxisRaw("Horizontal") >0){
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.right;
            Renderer.flipX = false;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump(){
        if (!isJumping){
            return;
        }

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0,jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        
        
        isJumping = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy" && !isUnBeatTime)
        {      
            Vector2 killVelocity;

            if(other.gameObject.transform.position.x < this.transform.position.x)
            {
                killVelocity = new Vector2(0.7f, 0.9f);
            }
            else
            {
                killVelocity = new Vector2(-0.7f, 0.9f);
            }    
                
            rigid.AddForce(killVelocity, ForceMode2D.Impulse);
            
            health--;

            isUnBeatTime = true;
            StartCoroutine ("UnBeatTime");
        }
        if (other.gameObject.tag == "coin")
        {
            BlockStatus coin = other.gameObject.GetComponent<BlockStatus>();
            ScoreManager.setScore((int)coin.value);

            Destroy(other.gameObject, 0f);

        }
        if (other.gameObject.tag == "End")
        {
            GM.EndGame();
        }

        if (other.gameObject.tag == "Bottom"){
            health = 0;
        }
    }
    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        while (countTime < 10) {
            if (countTime % 2 == 0)
                Renderer.color = new Color32(255,255,255,90);
            else
                Renderer.color = new Color32(255,255,255,180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        Renderer.color = new Color32(255,255,255,255);

        isUnBeatTime = false;

        yield return null;
    }

    void OnGUI()
    {
        if (!GM.isEnded && !GM.isRestart)
        {
            GUI.skin = GUISkin1;
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);

            string heart = "";
            for (int i = 0; i < health; i++) {
                heart += "♥ ";
            }
            GUILayout.Label(heart);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
        
}