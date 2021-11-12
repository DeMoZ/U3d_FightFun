using UnityEngine;

[CreateAssetMenu(fileName = "BotStats", menuName = "ScriptableObjects/BotStats")]
public class BotStats : ScriptableObject
{
    public int Health = 15;
    public int Power = 1;
    public float thingSpeed = 1;
    public float AttackSpeed = 1;
    public float DefenceSpeed = 1;
    public float Energy = 10;
    public Vector3 StartPosition;
    public Vector3 StartRotation;
    
    public Vector2 ActTimerRange = new Vector2(0.01f, 1f);
}
