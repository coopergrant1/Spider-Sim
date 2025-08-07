using UnityEngine;

public class SpiderNeeds : MonoBehaviour
{
    public float hunger = 100f;
    public float energy = 100f;
    public float health = 100f;

    public float hungerDecayRate = 1f;
    public float energyDecayRate = 2f;
    public float healthDecayRate = 5f;

    private TimeManager timeManager;

    void Start()
    {
        timeManager = FindFirstObjectByType<TimeManager>();
    }

    void Update()
    {
        if (timeManager == null) return;

        // Hunger always decays
        hunger -= hungerDecayRate * Time.deltaTime * timeManager.timeMultiplier;
        hunger = Mathf.Clamp(hunger, 0f, 100f);

        // Energy only decays when awake
        if (!IsSleeping())
        {
            energy -= energyDecayRate * Time.deltaTime * timeManager.timeMultiplier;
            energy = Mathf.Clamp(energy, 0f, 100f);
        }

        // Health depletes if hunger or energy hits 0
        if (hunger <= 0f || energy <= 0f)
        {
            health -= healthDecayRate * Time.deltaTime * timeManager.timeMultiplier;
            health = Mathf.Clamp(health, 0f, 100f);
        }
    }

    public bool IsSleeping()
    {
        return timeManager.currentHour >= 22 || timeManager.currentHour < 6;
    }

    public bool IsHungry()
    {
        return hunger < 30f;
    }

    public bool IsTired()
    {
        return energy < 20f;
    }

    public bool IsDead()
    {
        return health <= 0f;
    }

    // ✅ New Methods for Item Interactions

    public void Feed(float amount)
    {
        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0f, 100f);
    }

    public void RestoreEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0f, 100f);
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0f, 100f);
    }

    public void GiveAffection()
    {
        Debug.Log("Spider received affection!");
        // Increase affection, spawn hearts, etc.
    }
}
