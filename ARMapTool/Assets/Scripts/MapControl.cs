using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Examples;

public class MapControl : MonoBehaviour
{
    [SerializeField] TouchCamera camMovement;
    [SerializeField] Camera mapCam;
    [SerializeField] AbstractMap _map;
    [SerializeField] DirectionsFactory _dir;

    [SerializeField] GameObject newDestinationUI;
    [SerializeField] GameObject locationMarker;

    private bool touching = false;
    private float touchTimer = 0.0f;
    private float touchTimeMax = 2.0f;

    private Vector3 targetDestination;

    private void Start()
    {
        newDestinationUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            touching = false;
            touchTimer = 0.0f;
        }

        else if (Input.touchCount == 1)
        {
            if (!touching)
            {
                touching = true;
            }
        }

        if (touching)
        {
            touchTimer += Time.deltaTime;

            if (touchTimer > touchTimeMax)
            {
                touching = false;
                touchTimer = 0.0f;

                targetDestination = Input.GetTouch(0).position;

                newDestinationUI.SetActive(true);
                EnableNewDestinationUI();
            }
        }
    }

    public void NewWaypoint()
    {
        DisableNewDestinationUI();

        Vector3 _pos = targetDestination;

        Vector3 pos = _pos;

        RaycastHit hit;
        Ray ray = mapCam.ScreenPointToRay(_pos);

        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;

            _dir.NewDestination(pos);
        }        
    }

    public void EnableNewDestinationUI()
    {
        camMovement.enabled = false;
        
        locationMarker.transform.position = targetDestination;
        newDestinationUI.SetActive(true);
    }

    public void DisableNewDestinationUI()
    {
        newDestinationUI.SetActive(false);
        camMovement.enabled = true;
    }

    public void MapZoomIn()
    {
        float newZoom = _map.Zoom;
        newZoom += 1;

        _map.UpdateMap(newZoom);
    }

    public void MapZoomOut()
    {
        float newZoom = _map.Zoom;
        newZoom -= 1;

        _map.UpdateMap(newZoom);
    }
}
