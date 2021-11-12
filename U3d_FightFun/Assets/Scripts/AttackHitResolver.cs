using UnityEngine;

public class AttackHitResolver : MonoBehaviour
{
    [SerializeField] private Bot _bot = default;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} attacked {other.name}");
        if (other.TryGetComponent(out Bot otherBot))
        {
            MessageHub.BotGotHit?.Invoke(_bot, otherBot);
        }
    }
}