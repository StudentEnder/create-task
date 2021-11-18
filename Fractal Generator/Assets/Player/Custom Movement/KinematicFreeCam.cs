using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class KinematicFreeCam : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed;

    //private float verticalInputAxis = 0f;
    //private float horizontalInputAxis = 0f;
    private Vector3 inputAxes = Vector3.zero;

    public float moveForce = 10f;
    //public float dragCoefficient = .9f;
    //public float dragMinimum = 2f;


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
        velocity = rigidbody.velocity;
        speed = rigidbody.velocity.magnitude;

        Move();
    }

    private void CaptureInputs()
    {
        //horizontalInputAxis = Input.GetAxisRaw("Horizontal");
        //verticalInputAxis = Input.GetAxisRaw("Vertical");
        //inputAxes.x = moveAction
        
    }

    private void Move()
    {
        // rigidbody.AddForce(KinematicUtils.DragForce(dragCoefficient, dragMinimum, rigidbody.velocity) * Time.fixedDeltaTime);

        //rigidbody.AddForce(Vector3.right * horizontalInputAxis * moveForce * Time.fixedDeltaTime);
        //rigidbody.AddForce(Vector3.forward * verticalInputAxis * moveForce * Time.fixedDeltaTime);

        rigidbody.AddForce(inputAxes * moveForce * Time.fixedDeltaTime);
    }
}
