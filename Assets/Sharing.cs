using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharing : MonoBehaviour {

    private string frsharedText = "Salut ! Essaie vite l'application AkuuVision ! Tu pourras découvrir les vidéos cachées derrière les photographies de l'exposition AKUU, sur le campus de st martin d'hères !\n\n"
                                + "Sur le Google Play http://opn.to/a/BujhI \n\n"
                                + "Sur l'apple store http://opn.to/a/1xR0g \n\n"
                                + "L'évènement : https://www.facebook.com/events/354817931788896/";
    private string ensharedText = "Hi ! Let's try the AkuuVision App! You will be able to discover the hidden videos behind the photographs of the AKUU exhibition, on the campus of St Martin d'Hères !\n\n"
                                + "On Google Play http://opn.to/a/BujhI \n\n"
                                + "On apple store http://opn.to/a/1xR0g \n\n"
                                + "The event : https://www.facebook.com/events/354817931788896/";

    public void shareApp()
    {
        StartCoroutine(myShare());
    }

    private IEnumerator myShare()
    {
        yield return new WaitForEndOfFrame();

        if (GlobalControl.Instance.getLanguage() == Language.fr)
        {
            new NativeShare().SetTitle("Partage avec tes amis ton expérience de l'exposition Akuu à grenoble !").SetSubject("L'application de l'exposition akuu à grenoble !").SetText(frsharedText).Share();
        }
        else
        {
            new NativeShare().SetTitle("Share with your firends your experience of Akuu exhibition in grenoble !").SetSubject("Akuu exposition app in Grenoble").SetText(ensharedText).Share();
        }
    }

}
