using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendCommentaryGoogleSheet : MonoBehaviour {

    public GameObject GOcommentary;
    public GameObject thumbUp;
    public GameObject thumbDown;

    private string commentary;
    private string thumb;
    private string deviceIdentifier;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSfnn7OpVUagTHo_ig3Lp0fY8cH-RCzcDkAGy0xwMtbv7sPalg/formResponse";

    IEnumerator Post(string commentary, string thumb, string deviceIdentifier)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.2096472290", deviceIdentifier);
        form.AddField("entry.1989613982", commentary);
        form.AddField("entry.170875846", thumb);

        byte[] rawData = form.data;

        // Post a request to an URL with our custom headers
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }

    public void Send()
    {
        commentary = GOcommentary.GetComponent<InputField>().text;
        GOcommentary.GetComponent<InputField>().text = "";
        Debug.Log("SENDING TO GOOGLE SHEETS");
        StartCoroutine(Post(commentary, this.thumb, deviceIdentifier));
    }

    private void Start()
    {
        deviceIdentifier = SystemInfo.deviceUniqueIdentifier;
        SetThumbUp();
    }

    public void SetThumbUp()
    {
        this.thumb = "up";
        thumbUp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        thumbUp.GetComponent<VariousInteractions>().bringToFront();
        thumbDown.GetComponent<Image>().color = new Color32(255, 255, 255, 90);
    }

    public void SetThumbDown()
    {
        this.thumb = "down";
        thumbDown.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        thumbDown.GetComponent<VariousInteractions>().bringToFront();
        thumbUp.GetComponent<Image>().color = new Color32(255, 255, 255, 90);
    }

}
