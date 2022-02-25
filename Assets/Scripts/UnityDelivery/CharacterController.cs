using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public delegate void CustomEvent(int playerIndex, int comboLevel);

public class CharacterController : MonoBehaviour, ICharacterView, IPunObservable
{
    public static string CharacterPrefabName = "Character";

    public event CustomEvent OnPlayerCombo;

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

    private Spine.Skin[] _skins;

    private void Awake()
    {
        _characterRenderer = new CharacterRenderer(_animator);
        _mapPresenter = ServiceLocator.GetServices<MapPresenter>();
        _map = _mapPresenter.Map;

        _skins = _animator.Skeleton.Data.Skins.Items;

    }

    public void Initialize(int playerIndex, int skinIndex)
    {
        _playerIndex = playerIndex;
        _characterPosition = _mapPresenter.GetPlayerMappedStartPosition(_playerIndex);

        _photonView.RPC(nameof(RPC_SetPlayerAvatar), RpcTarget.AllBuffered, skinIndex);
        _photonView.RPC(nameof(RPC_SetPlayerIndex), RpcTarget.AllBuffered, _playerIndex);

        _moveThePlayer = _Initialize;
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

    public void ChangeScenario(int comboValue)
    {
        if (comboValue == 0) return;
        if (_mapPresenter.CheckValidSpawnCombo(comboValue))
        {
            Vector2Int position = _mapPresenter.GetSpawnPosition(comboValue);
            _mapPresenter.SpawnObject(position.x, position.y, comboValue);
            _photonView.RPC(nameof(RPC_PlayerCombo), RpcTarget.Others, position.x, position.y, comboValue);

            OnPlayerCombo.Invoke(_playerIndex, comboValue);
        }

    }

    [PunRPC]
    public void RPC_SetPlayerIndex(int playerIndex)
    {
        _playerIndex = playerIndex;
    }

    [PunRPC]
    public void RPC_SetPlayerAvatar(int index)
    {
        _animator.Skeleton.SetSkin(_skins[index]);
        _animator.Skeleton.SetToSetupPose();
        _animator.Skeleton.SetSlotsToSetupPose();
    }

    [PunRPC]
    public void RPC_PlayerCombo(int x, int y, int value)
    {
        _mapPresenter.DamageCell(x, y, value);

        OnPlayerCombo?.Invoke(_playerIndex, value);
    }
}
