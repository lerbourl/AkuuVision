using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyManager : MonoBehaviour {

    public bool sceneIsMenu = false;
    public bool sceneUsesVuforia = false;
    public ScreenOrientation sceneOrientation = ScreenOrientation.AutoRotation;

    private void Awake()
    {
        Screen.orientation = sceneOrientation;
    }

    private void Start()
    {
        if (sceneUsesVuforia)
        {
            VuforiaBehaviour.Instance.enabled = true;
        }
        else {
            VuforiaBehaviour.Instance.enabled = false;
        }
        GlobalControl.Instance.updateLanguage();
    }

    private void Update()
    {
        // return to menu if scene is not menu on return (back button on android)
        if ((!sceneIsMenu) && Input.GetKey(KeyCode.Escape))
        {
            changeToScene("MainScene");
        }
    }

    #region USEFULL_FUNC

    public void Browse(string url)
    {
        Application.OpenURL(url);
    }

    public void changeToScene(string sceneName)
    {
        Debug.Log("Changing scene to " + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        foreach (var v in FindObjectsOfType<MyVideoController>())
        {
            v.Pause();
        }
    }


    // for buttons
    public void setlangageFR()
    {
        GlobalControl.Instance.setlanguage(Language.fr);
    }
    public void setlangageEN()
    {
        GlobalControl.Instance.setlanguage(Language.en);
    }

    #endregion USEFULL_FUNC
}
