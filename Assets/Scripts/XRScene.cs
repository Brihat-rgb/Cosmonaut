using UnityEngine;
using UnityEngine.XR.Management;

public class XRScene : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("XR stopped and deinitialized.");

        }
    }
}
