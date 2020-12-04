using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    [Range(0,3)]
    public byte MyTypeID = 0;
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
        if (x != this.GetComponent<Transform>().position.x)
        {
            if (y != this.GetComponent<Transform>().position.y)
            {
                if (z != this.GetComponent<Transform>().position.z)
                {
                    x = (byte)(this.GetComponent<Transform>().position.x / 10);
                    y = (byte)(this.GetComponent<Transform>().position.y / 10);
                    z = (byte)(this.GetComponent<Transform>().position.z / 10);
                    WaitChange = true;
                }
            }
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
