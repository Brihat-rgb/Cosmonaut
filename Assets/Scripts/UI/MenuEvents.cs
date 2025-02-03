using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class MenuEvents : MonoBehaviour
{
    UIDocument document;
    Button capsuleBtn;
    Button cubeBtn;
    Button cylinderBtn;
    Button sphereBtn;


    public GameObject currentPrefab;
    public GameObject capsulePrefab;
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;
    public GameObject spherePrefab;

    List<Button> menuBtns = new List<Button>();
    AudioSource audioSource;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        capsuleBtn = document.rootVisualElement.Q("Capsule") as Button;
        cubeBtn = document.rootVisualElement.Q("Cube") as Button;
        cylinderBtn = document.rootVisualElement.Q("Cylinder") as Button;
        sphereBtn = document.rootVisualElement.Q("Sphere") as Button;

        menuBtns = document.rootVisualElement.Query<Button>().ToList();
        audioSource = GetComponent<AudioSource>();

        for(int i = 0;menuBtns.Count > i;i++)
        {
            menuBtns[i].RegisterCallback<ClickEvent>(PlaySwapAudio);
        }

        //Register Btn Clikc event on the capsule btn
        capsuleBtn.RegisterCallback<ClickEvent>(OnCapsuleClick);
        cubeBtn.RegisterCallback<ClickEvent>(OnCubeClick);
        cylinderBtn.RegisterCallback<ClickEvent>(OnCylinderClick);
        sphereBtn.RegisterCallback<ClickEvent>(OnSphereClick);

    }

    void PlaySwapAudio(ClickEvent evnt)
    {
        audioSource.Play();
    }
    private void OnDisable()
    {
        //Good practice to unregister function call backs on button (event listeners)
        capsuleBtn.UnregisterCallback<ClickEvent>(OnCapsuleClick);
        cubeBtn.UnregisterCallback<ClickEvent>(OnCubeClick);
        cylinderBtn.UnregisterCallback<ClickEvent>(OnCylinderClick);
        sphereBtn.UnregisterCallback<ClickEvent>(OnSphereClick);

        for (int i = 0; menuBtns.Count > i; i++)
        {
            menuBtns[i].UnregisterCallback<ClickEvent>(PlaySwapAudio);
        }
    }

    void OnCapsuleClick(ClickEvent evnt)
    {
        ReplacePrefab(capsulePrefab, "Capsule");
        Debug.Log("Capsule Clicked!");
    }

    void OnCubeClick(ClickEvent evnt)
    {
        ReplacePrefab(cubePrefab, "Cube");
        Debug.Log("Cube Clicked!");
    }

    void OnCylinderClick(ClickEvent evnt)
    {
        ReplacePrefab(cylinderPrefab, "Cylinder");
        Debug.Log("Cylinder Clicked!");
    }

    void OnSphereClick(ClickEvent evnt)
    {
        ReplacePrefab(spherePrefab, "Sphere");
        Debug.Log("Sphere Clicked!");
    }

    void ReplacePrefab(GameObject newPrefab, string type)
    {
        if (currentPrefab != null)
        {
            Vector3 currentPosition = currentPrefab.transform.position;
            Quaternion currentRotation = currentPrefab.transform.rotation;

            Destroy(currentPrefab );
            currentPrefab = Instantiate(newPrefab, currentPosition, currentRotation);

            Debug.Log($"{type} Was Created at ${currentPosition} position with rotation {currentRotation}.");
        }
        else
        {
            Debug.LogWarning("Couldnt Load Prefab");
        }
    }
}
