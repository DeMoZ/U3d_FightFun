using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int GotHit = Animator.StringToHash("GotHit");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Defence = Animator.StringToHash("Defence");
    private static readonly int Defeated = Animator.StringToHash("Defeated");

    [SerializeField] private Animator _animator = default;
    
    public void AnimateIdle()
    {
        _animator.SetTrigger(Idle);
    }

    public void AnimateGotHit()
    {
        _animator.SetTrigger(GotHit);
    }

    public void AnimateAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void AnimateDefence()
    {
        _animator.SetTrigger(Defence);
    }
    
    public void AnimateDefeated()
    {
        _animator.SetTrigger(Defeated);
    }
}