using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{

    public float movePower = 1f;
    public float jumpPower = 2f;
    public int maxHealth = 3;

    Rigidbody2D rigid;
    SpriteRenderer Renderer;
    Animator animator;

    Vector3 movement;
    bool isJumping = false;
    bool isUnBeatTime = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        Renderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (health == 0)
        {
            if(!isDie)
                Die();
            return;
        }
        */
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (rigid.velocity.y == 0)
                isJumping = true;
        }
           
    }
    void FixedUpdate(){
        /*if (health == 0)
            return;
        */
        Move();
        Jump();
    }

    void Die()
    {
        //isDie = true;

        rigid.velocity = Vector2.zero;

        Invoke("RestartStage",2f);
    }
    void RestartStage()
    {
        //GameManager.RestartStage();
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
        if (other.gameObject.tag == "enemy" )
        {      
            Vector2 killVelocity;
            if(other.gameObject.transform.position.x < this.transform.position.x)
            //바운스
                killVelocity = new Vector2(0.4f, 0.6f);
            else
                killVelocity = new Vector2(-0.4f, 0.6f);
            rigid.AddForce(killVelocity, ForceMode2D.Impulse);
            //  health--;

            isUnBeatTime = true;
            StartCoroutine ("UnBeatTime");
        }
        if (other.gameObject.tag == "coin")
        {
            BlockStatus coin = other.gameObject.GetComponent<BlockStatus>();
            CoinManager.setScore((int)coin.value);

            Destroy(other.gameObject, 0f);

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
}