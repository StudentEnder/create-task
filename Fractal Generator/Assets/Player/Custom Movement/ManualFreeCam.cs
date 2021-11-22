using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualFreeCam : MonoBehaviour
{

    private float horizontalInputAxis = 0f;
    private float verticalInputAxis = 0f;

    public float maxMoveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInputs();
        Move();
    }

    private void CaptureInputs()
    {

    }

    private void Move()
    {
        transform.position += maxMoveSpeed * Time.deltaTime * verticalInputAxis * transform.forward;
        transform.position += horizontalInputAxis * maxMoveSpeed * Time.deltaTime * transform.right;
    }
}
