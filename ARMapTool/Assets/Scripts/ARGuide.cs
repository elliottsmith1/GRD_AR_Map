using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;

public class ARGuide : MonoBehaviour
{
    [SerializeField] DirectionsFactory dirFactory;
    [SerializeField] GameObject player;

    [SerializeField] Animator animator;

    private Vector3 lastPosition = Vector3.zero;
    private float vel;

    private float speed = 2.0f;
    private float turnSpeed = 2.0f;
    private Vector3 targetPos;

    private bool returningToPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 12)
        {
            returningToPlayer = true;            
        }

        if (returningToPlayer)
        {
            targetPos = player.transform.position;

            var rotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

            animator.SetFloat("speed", 1.0f + (vel * 10));

            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, player.transform.position) < 5)
            {
                returningToPlayer = false;
            }
            return;
        }

        targetPos = Vector3.Lerp(dirFactory.GetFirstArrowPosition(), player.transform.position, 0.1f);

        if (Vector3.Distance(transform.position, targetPos) > 1)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                var rotation = Quaternion.LookRotation(targetPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

                animator.SetFloat("speed", 1.0f + (vel * 10));

                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        }
        

        //if (Vector3.Distance(transform.position, player.transform.position) < 5)
        //{
        //    var rotation = Quaternion.LookRotation(dirFactory.GetFirstArrowPosition() - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

        //    speed = 10 / Vector3.Distance(transform.position, player.transform.position);

        //    animator.SetFloat("speed", 1.0f + (vel * 10));            

        //    transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //}       
        //else if (Vector3.Distance(transform.position, player.transform.position) > 5)
        //{
        //    var rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

        //    speed = Vector3.Distance(transform.position, player.transform.position);

        //    animator.SetFloat("speed", 1.0f + (vel * 10));

        //    transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //}
    }

    void FixedUpdate()
    {
        vel = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }
}
