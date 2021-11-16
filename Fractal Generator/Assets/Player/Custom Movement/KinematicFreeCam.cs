using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KinematicFreeCam : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private float verticalInputAxis = 0f;
    private float horizontalInputAxis = 0f;

    public float moveForce = 10f;
    public float dragCoefficient = .9f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CaptureInputs()
    {
        horizontalInputAxis = Input.GetAxisRaw("Horizontal");
        verticalInputAxis = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        rigidbody.AddForce(KinematicUtils.DragForce(dragCoefficient, rigidbody.velocity) * Time.fixedDeltaTime);

        rigidbody.AddForce(Vector3.right * horizontalInputAxis * moveForce * Time.fixedDeltaTime);
        rigidbody.AddForce(Vector3.forward * verticalInputAxis * moveForce * Time.fixedDeltaTime);
    }
}
