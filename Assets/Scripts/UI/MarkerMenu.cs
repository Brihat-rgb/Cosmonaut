using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MarkerMenu : MonoBehaviour
{
    UIDocument document;
    Button leftButton, rightButton, infoButton;
    GameObject instantiatedObject;
    bool isRotating = false;

    bool isVisible;
    Canvas popupCanvas;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        leftButton = document.rootVisualElement.Q("Left") as Button;
        rightButton = document.rootVisualElement.Q("Right") as Button;
        infoButton = document.rootVisualElement.Q("Infor") as Button;


        infoButton.RegisterCallback<ClickEvent>(ToggleCanvas);
        leftButton.RegisterCallback<ClickEvent>(RotateLeft);
    }

    void ToggleCanvas(ClickEvent evt)
    {
        if(isVisible)
        {
            popupCanvas.gameObject.SetActive(false);
            isVisible = false;
        }
        else
        {
            popupCanvas.gameObject.SetActive(true);
            isVisible = true;
        }
    }

    private void Update()
    {
        if (!instantiatedObject)
        {
            instantiatedObject = GameObject.FindGameObjectWithTag("Instantiated");
            if (instantiatedObject)
            {
                popupCanvas = instantiatedObject.GetComponentInChildren<Canvas>(true);
            }
        }
    }

    void RotateLeft(ClickEvent evt)

    {
        if (instantiatedObject && !isRotating)
        {
            //instantiatedObject.transform.Rotate(new Vector3(0, -25, 0));
            //instantiatedObject.transform.Rotate(new Vector3(0, -25, 0));

            //Coroutine for smooth rotation
            StartCoroutine(SmoothRotate(15));
        }
        else
        {
            Debug.LogWarning("No model found");
        }
    }

    IEnumerator SmoothRotate(float angle)
    {
        isRotating = true;
        Quaternion initialRotation = instantiatedObject.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, angle, 0);

        //Time to complete rotation
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration) 
        { 
            elapsedTime += Time.deltaTime;
            instantiatedObject.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            yield return null;
        }

        //Snap he model toward final rotation
        instantiatedObject.transform.rotation = targetRotation;
        isRotating = false;
    }
}