using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Blood : MonoBehaviour
{
    public static List<GameObject> Bloods = new List<GameObject>();
//    [SerializeField] private GameObject BloodMancer;
    public bool Active = false;
    private bool Grounded = false;

    private float rany;
    private float ranx; 
    
    // Start is called before the first frame update
    void Start()
    {
        Bloods.Add(gameObject);
        StartCoroutine(Decay());
        rany = Random.Range(-0.75f, 0.75f);
        ranx = Random.Range(-0.75f, 0.75f);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Active)
        {
            var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mpos.x + ranx, mpos.y + rany, 0f);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (ProtControl.Left)
            {
                transform.rotation = Quaternion.AngleAxis(45f, Vector3.forward);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(-45f, Vector3.forward);
            }
            var col = GetComponent<CapsuleCollider2D>();
            col.size = new Vector2(0.64f, 0.83f);

            while (transform.localScale.x < 4)
            {
                transform.localScale = new Vector3(transform.localScale.x + 4 * Time.deltaTime, 0.2f, 0.2f);
                

            }
        }
       
       
    }

    public static void DeactivateAll()
    {
        foreach (var b in Bloods)
        {
            b.GetComponent<Blood>().Active = false;
            b.GetComponent<Rigidbody2D>().gravityScale = 1;
            
        }
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(3);
        if(Grounded){
            print("DECAY");
            Bloods.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Grounded = false;
            gameObject.tag = "Spear";
            StopCoroutine(Decay());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground") && gameObject.tag.Equals("Spear"))
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            Grounded = true;
            StartCoroutine(Decay());
            GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }else if (other.gameObject.tag.Equals("Killer") && gameObject.tag.Equals("Spear") && !Grounded)
        {
            gameObject.layer = LayerMask.NameToLayer("Liquid");
            Wait(0.5f);
//            transform.SetParent(other.transform);
            transform.parent = other.transform;
            GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

    }

    public void GenRandomPosition()
    {
        ranx = Random.Range(-0.75f, 0.75f);
        rany = Random.Range(-0.75f, 0.75f);
    }

    public static void ShootBlood()
    {
        
        if (ProtControl.Left)
        {
            foreach (var b in Bloods)
            {
                if (b.GetComponent<Blood>().Active)
                {
                    print("SHOOT");            
                    b.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, -50f), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            foreach (var b in Bloods)
            {
                if (b.GetComponent<Blood>().Active)
                {
                    print("SHOOT");            
                    b.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f, -50f), ForceMode2D.Impulse);
                }
            }
        }
    }

    private IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
    }
}
