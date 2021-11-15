using UnityEngine;

public class BotsFactory : MonoBehaviour, IBotsFactory
{
    [SerializeField] private GameObject _allyBot = default;
    [SerializeField] private GameObject _enemyBot = default;

    private void OnValidate()
    {
        if (_allyBot && _allyBot.GetComponent<IBot>() == null)
            _allyBot = null;
        
        if (_enemyBot && _enemyBot.GetComponent<IBot>() == null)
            _enemyBot = null;
    }


    public void CreateBots()
    {
        var allyBot = _allyBot.GetComponent<IBot>();
        var enemyBot = _enemyBot.GetComponent<IBot>();
        
        
        var ally = Instantiate(_allyBot, allyBot.BotStats.StartPosition,
            Quaternion.Euler(allyBot.BotStats.StartRotation)).GetComponent<IBot>();
        var enemy = Instantiate(_enemyBot, enemyBot.BotStats.StartPosition,
            Quaternion.Euler(enemyBot.BotStats.StartRotation)).GetComponent<IBot>();

        ally.Init(enemy);
        enemy.Init(ally);
    }
}