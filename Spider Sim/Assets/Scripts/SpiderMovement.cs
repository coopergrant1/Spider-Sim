using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpiderMovement : MonoBehaviour
{
    public Image spiderImage;

    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite sleepSprite;

    public RectTransform spiderTransform;         // Assign the Spider (Image) UI RectTransform
    public RectTransform movementArea;            // A parent RectTransform (like the Canvas or a panel) to constrain movement
    public float moveSpeed = 200f;                // Pixels per second
    public float waitTime = 2f;                   // Time to wait after reaching destination
    public bool isSleeping = true;                // Set to true externally to stop movement

    public float rotationSpeed = 360f;            // Degrees per second for turning

    private Vector2 targetPosition;
    private bool isWalking = false;

    private float walkTimer = 0f;
    public float walkFrameDuration = 0.3f;

    private bool showingWalkFrame = false;

    void Start()
    {
        StartCoroutine(Wander());
    }

    void Update()
    {
        if (isSleeping)
        {
            spiderImage.sprite = sleepSprite;
            return;
        }

        if (isWalking)
        {
            walkTimer += Time.deltaTime;
            if (walkTimer >= walkFrameDuration)
            {
                walkTimer = 0f;
                showingWalkFrame = !showingWalkFrame;
                spiderImage.sprite = showingWalkFrame ? walkSprite : idleSprite;
            }
        }
        else
        {
            spiderImage.sprite = idleSprite;
        }
    }

    IEnumerator Wander()
    {
        while (true)
        {
            if (!isSleeping)
            {
                targetPosition = GetRandomPosition();
                isWalking = true;

                // Move toward target
                while (!isSleeping && Vector2.Distance(spiderTransform.anchoredPosition, targetPosition) > 1f)
                {
                    Vector2 currentPos = spiderTransform.anchoredPosition;
                    Vector2 direction = targetPosition - currentPos;

                    // Rotate to face movement direction
                    if (direction.sqrMagnitude > 0.01f)
                    {
                        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
                        float currentZ = spiderTransform.localEulerAngles.z;

                        // Convert 0–360 to -180–180
                        if (currentZ > 180f) currentZ -= 360f;

                        float newZ = Mathf.MoveTowardsAngle(currentZ, targetAngle, rotationSpeed * Time.deltaTime);
                        spiderTransform.localEulerAngles = new Vector3(0f, 0f, newZ);
                    }

                    spiderTransform.anchoredPosition = Vector2.MoveTowards(
                        currentPos,
                        targetPosition,
                        moveSpeed * Time.deltaTime
                    );

                    yield return null;
                }

                // Stop and wait
                isWalking = false;
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        // Limit movement to the bounds of the movementArea RectTransform
        Vector2 areaSize = movementArea.rect.size;
        float padding = 375f;

        float x = Random.Range(-areaSize.x - padding, areaSize.x + padding);
        float y = Random.Range(-areaSize.y - padding, areaSize.y + padding);
        return new Vector2(x, y);
    }

    public void SetSleeping(bool sleep)
    {
        isSleeping = sleep;
        if (sleep)
        {
            spiderImage.sprite = sleepSprite;
        }
    }

    public void SetWalking(bool walk)
    {
        isWalking = walk;
        if (!walk)
        {
            walkTimer = 0f;
            showingWalkFrame = false;
            spiderImage.sprite = idleSprite;
        }
    }
}
