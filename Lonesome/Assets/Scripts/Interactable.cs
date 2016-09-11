//Kevin Friddle
//Created: 9/11/2016
//Last Updated: 9/11/2016
using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public Transform player;
    public GameObject icon; //Icon to be shown on interactable object
    public float interactDistance;  //maximum distance the player can be from the interactable and interact with it
    public float alphaDistance; //maximum distance at which the icon is visible
    public float maxAlpha;  //maximum alpha value

    float alpha;
    Renderer rd;
    Material iconMat;
    Color iconColor;
	// Use this for initialization
	void Start () {
        icon.SetActive(false);
        rd = GetComponent<Renderer>();
        iconMat = icon.GetComponent<Material>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(rd.isVisible)
        {
            alpha = ((Vector3.Distance(player.transform.position, transform.position) - interactDistance) / 
                (alphaDistance - interactDistance)) * 0.0f +                                                    //interpolate alpha where
                (1 - ((Vector3.Distance(player.transform.position, transform.position) - interactDistance) /    //t = (1-(currentDistance - interact distance) / 
                (alphaDistance - interactDistance))) * maxAlpha;                                                //(alphaDistance - interactDistance))

            if(alpha < 0)
            {
                alpha = 0.0f;
            }

            if(alpha == 0 && icon.activeInHierarchy)
            {
                icon.SetActive(false);
            }
            else if(alpha > 0 && !icon.activeInHierarchy)
            {
                icon.SetActive(true);
            }

            if(icon.activeInHierarchy)
            {
                iconColor = iconMat.color;
                iconColor.a = alpha;
                iconMat.color = iconColor;
            }
        }
        else
        {
            icon.SetActive(false);
        }
	}

    public virtual void Interact()
    {

    }

    public float Alpha
    {
        get { return alpha; }
        set { alpha = value; }
    }

    public Material IconMat
    {
        get { return iconMat; }
        set { iconMat = value; }
    }

    public Color IconColor
    {
        get { return iconColor; }
        set { iconColor = value; }
    }
}
