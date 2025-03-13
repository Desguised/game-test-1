using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;
    private enum State { idle, Run, jump, falling, hurt };
    private State state = State.idle;
    private Collider2D coll;
    private AudioSource footstep;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private int Coins = 0;
    [SerializeField] private Text CoinsText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI healthAmount;

    public int coin = 0;


    //start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
        footstep = GetComponent<AudioSource>();
    }

    //update is called once per frame
    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
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
        if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            jumpforce = 16f;
            GetComponent<SpriteRenderer>().color = Color.cyan;
            StartCoroutine(ResetPower());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {


            if (state == State.falling)
            {
                Destroy(other.gameObject);
            }
            else
            {
                state = State.hurt;
                HandleHealth();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.linearVelocity = new Vector2(-hurtForce, rb.linearVelocity.y);
                    //enemy is to right

                }
                else
                {
                    //enemy is to left
                    rb.linearVelocity = new Vector2(hurtForce, rb.linearVelocity.y);
                }




            }

        }

    }
    private void HandleHealth()
    {
        health -= 1;
        healthAmount.text = health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void VelocityState()
    {
        if (state == State.falling && !coll.IsTouchingLayers(Ground))
            return;


        if (state == State.jump)
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
        else if (rb.linearVelocity.y < .1f && !coll.IsTouchingLayers(Ground))
        {
            state = State.falling;

            rb.AddForceY(-1);
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
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection > 0)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            transform.localScale = new Vector2(1, 1);

        }
        else if (hDirection < 0)

        {

            rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
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


    }
    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        jumpforce = 8;
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    private void FootStep()
    {
        footstep.Play();
    }
}