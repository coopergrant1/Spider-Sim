using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingItem : MonoBehaviour
{
    public Image image;
    public InventoryItem.ItemType itemType;
    public float lifetime = 5f;

    private System.Action<InventoryItem.ItemType> onCollected;

    public void Initialize(Sprite sprite, InventoryItem.ItemType type, System.Action<InventoryItem.ItemType> collectCallback)
    {
        image.sprite = sprite;
        itemType = type;
        onCollected = collectCallback;
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {
        float timer = 0f;
        CanvasGroup cg = GetComponent<CanvasGroup>();

        while (timer < lifetime)
        {
            timer += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
            yield return null;
        }

        Destroy(gameObject);
    }

    public void OnClick()
    {
        onCollected?.Invoke(itemType);
        Destroy(gameObject);
    }
}
