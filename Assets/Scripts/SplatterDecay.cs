using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterDecay : MonoBehaviour
{
    private SpriteRenderer sr;

    private float t = 1;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        t -= Time.deltaTime / 5;
        if(t >= 0)
        {
//            print(t);
            Color tmp = sr.color;
            tmp.a = (int)Mathf.Lerp(0, 255, t);
            sr.color = tmp;            
            print(sr.color.a);

        }

        
    }
}
