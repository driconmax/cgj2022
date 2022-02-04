using System.Collections;
using UnityEngine;

public class GameInstaller : MonoBehaviour, Installer
{
    [SerializeField] private PhotonMultiplayerService photonServer;
    [SerializeField] private MenuView menu;

    private GameInitializer _gameInitializer;
    private MultiplayerConnector _multiplayerConector;

    private InitializeMultiplayerGame _Initialize => new InitializeMultiplayerGame(this, 
                                                                    _multiplayerConector, 
                                                                    menu,
                                                                    ServiceLocator.GetServices<MapPresenter>());

    private void Awake()
    {
        _multiplayerConector = new ConnectToServer(photonServer);

        _gameInitializer = _Initialize;
    }

    private void Start()
    {
        _gameInitializer.Start();
    }
    
}
