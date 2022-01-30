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
    ScenarioController _scenarioController;
    private Map _map;
    private Vector2Int _characterPosition;

    private MoveThePlayer _Initialize => new MoveThePlayer(this, _map, _characterPosition, _characterRenderer, _playerCombos, ChangeScenario);
    private CharacterRenderer _characterRenderer;

    private int _playerIndex;

    public void Initialize(Map map, Vector2Int characterPosition, int playerIndex, ScenarioController scenarioController)
    {
        _characterRenderer = new CharacterRenderer(_animator);
        _map = map;
        _characterPosition = characterPosition;
        _playerIndex = playerIndex;
        _scenarioController = scenarioController;

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

    public void ChangeScenario(int value)
    {
        //recibo el valor del objeto a instanciar
        //recibo el player index de qui�n lo hizo

        //tambien tengo que sincronizarlo a traves de la rpc
        // photonView.RPC(nameof(RPC_PlayerCombo), RpcTarget.AllBuffered, _playerIndex, _value);

    }

    [PunRPC]
    public void RPC_SetPlayerAvatar(int index)
    {
        //spriteRenderer
    }

    [PunRPC]
    public void RPC_PlayerCombo(int playerIndex, int value)
    {
        //spriteRenderer

        // le digo donde instanciar
        //_scenarioController
    }
}
