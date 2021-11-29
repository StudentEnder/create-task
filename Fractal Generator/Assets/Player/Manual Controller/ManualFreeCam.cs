using UnityEngine;
using UnityEngine.InputSystem;

public class ManualFreeCam : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Movement")]
    public float maxMoveSpeed = 10f;

    private Vector2 horizontalMovementInput = Vector2.zero;
    private float verticalMovementInput = 0f;

    [Header("Look")]
    public float fieldOfView = 60f;
    public float lookSensitivity = 10f;
    public bool xLookClamping = true; // on by default because strange behavior (axis looking is able to flip after the right turning) occurs when it's off. Is it possible to prevent this while clamping is off?

    private Vector2 lookInput = Vector2.zero;
    private float xRotation = 0f;

    public float tiltSpeed = 10f;
    private float tiltInput = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.fieldOfView = fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Move();
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


        // when xLookClamping is disabled, and all 3 euler rotation axis are traversed beyond 90 (or 180) degrees at once, there is an issue where look controls flip. 
        // This is likely due to euler gimbal-lock related problems, solved by relying more on quaternions.
        if (xLookClamping) xRotation = Mathf.Clamp(xRotation, -90f, 90f);

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
            ((playerCamera.transform.right * horizontalMovementInput.x) + // left and right
            (playerCamera.transform.forward * horizontalMovementInput.y)); // forward and back

        // vertical movement
        transform.position += maxMoveSpeed * Time.deltaTime * verticalMovementInput * playerCamera.transform.up;

        // (seems like a hacky solution: basing movement on camera direction. Likely a way to simplify it to the gameobject transform by applying the x (or y?) rotation the same way gameobject inheritance applies rotation.
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
