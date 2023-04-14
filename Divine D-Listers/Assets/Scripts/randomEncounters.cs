using UnityEngine;


[CreateAssetMenu(fileName = "New Encounter List", menuName = "Battle/New Encounter List")]

public class randomEncounters : ScriptableObject
{
    [SerializeField] private GameObject[] encounters;

    public GameObject getFighter(int index)
    {
        return encounters[index];
    }
    
    public int getLength()
    {
        return encounters.Length;
    }

    public GameObject getRandomFighter()
    {
        int rand = Random.Range(0, encounters.Length);
        return encounters[rand];
    }
}
