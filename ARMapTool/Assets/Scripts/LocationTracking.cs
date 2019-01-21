using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracking : MonoBehaviour {

    private float user_longitude = 0;
    private float user_latitude = 0;

	// Use this for initialization
	void Start ()
    {
		if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            Debug.Log("Location sharing enabled");
            Debug.Log("Starting location polling");
        }

        else
        {
            Debug.Log("Location sharing not enabled");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetLongLat();

        Debug.Log(user_longitude);
        Debug.Log(user_latitude);
    }

    void GetLongLat()
    {
        if (Input.location.isEnabledByUser)
        {
            user_latitude = Input.location.lastData.latitude;
            user_longitude = Input.location.lastData.longitude;
        }
    }
}
