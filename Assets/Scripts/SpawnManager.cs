using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    InputAction spaceBarAction;
    public GameObject cubePrefab;
    int cubeCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spaceBarAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        if (spaceBarAction.WasPressedThisFrame())
        {
            //Debug.Log("Spacebar was pressed");
            InstantiateCube();
        }
    }

    void InstantiateCube()
    {
        Vector3 position = new Vector3(2, 0, 3 * cubeCount);
        GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);

        RotateCube rotateCube = cube.GetComponent<RotateCube>();
        cube.name = "Cube_" + cubeCount;

        Debug.Log($"{cube.name} was create a position {position}");

        if (rotateCube)
        {
            rotateCube.rotationSpeed = 5f;
            rotateCube.cubeRotation = new Vector3(4, 5, 6);
        }

        cubeCount++;
    }
}
