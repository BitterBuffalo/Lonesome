//Kevin Friddle
//Created: 11/07/2016
//Last Updated: 11/07/2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class newInspectITBL : Interactable {

    public GameObject inspectionCam;
    public GameObject dupRef;    //the object to be duplicated upon inspection
    public Text printTextObj;
    public string printText;
    public float objOffset;

    bool displayingText;
    bool camSwapped;

    Ray rayForward; //ray that allows us to get a point along the camera's forward vector where the inspected object will end up
    GameObject inspectedObject;
    Vector3 prevMousePos;
    Vector3 rotVector;
	// Use this for initialization
	public override void Start () {
        base.Start();

        displayingText = false;
        printTextObj.text = "";
        camSwapped = false;
        inspectionCam.GetComponent<Camera>().enabled = false;

        rayForward = new Ray();
        rotVector = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Interact()
    {
        if (camSwapped)
        {
            #region GET_INPUT
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0)) //initialize the previous mouse position on first click
                { prevMousePos = Input.mousePosition; }

                if (prevMousePos.x != Input.mousePosition.x || prevMousePos.y != Input.mousePosition.y)
                {
                    rotVector.x += Input.mousePosition.x - prevMousePos.x;
                    rotVector.y += Input.mousePosition.y - prevMousePos.y;

                    inspectedObject.transform.Rotate(rotVector);
                }

                prevMousePos = Input.mousePosition;
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                displayingText = false;
                printTextObj.text = "";

                camSwapped = false;
                inspectionCam.GetComponent<Camera>().enabled = false;
                actor.cam.GetComponent<Camera>().enabled = true;

                interacted = false;
                actor.Interacting = false;
                Destroy(inspectedObject);
            }

            if (Input.GetKeyDown(KeyCode.Tab))//Display text
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
            #endregion GET_INPUT
        }
        else
        {
            inspectionCam.GetComponent<Camera>().enabled = true;
            actor.cam.GetComponent<Camera>().enabled = false;
            camSwapped = true;

            rayForward.origin = inspectionCam.transform.position;
            rayForward.direction = inspectionCam.transform.forward;
            inspectedObject = Instantiate(dupRef, rayForward.GetPoint(objOffset), Quaternion.identity) as GameObject;
        }
    }
}
