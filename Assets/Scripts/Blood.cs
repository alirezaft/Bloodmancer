using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public static List<GameObject> Bloods = new List<GameObject>();
    public bool Active = false;
    private bool Grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        Bloods.Add(gameObject);
        StartCoroutine(Decay());
        
    }
    
    
    
    
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Active == true)
        {
            var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mpos.x, mpos.y, 0f);
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
        }
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(3);
        print("DECAY");
        Bloods.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Grounded = false;
            StopCoroutine(Decay());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Grounded = true;
            Decay();
        }
    }
}
