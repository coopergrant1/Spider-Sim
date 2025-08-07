using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    public TMP_Text notificationText;
    public float displayDuration = 3f;

    void Awake()
    {
        Instance = this;
    }

    public static void ShowMessage(string message)
    {
        if (Instance != null)
        {
            Instance.StopAllCoroutines();
            Instance.StartCoroutine(Instance.Instance_DisplayMessage(message));
        }
    }

    private IEnumerator Instance_DisplayMessage(string message)
    {
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        notificationText.gameObject.SetActive(false);
    }
}
