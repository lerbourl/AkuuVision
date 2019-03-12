using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    public void setMessage(string v)
    {
        this.GetComponentInChildren<Text>().text = v;
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
