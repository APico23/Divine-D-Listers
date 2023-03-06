using UnityEngine;

[System.Serializable]
public class Stats
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int luck;
    public int defence;
    public int mana;
    public int currentMana;
    public int speed;

    public int maxHP;
    public int currentHp;

    public bool isDead = false;
}
