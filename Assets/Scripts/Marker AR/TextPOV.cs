using UnityEngine;

public class TextPOV : MonoBehaviour
{
    Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.LookAt(cameraTransform.forward + transform.position);
    }
}
