using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float leftcap;
    [SerializeField] private float rightcap;
    [SerializeField] private float jumpLength = 2;
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private LayerMask Ground;

    private Collider2D coll;
    private Rigidbody2D rb;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (facingLeft)
        {
            if (transform.position.x > leftcap)
            {
                if (transform.localScale.x != 1)
                {

                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (coll.IsTouchingLayers(Ground))
                {
                    rb.linearVelocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else
            {          
                facingLeft = false;
            }
        
        }
        else
        {
            if (transform.position.x < rightcap)
            {
                if (transform.localScale.x != -1)
                {

                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (coll.IsTouchingLayers(Ground))
                {
                    rb.linearVelocity = new Vector2(jumpLength, jumpHeight);
                }

            }
            else
            {
                facingLeft = true;

            }
        }
    }
}
