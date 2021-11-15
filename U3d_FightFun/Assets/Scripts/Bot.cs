using System;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : MonoBehaviour, IBot
{
    [SerializeField] private BotAnimator _animator = default;
    [field: SerializeField] public BotStats BotStats { get; private set; }
    [field: SerializeField] public BotState BotState { get; private set; } = BotState.Idle;

    //private float _timer;
    private IBot _target;

    [SerializeField, ReadOnly] private bool _readyAttack;
    [SerializeField, ReadOnly] private bool _readyDefence;

    private bool _targetStateChanged;
    public event Action StateChanged;
    [SerializeField, ReadOnly] private BotState? _nextState;
    [SerializeField, ReadOnly] private float _attackTimer;
    [SerializeField, ReadOnly] private float _defenceTimer;

    public void Init(IBot target)
    {
        _attackTimer = RandomiseTimer(BotStats.AttackTimerRange);
        _defenceTimer = RandomiseTimer(BotStats.DefenceTimerRange);

        _animator.SwitchedToIdle += () => SetBotState(BotState.Idle);
        
        _target = target;
        _target.StateChanged += SetNextStateAfterTargetState;
    }

    private void Start()
    {
        SetBotState(BotState.Idle);
    }

    private float RandomiseTimer(Range range) =>
        Random.Range(range.Min, range.Max);


    private void SetNextStateAfterTargetState()
    {
        _nextState = _target.BotState switch
        {
            BotState.Idle => BotState.Attack,
            BotState.Attack => BotState.Defence,
            BotState.Defence => BotState.Idle,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (BotState != BotState.Defence && _nextState == BotState.Defence)
            _defenceTimer = RandomiseTimer(BotStats.DefenceTimerRange);

        if (BotState == BotState.Attack && _nextState == BotState.Attack)
            _attackTimer = RandomiseTimer(BotStats.AttackTimerRange);

        Debug.Log($"{name} next state = {_nextState}");
    }

    private void OnGotHit()
    {
        _attackTimer = RandomiseTimer(BotStats.AttackTimerRange);
        _defenceTimer = RandomiseTimer(BotStats.DefenceTimerRange);
    }

    private void Update()
    {
        Update2(Time.deltaTime);
    }

    private void Update2(float deltaTime)
    {
        //if (increaseButtonPressed) deltaTime += increaseValue;

        if (_animator.AnimatorState != AnimatorState.Defence)
            _readyDefence = CountTimer(ref _defenceTimer, deltaTime);

        if (_animator.AnimatorState != AnimatorState.Attack)
            _readyAttack = CountTimer(ref _attackTimer, deltaTime);

        if (!_nextState.HasValue)
            SetNextStateAfterTargetState();

        if (_animator.AnimatorState == AnimatorState.Idle && _nextState.HasValue)
        {
            if (_readyDefence && _nextState == BotState.Defence)
                SetBotState(BotState.Defence);
            else if (_readyAttack && _nextState == BotState.Attack)
                SetBotState(BotState.Attack);
        }
    }

    private void SetBotState(BotState state)
    {
        Debug.Log($"{name} set bot state to {state}");
        _nextState = null;
        BotState = state;
        StateChanged?.Invoke();
         
        switch (BotState)
        {
            case BotState.Idle:
                //SwitchIdle();
                break;
            case BotState.Attack:
                SwitchAttack();
                break;
            case BotState.Defence:
                SwitchDefence();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool CountTimer(ref float timer, float deltaTime)
    {
        timer -= deltaTime;
        timer = timer < 0 ? 0 : timer;

        return timer == 0;
    }

    /*private void Update1()
    {
        if (ReadyToAct(Time.deltaTime))
        {
            //_timer = RandomiseTimer();

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
    }*/

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

    /*private bool ReadyToAct(float deltaTime)
    {
        _timer -= deltaTime;
        _timer = _timer < 0 ? 0 : _timer;
        return _timer == 0;
    }*/
}