using UnityEngine;
using Spine.Unity;

public class CharacterRenderer
{
    public const string Idle = "IDLE_LVL";
    public const string Jump = "LEVEL_MOVE";
    public const string Win = "LEVEL_WIN";
    public const string Loss = "LOSS_LVL";

    private readonly SkeletonAnimation _animator;

    public CharacterRenderer(SkeletonAnimation animator)
    {
        _animator = animator;
        _animator.AnimationState.Complete += OnSpineAnimationComplete;
    }

    public void SetJumpAnimation()
    {
        _animator.AnimationState.SetAnimation(0, Jump, false);
    }

    public void SetWinAnimation()
    {
        _animator.AnimationState.SetAnimation(0, Win, false);
    }

    public void SetLossAnimation()
    {
        _animator.AnimationState.SetAnimation(0, Loss, false);
    }

    public void SetIdleAnimation()
    {
        _animator.AnimationState.SetAnimation(0, Idle, true);
    }

    private void OnSpineAnimationComplete(Spine.TrackEntry trackEntry)
    {
        SetIdleAnimation();
    }


    public string GetAnimationName()
    {
        return _animator.AnimationName;
    }

    public void PlayAnimation(string animationClipName)
    {
        _animator.AnimationName = animationClipName;
    }
}