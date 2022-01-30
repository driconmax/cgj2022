using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, CharacterView, IPunObservable
{
    public static string CharacterPrefabName = "Character";

    [SerializeField] private PhotonView _photonView = null;
    [SerializeField] private Animator _animator = null;

    private PlayerMovementController _moveThePlayer;
    private Map _map;
    private Vector2Int _characterPosition;

    private MoveThePlayer _Initialize => new MoveThePlayer(this, _map, _characterPosition, _characterRenderer);
    private CharacterRenderer _characterRenderer;

    public void Initialize(Map map, Vector2Int characterPosition)
    {
        _characterRenderer = new CharacterRenderer(_animator);
        _map = map;
        _characterPosition = characterPosition;
        _moveThePlayer = _Initialize;
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
            _moveThePlayer.MoveCharacterLeft();
        else if (Input.GetKeyDown(KeyCode.D))
            _moveThePlayer.MoveCharacterRight();
        else if (Input.GetKeyDown(KeyCode.W))
            _moveThePlayer.MoveCharacterUp();
        else if (Input.GetKeyDown(KeyCode.S))
            _moveThePlayer.MoveCharacterDown();
    }

    public void MovePlayerToCell(int row, int column)
    {
        MovePlayerToCell(_map.grid[column][row]);
    }

    private void MovePlayerToCell(Cell gridCell)
    {
        transform.localPosition = gridCell.GetPosition();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_characterRenderer.GetAnimationName());
        }
        else
        {
            var animatorClipName = (string)stream.ReceiveNext();
           // _characterRenderer.PlayAnimation(animatorClipName);
        }
    }

    [PunRPC]
    public void RPC_SetPlayerAvatar(int index)
    {
        //spriteRenderer
    }
}

public interface CharacterView
{
    void MovePlayerToCell(int row, int column);
}

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