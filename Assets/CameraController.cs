using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 mouseLastPosition;
    float cameraZoom = 5;
    Vector3 dragOrigin;

    Vector2 minmaxZoom = new Vector3(1, 20);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2)||Input.GetKeyDown(KeyCode.Space))
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(2)||Input.GetKey(KeyCode.Space))
        {
            var diff = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition); 

            transform.position+= diff;
        }
        cameraZoom -= Input.mouseScrollDelta.y;
        cameraZoom = Mathf.Clamp(cameraZoom,minmaxZoom.x,minmaxZoom.y);
        Camera.main.orthographicSize = cameraZoom;
        // var screenPoint = Input.mousePosition;
        // screenPoint.z = 10.0f;
        // Vector3 pos = Camera.main.ScreenToWorldPoint(screenPoint);
        // if (Input.GetMouseButton(2))
        //     Camera.main.transform.position += mouseLastPosition - pos;
        // mouseLastPosition = pos;
    }
}
