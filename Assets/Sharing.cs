using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharing : MonoBehaviour {

    private string sharedText = "https://play.google.com/store/apps/details?id=com.akuu.akuuexhibitionapp \n\nSalut ! Essaie vite AkuuVision ! Tu pourras découvrir les vidéos cachées derrière les photographies de l'exposition AKUU, sur le campus de st martin d'hères ! \n\n https://www.facebook.com/events/354817931788896/";

    public void shareApp()
    {
        StartCoroutine(myShare());
    }

    private IEnumerator myShare()
    {
        yield return new WaitForEndOfFrame();

        if (GlobalControl.Instance.getLanguage() == Language.fr)
        {
            new NativeShare().SetTitle("Partage avec tes amis ton expérience de l'exposition Akuu à grenoble !").SetSubject("L'application de l'exposition akuu à grenoble !").SetText(sharedText).Share();
        }
        else
        {
            new NativeShare().SetTitle("Share with your firends your experience of Akuu exhibition in grenoble !").SetSubject("Akuu exposition app in Grenoble").SetText(sharedText).Share();
        }
    }

}
