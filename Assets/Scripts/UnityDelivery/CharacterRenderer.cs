using UnityEngine;

public class CharacterRenderer
{
    public static readonly string[] StaticDirections = { "still_N", "still_NO", "still_O", "still_SO", "still_S", "still_SE", "still_E", "still_NE" };
    public static readonly string[] JumpDirections = { "jump_N", "jump_NO", "jump_O", "jump_SO", "jump_S", "jump_SE", "jump_E", "jump_NE" };

    private readonly int _sliceCountDirections = 4;
    private readonly Animator _animator;
    private int _lastDirection = 0;
    private string[] _directionArray = StaticDirections;

    public CharacterRenderer(Animator animator, int sliceCountDirections = 4)
    {
        _animator = animator;
        _sliceCountDirections = sliceCountDirections;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.magnitude < .01f) {
            _directionArray = StaticDirections;
        } else {
            _directionArray = JumpDirections;
            _lastDirection = DirectionToIndex(direction, _sliceCountDirections);
        }

        //_animator.Play(_directionArray[_lastDirection]);
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
        return _directionArray[_lastDirection];
    }

    public void PlayAnimation(string animationClipName)
    {
       // _animator.Play(animationClipName);
    }
}