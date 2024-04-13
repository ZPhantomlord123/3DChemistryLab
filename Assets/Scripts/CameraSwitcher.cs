using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    // Enum to define different camera types
    public enum CameraType
    {
        IntroView,
        FullView,
        FlaskView,
        TestTubeView
    }

    // The currently active camera type, serialized for debugging in the Inspector
    [SerializeField] private CameraType activeCameraType;

    // Serialized references to the Cinemachine virtual cameras
    [SerializeField] private CinemachineVirtualCamera introViewCamera;
    [SerializeField] private CinemachineVirtualCamera fullViewCamera;
    [SerializeField] private CinemachineVirtualCamera flaskViewCamera;
    [SerializeField] private CinemachineVirtualCamera testTubeViewCamera;

    // This method is called whenever the script's Inspector values are changed
    private void OnValidate()
    {
        // Activate the camera type specified in the Inspector
        ActivateCamera(activeCameraType);
    }

    // This method is called when the script starts
    private void Start()
    {
        // Coroutine to initialize the cameras with a delay for smoother transition
        StartCoroutine(InitializeCameras());
    }

    // Coroutine to initialize the cameras
    IEnumerator InitializeCameras()
    {
        // Start with IntroView camera if not null
        if (introViewCamera != null)
        {
            ActivateCamera(CameraType.IntroView);

            // Wait for 1 second before switching to FullView
            yield return new WaitForSeconds(1f);
        }

        // Switch to FullView camera if not null
        if (fullViewCamera != null)
        {
            ActivateCamera(CameraType.FullView);
        }
    }

    // Public method to activate IntroView camera (can be called from other scripts)
    public void ActivateIntroViewCamera()
    {
        if (introViewCamera != null)
        {
            ActivateCamera(CameraType.IntroView);
        }
    }

    // Public method to activate FullView camera (can be called from other scripts)
    public void ActivateFullViewCamera()
    {
        if (fullViewCamera != null)
        {
            ActivateCamera(CameraType.FullView);
        }
    }

    // Public method to activate FlaskView camera (can be called from other scripts)
    public void ActivateFlaskViewCamera()
    {
        if (flaskViewCamera != null)
        {
            ActivateCamera(CameraType.FlaskView);
        }
    }

    // Public method to activate TestTubeView camera (can be called from other scripts)
    public void ActivateTestTubeViewCamera()
    {
        if (testTubeViewCamera != null)
        {
            ActivateCamera(CameraType.TestTubeView);
        }
    }

    // Method to activate the specified camera type
    public void ActivateCamera(CameraType cameraType)
    {
        // Reset all camera priorities to 0 to ensure only one camera is active
        SetAllCamerasPriority(0);

        // Activate the specified camera type by setting its priority to 10 (higher priority)
        switch (cameraType)
        {
            case CameraType.IntroView:
                if (introViewCamera != null)
                {
                    introViewCamera.Priority = 10;
                }
                break;
            case CameraType.FullView:
                if (fullViewCamera != null)
                {
                    fullViewCamera.Priority = 10;
                }
                break;
            case CameraType.FlaskView:
                if (flaskViewCamera != null)
                {
                    flaskViewCamera.Priority = 10;
                }
                break;
            case CameraType.TestTubeView:
                if (testTubeViewCamera != null)
                {
                    testTubeViewCamera.Priority = 10;
                }
                break;
        }

        // Update the active camera type
        activeCameraType = cameraType;
    }

    // Helper method to set all camera priorities to a specified value
    private void SetAllCamerasPriority(int priority)
    {
        if (introViewCamera != null)
        {
            introViewCamera.Priority = priority;
        }
        if (fullViewCamera != null)
        {
            fullViewCamera.Priority = priority;
        }
        if (flaskViewCamera != null)
        {
            flaskViewCamera.Priority = priority;
        }
        if (testTubeViewCamera != null)
        {
            testTubeViewCamera.Priority = priority;
        }
    }
}
