using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private Vector3 previousPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { // if button is clicked
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition); // getting mouse position
        }

        if (Input.GetMouseButton(0)) { // if buttion is held down
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition); // distance moved since last frame

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180); 
            cam.transform.Rotate(new Vector3(0, 1, 0), direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -5));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        
    }
}
