using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private GameObject _iBots = default;
    private IBotsFactory _bots;

    void Start()
    {
        _bots = Instantiate(_iBots).GetComponent<IBotsFactory>();
        _bots.CreateBots();
    }

    private void OnValidate()
    {
        if (_iBots && _iBots.GetComponent<IBotsFactory>() == null)
            _iBots = null;
    }
}