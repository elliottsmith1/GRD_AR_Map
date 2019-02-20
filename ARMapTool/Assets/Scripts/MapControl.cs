using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;

public class MapControl : MonoBehaviour
{
    [SerializeField] Camera mapCam;
    [SerializeField] AbstractMap _map;
    [SerializeField] DirectionsFactory _dir;

    private bool touching = false;
    private float touchTimer = 0.0f;
    private float touchTimeMax = 1.0f;

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

                NewWaypoint(Input.GetTouch(0).position);
            }
        }
    }

    void NewWaypoint(Vector3 _pos)
    {
        Vector3 pos = mapCam.ScreenToWorldPoint(_pos);

        _dir.NewDestination(pos);
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
