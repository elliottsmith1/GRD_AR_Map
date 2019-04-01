using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] float intensity = 0.5f;

    [SerializeField] AbstractMap _map;

    private GameObject user;

    private Vector3 nextPos;
    private Vector2d worldPos;

    private bool activated = false;
    private bool queueForDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_map)
        {
            worldPos = _map.WorldToGeoPosition(transform.position);
        }

        //foreach (Transform child in transform)
        //{
        //    child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = _map.GeoToWorldPosition(worldPos, true);

        //Vector3 newPos = transform.position;
        //newPos.y = Mathf.Sin(Time.time) + intensity;

        //transform.position = newPos;

        transform.LookAt(nextPos);

        if (!activated)
        {
            if (Vector3.Distance(user.transform.position, transform.position) < 10)
            {
                Activate();
            }
        }
        else
        {
            if (Vector3.Distance(user.transform.position, transform.position) > 10)
            {
                if (queueForDestroy)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    Deactivate();
                }
            }
        }
    }

    private void Activate()
    {
        activated = true;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void Deactivate()
    {
        activated = false;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
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

    public void SetUser(GameObject _user)
    {
        user = _user;
    }

    public bool GetActivated()
    {
        return activated;
    }

    public void SetQueueForDestroy(bool _queue)
    {
        queueForDestroy = _queue;
    }
}
