using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform target;

    public float smoothspeed = 10f;
    public Vector3 offset;


    // Update is called once per frame
    void LateUpdate()
    {

        //faz a camera seguir o player
        Vector3 desiredposition = target.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed * Time.deltaTime);

        if(smoothedposition.y < 1)
        {
            smoothedposition.y = 1;
        }
        else if (smoothedposition.y >1.7f)
        {
            smoothedposition.y = 1.7f;
        }
            transform.position = smoothedposition;
    }
}
