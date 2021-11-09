using UnityEngine;

public class BotsFactory : MonoBehaviour, IBotsFactory
{
    [SerializeField] private Bot _allyBot = default;
    [SerializeField] private Bot _enemyBot = default;
    
    public void CreateBots()
    {
        var ally = Instantiate(_allyBot, Vector3.right * -1.3f, Quaternion.Euler(0, 90, 0));
        var enemy = Instantiate(_enemyBot, Vector3.right * 1.3f, Quaternion.Euler(0, -90, 0));
    }
}