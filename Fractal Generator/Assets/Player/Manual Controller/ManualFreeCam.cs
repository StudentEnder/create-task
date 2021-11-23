using UnityEngine;
using UnityEngine.InputSystem;

public class ManualFreeCam : MonoBehaviour
{
    public Camera playerCamera;

    public float maxMoveSpeed = 10f;

    private Vector2 horizontalMovementInput = Vector2.zero;

    private float verticalMovementInput = 0f;


    public float lookSensitivity = 10f;


    public bool xLookClamping = true; // on by default because strange behavior (axis looking is able to flip after the right turning) occurs when it's off. Is it possible to prevent this while clamping is off?


    private Vector2 lookInput = Vector2.zero;
    private float xRotation = 0;

    public float tiltSpeed = 10f;
    private float tiltInput = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInputs();
        Look();
        Move();
    }

    private void CaptureInputs()
    {

    }

    /// <summary>
    /// Rotates gameobject according to look inputs
    /// Looking code inspired by lukeskt: https://github.com/lukeskt/InputSystemFirstPersonCharacter/blob/main/InputSystemFirstPersonCharacterScripts/InputSystemFirstPersonCharacter.cs
    /// </summary>
    private void Look()
    {
        float lookX = lookInput.x * lookSensitivity * Time.deltaTime; // look left and right
        float lookY = lookInput.y * lookSensitivity * Time.deltaTime; // look up and down
        float lookZ = tiltInput * tiltSpeed * Time.deltaTime; // tilt leftward and rightward

        xRotation -= lookY;
        if (xLookClamping) xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // separating camera like this causes issues after tilting. The movement direction becomes misaligned from where the camera is pointing. (possible solution: base movement off of camera's transform instead of parent object. This seems hacky though.) 
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.y, transform.localRotation.z);
        transform.Rotate(0f, lookX, -lookZ);
    }

    /// <summary>
    /// Moves gameobject according to translation inputs
    /// Move should be called after Look, as movement is relative to look direction
    /// </summary>
    private void Move()
    {
        // horizontal movement
        transform.position += maxMoveSpeed * Time.deltaTime *
            ((transform.right * horizontalMovementInput.x) + // left and right
            (transform.forward * horizontalMovementInput.y)); // forward and back

        // vertical movement
        transform.position += maxMoveSpeed * Time.deltaTime * verticalMovementInput * transform.up;
    }


    // INPUT MESSAGES

    private void OnHorizontalTranslation(InputValue input)
    {
        horizontalMovementInput = input.Get<Vector2>();
        //Debug.Log("Horizontal Movement: " + horizontalMovementInput);
    }

    private void OnVerticalTranslation(InputValue input)
    {
        verticalMovementInput = input.Get<float>();
        //Debug.Log("Vertical Movement: " + verticalMovementInput);
    }

    private void OnLook(InputValue input)
    {
        lookInput = input.Get<Vector2>();
        //Debug.Log("Look input: " + lookInput);
    }

    private void OnTilt(InputValue input)
    {
        tiltInput = input.Get<float>();
        //Debug.Log("Tilt input: " + tiltInput);
    }

    private void OnOpenMenu()
    {
        //Debug.Log("Menu Opened");
    }
}
