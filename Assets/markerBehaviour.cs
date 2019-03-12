using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markerBehaviour : MonoBehaviour {
    
    private Sprite sprite;
    private string imageName;

    public void init(Sprite sprite, string imageName)
    {
        this.sprite = sprite;
        this.imageName = imageName;
    }

    public void ImageOnGUI()
    {
        GameObject clone = Instantiate(Resources.Load("Prefab/FullscreenImageLandscape") as GameObject, GameObject.FindGameObjectWithTag("infocanva").transform);
        clone.GetComponent<FullScreenImage>().setImage(sprite, imageName);
    }
}
