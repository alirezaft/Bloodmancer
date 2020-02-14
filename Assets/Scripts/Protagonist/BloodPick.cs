using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class BloodPick : MonoBehaviour
{
    public List<GameObject> Bloods;
    [SerializeField] private GameObject Picker;
    private GameObject CurrPicker;
    private bool Dragging = false;
    private bool posinti = false;
    void OnMouseDown()
    {
        
    }

    void OnMouseDrag()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Bloods = new List<GameObject>();
        print("START");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Dragging)
        {
            Dragging = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
//            print(ray.GetPoint(0));
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                print("Got it!");
                if (hit.collider.tag.Equals("Blood"))
                {
                    Bloods.Add(hit.collider.gameObject);
                }
            }
            CurrPicker = Instantiate(Picker, new Vector3(mousepos.x, mousepos.y, 0f), new Quaternion(0f, 0f, 0f, 0f));
//            Debug.Break();
        }

        if (Dragging)
        {
            print("Moving");
            var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject obj in Bloods)
            {
                var xofs = obj.transform.position.x - mousepos.x;    
                var yofs = obj.transform.position.y - mousepos.y;
                print(xofs + ", " + yofs);
            
                obj.transform.position = new Vector3(mousepos.x, mousepos.y, 0);
            }
        }

        if (Input.GetMouseButtonUp(1) && Dragging)
        {
            Dragging = false;
            Bloods.Clear();
            Blood.ShootBlood();
//            Destroy(CurrPicker);
            Blood.DeactivateAll();
        }
    }
}
