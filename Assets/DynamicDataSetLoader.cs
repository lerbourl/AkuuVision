using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Vuforia;

public class DynamicDataSetLoader : MonoBehaviour
{
    // Model is the GameObject to be augmented
    public GameObject Model;
    string dataSetName = "AkuuExpo.xml";

    // Use this for initialization
    void Start()
    {
        // Registering call back to know when Vuforia is ready
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
        // get all current TrackableBehaviours
        IEnumerable<TrackableBehaviour> trackableBehaviours =
        TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();

        // Loop over all TrackableBehaviours.
        foreach (TrackableBehaviour trackableBehaviour in trackableBehaviours)
        {
            GameObject go = trackableBehaviour.gameObject;
            string imageTargetName = go.GetComponent<ImageTargetBehaviour>().ImageTarget.Name;
            int imageTargetId = GlobalControl.Instance.GetTargetID(imageTargetName);
        
            // Add a Trackable event handler to the Trackable.
            // This Behaviour handles Trackable lost/found callbacks.
            go.AddComponent<MyNetworkManager>();
            go.AddComponent<PhotographieID>();
            go.GetComponent<PhotographieID>().setId(imageTargetId);
            go.AddComponent<MyVideoTrackableEventHandler>();

            // Instantiate the model.
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject clone = Instantiate(Model) as GameObject;

            // Attach the cube to the Trackable and make sure it has a proper size.
            clone.transform.parent = trackableBehaviour.transform;
            //cube.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            //cube.transform.localPosition = new Vector3(0.0f, 0.35f, 0.0f);
            //cube.transform.localRotation = Quaternion.identity;
            clone.SetActive(true);
            trackableBehaviour.gameObject.SetActive(true);
        }
    }
}
