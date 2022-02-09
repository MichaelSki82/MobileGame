using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _countMoneyText;

    private IReadOnlySubscriptionProperty<int> _goldProperty;

    public void Init(UnityAction startGame, UnityAction buy, IReadOnlySubscriptionProperty<int> goldProperty)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buyButton.onClick.AddListener(buy);
        _goldProperty = goldProperty;
        _goldProperty.SubscribeOnChange(UpdateGold);

    }


    private void UpdateGold(int value)
    {
        _countMoneyText.text = value.ToString();
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buyButton.onClick.RemoveAllListeners();
        _goldProperty.UnSubscriptionOnChange(UpdateGold);
    }
}