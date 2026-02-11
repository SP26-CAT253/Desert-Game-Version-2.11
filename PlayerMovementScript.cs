using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    private float moveSpeed;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    public CoinManager cm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y == 0)
        {
            rb.AddForce(Vector2.up * 800f);
        }


        //Animation Code
        if (Mathf.Abs(dirX) > 0 && rb.linearVelocity.y == 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (rb.linearVelocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            //anim.SetBool("isFalling", false);    <----If you want to use this, get a sprite sheet of the player falling
        }
        if (rb.linearVelocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }
        if (rb.linearVelocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            //anim.SetBool("isFalling", true);    <----If you want to use this, get a sprite sheet of the player falling
        }

        // ... (other animation conditions)

        // End of animation code
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dirX, rb.linearVelocity.y);
    }

    void LateUpdate()
    {
        if (dirX > 0)
        {
            facingRight = true;
            //transform.rotation = Quaternion.Euler(0, 0, 0); //mod
        }
        else if (dirX < 0)
        {
            facingRight = false;
            //transform.rotation = Quaternion.Euler(0, -180, 0); //mod
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CoinCollection"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }
}
