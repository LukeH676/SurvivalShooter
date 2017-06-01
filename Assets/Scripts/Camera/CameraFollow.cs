using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position + target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset; // target trying to find a pacle for the camera to be up above the level, its the positoin fo the player + the offset position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);// smoothly move between positions
    }
}
