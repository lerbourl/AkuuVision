/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.

MODIFIED BY LOUIS LERBOURG FOR AKUUVISION APP
===============================================================================*/
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
public class MyVideoController : MonoBehaviour
{
    #region PRIVATE_MEMBERS

    private VideoPlayer videoPlayer;
    static private int frameDropNumber = 150;

    #endregion //PRIVATE_MEMBERS


    #region PUBLIC_MEMBERS

    public Button m_PlayButton;
    public Graphic m_waitImage;
    public RectTransform m_ProgressBar;

    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Setup Delegates
        videoPlayer.errorReceived += HandleVideoError;
        videoPlayer.started += HandleStartedEvent;
        videoPlayer.prepareCompleted += HandlePrepareCompleted;
        videoPlayer.seekCompleted += HandleSeekCompleted;
        videoPlayer.loopPointReached += HandleLoopPointReached;

        LogClipInfo();
        ShowWaitImage(false);
    }

    void Update()
    {
        if (videoPlayer.isPrepared)
        {
            if (m_waitImage.enabled == true) ShowWaitImage(false);
            if (videoPlayer.frameCount < float.MaxValue)
            {
                float frame = (float)videoPlayer.frame;
                float count = (float)videoPlayer.frameCount;

                float progressPercentage = 0;

                if (count > 0)
                    progressPercentage = (frame / count) * 100.0f;

                if (m_ProgressBar != null)
                    m_ProgressBar.sizeDelta = new Vector2((float)progressPercentage, m_ProgressBar.sizeDelta.y);
            }
            if (videoPlayer.isPlaying)
            {
                ShowPlayButton(false);
            }
            else
            {
                ShowPlayButton(true);
            }
        }

    }

    void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause(" + pause + ") called.");
        if (pause)
            Pause();
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void Play()
    {
        if (!videoPlayer.isPrepared)
        {
            ShowWaitImage(true);
            ShowPlayButton(false);
        }
        Debug.Log("Play Video");
        PauseAudio(false);
        videoPlayer.Play();
        ShowPlayButton(false);
    }

    public void Pause()
    {
        if (videoPlayer)
        {
            Debug.Log("Pause Video");
            if (m_waitImage.enabled == true) ShowWaitImage(false);
            PauseAudio(true);
            videoPlayer.Pause();
            ShowPlayButton(true);
        }
    }
    
    public void forward()
    {
        if (videoPlayer.frame + frameDropNumber < (long)videoPlayer.frameCount) videoPlayer.frame += frameDropNumber;
        else videoPlayer.frame = (long)videoPlayer.frameCount - 1;
    }

    public void backward()
    {
        if (videoPlayer.frame - frameDropNumber >= 0) videoPlayer.frame -= frameDropNumber;
        else videoPlayer.frame = 0;
    }

    public void resetVideo()
    {
        videoPlayer.frame = 0;
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private void PauseAudio(bool pause)
    {
        for (ushort trackNumber = 0; trackNumber < videoPlayer.audioTrackCount; ++trackNumber)
        {
            if (pause)
                videoPlayer.GetTargetAudioSource(trackNumber).Pause();
            else
                videoPlayer.GetTargetAudioSource(trackNumber).UnPause();
        }
    }

    private void ShowPlayButton(bool enable)
    {
        m_PlayButton.enabled = enable;
        m_PlayButton.GetComponent<Image>().enabled = enable;
    }

    private void ShowWaitImage(bool enable)
    {
        m_waitImage. enabled = enable;
        m_waitImage.GetComponent<Image>().enabled = enable;
    }

    private void LogClipInfo()
    {
        if (videoPlayer.clip != null)
        {
            string stats =
                "\nName: " + videoPlayer.clip.name +
                "\nAudioTracks: " + videoPlayer.clip.audioTrackCount +
                "\nFrames: " + videoPlayer.clip.frameCount +
                "\nFPS: " + videoPlayer.clip.frameRate +
                "\nHeight: " + videoPlayer.clip.height +
                "\nWidth: " + videoPlayer.clip.width +
                "\nLength: " + videoPlayer.clip.length +
                "\nPath: " + videoPlayer.clip.originalPath;

            Debug.Log(stats);
        }
    }

    #endregion // PRIVATE_METHODS


    #region DELEGATES

    void HandleVideoError(VideoPlayer video, string errorMsg)
    {
        Debug.LogError("Error: " + video.clip.name + "\nError Message: " + errorMsg);
    }

    void HandleStartedEvent(VideoPlayer video)
    {
        Debug.Log("Started: " + video.clip.name);
    }

    void HandlePrepareCompleted(VideoPlayer video)
    {
        Debug.Log("Prepare Completed: " + video.clip.name);
    }

    void HandleSeekCompleted(VideoPlayer video)
    {
        Debug.Log("Seek Completed: " + video.clip.name);
    }

    void HandleLoopPointReached(VideoPlayer video)
    {
        Debug.Log("Loop Point Reached: " + video.clip.name);

        ShowPlayButton(true);
    }

    #endregion //DELEGATES

}
