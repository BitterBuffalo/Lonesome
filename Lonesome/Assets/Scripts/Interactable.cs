//Kevin Friddle
//Created: 9/11/2016
//Last Updated: 10/3/2016
using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public Transform player;
    public GameObject icon; //Icon to be shown on interactable object
    public Interact actor;

    protected bool interacted; //determines if the object is being interacted with
    Renderer iconRenderer;  //renderer of the icon. Set enabled to false to make the icon invisible

	// Use this for initialization
	public virtual void Start () {
        iconRenderer = icon.GetComponent<Renderer>();
        iconRenderer.enabled = false;
        interacted = false;
	}
	

	void FixedUpdate () {
        if(interacted)
        { Interact(); }

        if (actor.Interacting && iconRenderer.enabled)
        { iconRenderer.enabled = false; }
        else if(!actor.Interacting)
        {
            if (Vector3.Distance(player.position, gameObject.transform.position) <= actor.rayDistance &&
            !iconRenderer.enabled) { iconRenderer.enabled = true; }

            else if (Vector3.Distance(player.position, gameObject.transform.position) > actor.rayDistance &&
                iconRenderer.enabled) { iconRenderer.enabled = false; }
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        interacted = false;
        actor.Interacting = false;
    }

    public bool Interacted
    {
        get { return interacted; }
        set { interacted = value; }
    }
}
