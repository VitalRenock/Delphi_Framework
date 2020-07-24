using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Camera))]
public class CameraRotation_Controller : MonoBehaviour
{
    public Space SpaceRotation = Space.Self;

    [HorizontalGroup("Enable")]
    [BoxGroup("Enable/Can Rotate")] [HideLabel]
    public bool CanRotate = true;
    [BoxGroup("Enable/Rotate on X")] [HideLabel]
    public bool CanRotateOnX = true;
    [BoxGroup("Enable/Rotate on Y")] [HideLabel]
    public bool CanRotateOnY = false;
    [BoxGroup("Enable/Rotate on Z")] [HideLabel]
    public bool CanRotateOnZ = false;

    [BoxGroup("CanRotateOnX/Rotation X")] [ShowIfGroup("CanRotateOnX")]
    public string XRotationAxis = "Mouse Y";
    [BoxGroup("CanRotateOnX/Rotation X")] [ShowIfGroup("CanRotateOnX")]
    public float SpeedOnX = 120f;
    [BoxGroup("CanRotateOnX/Rotation X")] [ShowIfGroup("CanRotateOnX")] [Range(0, 180)]
    public float MaxAngleOnX = 45.0f;

    [BoxGroup("CanRotateOnY/Rotation Y")] [ShowIfGroup("CanRotateOnY")]
    public string YRotationAxis = "Mouse X";
    [BoxGroup("CanRotateOnY/Rotation Y")] [ShowIfGroup("CanRotateOnY")]
    public float SpeedOnY = 120f;
    [BoxGroup("CanRotateOnY/Rotation Y")] [ShowIfGroup("CanRotateOnY")] [Range(0, 180)]
    public float MaxAngleOnY = 45.0f;

    [BoxGroup("CanRotateOnZ/Rotation Z")] [ShowIfGroup("CanRotateOnZ")]
    public string ZRotationAxis = "Mouse ScrollWheel";
    [BoxGroup("CanRotateOnZ/Rotation Z")] [ShowIfGroup("CanRotateOnZ")]
    public float SpeedOnZ = 120f;
    [BoxGroup("CanRotateOnZ/Rotation Z")] [ShowIfGroup("CanRotateOnZ")] [Range(0, 180)]
    public float MaxAngleOnZ = 45.0f;

    Quaternion cameraRotation;

    private void Start()
    {
        if (SpaceRotation == Space.Self)
            cameraRotation = transform.localRotation;
        else
            cameraRotation = transform.rotation;
    }

    private void Update()
    {
        if (!CanRotate)
            return;

        if (CanRotateOnX)
            cameraRotation.x = ComputeSingleRotation(cameraRotation.x, -Input.GetAxis(XRotationAxis), SpeedOnX, MaxAngleOnX);
        if (CanRotateOnY)
            cameraRotation.y = ComputeSingleRotation(cameraRotation.y, Input.GetAxis(YRotationAxis), SpeedOnY, MaxAngleOnY);
        if (CanRotateOnZ)
            cameraRotation.z = ComputeSingleRotation(cameraRotation.z, Input.GetAxis(ZRotationAxis), SpeedOnZ, MaxAngleOnZ);

        if (SpaceRotation == Space.Self)
            transform.localRotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
        else
            transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
    }

    // Clamp un quaternion.
    float ComputeSingleRotation(float targetRotation, float axisValue, float speed, float maxAngle)
    {
        targetRotation += axisValue * speed * Time.deltaTime;
        targetRotation = Mathf.Clamp(targetRotation, -maxAngle, maxAngle);
        return targetRotation;
    }
}