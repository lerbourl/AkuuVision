 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryInstanciate : MonoBehaviour {

    public GameObject PhotoFoundPrefab;
    public GameObject PhotoNotFoundPrefab;
    
    public GameObject galleryContent;

	// Use this for initialization
	private void Start () {
        for (var i = 0; i < GlobalControl.NUMBER_OF_PHOTOS; i++)
        {
            instatiateLine(i);
        }
	}

    private void instatiateLine(int id)
    {
        GameObject clone;
        if (GlobalControl.Instance.photoIsFound(id))
        {
            clone = Instantiate(PhotoFoundPrefab, galleryContent.transform);
            MediaToGui media = clone.GetComponent<MediaToGui>();
            media.SetUrlLanguage(GlobalControl.Instance.getUrlPhoto(id));
            media.SetImage(GlobalControl.Instance.getAssociatedSprite(id), GlobalControl.Instance.getPhotoName(id));
        }
        else
        {
            clone = Instantiate(PhotoNotFoundPrefab, galleryContent.transform);
        }
        clone.GetComponentInChildren<Text>().text = GlobalControl.Instance.getPhotoName(id);
    }
}
