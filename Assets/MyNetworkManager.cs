using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : MonoBehaviour {

    private bool online;
    private bool checking;

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("https://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
    public void checkConnection()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            if (!isConnected)
            {
                GameObject clone = Instantiate(Resources.Load("Prefab/InformationPanel") as GameObject, GameObject.FindGameObjectWithTag("infocanva").transform);
                if (GlobalControl.Instance.getLanguage() == Language.fr)
                {
                    clone.GetComponent<InfoScript>().setMessage("Oups ! Il semblerait que tu ne sois pas connecté(e) à internet !");
                }
                else
                {
                    clone.GetComponent<InfoScript>().setMessage("You may not be connected to the internet, please check your internet connexion !");
                }
            }
        }));
    }

    public void checkConnectionAccuseReception()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            GameObject clone = Instantiate(Resources.Load("Prefab/InformationPanel") as GameObject, GameObject.FindGameObjectWithTag("infocanva").transform);
            if (!isConnected)
            {
                if (GlobalControl.Instance.getLanguage() == Language.fr)
                {
                    clone.GetComponent<InfoScript>().setMessage("Oups ! Il semblerait que tu ne sois pas connecté(e) à internet !");
                }
                else
                {
                    clone.GetComponent<InfoScript>().setMessage("You may not be connected to the internet, please check your internet connexion !");
                }
            }
            else
            {
                if (GlobalControl.Instance.getLanguage() == Language.fr)
                {
                    
                    clone.GetComponent<InfoScript>().setMessage("Merci beaucoup !\nVotre message est bien envoyé !");
                }
                else
                {
                    clone.GetComponent<InfoScript>().setMessage("Thanks a lot !\n Your message has been sent !");
                }
            }
        }));
    }
}
