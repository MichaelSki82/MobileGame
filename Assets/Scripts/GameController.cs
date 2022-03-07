using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryModel inventoryModel, Transform uiRoot)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);

        var abilityRepository = new AbilityRepository(configs);
        AbilitiesView abilityView =
            ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);

        

        var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository, abilityView);
        AddController(abilitiesController);

        var battleStartController = CreateBattleStartController(uiRoot, profilePlayer);
        AddController(battleStartController);

    }

    private BattleStartController CreateBattleStartController(Transform uiRoot, ProfilePlayer model)
    {
        var startView =
            ResourceLoader.LoadAndInstantiateView<BattleStartView>(
                new ResourcePath() { PathResource = "Prefabs/BattleStart" }, uiRoot);
        AddGameObjects(startView.gameObject);
        return new BattleStartController(startView, model);

    }
}

