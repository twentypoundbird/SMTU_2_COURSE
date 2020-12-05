using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    [Range(0,3)]
    public byte MyTypeID = 0;
    [HideInInspector]
    public bool DeleteObject = false;

    private bool WaitChange = false;
    private byte x;
    private byte y;
    private byte z;

    // Start is called before the first frame update
    void returnCoords()
    {
        Debug.Log("Object: " + x + ":" + y + ":" + z);
    }
    void Start()
    {
        StartCoroutine(changeCoords());
        StartCoroutine(removeThisScript());
    }
    private void FixedUpdate()
    {
        if (x != (byte)(this.GetComponent<Transform>().position.x / 10 + 0.1) || y != (byte)(this.GetComponent<Transform>().position.y / 10 + 0.1) || z != (byte)(this.GetComponent<Transform>().position.z / 10 + 0.1))
        {
            x = (byte)(this.GetComponent<Transform>().position.x / 10 + 0.1);
            y = (byte)(this.GetComponent<Transform>().position.y / 10 + 0.1);
            z = (byte)(this.GetComponent<Transform>().position.z / 10 + 0.1);
            WaitChange = true;
        }
    }

    IEnumerator changeCoords()
    {
        while(!DeleteObject)
        { 
            WaitChange = false;
            yield return new WaitUntil(() => WaitChange);
            returnCoords();
        }
    }
    IEnumerator removeThisScript()
    {
        yield return new WaitUntil(() => DeleteObject);
        Destroy(this.GetComponent<ObjectID>());
    }
}
