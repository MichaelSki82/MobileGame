using Profile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ResourcePath _viewTrailPath = new ResourcePath { PathResource = "Prefabs/TrailRenderer" };
    private readonly MainMenuInputController _mainMenuInputController;
    private readonly TrailController _trailController;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    //private readonly IShop _shop;
    private readonly MainMenuView _view;


    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer,IAnalyticTools analytics, IAdsShower ads)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        //_shop = shop;

       // profilePlayer.SetupText(text);
        _view = LoadView(placeForUi);
        _view.Init(StartGame);// new UnityAction(()=>_shop.Buy("com.SMITandKo.racingGame.goldPack500")), _profilePlayer.Gold);
        _mainMenuInputController = new MainMenuInputController();
        var trailView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewTrailPath)).GetComponent<TrailRendererView>();
        _trailController = new TrailController(trailView);
        _mainMenuInputController.MousePosition += _trailController.SetPosition;

    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _ads.ShowInterstitial();
        _analytics.SendMessage("Start_game", new Dictionary<string, object>());
        _profilePlayer.CurrentState.Value = GameState.Game;

    }

    protected override void OnDispose()
    {
        _mainMenuInputController.MousePosition -= _trailController.SetPosition;
        _mainMenuInputController?.Dispose();
        _trailController?.Dispose();
        base.OnDispose();
    }
}

