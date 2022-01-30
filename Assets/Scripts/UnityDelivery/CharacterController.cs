using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, ICharacterView, IPunObservable
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
