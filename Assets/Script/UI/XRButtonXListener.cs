using UnityEngine;
using UnityEngine.XR;

public class XRButtonXListener : MonoBehaviour
{
    private InputDevice leftController;
    private bool wasPressedLastFrame = false;
    [SerializeField] private PauseManager pauseManager;
    void Start()
    {
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        if (!leftController.isValid)
        {
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            return;
        }

        bool isPressed;
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed))
        {
            // Détection du "press" (appui unique)
            if (isPressed && !wasPressedLastFrame)
            {
                OnXButtonPressed();
            }

            wasPressedLastFrame = isPressed;
        }
    }

    void OnXButtonPressed()
    {
        Debug.Log("Bouton X appuyé !");
        if (pauseManager.IsPause())
        {
            pauseManager.StopPause();
        }
        else
        {
            pauseManager.SetPause();
        }
            
    }
}
