using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpiral : MonoBehaviour
{
    public Quaternion rotation; 
    public Transform topPoint;
    public Transform bottomPoint;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 top = topPoint.position;
        Vector3 bottom = bottomPoint.position;

        Instantiate(gameObject, top, rotation, transform);

        Debug.Log("created first component of spiral");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
