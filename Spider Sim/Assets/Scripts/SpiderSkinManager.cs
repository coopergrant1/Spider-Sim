using UnityEngine;
using UnityEngine.UI;

public class SpiderSkinManager : MonoBehaviour
{
    public Image spiderImage;
    public Sprite[] spiderSkins;

    void Start()
    {
        ApplySkin(0); // Default skin on start
    }

    public void ApplySkin(int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < spiderSkins.Length)
        {
            spiderImage.sprite = spiderSkins[skinIndex];
        }
    }
}
