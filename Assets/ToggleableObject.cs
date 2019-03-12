using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableObject : MonoBehaviour {

	public void toggle()
    {
        GameObject go = gameObject;
        if (go.activeSelf) go.SetActive(false);
        else go.SetActive(true);
    }
}
