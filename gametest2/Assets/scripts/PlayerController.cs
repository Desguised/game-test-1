using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;
    private enum State {idle, Run, jump, falling};
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground;


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
            rb.linearVelocity = new Vector2(5, 0);
            transform.localScale = new Vector2(1, 1);
            
        }
        else if (hDirection < 0)
            
        {

            rb.linearVelocity = new Vector2(-5, 0);
            transform.localScale = new Vector2(-1, 1);
            
        }
        

        else
        {
            
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);
            state = State.jump;
        }
        VelocityState();
        anim.SetInteger("State", (int)state);
    }
    private void VelocityState()
    {
        if(state == State.jump)
        {
          if(rb.angularVelocity.y < .1f)
            {
                state = State.falling;
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