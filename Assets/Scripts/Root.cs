using Profile;
using UnityEngine;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;
    [SerializeField]
    private UnityAdsTools _ads;
    private IAnalyticTools _analyticsTools;
    [SerializeField] private ShopTools _shop;

    [SerializeField] private Text _countCoins;
    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f);
        _analyticsTools = new UnityAnalyticTools();
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsTools, _ads);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
