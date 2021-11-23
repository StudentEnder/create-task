using UnityEngine;
using UnityEngine.InputSystem;

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


    // INPUT MESSAGES

    private void OnHorizontalTranslation(InputValue input)
    {
        Vector2 horizontalMovementInput = input.Get<Vector2>();
        //Debug.Log("Horizontal Movement: " + horizontalMovementInput);
    }

    private void OnVerticalTranslation(InputValue input)
    {
        float verticalMovementInput = input.Get<float>();
        //Debug.Log("Vertical Movement: " + verticalMovementInput);
    }

    private void OnLook(InputValue input)
    {
        Vector2 lookInput = input.Get<Vector2>();
        //Debug.Log("Look input: " + lookInput);
    }

    private void OnTilt(InputValue input)
    {
        float tiltInput = input.Get<float>();
        //Debug.Log("Tilt input: " + tiltInput);
    }

    private void OnOpenMenu()
    {
        //Debug.Log("Menu Opened");
    }
}
