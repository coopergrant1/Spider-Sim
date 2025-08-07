using UnityEngine;
using UnityEngine.UI;

public class HeartPopUp : MonoBehaviour
{
    public float riseSpeed = 30f;
    public float fadeSpeed = 2f;

    private RectTransform rectTransform;
    private Image image;
    private Color originalColor;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    void Update()
    {
        // Move upward
        rectTransform.anchoredPosition += Vector2.up * riseSpeed * Time.deltaTime;

        // Fade out
        Color c = image.color;
        c.a -= fadeSpeed * Time.deltaTime;
        image.color = c;

        // Destroy when fully transparent
        if (c.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
