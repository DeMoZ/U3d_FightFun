using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : MonoBehaviour
{
    [SerializeField] private BotAnimator _animator = default;
    [field: SerializeField] public BotStats BotStats { get; private set; }

    [field: SerializeField] public BotState BotState { get; private set; } = BotState.Idle;
    [field: SerializeField] public BotSubState? BotHitState { get; private set; } = null;
    [field: SerializeField] public BotSubState? BotDefendState { get; private set; } = null;

    private float _timer = 0;
    private Bot _target;

    public void Init(Bot target)
    {
        _target = target;
        _timer = RandomiseTimer();
    }

    private float RandomiseTimer()
    {
        return Random.Range(BotStats.ActTimerRange.x, BotStats.ActTimerRange.y);
    }

    private void Update()
    {
        if (ReadyToAct(Time.deltaTime))
        {
            _timer = RandomiseTimer();

            switch (_target.BotState)
            {
                default: // Idle
                    SwitchAttack();
                    break;
                case BotState.Attack:
                    SwitchDefence();
                    break;
                case BotState.Defence:
                    SwitchIdle();
                    break;
            }

            Debug.Log($"{name} {BotState}");
        }
    }

    private void SwitchIdle()
    {
        BotState = BotState.Idle;
        _animator.AnimateIdle();
    }

    private void SwitchDefence()
    {
        BotState = BotState.Defence;
        _animator.AnimateDefence();
    }

    private void SwitchAttack()
    {
        BotState = BotState.Attack;
        _animator.AnimateAttack();
    }

    private bool ReadyToAct(float deltaTime)
    {
        _timer -= deltaTime;
        _timer = _timer < 0 ? 0 : _timer;
        return _timer == 0;
    }
}