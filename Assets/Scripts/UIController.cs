using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private GameObject clickFlaskUI;
    [SerializeField] private GameObject selectFlaskUI;
    [SerializeField] private GameObject clickToStirUI;
    [SerializeField] private GameObject selectSecondFlaskUI;
    [SerializeField] private GameObject replayUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleClickFlaskUI(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(clickFlaskUI, value, delay));
    }

    public void ToggleSelectFlaskUI(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(selectFlaskUI, value, delay));
    }

    public void ToggleClickStirUI(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(clickToStirUI, value, delay));
    }

    public void ToggleSelectSecondFlaskUI(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(selectSecondFlaskUI, value, delay));
    }

    public void ToggleReplayUI(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(replayUI, value, delay));
    }

    private IEnumerator ToggleUIWithDelay(GameObject uiElement, bool value, float delay)
    {
        yield return new WaitForSeconds(delay); // Delay of 1 second

        if (uiElement != null)
        {
            uiElement.SetActive(value);
        }
    }
}
