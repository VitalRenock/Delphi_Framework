using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[RequireComponent(typeof(CharacterController))]
public class FPS_Controller : MonoBehaviour
{
    public bool CanMove = true;

    [TabGroup("Axis")]
    public string TranslateAxis = "Vertical";
    [TabGroup("Axis")]
    public string StrafeAxis = "Horizontal";
    [TabGroup("Axis")]
    public string YRotationAxis = "Mouse X";
    [TabGroup("Axis")]
    public KeyCode RunKey = KeyCode.LeftShift;
    [TabGroup("Axis")]
    public KeyCode JumpKey = KeyCode.Space;

    [TabGroup("Speeds")]
    public float WalkingSpeed = 7.5f;
    [TabGroup("Speeds")]
    public float RunningSpeed = 11.5f;
    [TabGroup("Speeds")]
    public float RotationSpeed = 2.0f;
    [TabGroup("Speeds")]
    public float JumpSpeed = 8.0f;
    [TabGroup("Speeds")]
    public float Gravity = 20.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;


    void Start() => characterController = GetComponent<CharacterController>();

    void Update()
    {
        // Calculate player direction for translation.
        ComputeTranslateDirection();

        // Player translation.
        characterController.Move(moveDirection * Time.deltaTime);

        // Player rotation.
        if (CanMove)
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * RotationSpeed, 0);
    }

    void ComputeTranslateDirection()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(RunKey);

        float curSpeedX = CanMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis(TranslateAxis) : 0;
        float curSpeedY = CanMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis(StrafeAxis) : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jump
        if (Input.GetKey(JumpKey) && CanMove && characterController.isGrounded)
            moveDirection.y = JumpSpeed;
        else
            moveDirection.y = movementDirectionY;

        // Apply gravity
        if (!characterController.isGrounded)
            moveDirection.y -= Gravity * Time.deltaTime;
    }
}