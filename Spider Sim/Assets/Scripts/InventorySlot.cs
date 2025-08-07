using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public GameObject itemPrefab;

    public bool IsEmpty()
    {
        return icon.sprite == null;
    }

    public void AddItem(InventoryItem.ItemType type)
    {
        GameObject itemGO = Instantiate(itemPrefab, transform);
        InventoryItem item = itemGO.GetComponent<InventoryItem>();
        item.itemType = type;
        itemGO.GetComponent<Image>().sprite = GetSpriteForType(type);
        itemGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    Sprite GetSpriteForType(InventoryItem.ItemType type)
    {
        return FindObjectOfType<ItemSpawner>().GetComponent<ItemSpawner>().GetSpriteForType(type);
    }
}
