using UnityEngine;


[CreateAssetMenu(fileName = "New Encounter List", menuName = "Battle/New Encounter List")]

public class randomEncounters : ScriptableObject
{
    [SerializeField] private Fighter[] encounters;

    public Fighter getFighter(int index)
    {
        return encounters[index];
    }
}
