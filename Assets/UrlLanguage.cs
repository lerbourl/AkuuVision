using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class UrlLanguage : MonoBehaviour {

    private void Start()
    {
        this.GetComponent<VideoPlayer>().url = GlobalControl.Instance.getUrlPhoto(GetComponentInParent<PhotographieID>().getId());
    }
}
