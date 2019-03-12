using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour {

    public GameObject notVictory;
    public GameObject Victory;
	// Use this for initialization

    public void bringVictoryPanelToFront()
    {
        if (GlobalControl.Instance.getScore() >= 23)
        {
            Victory.GetComponent<VariousInteractions>().bringToFront();
        }
        else
        {
            notVictory.GetComponent<VariousInteractions>().bringToFront();
        }
    }
}
