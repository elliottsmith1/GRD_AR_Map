using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class MapControl : MonoBehaviour
{
    [SerializeField] AbstractMap _map;

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
