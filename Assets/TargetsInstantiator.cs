using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetsInstantiator : MonoBehaviour
{
    public GameObject targetPrefab;

    // Use this for initialization
    private void Start()
    {
        Instantiate(targetPrefab);
        /*
        for (var i = 0; i < GlobalControl.NUMBER_OF_PHOTOS; i++)
        {
            instatiateTarget(i);
        }
        */
    }
    /*
    private void instatiateTarget(int id)
    {
        Texture texture = GlobalControl.Instance.associateTexture[id];
        if (texture != null)
        {
            GameObject clone = Instantiate(targetPrefab);
            Renderer rend = clone.GetComponentInChildren<Renderer>();
            rend.material = new Material(Shader.Find("Unlit/Texture"));
            rend.material.mainTexture = texture;
            ImageTargetBehaviour itb = clone.GetComponent<ImageTargetBehaviour>();
        }
    }*/
}
