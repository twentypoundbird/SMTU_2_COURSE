using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraLogic : MonoBehaviour
{
    Camera camera;

    private float limitY = 80, distance = MapSizeEditor.countZ * MapSizeEditor.step , sensitivity = 1f;
    private float x, y;
    private Vector3 offset = new Vector3(0,0,0), target;
    private Vector3 ray_Start_Pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private void Start()
    {
        limitY = Mathf.Abs(limitY);
        if (limitY > 90) limitY = 90;
        offset = new Vector3(0, 0, -distance);

        transform.position = new Vector3((MapSizeEditor.step * MapSizeEditor.countX / 2), (MapSizeEditor.step * MapSizeEditor.countY / 2), -(MapSizeEditor.step * MapSizeEditor.countZ));

        camera = gameObject.GetComponent<Camera>();
    }
    private void Update()
    {

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            transform.position += transform.right* Input.GetAxis("Horizontal")*Time.deltaTime * 100;
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 100;
        }
        if (Mathf.Abs(Input.GetAxis("Jump")) > 0)
        {
            transform.position += transform.up * Input.GetAxis("Jump") * Time.deltaTime * 100;
        }
        if (Mathf.Abs(Input.GetAxis("Ctrl")) > 0)
        {
            transform.position += transform.up * -1 * Input.GetAxis("Ctrl") * Time.deltaTime * 100;
        }
        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit,1000 * MapSizeEditor.step))
            {
                if (target != hit.point)
                {
                    target = hit.point;
                    distance = Vector3.Distance(hit.point, transform.position);
                }
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
            offset.z = -distance;

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
