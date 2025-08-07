using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public RectTransform spawnArea;
    public GameObject floatingItemPrefab;
    public Sprite foodSprite;
    public Sprite energySprite;
    public Sprite healthSprite;

    public float spawnInterval = 8f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        InventoryItem.ItemType randomType = (InventoryItem.ItemType)Random.Range(0, 3);
        Sprite sprite = GetSpriteForType(randomType);

        GameObject itemGO = Instantiate(floatingItemPrefab, spawnArea);
        RectTransform rt = itemGO.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(
            Random.Range(-spawnArea.rect.width / 3.5f, spawnArea.rect.width / 3.5f),
            Random.Range(-spawnArea.rect.height / 3.5f, spawnArea.rect.height / 3.5f)
        );

        FloatingItem item = itemGO.GetComponent<FloatingItem>();
        item.Initialize(sprite, randomType, TryAddToInventory);
        item.GetComponent<Button>().onClick.AddListener(item.OnClick);
    }

    public Sprite GetSpriteForType(InventoryItem.ItemType type)
    {
        switch (type)
        {
            case InventoryItem.ItemType.Food: return foodSprite;
            case InventoryItem.ItemType.Energy: return energySprite;
            case InventoryItem.ItemType.Health: return healthSprite;
            default: return null;
        }
    }

    void TryAddToInventory(InventoryItem.ItemType type)
    {
        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItem(type);
                break;
            }
        }
    }
}
