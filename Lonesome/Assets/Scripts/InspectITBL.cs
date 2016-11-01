//Kevin Friddle
//Created: 10/24/2016
//Last Updated: 10/31/2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectITBL : Interactable {

    public Text printTextObj;
    public string printText;
    public float bezierSpeed;
    public float distanceFromCamera;
    public GameObject inspectionFilter;
    public float filterOffset;
    public float filterAlpha;

    GameObject inspectedObject;
    Vector3 startPos;
    Vector3 midPos; //the middle node of the bezier curve to be determined upon interaction.
    Vector3 endPos;
    bool hasSpawned;    //whether or not the new model has spawned or not.
    bool hasInterpolated;   //whether or not the new model has finished moving along the bezier curve
    bool canceledInterpolation; //whether or not the inspected object has finished moving along the curve after inspection was canceled
    Vector3 prevMousePos;   //the mouse position on the last frame (only while rotating the inspected object).
    bool inspectionCanceled;    //true when the player wishes to end inspection
    bool displayingText;    //true when the player chooses to display plain text
    Ray rayForward; //ray that allows us to get a point along the camera's forward vector where the inspected object will end up
    Vector3 rotVector;

	// Use this for initialization
	public override void Start () {
        base.Start();

        rayForward = new Ray();
        rotVector = new Vector3(0.0f, 0.0f, 0.0f);
        inspectionFilter.SetActive(false);
	}

    public override void Interact()
    {
        if(hasSpawned)
        {
            if(hasInterpolated)
            {
                #region GET_INPUT
                if (Input.GetMouseButton(0))
                {
                    if(Input.GetMouseButtonDown(0)) //initialize the previous mouse position on first click
                    { prevMousePos = Input.mousePosition; }

                    if(prevMousePos.x != Input.mousePosition.x || prevMousePos.y != Input.mousePosition.y)
                    {
                        rotVector.x +=  Input.mousePosition.x - prevMousePos.x;
                        rotVector.y += Input.mousePosition.y - prevMousePos.y;

                        inspectedObject.transform.Rotate(rotVector);
                    }

                    prevMousePos = Input.mousePosition;
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    inspectionCanceled = true;
                    displayingText = false;
                    printTextObj.text = "";
                    inspectionFilter.SetActive(false);
                }

                if(Input.GetKeyDown(KeyCode.Tab))//Display text
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

                #region CANCELED_INTERPOLATION
                if (inspectionCanceled)
                {
                    inspectedObject.transform.position = (Mathf.Pow(1 - (Time.time * bezierSpeed), 2) * endPos) +
                    (2 * (1 - Time.time * bezierSpeed) * (Time.time * bezierSpeed) * midPos) + (Time.time * bezierSpeed * startPos);

                    if ((Time.time * bezierSpeed) >= 1.0f)
                    {
                        canceledInterpolation = true;
                        inspectionCanceled = false;
                        hasInterpolated = false;
                        hasSpawned = false;
                    }
                }
                else if(canceledInterpolation)  //if the interpolation is finished
                {
                    Destroy(inspectedObject);
                    actor.Interacting = false;
                    interacted = false;
                }
                #endregion CANCELED_INTERPOLATION
            }
            else
            {
                inspectedObject.transform.position = (Mathf.Pow(1 - (Time.time * bezierSpeed), 2) * startPos) + 
                    (2 * (1 - Time.time * bezierSpeed) * (Time.time * bezierSpeed) * midPos) + (Time.time * bezierSpeed * endPos);

                if((Time.time * bezierSpeed) >= 1.0f)
                {
                    hasInterpolated = true;
                    inspectionFilter.SetActive(true);
                    inspectionFilter.transform.position = endPos + actor.cam.transform.forward * filterOffset;
                    inspectionFilter.GetComponent<Renderer>().material.color = new Color(inspectionFilter.GetComponent<Renderer>().material.color.r,
                        inspectionFilter.GetComponent<Renderer>().material.color.g, 
                        inspectionFilter.GetComponent<Renderer>().material.color.b, filterAlpha);
                }
            }
        }
        else
        {
            inspectedObject = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
            inspectedObject.layer = 8;  //set to the inspection layer
            hasSpawned = true;
            startPos = transform.position;
            rayForward.origin = actor.cam.transform.position;
            rayForward.direction = actor.cam.transform.forward;
            endPos = rayForward.GetPoint(distanceFromCamera);

            //////////DETERMINING THE MIDPOINT/////////////
            /*The midpoint is determined by taking the endpoint and adding the difference between the projection of the endpoint
             onto the startpoint and the startpoint itself, then subtracting the vector perpendicular to the startpoint from the
             endpoint which will give us a new vector that is between both the startpoint and the endpoint, allowing for a nicer
             curve during interpolation.*/
            midPos = (endPos + (startPos - ((DotProduct3D(endPos, startPos) / DotProduct3D(startPos, startPos)) * startPos))) - 
                (endPos - ((DotProduct3D(endPos, startPos) / DotProduct3D(startPos, startPos)) * startPos));
        }
    }

    float DotProduct3D(Vector3 v1, Vector3 v2)
    {
        return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
    }
}
