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
        StartCoroutine(EnableInput(1f));
        Debug.Log("Game started with IntroToFullView");
    }

    IEnumerator EnableInput(float secs)
    {
        yield return new WaitForSeconds(secs);
        isTouchEnabled = true;
        UpdateGameState(GameState.ClickFlaskToSelect);
    }

    private void Update()
    {
        // No input handling here
    }

    private void SelectConicalFlask(GameObject flask)
    {
        if (currentState != GameState.ClickFlaskToSelect)
        {
            Debug.LogWarning("Cannot select flask. Current state is not ClickFlaskToSelect.");
            return;
        }

        if (flask == flaskA || flask == flaskB)
        {
            selectedFlask = flask;
            cameraSwitcher.ActivateFlaskViewCamera();
            isTouchEnabled = false;
            UpdateGameState(GameState.FirstFlaskAndTestTubeAnimation);
        }
        else
        {
            Debug.LogWarning("Invalid selection. Click on one of the two flasks.");
        }
    }

    private void SelectFlaskAorB(GameObject flask)
    {
        if (currentState != GameState.SelectFlaskAorB)
        {
            Debug.LogWarning("Cannot select flask. Current state is not SelectFlaskAorB.");
            return;
        }

        if (flask == flaskA || flask == flaskB)
        {
            selectedFlask = flask;
            isTouchEnabled = false;
            UpdateGameState(GameState.DragTestTubeForPouring);
        }
        else
        {
            Debug.LogWarning("Invalid selection. Click on one of the two flasks.");
        }
    }

    private void SwipeFlaskToShake(GameObject flask)
    {
        if (currentState != GameState.FirstSwipeFlaskToShake)
        {
            Debug.LogWarning("Cannot swipe flask. Current state is not FirstSwipeFlaskToShake.");
            return;
        }

        if (flask == selectedFlask)
        {
            // Implement swipe logic here (Not implemented here for brevity)

            // After swipe, proceed to next step
            UpdateGameState(GameState.FirstStepEndAnimation);
        }
        else
        {
            Debug.LogWarning("Invalid flask selection for swipe.");
        }
    }

    private void SelectSecondFlask(GameObject flask)
    {
        if (currentState != GameState.SecondSwipeFlaskToSelect)
        {
            Debug.LogWarning("Cannot select flask. Current state is not SecondSwipeFlaskToSelect.");
            return;
        }

        if (flask == flaskA || flask == flaskB)
        {
            selectedFlask = flask;
            isTouchEnabled = false;
            UpdateGameState(GameState.SecondFlaskAndTestTubeAnimation);
        }
        else
        {
            Debug.LogWarning("Invalid selection. Click on one of the two flasks.");
        }
    }

    private void UpdateGameState(GameState nextState)
    {
        currentState = nextState;
        Debug.Log("Current state: " + currentState);

        // Handle input based on the new game state
        switch (currentState)
        {
            case GameState.ClickFlaskToSelect:
                // Enable touch input to select flask
                isTouchEnabled = true;
                break;

            case GameState.SelectFlaskAorB:
                // Enable touch input to select flask A or B
                isTouchEnabled = true;
                break;

            case GameState.FirstSwipeFlaskToShake:
                // Enable touch input for swiping the flask
                isTouchEnabled = true;
                break;

            case GameState.SecondSwipeFlaskToSelect:
                // Enable touch input to select second flask
                isTouchEnabled = true;
                break;

            default:
                // Disable touch input for other states
                isTouchEnabled = false;
                break;
        }
    }
}
