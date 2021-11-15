using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int GotHit = Animator.StringToHash("GotHit");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Defence = Animator.StringToHash("Defence");
    private static readonly int Defeated = Animator.StringToHash("Defeated");
    
    [SerializeField] private Animator _animator = default;
    
    private float? _animationTimer;
    public AnimatorState AnimatorState { get; private set; } = AnimatorState.Idle;
    public event Action SwitchedToIdle;
    private void Update()
    {
        if (_animationTimer.HasValue)
        {
            if (_animationTimer > 0)
            {
                _animationTimer -= Time.deltaTime;
            }
            else
            {
                _animationTimer = null;
                AnimatorState = AnimatorState.Idle;
                SwitchedToIdle?.Invoke();
            }
        }
    }

    private void SetTimer()
    {
        StartCoroutine(IESetTimer());
    }

    private IEnumerator IESetTimer()
    {
        yield return new WaitForSeconds(0.26f);
        _animationTimer = _animator.GetCurrentAnimatorStateInfo(0).length;

        // var info = _animator.GetCurrentAnimatorStateInfo(0);
        // info.length * info.speed;
    }

    public void AnimateIdle()
    {
        _animator.SetTrigger(Idle);
        AnimatorState = AnimatorState.Idle;
        // SetTimer();
    }

    public void AnimateGotHit()
    {
        _animator.SetTrigger(GotHit);
        AnimatorState = AnimatorState.GotHit;
        SetTimer();
    }

    public void AnimateAttack()
    {
        _animator.SetTrigger(Attack);
        AnimatorState = AnimatorState.Attack;
        SetTimer();
    }

    public void AnimateDefence()
    {
        _animator.SetTrigger(Defence);
        AnimatorState = AnimatorState.Defence;
        SetTimer();
    }

    public void AnimateDefeated()
    {
        _animator.SetTrigger(Defeated);
        AnimatorState = AnimatorState.Defeated;
        SetTimer();
    }
    
    
}