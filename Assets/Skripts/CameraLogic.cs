using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraLogic : MonoBehaviour
{
    Camera camera;

    private float limitY = 80, zoomMax = MapSizeEditor.sizeZ * 2, zoomMin = 5, zoomSensivity = 0.25f, sensitivity = 1;
    private float x, y;
    private Vector3 offset, target;
    private Vector3 ray_Start_Pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private void Start()
    {
        limitY = Mathf.Abs(limitY);
        if (limitY > 90) limitY = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);

        transform.position = new Vector3(0, 0, 0) + offset;


        camera = gameObject.GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoomSensivity;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoomSensivity;
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = ray = camera.ScreenPointToRay(ray_Start_Pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                zoomMax = hit.distance;
            }
        }

        if (Input.GetMouseButton(2))
        {

            //offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
            offset.z = -Mathf.Abs(zoomMax);

            x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            y += Input.GetAxis("Mouse Y") * sensitivity;
            y = Mathf.Clamp(y, -limitY, limitY);
            transform.localEulerAngles = new Vector3(-y, x, 0); 
            transform.position = transform.localRotation * offset + target;
        }
        if (Input.GetMouseButton(1))
        {
            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

            x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            y += Input.GetAxis("Mouse Y") * sensitivity;
            y = Mathf.Clamp(y, -limitY, limitY);
            transform.localEulerAngles = new Vector3(-y, x, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            gameObject.GetComponent<PostProcessVolume>().enabled = true;
            Debug.Log("enable");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            gameObject.GetComponent<PostProcessVolume>().enabled = false;
            Debug.Log("unenable");
        }

    }
}
