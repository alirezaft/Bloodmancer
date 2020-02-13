using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectBlood : MonoBehaviour
{
    [SerializeField]private GameObject BloodPicker;
    private BloodPick bloodPick;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag.Equals("Blood"))
        {
            print("BLOOD!");
//            bloodPick.Bloods.Add(other.gameObject);
            Blood.Bloods[Blood.Bloods.IndexOf(other.gameObject)].GetComponent<Blood>().Active = true;
            other.gameObject.layer = LayerMask.NameToLayer("Spear");
            other.gameObject.GetComponent<Blood>().GenRandomPosition();
        }
    }

//    private void OnTriggerStay(Collider other)
//    {
//        
//    }

    // Start is called before the first frame update
    void Start()
    {
        bloodPick = BloodPicker.GetComponent<BloodPick>();
        Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        print(bloodPick.Bloods.Count);
    }
}
