using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenImage : MonoBehaviour {
    public Image image;
    public Text imageName;

	public void destroy()
    {
        Destroy(this.gameObject);
    }

    public void setImage(Sprite sprite, string name) {
        image.sprite = sprite;
        imageName.text = name;
    }
}
