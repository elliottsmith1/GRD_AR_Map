using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] float intensity = 0.5f;

    private Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.y = Mathf.Sin(Time.time) + intensity;

        transform.position = newPos;

        transform.LookAt(nextPos);
    }

    public void SetNextPos(Vector3 _pos)
    {
        nextPos = _pos;
    }
}
