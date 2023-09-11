using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;

public class ObjectManager : MonoBehaviour
{
    public bool simMode = false;

    // The prefab that will be instantiated when placing objects
    public GameObject objectPrefab;

    // Reference to the ARRaycastManager component
    private ARRaycastManager arRaycastManager;

    // List to store the results of AR raycasts
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    // Called when the script is initialized
    private void Awake()
    {
        // Get the ARRaycastManager component attached to the same GameObject
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Called every frame
    private void Update()
    {
        // sim mode for testing in FPS mode, in objectmanager and collectible
        if (simMode)
        {
            // Perform a raycast using the camera position and store the results in hitResults
            RaycastHit rayhitResults;
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayhitResults) && !rayhitResults.collider.gameObject.CompareTag("Collectible"))
            {
                // Get the pose of the first hit result
                Transform hitTransform = rayhitResults.transform;

                // Instantiate the objectPrefab at the hitPose position and rotation
                PhotonNetwork.Instantiate(objectPrefab.name, rayhitResults.point, hitTransform.rotation);
            }
        }

        // If no touch input is detected, return
        if (Input.touchCount == 0) return;

        // Get the first touch input
        Touch touch = Input.GetTouch(0);

        // If the touch input is in the Began phase
        if (touch.phase == TouchPhase.Began)
        {
            // Perform an AR raycast using the touch position and store the results in hitResults
            if (arRaycastManager.Raycast(touch.position, hitResults, TrackableType.Planes))
            {
                // Get the pose of the first hit result
                Pose hitPose = hitResults[0].pose;

                // Instantiate the objectPrefab at the hitPose position and rotation
                PhotonNetwork.Instantiate(objectPrefab.name, hitPose.position, hitPose.rotation);
            }
        }
    }
}