using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendEmailGoogleSheet : MonoBehaviour {

    public GameObject GOcommentary;

    private string email;
    private string deviceIdentifier;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSdIm60OCBnnLZW5FNC4sC6ppuqPy6_H1TY6daxyAu1dh3xlNA/formResponse";

    IEnumerator Post(string email, string deviceIdentifier)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.268860784", deviceIdentifier);
        form.AddField("entry.230581023", email);

        byte[] rawData = form.data;

        // Post a request to an URL with our custom headers
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }

    public void Send()
    {
        email = GOcommentary.GetComponent<InputField>().text;
        GOcommentary.GetComponent<InputField>().text = "";
        Debug.Log("SENDING TO GOOGLE SHEETS");
        StartCoroutine(Post(email, deviceIdentifier));
    }

    private void Start()
    {
        deviceIdentifier = SystemInfo.deviceUniqueIdentifier;
    }
}
