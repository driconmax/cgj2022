using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, ICharacterView, IPunObservable
{
    public static string CharacterPrefabName = "Character";

    [SerializeField] private PhotonView _photonView = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private List<PlayerCombo> _playerCombos = null;

    private PlayerMovementController _moveThePlayer;
    private Map _map;
    private Vector2Int _characterPosition;
    private Action<int> _OnPlayerMove;

    private MoveThePlayer _Initialize => new MoveThePlayer(this, _map, _characterPosition, _characterRenderer, _playerCombos, _OnPlayerMove);
    private CharacterRenderer _characterRenderer;

    private int _playerIndex;

    public void Initialize(Map map, Vector2Int characterPosition, int playerIndex, Action<int> OnPlayerMove)
    {
        _characterRenderer = new CharacterRenderer(_animator);
        _map = map;
        _OnPlayerMove += OnPlayerMove;
        _characterPosition = characterPosition;
        _moveThePlayer = _Initialize;
        _playerIndex = playerIndex;
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
            _moveThePlayer.MoveCharacterLeft(_playerIndex);
        else if (Input.GetKeyDown(KeyCode.D))
            _moveThePlayer.MoveCharacterRight(_playerIndex);
        else if (Input.GetKeyDown(KeyCode.W))
            _moveThePlayer.MoveCharacterUp(_playerIndex);
        else if (Input.GetKeyDown(KeyCode.S))
            _moveThePlayer.MoveCharacterDown(_playerIndex);
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
