//Kevin Friddle
//Created: 9/6/2016
//Last Updated: 10/3/2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public GameObject cam;  //Player camera
    public float rayDistance;   //distance that the raycast extends (furthest away the player can interact)
    public string interTag; //the tag for an interactable item
    public float iconScale; //scale value for icon of selected interactable

    RaycastHit hit;
    GameObject currentHit;
    bool interacting; //determines the player is interacting with an object

    // Use this for initialization
    void Start()
    {
        currentHit = null;
        interacting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHit && currentHit.GetComponent<Interactable>())
        {
            if(Input.GetMouseButtonDown(0))
            {
                interacting = true;
                currentHit.GetComponent<Interactable>().Interacted = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!interacting)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayDistance))
            {
                if (hit.collider.gameObject != currentHit)
                {
                    if (currentHit != null)
                    {
                        if (currentHit.tag == interTag && currentHit.GetComponent<Interactable>())
                        { currentHit.GetComponent<Interactable>().icon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }  //Return previous interactable icon to normal scale
                    }

                    currentHit = hit.collider.gameObject;

                    if (currentHit.tag == interTag && currentHit.GetComponent<Interactable>())
                    { currentHit.GetComponent<Interactable>().icon.transform.localScale = new Vector3(iconScale, iconScale, 1.0f); }    //Make new interactable icon larger scale
                }
            }
            else
            {
                if (currentHit != null)
                {
                    if (currentHit.tag == interTag && currentHit.GetComponent<Interactable>())
                    { currentHit.GetComponent<Interactable>().icon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }  //Return previous interactable icon to normal scale

                    currentHit = null;
                }
            }
        }
    }

    public bool Interacting
    {
        get { return interacting; }
        set { interacting = value; }
    }
}
