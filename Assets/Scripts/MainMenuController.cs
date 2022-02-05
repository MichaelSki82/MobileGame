using Profile;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ResourcePath _viewTrailPath = new ResourcePath { PathResource = "Prefabs/TrailRenderer" };
    private readonly MainMenuInputController _mainMenuInputController;
    private readonly TrailController _trailController;
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame);
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
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

