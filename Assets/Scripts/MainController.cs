using Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private DailyRewardController _dailyRewardController;
    private WeeklyRewardController _weeklyRewardController;
    private FightController _fighttController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, 
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        IReadOnlyList<AbilityItemConfig> abilityItems)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;

        var itemsSource =
            ResourceLoader.LoadDataSource<ItemConfig>(new ResourcePath()
            { PathResource = "Data/ItemsSource" });
        _itemsConfig = itemsSource.Content.ToList();
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }


    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _shedController = new ShedController(_upgradeItems, _itemsConfig, _profilePlayer.CurrentCar);
                _shedController.Enter();
                _shedController.Exit();
                _gameController?.Dispose();
                _inventoryController?.Dispose();
                break;
            case GameState.Game:
                var inventoryModel = new InventoryModel();
                _inventoryController = new InventoryController(_itemsConfig, inventoryModel);
                _inventoryController.ShowInventory();
                _gameController = new GameController(_profilePlayer, _abilityItems, inventoryModel, _placeForUi);
                _mainMenuController?.Dispose();
                break;
            case GameState.None:
                break;
            case GameState.Fight:
                _inventoryController?.Dispose();
                _gameController?.Dispose();
                _fighttController = CreateFightController();
                break;
            case GameState.Rewards:
                _inventoryController?.Dispose();
                _mainMenuController?.Dispose();
                _dailyRewardController = DailyConfigureRewardController();
                //_weeklyRewardController = WeeklyConfigureRewardController();
                
                break;
            default:
                AllClear();
                break;
        }
    }

    private FightController CreateFightController()
    {
        var fightView = ResourceLoader.LoadAndInstantiateView<FightWindowView>(new ResourcePath()
        { PathResource = "Prefabs/FightView" }, _placeForUi);

        return new FightController(fightView,  _profilePlayer);

    }

    //private WeeklyRewardController WeeklyConfigureRewardController()
    //{
    //    var RewardView = ResourceLoader.LoadAndInstantiateView<RewardView>(new ResourcePath()
    //    { PathResource = "Prefabs/DailyReward" }, _placeForUi);

    //    var currencyWindow =
    //        ResourceLoader.LoadAndInstantiateView<CurrencyWindow>(new ResourcePath()
    //        { PathResource = "Prefabs/Currency Window" }, _placeForUi);


    //    return new WeeklyRewardController(RewardView, currencyWindow, _profilePlayer);
    //}

    private DailyRewardController DailyConfigureRewardController()
    {
        var RewardView = ResourceLoader.LoadAndInstantiateView<RewardView>(new ResourcePath()
        { PathResource = "Prefabs/DailyReward" }, _placeForUi);

        var currencyWindow =
            ResourceLoader.LoadAndInstantiateView<CurrencyWindow>(new ResourcePath()
            { PathResource = "Prefabs/Currency Window" }, _placeForUi);
        return new DailyRewardController(RewardView, currencyWindow, _profilePlayer);
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
