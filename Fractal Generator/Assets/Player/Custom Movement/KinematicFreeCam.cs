using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class KinematicFreeCam : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField] private Vector3 inputDirection = Vector3.zero;

    private float moveForce;
    public float maxSpeed = 1f;

    public float dragConstant = .5f;
    public float forceConstant = 10f;
    public float ratioConstant = 196f;
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
        Move();
    }

    private void CaptureInputs()
    {
        
    }

    private void Move()
    {
        moveForce = forceConstant * maxSpeed;
        rigidbody.drag = dragConstant * ratioConstant;
        rigidbody.AddForce(inputDirection * moveForce * Time.fixedDeltaTime);
    }
}
