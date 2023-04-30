using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/New Speaker")]

public class Speaker : ScriptableObject
{
    [SerializeField] private string speakerName;
    [SerializeField] private Sprite speakerSprite;
    public bool faceLeft;

    public string getName()
    {
        return speakerName;
    }

    public Sprite getSprite()
    {
        return speakerSprite;
    }
}
