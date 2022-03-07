using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;



public class MainMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private RectTransform _panelMenu;
    [SerializeField] private Button _buttonRewards;
    [SerializeField] private Button _buttonExit;
    private float _durationPanel= 3f;
    private float _durationButton = 6f;
    private float  _endValuePanel = 0f;
    private float _endValueRewards = 0f;
    private float _endValueExit = 150f;
    private float _endValueStart = 250f;

    

    private void Start()
    {
                
        _panelMenu.DOAnchorPosX(_endValuePanel, _durationPanel);
       // _buttonRewards.transform.DOMoveX(_endValueRewards, _durationButton);
        
        _buttonRewards.gameObject.GetComponent<RectTransform>().DOAnchorPosX(_endValueRewards, _durationButton);

        //_buttonExit.transform.DOMoveY(_endValueExit, _durationButton);
        _buttonExit.gameObject.GetComponent<RectTransform>().DOMoveY(_endValueExit, _durationButton);
        _buttonStart.gameObject.GetComponent<RectTransform>().DOMoveY(_endValueStart, _durationButton);

    }

    public void Hide()
    {
        
    }

    public void Init(UnityAction startGame, UnityAction openRewards)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonRewards.onClick.AddListener(openRewards);
    }

    public void Show()
    {
        
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonRewards.onClick.RemoveAllListeners();
    }
}