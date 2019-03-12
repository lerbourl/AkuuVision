using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotographieID : MonoBehaviour {

    public int photoId;

    public void setId(int photoId)
    {
        this.photoId = photoId;
    }

    public int getId()
    {
        return this.photoId;
    }

    public void setNewPhotoFound()
    {
        GlobalControl.Instance.setNewPhotoFound(photoId);
    }
}
