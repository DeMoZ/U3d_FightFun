using UnityEngine;

public class BotsFactory : MonoBehaviour, IBotsFactory
{
    [SerializeField] private Bot _allyBot = default;
    [SerializeField] private Bot _enemyBot = default;
    
    public void CreateBots()
    {
        var ally = Instantiate(_allyBot, _allyBot.BotStats.StartPosition,Quaternion.Euler(_allyBot.BotStats.StartRotation));
        var enemy = Instantiate(_enemyBot, _enemyBot.BotStats.StartPosition,Quaternion.Euler(_enemyBot.BotStats.StartRotation));
        
        ally.Init(enemy);
        enemy.Init(ally);
    }
}