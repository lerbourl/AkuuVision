using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class FullScreenMode : MonoBehaviour {

	public void playFullScreen()
    {
        Debug.Log("fullscreen mode ! " + this.GetComponent<VideoPlayer>().url);
        Handheld.PlayFullScreenMovie(this.GetComponent<VideoPlayer>().url);
    }
}
