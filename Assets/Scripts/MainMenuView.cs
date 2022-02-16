using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStart;

    public void Hide()
    {
        
    }

    public void Init(UnityAction startGame)
    {
        _buttonStart.onClick.AddListener(startGame);
    }

    public void Show()
    {
        
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
    }
}