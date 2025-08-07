using UnityEngine;
using TMPro;

public class AffectionManager : MonoBehaviour
{
    public TMP_Text affectionText;
    private int affection = 0;

    void Start()
    {
        UpdateAffectionDisplay();
    }

    public void IncreaseAffection(int amount)
    {
        affection += amount;
        UpdateAffectionDisplay();
    }

    void UpdateAffectionDisplay()
    {
        affectionText.text = "Affection: " + affection;
    }
}
