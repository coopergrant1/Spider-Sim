using UnityEngine;
using UnityEngine.UI;

public class SpiderNeedsUI : MonoBehaviour
{
    public SpiderNeeds spiderNeeds;

    [Header("Hunger Bar")]
    public Image hungerFill;
    public Color healthyColor = Color.green;
    public Color lowColor = Color.red;

    [Header("Energy Bar")]
    public Image energyFill;
    public Color healthyEnergyColor = Color.blue;
    public Color lowEnergyColor = Color.yellow;

    [Header("Health Bar")]
    public Image healthFill;
    public Color healthyHealthColor = Color.magenta;
    public Color lowHealthColor = Color.black;

    public GameObject spiderObject;

    private bool isDead = false;

    void Update()
    {
        if (spiderNeeds == null || isDead) return;

        UpdateBar(hungerFill, spiderNeeds.hunger, healthyColor, lowColor);
        UpdateBar(energyFill, spiderNeeds.energy, healthyEnergyColor, lowEnergyColor);
        UpdateBar(healthFill, spiderNeeds.health, healthyHealthColor, lowHealthColor);

        if (spiderNeeds.IsDead() && !isDead)
        {
            HandleSpiderDeath();
        }
    }

    void UpdateBar(Image bar, float value, Color fullColor, Color lowColor)
    {
        float normalized = Mathf.Clamp01(value / 100f);
        bar.fillAmount = normalized;
        bar.color = Color.Lerp(lowColor, fullColor, normalized);
    }

    void HandleSpiderDeath()
    {
        isDead = true;
        NotificationManager.ShowMessage("The spider has died...");

        if (spiderObject != null)
        {
            spiderObject.SetActive(false);
        }
    }
}
