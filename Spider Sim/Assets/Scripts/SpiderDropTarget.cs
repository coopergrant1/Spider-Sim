using UnityEngine;
using UnityEngine.EventSystems;

public class SpiderDropTarget : MonoBehaviour, IDropHandler
{
    public SpiderNeeds spiderNeeds;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (item == null) return;

        switch (item.itemType)
        {
            case InventoryItem.ItemType.Food:
                spiderNeeds.Feed(20f);
                NotificationManager.ShowMessage("The spider eats the food!");
                break;
            case InventoryItem.ItemType.Energy:
                spiderNeeds.RestoreEnergy(20f);
                NotificationManager.ShowMessage("The spider feels energized!");
                break;
            case InventoryItem.ItemType.Health:
                spiderNeeds.Heal(20f);
                NotificationManager.ShowMessage("The spider looks healthier!");
                break;
        }

        Destroy(item.gameObject); // Item consumed
    }
}
