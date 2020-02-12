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
            anim.enabled = true;
            sr.flipX = false;
            transform.position = new Vector3(transform.position.x +  SPEED * Time.fixedDeltaTime, 0f, 0f);
            anim.Play("Walk");
        }else if (Input.GetKey(KeyCode.A))
        {
            anim.enabled = true;
            anim.Play("Walk");
            sr.flipX = true;
            transform.position = new Vector3(transform.position.x +  -SPEED * Time.fixedDeltaTime, 0f, 0f);
        }else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.enabled = false;
        }
    }
}
