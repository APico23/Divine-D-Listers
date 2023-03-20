using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private Sprite sprite;

    public Stats GetUnit() { return stats; } 
    public Sprite GetSprite() { return sprite; }
}
