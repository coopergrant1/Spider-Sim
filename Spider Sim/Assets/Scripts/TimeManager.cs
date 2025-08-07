using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeText;
    public Image roomTintOverlay;
    private Color targetRoomTint;

    public SpriteRenderer windowOverlay;
    private Color targetWindowColor;

    [Header("Time Settings")]
    public float timeMultiplier = 0.5f;

    private float timeOfDay = 0f;
    private int lastHour = -1;
    public int currentHour { get; private set; }

    public SpiderMovement SpiderMovement;

    void Update()
    {
        timeOfDay += Time.deltaTime * timeMultiplier;

        if (timeOfDay >= 24f)
            timeOfDay = 0f;

        UpdateTimeDisplay();
        UpdateRoomLighting();
        UpdateWindowOverlay();
        HandleHourChange();
    }

    void UpdateTimeDisplay()
    {
        currentHour = Mathf.FloorToInt(timeOfDay);
        int minute = Mathf.FloorToInt((timeOfDay - currentHour) * 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", currentHour, minute);
    }

    void UpdateRoomLighting()
    {
        float darkness = 0f;

        if (timeOfDay >= 18f || timeOfDay < 6f)
        {
            darkness = 0.6f;
        }
        else if (timeOfDay >= 6f && timeOfDay < 8f)
        {
            darkness = Mathf.Lerp(0.6f, 0f, (timeOfDay - 6f) / 2f);
        }
        else if (timeOfDay >= 16f && timeOfDay < 18f)
        {
            darkness = Mathf.Lerp(0f, 0.6f, (timeOfDay - 16f) / 2f);
        }

        targetRoomTint = new Color(0f, 0f, 0f, darkness);
        roomTintOverlay.color = Color.Lerp(roomTintOverlay.color, targetRoomTint, Time.deltaTime * 2f);
    }

    void UpdateWindowOverlay()
    {
        if (timeOfDay >= 6f && timeOfDay < 8f)
        {
            targetWindowColor = new Color(1f, 0.6f, 0.2f, 0.3f);
        }
        else if (timeOfDay >= 8f && timeOfDay < 16f)
        {
            targetWindowColor = new Color(0.8f, 0.9f, 1f, 0.2f);
        }
        else if (timeOfDay >= 16f && timeOfDay < 18f)
        {
            targetWindowColor = new Color(1f, 0.5f, 0.2f, 0.3f);
        }
        else
        {
            targetWindowColor = new Color(0.1f, 0.1f, 0.3f, 0.8f);
        }

        windowOverlay.color = Color.Lerp(windowOverlay.color, targetWindowColor, Time.deltaTime * 2f);
    }

    void HandleHourChange()
    {
        int hour = Mathf.FloorToInt(timeOfDay);

        if (hour != lastHour)
        {
            if (hour == 7)
            {
                NotificationManager.ShowMessage("The spider greets you sleepily.");
                SpiderMovement.SetSleeping(false);
            }
            else if (hour == 22)
            {
                NotificationManager.ShowMessage("The spider curls up to sleep.");
                SpiderMovement.SetSleeping(true);
            }

            lastHour = hour;
        }
    }
}
