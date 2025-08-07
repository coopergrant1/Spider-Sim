using UnityEngine;
using UnityEngine.EventSystems;

public class SpiderInteraction : MonoBehaviour, IPointerClickHandler
{
    private AffectionManager affectionManager;
    private SpiderNeeds spiderNeeds;

    public GameObject heartPrefab;
    public RectTransform heartSpawnParent;

    [SerializeField] private float verticalOffset = 50f;

    void Start()
    {
        affectionManager = FindFirstObjectByType<AffectionManager>();
        spiderNeeds = GetComponent<SpiderNeeds>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (spiderNeeds != null && spiderNeeds.IsSleeping())
        {
            NotificationManager.ShowMessage("The spider is sleeping...");
            return;
        }

        if (spiderNeeds != null && spiderNeeds.IsHungry())
        {
            NotificationManager.ShowMessage("The spider looks hungry...");
        }

        if (affectionManager != null)
        {
            affectionManager.IncreaseAffection(1);
        }

        if (heartPrefab != null && heartSpawnParent != null)
        {
            Vector2 clickPos = eventData.position;
            clickPos += new Vector2(0, verticalOffset);

            Vector2 spawnPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                heartSpawnParent,
                clickPos,
                null,
                out spawnPos
            );

            GameObject heart = Instantiate(heartPrefab, heartSpawnParent);
            RectTransform heartRect = heart.GetComponent<RectTransform>();
            heartRect.anchoredPosition = spawnPos;
            heartRect.localScale = Vector3.one;
        }
    }
}
