using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeCamController : MonoBehaviour
{

    private float verticalInputAxis = 0f;
    private float horizontalInputAxis = 0f;

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

    private void Move()
    {

    }

    private void CaptureInputs()
    {
        horizontalInputAxis = Input.GetAxisRaw("Vertical");
        verticalInputAxis = Input.GetAxisRaw("Horizontal");
    }
}
