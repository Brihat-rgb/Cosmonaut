using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Refactor : MonoBehaviour
{
    UIDocument document;
    Button capsuleButton, cylinderButton, sphereButton,
     cubeButton;
    public GameObject currentPrefab, capsulePrefab, cylinderPrefab, spherePrefab, cubePrefab;
    List<Button> menuButtons = new List<Button>();
    AudioSource audioSource;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        // Button Instantiaton
        capsuleButton = document.rootVisualElement.Q("Capsule") as Button;
        cylinderButton = document.rootVisualElement.Q("Cylinder") as Button;
        sphereButton = document.rootVisualElement.Q("Sphere") as Button;
        cubeButton = document.rootVisualElement.Q("Cube") as Button;

        // Event Registration
        capsuleButton.RegisterCallback<ClickEvent>(evt => OnButtonClick(evt, capsulePrefab, capsuleButton));
        cylinderButton.RegisterCallback<ClickEvent>(evt => OnButtonClick(evt, cylinderPrefab, cylinderButton));
        sphereButton.RegisterCallback<ClickEvent>(evt => OnButtonClick(evt, spherePrefab, sphereButton));
        cubeButton.RegisterCallback<ClickEvent>(evt => OnButtonClick(evt, cubePrefab, cubeButton));


        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnDisable()
    {
        capsuleButton.UnregisterCallback<ClickEvent>(evt => OnButtonClick(evt, capsulePrefab, capsuleButton));
        cylinderButton.UnregisterCallback<ClickEvent>(evt => OnButtonClick(evt, cylinderPrefab, cylinderButton));
        sphereButton.UnregisterCallback<ClickEvent>(evt => OnButtonClick(evt, spherePrefab, sphereButton));
        cubeButton.UnregisterCallback<ClickEvent>(evt => OnButtonClick(evt, cubePrefab, cubeButton));

    }

    public void OnButtonClick(ClickEvent evt, GameObject prefab, Button button)
    {
        if (button.ClassListContains("active"))
        {
            Debug.Log("Button");
            Debug.Log(button);
            return;
        }
        SetActiveButton(button);
        ReplacePrefab(prefab);
        audioSource.Play();

    }

    public void SetActiveButton(Button button)
    {
        menuButtons.ForEach(menuButton => menuButton.RemoveFromClassList("active"));
        button.AddToClassList("active");
    }

    void ReplacePrefab(GameObject newPrefab)
    {
        if (currentPrefab != null)
        {
            Vector3 currentPosition = currentPrefab.transform.position;
            Quaternion currentRotation = currentPrefab.transform.rotation;

            Destroy(currentPrefab);

            currentPrefab = Instantiate(newPrefab, currentPosition, currentRotation);

            Debug.Log($"Capsule was created a ${currentPosition} position with rotation {currentPosition}");
        }
        else
        {
            Debug.LogWarning("Couldn't find current prefab");
        }
    }



}
