using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [Tooltip("Rotates Cube")]
    public Vector3 cubeRotation;
    [Tooltip("Cube Rotation Speed")]
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(cubeRotation * Time.deltaTime * rotationSpeed);
    }
}
