using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraLogic : MonoBehaviour
{
    Camera camera;

    private float limitY = 80, distance = MapSizeEditor.sizeZ * 2, sensitivity = 1;
    private float x, y;
    private Vector3 offset, target;
    private Vector3 ray_Start_Pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private void Start()
    {
        limitY = Mathf.Abs(limitY);
        if (limitY > 90) limitY = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(distance) / 2);

        transform.position = new Vector3(0, 0, 0) + offset;


        camera = gameObject.GetComponent<Camera>();
    }
    private void Update()
    {
        

        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = ray = camera.ScreenPointToRay(ray_Start_Pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                distance = hit.distance;
            }
            else
            {
                target = transform.position + transform.forward * 100;
                distance = 100;
            }
        }

        if (Input.GetMouseButton(2))
        {

            //offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(distance), -Mathf.Abs(zoomMin));
            offset.z = -Mathf.Abs(distance);

            x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            y += Input.GetAxis("Mouse Y") * sensitivity;
            y = Mathf.Clamp(y, -limitY, limitY);
            transform.localEulerAngles = new Vector3(-y, x, 0); 
            transform.position = transform.localRotation * offset + target;
        }
        if (Input.GetMouseButton(1))
        {
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
