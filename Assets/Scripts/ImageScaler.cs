using UnityEngine;
using UnityEngine.UI;

public class ImageScaler : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public float scaleSpeed = 1.0f;
    public float rotationSpeed = 30.0f; // Rotation speed in degrees per second

    private RectTransform rectTransform;
    private Vector2 initialScale;
    private bool isExpanding = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialScale = rectTransform.localScale;
    }

    private void Update()
    {
        float targetScale = isExpanding ? maxScale : minScale;
        float newScale = Mathf.MoveTowards(rectTransform.localScale.x, targetScale, Time.deltaTime * scaleSpeed);

        // Apply new scale
        rectTransform.localScale = new Vector2(newScale, newScale);

        // Rotate along the Z-axis
        float newRotation = Time.deltaTime * rotationSpeed;
        rectTransform.Rotate(new Vector3(0f, 0f, newRotation));

        // Check if reached target scale
        if (SquaredDistance(rectTransform.localScale, targetScale) < 0.001f)
        {
            isExpanding = !isExpanding;
        }
    }

    // Squared distance comparison for better performance
    private float SquaredDistance(Vector2 a, float b)
    {
        float diff = a.x - b;
        return diff * diff;
    }
}
