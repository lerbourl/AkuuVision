using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariousInteractions : MonoBehaviour {

    public void bringToFront()
    {
        GameObject go = gameObject;
        go.transform.SetAsLastSibling();
    }
}
