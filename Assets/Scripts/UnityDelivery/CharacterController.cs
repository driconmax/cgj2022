using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterController : MonoBehaviour, ICharacterView, IPunObservable
{
    public static string CharacterPrefabName = "Character";

    [SerializeField] private PhotonView _photonView = null;
    [SerializeField] private SkeletonAnimation _animator = null;
    [SerializeField] private List<PlayerCombo> _playerCombos = null;

    private PlayerMovementController _moveThePlayer;
    private Map _map;
    private Vector2Int _characterPosition;
    private MapPresenter _mapPresenter;

    private MoveThePlayer _Initialize => new MoveThePlayer(this, _map, _characterPosition, _characterRenderer, _playerCombos, ChangeScenario);
    private CharacterRenderer _characterRenderer;

    private int _playerIndex;
    private CellView _lastCell;

    private void Awake()
    {
        _characterRenderer = new CharacterRenderer(_animator);
    }

    public void Initialize(int playerIndex, string skinName, MapPresenter mapPresenter)
    {
        _map = mapPresenter.Map;
        _mapPresenter = mapPresenter;
        _characterPosition = mapPresenter.GetPlayerMappedStartPosition(playerIndex);

        _moveThePlayer = _Initialize;

        _photonView.RPC(nameof(RPC_SetPlayerIndex), RpcTarget.AllBuffered, playerIndex);
        _photonView.RPC(nameof(RPC_SetPlayerAvatar), RpcTarget.AllBuffered, skinName);
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
        _mapPresenter.ToggleButton(gridCell);

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
            _characterRenderer.PlayAnimation(animatorClipName);
        }
    }

    public void ChangeScenario(int value)
    {
        if (value == 0) return;
        if (_mapPresenter.CheckValidSpawnCombo(value))
        {
            Vector2Int position = _mapPresenter.GetSpawnPosition(value);
            _photonView.RPC(nameof(RPC_PlayerCombo), RpcTarget.AllBuffered, _playerIndex, position.x, position.y, value);
        }

    }

    [PunRPC]
    public void RPC_SetPlayerIndex(int playerIndex)
    {
        _playerIndex = playerIndex;
    }

    [PunRPC]
    public void RPC_SetPlayerAvatar(string skinName)
    {
        //spriteRenderer
        _animator.skeleton.SetSkin(skinName);
    }

    [PunRPC]
    public void RPC_PlayerCombo(int playerIndex, int x, int y, int value)
    {
        //spriteRenderer

        // le digo donde instanciar
        //_scenarioController

        _mapPresenter.SpawnObjectRandom(x, y, value);
    }
}
