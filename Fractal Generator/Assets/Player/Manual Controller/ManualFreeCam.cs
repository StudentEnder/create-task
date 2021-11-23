using UnityEngine;
using UnityEngine.InputSystem;

public class ManualFreeCam : MonoBehaviour
{

    public float maxMoveSpeed = 10f;

    private Vector2 horizontalMovementInput = Vector2.zero;

    private float verticalMovementInput = 0f;


    public float lookSensitivity = 10f;
    private Vector2 lookInput = Vector2.zero;

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

        transform.Rotate(-lookY, lookX, -lookZ);
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
