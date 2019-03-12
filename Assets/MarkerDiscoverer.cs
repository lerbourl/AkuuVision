using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerDiscoverer : MonoBehaviour {

    public GameObject[] markers = new GameObject[GlobalControl.NUMBER_OF_PHOTOS];
    public Sprite notFoundImage;
    private string notFoundString;

	// Use this for initialization
	void Start () {
        if (GlobalControl.Instance.getLanguage() == Language.fr)
        {
            notFoundString = "photographie non découverte";
        }
        else if (GlobalControl.Instance.getLanguage() == Language.en)
        {
            notFoundString = "photograph not found";
        }

        for (int i = 0; i < GlobalControl.NUMBER_OF_PHOTOS; i ++)
        {
            if (GlobalControl.Instance.photoIsFound(i))
            {
                markers[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("graphismes/icon_localisation_photo-trouve");
                markers[i].GetComponent<markerBehaviour>().init(GlobalControl.Instance.getAssociatedSprite(i), GlobalControl.Instance.getPhotoName(i));
            }
            else
            {
                markers[i].GetComponent<markerBehaviour>().init(notFoundImage, notFoundString);
            }
        }
	}
}
