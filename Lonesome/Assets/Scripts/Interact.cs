﻿//Kevin Friddle
//Created: 9/6/2016
//Last Updated: 9/12/2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour {
    public GameObject prompt;
    public GameObject cam;
    public float rayDistance;
    public string iTag;

    RaycastHit hit;
    //Text promptText;
    bool canInteract;

	// Use this for initialization
	void Start () {
        //promptText = prompt.GetComponent<Text>();
        prompt.SetActive(false);

        canInteract = false;
        hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
	    if(canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacted!");
            }
        }
	}

    void FixedUpdate()
    {


        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == iTag)
            {
                prompt.SetActive(true);
                canInteract = true;
            }
            else
            {
                prompt.SetActive(false);
                canInteract = false;
            }
        }
        else
        {
            prompt.SetActive(false);
            canInteract = false;
        }
    }
}
