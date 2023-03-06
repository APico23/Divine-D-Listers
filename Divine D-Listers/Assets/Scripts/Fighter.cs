using UnityEngine;


[CreateAssetMenu(fileName = "New Fighter", menuName = "Battle/New Fighter")]

public class Fighter : ScriptableObject
{
    [SerializeField] private Stats stats;
    [SerializeField] private Sprite sprite;

    public Stats GetUnit() { return stats; } 
    public Sprite GetSprite() { return sprite; }
}
