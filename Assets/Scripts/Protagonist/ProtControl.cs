using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtControl : MonoBehaviour
{
    readonly float SPEED = 4f;
    private float Health = 100;
    private float Wounded = 0;
    public bool Hitting = true;
    private Animator anim;
    private bool Jump = false;

    public static bool Left = false;
    
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Left = false;
            anim.enabled = true;
            sr.flipX = false;
            transform.position = new Vector3(transform.position.x +  SPEED * Time.fixedDeltaTime, transform.position.y, 0f);
            anim.Play("Walk");
        }else if (Input.GetKey(KeyCode.A))
        {
            Left = true;
            anim.enabled = true;
            anim.Play("Walk");
            sr.flipX = true;
            transform.position = new Vector3(transform.position.x +  -SPEED * Time.fixedDeltaTime, transform.position.y, 0f);
        } if (Input.GetKeyDown(KeyCode.Space) && !Jump)
        {
            print("FLY");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
            Jump = true;
        }else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.Play("Idle");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Jump = false;
        }
    }
}
