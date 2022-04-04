using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    Camera cam;
    Vector2 mousePosition;
    private Plane draggingPlane;
    private Vector3 offset;

    //bool dragging = false;

    public event Action OnPlayerMouseClick;
    public event Action OnPlayerMouseRelease;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !dragging)
        //{
        //    Debug.Log("Start Dragging");
        //    dragging = true;
        //    draggingPlane = new Plane(camera.transform.forward, transform.position);
        //    Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);

        //    float planeDistance;
        //    draggingPlane.Raycast(cameraRay, out planeDistance);
        //    offset = transform.position - cameraRay.GetPoint(planeDistance);
        //}
        //else if (Input.GetMouseButton(0) && dragging)
        //{
        //    Debug.Log("Dragging");
        //    Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        //    float planeDistance;
        //    draggingPlane.Raycast(cameraRay, out planeDistance);
        //    transform.position = cameraRay.GetPoint(planeDistance) + offset;
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    Debug.Log("Stop Dragging");
        //    dragging = false;
        //}
    }

    private void OnMouseOver()
    {
        //Debug.Log(transform.name);
    }

    void OnMouseDown()
    {
        //Debug.Log(transform.name);
        draggingPlane = new Plane(cam.transform.forward, transform.position);
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

        float planeDistance;
        draggingPlane.Raycast(cameraRay, out planeDistance);
        offset = transform.position - cameraRay.GetPoint(planeDistance);
        Cursor.visible = false;

        OnPlayerMouseClick?.Invoke();
    }

    void OnMouseDrag()
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        float planeDistance;
        draggingPlane.Raycast(cameraRay, out planeDistance);
        transform.position = cameraRay.GetPoint(planeDistance) + offset;
    }

    void OnMouseUp()
    {

        Cursor.visible = true;
        OnPlayerMouseRelease?.Invoke();
    }
}
