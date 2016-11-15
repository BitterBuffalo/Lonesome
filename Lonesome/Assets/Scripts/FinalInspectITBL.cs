//Kevin Friddle
//Created: 11/14/16
//Last Updated: 11/14/16
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class FinalInspectITBL : Interactable {

    public GameObject dupRef;   //the object to be duplicated upon inspection
    public Text printTextObj;
    public string printText;
    public float objOffset; //the distance between the camera and the duplicated object
    public float rotSpeed;
    public CameraController camCon;
    public CharacterController charCon;

    bool displayingText;
    bool iInitialized;  //if the interaction has been initialized;

    Ray rayForward; //ray that allows us to get a point along the camera's forward vector where the inspected object will end up
    GameObject inspectedObject;
    Vector3 prevMousePos;
    Vector3 rotVector;
    // Use this for initialization
    public override void Start () {
        base.Start();

        displayingText = false;
        iInitialized = false;
        printTextObj.text = "";

        rayForward = new Ray();
        rotVector = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Interact()
    {
        if(iInitialized)
        {
            #region GET_INPUT
            if (Input.GetMouseButton(0))
            {
                if( Input.GetAxis("Mouse X") != 0.0f || Input.GetAxis("Mouse Y") != 0.0f)
                {
                    rotVector.x = Input.GetAxis("Mouse Y");
                    rotVector.y = -Input.GetAxis("Mouse X");

                    inspectedObject.transform.Rotate(rotVector * Time.deltaTime * rotSpeed);
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))  // Toggle Display text
            {
                if (displayingText)
                {
                    displayingText = false;
                    printTextObj.text = "";
                }
                else
                {
                    displayingText = true;
                    printTextObj.text = printText;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                displayingText = false;
                iInitialized = false;
                printTextObj.text = "";

                if (actor.cam.GetComponent<BlurOptimized>())
                { actor.cam.GetComponent<BlurOptimized>().enabled = false; }

                if (camCon && charCon)
                {
                    camCon.enabled = true;
                    charCon.enabled = true;
                }

                interacted = false;
                actor.Interacting = false;
                Destroy(inspectedObject);
            }
            #endregion GET_INPUT
        }
        else
        {
            rayForward.origin = actor.cam.transform.position;
            rayForward.direction = actor.cam.transform.forward;
            inspectedObject = Instantiate(dupRef, rayForward.GetPoint(objOffset), Quaternion.identity) as GameObject;
            inspectedObject.layer = 8;  //set to inspection layer

            if(actor.cam.GetComponent<BlurOptimized>())
            { actor.cam.GetComponent<BlurOptimized>().enabled = true; }

            if (camCon && charCon)
            {
                camCon.enabled = false;
                charCon.enabled = false;
            }
            Debug.Log("Second object created!");
            iInitialized = true;
        }
    }
}
