using UnityEngine;

public class CameraControllerTemp : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float rotationSpeed = 5f;

    private float currentRotationAngle = 0f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired rotation angle based on player input
        float desiredRotationAngle = target.eulerAngles.y;
        float rotationAngle = Mathf.LerpAngle(currentRotationAngle, desiredRotationAngle, rotationSpeed * Time.deltaTime);

        // Convert the rotation angle to a quaternion
        Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);

        // Calculate the desired position of the camera based on rotation and offset
        Vector3 desiredPosition = target.position - (rotation * offset);

        // Smoothly move the camera towards the desired position and update rotation
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;

        // Update the current rotation angle
        currentRotationAngle = rotationAngle;
    }
}
