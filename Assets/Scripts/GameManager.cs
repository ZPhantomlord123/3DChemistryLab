using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;
    public GameObject flaskA;
    public GameObject flaskB;
    public GameObject testTube;

    private GameObject selectedFlask;
    private bool isTouchEnabled = false;

    private UIController uiController;  // Reference to UIController

    public enum GameState
    {
        IntroToFullView,
        ClickFlaskToSelect,
        SelectFlaskAorB,
        FirstFlaskAndTestTubeAnimation,
        DragTestTubeForPouring,
        FirstPouringAnimation,
        FirstSwipeFlaskToShake,
        FirstStepEndAnimation,
        ClickOtherFlaskToSelect,
        SecondFlaskAndTestTubeAnimation,
        DragTestTubeForPouringRemaining,
        SecondPouringAnimation,
        SecondSwipeFlaskToSelect,
        SecondStepEndAnimation,
    }

    private GameState currentState = GameState.IntroToFullView;

    private void Start()
    {
        cameraSwitcher.InitCameras();
        uiController = UIController.Instance;  // Get reference to UIController singleton
        StartCoroutine(EnableInput(3f));  // Enable input after a delay
        Debug.Log("Game started with IntroToFullView");
    }

    IEnumerator EnableInput(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UpdateGameState(GameState.ClickFlaskToSelect);  // Transition to ClickFlaskToSelect state
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (!isTouchEnabled) return;  // If touch is not enabled, do nothing

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                switch (currentState)
                {
                    case GameState.ClickFlaskToSelect:
                        if (hitObject == flaskA || hitObject == flaskB)
                        {
                            ClickFlasks(hitObject);
                        }
                        break;

                    case GameState.SelectFlaskAorB:
                        // Handle flask A or B selection if needed
                        break;

                    // Other cases for input handling as needed

                    default:
                        break;
                }
            }
        }
    }

    private void ClickFlasks(GameObject flask)
    {
        selectedFlask = flask;
        Debug.Log("Selected flask: " + flask.name);

        cameraSwitcher.ActivateFlaskViewCamera();

        UpdateGameState(GameState.SelectFlaskAorB);
    }

    private void UpdateGameState(GameState nextState)
    {
        currentState = nextState;
        Debug.Log("Current state: " + currentState);

        switch (currentState)
        {
            case GameState.IntroToFullView:
                isTouchEnabled = false;
                break;

            case GameState.ClickFlaskToSelect:
                // Update UI to display context clue for selecting Flask A or B
                uiController.ToggleClickFlaskUI(true,0);
                isTouchEnabled = true;
                break;

            case GameState.SelectFlaskAorB:
                uiController.ToggleClickFlaskUI(false,0);
                uiController.ToggleSelectFlaskUI(true, 1.6f);
                isTouchEnabled = true;
                break;

            case GameState.FirstFlaskAndTestTubeAnimation:
                isTouchEnabled = false;
                break;

            case GameState.DragTestTubeForPouring:
                isTouchEnabled = true;
                break;

            case GameState.FirstPouringAnimation:
                isTouchEnabled = false;
                break;

            case GameState.FirstSwipeFlaskToShake:
                isTouchEnabled = true;
                break;

            case GameState.FirstStepEndAnimation:
                isTouchEnabled = false;
                break;

            case GameState.ClickOtherFlaskToSelect:
                isTouchEnabled = true;
                break;

            case GameState.SecondFlaskAndTestTubeAnimation:
                isTouchEnabled = false;
                break;

            case GameState.DragTestTubeForPouringRemaining:
                isTouchEnabled = true;
                break;

            case GameState.SecondPouringAnimation:
                isTouchEnabled = false;
                break;

            case GameState.SecondSwipeFlaskToSelect:
                isTouchEnabled = true;
                break;

            case GameState.SecondStepEndAnimation:
                isTouchEnabled = false;
                break;

            default:
                isTouchEnabled = false;
                break;
        }
    }
}
