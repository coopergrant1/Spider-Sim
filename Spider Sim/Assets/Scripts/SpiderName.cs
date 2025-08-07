using UnityEngine;

public class SpiderName : MonoBehaviour
{
    public string currentName = "Carlos";

    public void SetName(string newName)
    {
        currentName = newName;
        Debug.Log("Spider's name is now: " + currentName);
    }
}
