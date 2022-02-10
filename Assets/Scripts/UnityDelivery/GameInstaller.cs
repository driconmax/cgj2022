using System.Collections;
using UnityEngine;

public class GameInstaller : MonoBehaviour, Installer
{
    [SerializeField] private PhotonMultiplayerService _photonServer;
    [SerializeField] private MenuView _menu;
    [SerializeField] private MapView _mapView;

    private GameInitializer _gameInitializer;
    private MultiplayerConnector _multiplayerConector;

    private InitializeMultiplayerGame _Initialize => new InitializeMultiplayerGame(this, 
                                                                    _multiplayerConector, 
                                                                    _menu,
                                                                    _mapView);

    private void Awake()
    {
        _multiplayerConector = new ConnectToServer(_photonServer);

        _gameInitializer = _Initialize;
    }

    private void Start()
    {
        _gameInitializer.Start();
    }
    
}
