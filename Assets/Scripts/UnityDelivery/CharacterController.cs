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
    ScenarioController _scenarioController;
    private Map _map;
    private Vector2Int _characterPosition;
    private Installer _installer;

    private MoveThePlayer _Initialize => new MoveThePlayer(this, _map, _characterPosition, _characterRenderer, _playerCombos, ChangeScenario);
    private CharacterRenderer _characterRenderer;

    private int _playerIndex;
    private CellView _lastCell;

    private void Awake()
    {
        _characterRenderer = new CharacterRenderer(_animator);
        _scenarioController = ServiceLocator.GetServices<ScenarioController>();
    }

    public void Initialize(Map map, Vector2Int characterPosition, int playerIndex, string skinName, Installer installer)
    {
        _map = map;
        _characterPosition = characterPosition;
        _playerIndex = playerIndex;
        _installer = installer;

        _animator.skeleton.SetSkin(skinName);

        _moveThePlayer = _Initialize;
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        int comboScore = 0;

        if (Input.GetKeyDown(KeyCode.A))
            comboScore = _moveThePlayer.MoveCharacterLeft(_playerIndex);
        else if (Input.GetKeyDown(KeyCode.D))
            comboScore = _moveThePlayer.MoveCharacterRight(_playerIndex);
        else if (Input.GetKeyDown(KeyCode.W))
            comboScore = _moveThePlayer.MoveCharacterUp(_playerIndex);
        else if (Input.GetKeyDown(KeyCode.S))
            comboScore = _moveThePlayer.MoveCharacterDown(_playerIndex);

        if (comboScore > 0)
        {
            ChangeScenario(comboScore);
        }
    }

    public void MovePlayerToCell(int row, int column)
    {
        MovePlayerToCell(_map.grid[column][row]);
    }

    private void MovePlayerToCell(Cell gridCell)
    {
        if(_installer.CellToObject.TryGetValue(gridCell, out CellView cellObject)){
            _lastCell?.ChangeStatus(true);
            _lastCell = cellObject.ChangeStatus(false);
        }
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
        //recibo el valor del objeto a instanciar
        //recibo el player index de quién lo hizo

        //tambien tengo que sincronizarlo a traves de la rpc
        // photonView.RPC(nameof(RPC_PlayerCombo), RpcTarget.AllBuffered, _playerIndex, _value);
        _photonView.RPC(nameof(RPC_PlayerCombo), RpcTarget.AllBuffered, _playerIndex, value);

    }

    [PunRPC]
    public void RPC_SetPlayerAvatar(int index)
    {
        //spriteRenderer
    }

    [PunRPC]
    public void RPC_PlayerCombo(int playerIndex, int value)
    {
        if (value == 0) return;
        //spriteRenderer

        // le digo donde instanciar
        //_scenarioController

        _scenarioController.SpawnObjectRandom(value);
    }
}
