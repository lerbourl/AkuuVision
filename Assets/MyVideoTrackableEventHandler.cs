/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.

MODIFIED BY LOUIS LERBOURG FOR AKUUVISION APP
===============================================================================*/

using UnityEngine;

[RequireComponent(typeof(PhotographieID))]
[RequireComponent(typeof(MyNetworkManager))]
public class MyVideoTrackableEventHandler : DefaultTrackableEventHandler
{
    #region PROTECTED_METHODS

    protected override void OnTrackingLost()
    {
        mTrackableBehaviour.GetComponentInChildren<MyVideoController>().Pause();
        base.OnTrackingLost();
    }
    protected override void OnTrackingFound()
    {
        this.GetComponent<PhotographieID>().setNewPhotoFound();
        this.GetComponent<MyNetworkManager>().checkConnection();
        mTrackableBehaviour.GetComponentInChildren<MyVideoController>().Play();
        base.OnTrackingFound();
    }

    #endregion // PROTECTED_METHODS
}
