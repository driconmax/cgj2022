using System.Collections;
using UnityEngine;

public class GameInstaller : MonoBehaviour, Installer
{
    [SerializeField] private PhotonMultiplayerService _photonServer;
    [SerializeField] private MenuView _menu;
    [SerializeField] private MapView _mapView;

    private GameInitializer _gameInitializer;

    private InitializeMultiplayerGame _Initialize => new InitializeMultiplayerGame(this,
                                                                    _photonServer, 
                                                                    _menu,
                                                                    _mapView);

    private void Awake()
    {
        _gameInitializer = _Initialize;
    }

    private void Start()
    {
        _gameInitializer.Start();
    }
    
}
