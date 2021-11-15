using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BotStats", menuName = "ScriptableObjects/BotStats")]
public class BotStats : ScriptableObject
{
    public int Health = 15;
    public int Power = 1;
    public float thinkSpeed = 1;
    public float AttackSpeed = 1;
    public float DefenceSpeed = 1;
    public float Energy = 10;
    public Vector3 StartPosition;
    public Vector3 StartRotation;

    public Range AttackTimerRange = new Range(1f, 3f);
    public Range DefenceTimerRange = new Range(0.01f, 0.5f);
}

[Serializable]
public class Range
{
    public float Min;
    public float Max;

    public Range(float min, float max)
    {
        Min = min;
        Max = max;
    }
}