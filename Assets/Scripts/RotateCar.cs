using System;
using UnityEngine;

public class RotateCar : MonoBehaviour
{
    [Tooltip ("Changes orientation of the car")]
    public Vector3 cubeRotation;

    [Tooltip ("Changes the rotation speed of the car")]
    public float rotateSpeed = 3f;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime for smooth rotation, frame-rate independence
        transform.Rotate(cubeRotation * Time.deltaTime * rotateSpeed);
    }
}
