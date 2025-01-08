using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;
    private enum State {idle, Run, jump, falling};
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float speed =5f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private int Coins = 0;
    [SerializeField] private Text CoinsText;
    


    public int coin = 0;


    //start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    //update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection > 0)
        {
            rb.linearVelocity = new Vector2(speed,rb.linearVelocityY);
            transform.localScale = new Vector2(1, 1);
            
        }
        else if (hDirection < 0)
            
        {

            rb.linearVelocity = new Vector2(-speed,rb.linearVelocityY);
            transform.localScale = new Vector2(-1, 1);
            
        }  
        else
        {
            
        }


        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
            state = State.jump;
        }
        VelocityState();
        anim.SetInteger("State", (int)state);
        anim.SetBool("grounded", coll.IsTouchingLayers(Ground));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            coin += 1;
            CoinsText.text = coin + "";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" && state == State.falling)
        {
            Destroy(other.gameObject);
        }
    }
    private void VelocityState()
    {
        if(state == State.jump)
        {
            if (rb.linearVelocity.y < .1f && !coll.IsTouchingLayers(Ground))
            {
                state = State.falling;
            }
            else if (state == State.falling)
            {
                rb.AddForceY(-1);

                if (coll.IsTouchingLayers(Ground))
                {

                    state = State.idle;
                }
            }
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 2f)
        {
            state = State.Run;
        }
        else
        {
            state = State.idle;
        }
    }
    
}