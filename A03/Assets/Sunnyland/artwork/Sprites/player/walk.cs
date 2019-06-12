using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    public float speed = 6f;
    public float ve;
    Rigidbody2D rgbd;
    private bool isGround;
    private bool isDead;
    public BoxCollider2D mainchar;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Ground")
        {
            isGround = true;
          //  return;
        }
        if (collision.gameObject != null && collision.gameObject.tag == "Enemy")
            isDead = true;
        Debug.Log(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ve = rgbd.velocity.x;
        if (Mathf.Abs(rgbd.velocity.x)<20)
            GetComponent<Animator>().SetBool("running",false);

        //float move = Input.GetAxis("Horizontal");
        //rgbd.AddForce(new Vector2(move * speed , 0));

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rgbd.AddForce(new Vector2(speed,0));
            GetComponent<SpriteRenderer>().flipX = false;
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || !isGround))
                GetComponent<Animator>().SetBool("running",true);
        }
        if (!isDead && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rgbd.AddForce(new Vector2(-speed, 0));
            GetComponent<SpriteRenderer>().flipX = true;
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || !isGround))
                GetComponent<Animator>().SetBool("running",true);
        }
        if (!isDead && (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.Space))&& isGround)
        {
            rgbd.AddForce(new Vector2(0,7),ForceMode2D.Impulse);
            //GetComponent<Animator>().SetBool("Jump", true);
        }
        if (isDead == true && mainchar.enabled)
        {
            //if(isGround)
            rgbd.AddForce(new Vector2(9, 20), ForceMode2D.Impulse);
            rgbd.isKinematic = false;
           mainchar.enabled = false;
            rgbd.gravityScale = 3;
            //Destroy(rgbd);
            
        }
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        //Debug.Log(isGround);
    }
}
