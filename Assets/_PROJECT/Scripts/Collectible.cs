using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Photon.Pun;

public class Collectible : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private Camera mainCamera;
    public bool simMode = false;

    private void Awake()
    {
        // Get the ARRaycastManager and main camera from the AR Session Origin
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        mainCamera = Camera.main;
    }

    private void Update()
    {

        RaycastHit hit;
        // sim mode for testing in FPS mode, in objectmanager and collectible

        if (simMode)
        {
            // Perform an AR raycast using the touch position and store the results in hitResults
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit) && hit.collider.gameObject.CompareTag("Collectible"))
            {

                    // Destroy the Collectible object
                    Inventory inventory = FindObjectOfType<Inventory>();
                    inventory.AddObject(hit.collider.gameObject.name);
                    PhotonNetwork.Destroy(hit.collider.gameObject);
            }
        }
        // Check if there's any touch input
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // Only process the touch input if it's in the Began phase
        if (touch.phase == TouchPhase.Began)
        {
            // Perform a raycast from the touch position
            Ray ray = mainCamera.ScreenPointToRay(touch.position);

            // Check if the raycast hits a Collectible object
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Collectible"))
            {
                // Destroy the Collectible object
                Inventory inventory = FindObjectOfType<Inventory>();
                inventory.AddObject(hit.collider.gameObject.name);
                PhotonNetwork.Destroy(hit.collider.gameObject);
            }
        }
    }
}