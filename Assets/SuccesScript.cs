using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SuccesScript : MonoBehaviour {
    private Text text;

    // Use this for initialization
    private void Awake()
    {
        text = this.GetComponent<Text>();
    }
    private void Start()
    {
        UpdateScore(GlobalControl.Instance.getScore());
    }
    public void UpdateScore (int score) {
        text.text = score + "/" + GlobalControl.NUMBER_OF_PHOTOS;
	}
}
