using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    public Rigidbody2D Rb;
    public Animator Ani;
    public int acceleration = 20;
    public int maxSpeed = 10;
    public int jumpForce = 100;
    public GameObject topL;
    public GameObject botR;
    public bool canJump = true;
    public bool canRun = true;
    public Collider2D[] rez;
    public ContactFilter2D  cf;

    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
        Ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            Ani.SetBool("Run", true);
            if(canRun == true)
            {
                Rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*acceleration, 0));
            }else
            {
                Rb.velocity = new Vector2(Input.GetAxis("Horizontal")*(maxSpeed+1), Rb.velocity.y);
            }

            if(Rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }else
            {
                 transform.localScale = new Vector3(-1, 1, 1);
            }

            if(Rb.velocity.x > maxSpeed || -Rb.velocity.x > maxSpeed)
            {
                canRun = false;
                Rb.velocity = new Vector2(Input.GetAxis("Horizontal")*(maxSpeed+1), Rb.velocity.y);
            }else
            {
                canRun = true;
            }
        }else
        {
            Ani.SetBool("Run", false);
        }
        
        if(Input.GetAxis("Jump") == 1 && Physics2D.OverlapArea(topL.transform.position, botR.transform.position, cf, rez) > 1 && canJump)
        {
            Rb.AddForce(new Vector2(0, Input.GetAxis("Jump")*jumpForce));
            canJump = false;
            Ani.SetBool("Jump", true);
            StartCoroutine(isFalling(0.1f));
        }

        if(Physics2D.OverlapArea(topL.transform.position, botR.transform.position, cf, rez) > 1 && canJump == false)
        {
            Ani.SetBool("Fall", false);
        }
    }

    IEnumerator isFalling(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        while(Rb.velocity.y > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
        Ani.SetBool("Fall", true);
    }
}
