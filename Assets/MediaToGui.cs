using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaToGui : MonoBehaviour {

    public string urlLanguage;
    private Sprite sprite;
    public GameObject image;
    private string imageName;

    public void SetUrlLanguage(string urlLanguage)
    {
        this.urlLanguage = urlLanguage;
    }

    public void SetImage(Sprite sprite, string imageName)
    {
        this.sprite = sprite;
        this.imageName = imageName;
        image.GetComponent<Image>().sprite = sprite;
    }

    public void ImageOnGUI()
    {
        GameObject clone = Instantiate(Resources.Load("Prefab/FullscreenImage"), FindObjectOfType<Canvas>().transform) as GameObject;
        clone.GetComponent<FullScreenImage>().setImage(sprite, imageName);
    }

    public void VideoOnGUI()
    {
        ScreenOrientation previousScreenOrientation = Screen.orientation;
        Handheld.PlayFullScreenMovie(urlLanguage);
    }
}
