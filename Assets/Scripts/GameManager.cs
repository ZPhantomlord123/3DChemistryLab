using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;
    public Animator characterAnimator;
    public ObjectAnimator animator;
    public GameObject flaskA;
    public GameObject flaskB;
    public GameObject testTube;

    private GameObject selectedFlask;
    public bool isTouchEnabled = false;

    private UIController uiController;  // Reference to UIController

    public enum GameState
    {
        IntroToFullView,
        ClickFlaskToSelect,
        SelectFlaskAorB,
        FirstFlaskAndTestTubeAnimation,
        FirstClickFlaskToShake,
        FirstFlaskShakeAnimation,
        FirstStepEndAnimation,
        ClickOtherFlaskToSelect,
        SecondFlaskAndTestTubeAnimation,
        SecondClickFlaskToShake,
        SecondClickFlaskToShakeAnimation,
        SecondStepEndAnimation,
    }

    private GameState currentState = GameState.IntroToFullView;

    private void Start()
    {
        animator.PlayAnimation("None");
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
                            AudioManager.instance.PlaySoundEffect();
                            ClickFlasks(hitObject);
                        }
                        else
                        {
                            // Handle invalid selection (optional)
                            Debug.Log("Invalid selection. Please click correctly");
                        }
                        break;

                    case GameState.SelectFlaskAorB:
                        if (hitObject == flaskA || hitObject == flaskB)
                        {
                            AudioManager.instance.PlaySoundEffect();
                            PlayFirstFlaskAndTesTubeAnimation(hitObject);
                        }
                        else
                        {
                            // Handle invalid selection (optional)
                            Debug.Log("Invalid selection. Please select Flask A or B.");
                        }
                        break;

                    case GameState.FirstClickFlaskToShake:
                        if (hitObject == selectedFlask)
                        {
                            AudioManager.instance.PlaySoundEffect();
                            UpdateGameState(GameState.FirstFlaskShakeAnimation);
                        }
                        else
                        {
                            // Handle invalid selection (optional)
                            Debug.Log("Invalid selection. Please click correctly");
                        }
                        break;

                    case GameState.ClickOtherFlaskToSelect:
                        if (hitObject != selectedFlask && (hitObject == flaskA || hitObject == flaskB))
                        {
                            AudioManager.instance.PlaySoundEffect();
                            selectedFlask = hitObject;
                            UpdateGameState(GameState.SecondFlaskAndTestTubeAnimation);
                        }
                        else
                        {
                            // Handle invalid selection (optional)
                            Debug.Log("Invalid selection. Please click correctly");
                        }
                        break;

                    case GameState.SecondClickFlaskToShake:
                        if (hitObject == selectedFlask)
                        {
                            AudioManager.instance.PlaySoundEffect();
                            UpdateGameState(GameState.SecondClickFlaskToShakeAnimation);
                        }
                        else
                        {
                            // Handle invalid selection (optional)
                            Debug.Log("Invalid selection. Please click correctly");
                        }
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

    private void PlayFirstFlaskAndTesTubeAnimation(GameObject flask)
    {
        selectedFlask = flask;
        Debug.Log("Selected flask: " + flask.name);

        cameraSwitcher.ActivateFullViewCamera();

        UpdateGameState(GameState.FirstFlaskAndTestTubeAnimation);
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
                uiController.ToggleClickFlaskUI(true, 0);
                isTouchEnabled = true;
                break;

            case GameState.SelectFlaskAorB:
                uiController.ToggleClickFlaskUI(false, 0);
                uiController.ToggleSelectFlaskUI(true, 1.6f);
                isTouchEnabled = true;
                break;

            case GameState.FirstFlaskAndTestTubeAnimation:
                uiController.ToggleSelectFlaskUI(false, 0);
                isTouchEnabled = false;
                // Play the animation based on selected flask
                string animationName = (selectedFlask == flaskA) ? "SelectFlaskAAndTestTube" : "SelectFlaskBAndTestTube";
                float animationLength = animator.PlayAnimation(animationName);

                // Transition to next state after animation length
                StartCoroutine(TransitionAfterAnimation(GameState.FirstClickFlaskToShake, animationLength));
                break;

            case GameState.FirstClickFlaskToShake:
                uiController.ToggleClickStirUI(true, 0);
                isTouchEnabled = true;
                break;

            case GameState.FirstFlaskShakeAnimation:
                uiController.ToggleClickStirUI(false, 0);
                isTouchEnabled = false;
                // Play the shake animation based on selected flask
                string shakeAnimation = (selectedFlask == flaskA) ? "FlaskAShake" : "FlaskBShake";
                float shakeAnimationLength = animator.PlayAnimation(shakeAnimation);
                characterAnimator.Play("ExcitedAnim");

                // Transition to next state after animation length
                StartCoroutine(TransitionAfterAnimation(GameState.FirstStepEndAnimation, shakeAnimationLength));
                break;

            case GameState.FirstStepEndAnimation:
                isTouchEnabled = false;

                // Play the end animation based on the selected flask
                string endAnimation = (selectedFlask == flaskA) ? "FlaskAEndAnimation" : "FlaskBEndAnimation";
                float endAnimationLength = animator.PlayAnimation(endAnimation);
                characterAnimator.Play("Idle");

                // Transition to next state after animation length
                StartCoroutine(TransitionAfterAnimation(GameState.ClickOtherFlaskToSelect, endAnimationLength));
                break;

            case GameState.ClickOtherFlaskToSelect:
                isTouchEnabled = true;
                uiController.ToggleSelectSecondFlaskUI(true, 0);
                break;
            case GameState.SecondFlaskAndTestTubeAnimation:
                uiController.ToggleSelectSecondFlaskUI(false, 0);
                isTouchEnabled = false;
                // Play the animation based on selected flask
                string secondAnimationName = (selectedFlask == flaskA) ? "SecondSelectFlaskAAndTestTube" : "SecondSelectFlaskBAndTestTube";
                float secondAnimationLength = animator.PlayAnimation(secondAnimationName);

                // Transition to next state after animation length
                StartCoroutine(TransitionAfterAnimation(GameState.SecondClickFlaskToShake, secondAnimationLength));
                break;

            case GameState.SecondClickFlaskToShake:
                uiController.ToggleClickStirUI(true, 0);
                isTouchEnabled = true;
                break;

            case GameState.SecondClickFlaskToShakeAnimation:
                uiController.ToggleClickStirUI(false, 0);
                isTouchEnabled = false;
                // Play the shake animation based on selected flask
                string secondShakeAnimation = (selectedFlask == flaskA) ? "SecondFlaskAShake" : "SecondFlaskBShake";
                float secondShakeAnimationLength = animator.PlayAnimation(secondShakeAnimation);
                characterAnimator.Play("IrritatedAnim");

                // Transition to next state after animation length
                StartCoroutine(TransitionAfterAnimation(GameState.SecondStepEndAnimation, secondShakeAnimationLength));
                break;

            case GameState.SecondStepEndAnimation:
                isTouchEnabled = false;
                uiController.ToggleReplayUI(true, 2f);
                break;

            // Other cases for state transitions as needed

            default:
                break;
        }
    }



    IEnumerator TransitionAfterAnimation(GameState gameState, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UpdateGameState(gameState); // Transition to next state after animation
    }

    // Function to handle replay button press
    public void OnReplayButtonPressed()
    {
        // Reload the current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
