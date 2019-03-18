using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] float intensity = 0.5f;

    [SerializeField] AbstractMap _map;

    private Vector3 nextPos;
    private Vector2d worldPos;

    // Start is called before the first frame update
    void Start()
    {
        if (_map)
        {
            worldPos = _map.WorldToGeoPosition(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = _map.GeoToWorldPosition(worldPos, true);

        //Vector3 newPos = transform.position;
        //newPos.y = Mathf.Sin(Time.time) + intensity;

        //transform.position = newPos;

        transform.LookAt(nextPos);
    }

    public void SetNextPos(Vector3 _pos)
    {
        nextPos = _pos;
    }

    public void SetWorldPos(Vector2d _pos)
    {
        worldPos = _pos;
    }

    public void SetMap (AbstractMap map)
    {
        _map = map;
    }
}
