using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MarkerMenu : MonoBehaviour
{
    UIDocument document;
    Button leftButton, rightButton, infoButton;
    GameObject instantiatedObject;
    bool isRotating = false;

    // boolean to control world space canvas

    Canvas popUpCanvas;
    bool isVisible;

    // for continously rotating model
    bool rotatingLeft, rotatingRight;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        leftButton = document.rootVisualElement.Q("Left") as Button;
        rightButton = document.rootVisualElement.Q("Right") as Button;
        infoButton = document.rootVisualElement.Q("Info") as Button;

        //leftButton.RegisterCallback<ClickEvent>(RotateLeft);
        //rightButton.RegisterCallback<ClickEvent>(RotateRight);

        leftButton.RegisterCallback<PointerDownEvent>(RotateLeftContinously, TrickleDown.TrickleDown);
        rightButton.RegisterCallback<PointerDownEvent>(RotateRightContinously, TrickleDown.TrickleDown);

        leftButton.RegisterCallback<PointerUpEvent>(StopRotation, TrickleDown.TrickleDown);
        rightButton.RegisterCallback<PointerUpEvent>(StopRotation, TrickleDown.TrickleDown);

        infoButton.RegisterCallback<ClickEvent>(ToggleCanvas);

    }

    void ToggleCanvas(ClickEvent evt)
    {
        if (isVisible)
        {
            popUpCanvas.gameObject.SetActive(false);
            isVisible = false;
        }
        else
        {
            popUpCanvas.gameObject.SetActive(true);
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
                popUpCanvas = instantiatedObject.GetComponentInChildren<Canvas>(true);
            }

        }

        if (instantiatedObject)
        {
            if (rotatingLeft)
            {
                // Vector3.down -> (0,-1,0)
                instantiatedObject.transform.Rotate(Vector3.down * 30 * Time.deltaTime);
            }

            if (rotatingRight)
            {
                // Vector3.up -> (0,1,0)
                instantiatedObject.transform.Rotate(Vector3.up * 30 * Time.deltaTime);
            }
        }
    }

    void RotateLeftContinously(PointerDownEvent evt)
    {
        rotatingLeft = true;
    }

    void RotateRightContinously(PointerDownEvent evt)
    {
        rotatingRight = true;
    }

    void StopRotation(PointerUpEvent evt)
    {
        rotatingRight = false;
        rotatingLeft = false;
    }


    void RotateLeft(ClickEvent evt)
    {
        if (instantiatedObject && !isRotating)
        {
            // Coroutine for smooth transition;
            StartCoroutine(SmoothRotate(15f));
        }
        else
        {
            Debug.LogWarning("No model found");
        }
    }

    void RotateRight(ClickEvent evt)
    {
        if (instantiatedObject && !isRotating)
        {
            // Coroutine for smooth transition;
            StartCoroutine(SmoothRotate(-15f));
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

        // Time to complete the rotation
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            instantiatedObject.transform.rotation =
                Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            yield return null;
        }

        // Snap the model toward the final intended rotation
        instantiatedObject.transform.rotation = targetRotation;
        isRotating = false;
    }
}
