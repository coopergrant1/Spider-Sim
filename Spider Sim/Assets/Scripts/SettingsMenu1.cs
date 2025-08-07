using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public float volumeStep = 0.1f;

    public TMP_InputField nameInput;
    public TextMeshProUGUI nameDisplay;
    public SpiderName spiderNameManager;

    public Image volOnImage;
    public Image volOffImage;

    [Range(0f, 1f)] public float inactiveAlpha = 0.4f;

    void Start()
    {
        nameInput.text = spiderNameManager.currentName;
        UpdateVolumeButtons();
    }

    public void VolumeUp()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume + volumeStep, 0f, 1f);
        UpdateVolumeButtons();
    }

    public void VolumeDown()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume - volumeStep, 0f, 1f);
        UpdateVolumeButtons();
    }

    public void Mute()
    {
        audioSource.mute = true;
        UpdateVolumeButtons();
    }

    public void Unmute()
    {
        audioSource.mute = false;
        UpdateVolumeButtons();
    }

    public void ConfirmName()
    {
        spiderNameManager.SetName(nameInput.text);
        nameDisplay.text = spiderNameManager.currentName;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    void UpdateVolumeButtons()
    {
        if (audioSource.mute)
        {
            SetButtonAlpha(volOnImage, inactiveAlpha);
            SetButtonAlpha(volOffImage, 1f);
        }
        else
        {
            SetButtonAlpha(volOnImage, 1f);
            SetButtonAlpha(volOffImage, inactiveAlpha);
        }
    }

    void SetButtonAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
