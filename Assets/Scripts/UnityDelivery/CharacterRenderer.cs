using UnityEngine;
using Spine.Unity;

public class CharacterRenderer
{
    public static readonly string[] StaticDirections = { "IDLE_LVL" };
    public static readonly string[] JumpDirections = { "LEVEL_MOVE" };

    private readonly int _sliceCountDirections = 4;
    private readonly SkeletonAnimation _animator;
    private int _lastDirection = 0;
    private string[] _directionArray = StaticDirections;

    public CharacterRenderer(SkeletonAnimation animator, int sliceCountDirections = 4)
    {
        _animator = animator;
        _sliceCountDirections = sliceCountDirections;
    }

    public void SetJumpAnimation()
    { 
        _animator.AnimationName = (JumpDirections[0]);
    }

    public void SetIdleAnimation()
    {
        _animator.AnimationName = (StaticDirections[0]);
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.magnitude < .01f) {
            _directionArray = StaticDirections;
        } else {
            _directionArray = JumpDirections;
            _lastDirection = DirectionToIndex(direction, _sliceCountDirections);
        }

        _animator.AnimationName = (_directionArray[_lastDirection]);
    }

    public static int DirectionToIndex(Vector2 direction, int sliceCount)
    {
        var _step = 360f / sliceCount;
        var _halfStep = _step / 2;
        var _angle = Vector2.SignedAngle(Vector2.up, direction);

        _angle += _halfStep;
        if (_angle < 0) _angle += 360;

        var _stepCount = _angle / _step;

        return Mathf.FloorToInt(_stepCount);
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