using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class FurniturePlacementManager : MonoBehaviour
{

    public GameObject spawanbleFurniture;

    public XROrigin xrOrigin;

    public ARRaycastManager raycastManager;

    public ARPlaneManager planeManager;

    private List<ARRaycastHit> rayCastHits = new List<ARRaycastHit>();


    public void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began )
            {
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, rayCastHits, TrackableType.PlaneWithinPolygon);

                if (collision && isButtonPressed() == false)
                {
                    GameObject _object = Instantiate(spawanbleFurniture);
                    _object.transform.position = rayCastHits[0].pose.position;
                    _object.transform.rotation = rayCastHits[0].pose.rotation;
                }

                foreach(var planes in planeManager.trackables)
                {
                    planes.gameObject.SetActive(false);
                }
                planeManager.enabled = false;
            }
        }
    }

    public bool isButtonPressed()
    {
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void SwitchFurniture(GameObject furniture)
    {
        spawanbleFurniture = furniture;
    }
}
