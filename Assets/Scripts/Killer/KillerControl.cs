using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class KillerControl : MonoBehaviour
{
    private readonly float SPEED = 5f;
    private readonly float NEAR_THRESH = 0.7f;
    private float Wounded = 0f;
    private float blood = 100f;
    private int Bloodcnt = 0;
    [SerializeField] private bool Near = false;


    [SerializeField] private ParticleSystem BloodPunch;
    [SerializeField] private ParticleSystem BloodFountain;
    [SerializeField] private Transform BloodPlace;
    [SerializeField] private GameObject Blood;
    private GameObject Bloodmancer;
    private Transform BMTransform;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        Bloodmancer = GameObject.FindWithTag("Player");
        BMTransform = Bloodmancer.transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = BMTransform.position - transform.position;

        if (diff.x > 0 && !Near)
        {
            sr.flipX = false;
            transform.position = new Vector3(transform.position.x + (SPEED * Time.deltaTime), transform.position.y, 0f);
//            print("FORWARD");
        }
        else if (diff.x < 0 && !Near)
        {
            sr.flipX = true;
//            print("BACKWARD");
            transform.position = new Vector3(transform.position.x - (SPEED * Time.deltaTime), 0f, 0f);
        }

        if (Mathf.Abs(diff.x) <= NEAR_THRESH)
        {
            Near = true;
        }
        else
        {
            Near = false;
        }

        if (Wounded > 0)
        {
            if (Bloodcnt < 0)
            {
                Bloodcnt = (int) Random.Range(Wounded - 3, Wounded + 3);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("COL");
        if (other.gameObject.tag.Equals("Player") && other.gameObject.GetComponent<ProtControl>().Hitting)
        {
            print("WOUNDED");
            Wounded = Random.Range(5f, 15f);
            BloodPunch.Play();
            StartCoroutine(Dewound());
            print(Wounded);
            StartCoroutine(BloodDrop((int)Wounded));
        }


    }

    IEnumerator Dewound()
    {
        yield return new WaitForSeconds(3);
        Wounded = 0;
    }

    IEnumerator BloodDrop(int count)
    {
        for (int i = 0; i < count; i++)
        {
            print("BLEEDING");
            Instantiate(Blood, transform.position, 
                new Quaternion(0, 0, 0, 0));
            blood--;
            yield return new WaitForSeconds(0.3f);
        }
    }

}
