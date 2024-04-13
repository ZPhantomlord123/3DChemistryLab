using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private GameObject clickFlaskUI;
    [SerializeField] private GameObject selectFlaskUI;

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

    private IEnumerator ToggleUIWithDelay(GameObject uiElement, bool value, float delay)
    {
        yield return new WaitForSeconds(delay); // Delay of 1 second

        if (uiElement != null)
        {
            uiElement.SetActive(value);
        }
    }
}
