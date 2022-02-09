using Profile;
using System.Collections.Generic;
using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analyticsTools;
    private readonly IAdsShower _ads;
    private readonly IShop _shop;
    private readonly PurchaseProcessor _purchaseProcessor;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analyticTools, IAdsShower ads, IShop shop, List<ShopProduct> products)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _analyticsTools = analyticTools;
        _ads = ads;

        _shop = shop;
        _purchaseProcessor = new PurchaseProcessor(shop, profilePlayer, products);
        AddController(_purchaseProcessor);

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }


    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _analyticsTools, _ads);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
