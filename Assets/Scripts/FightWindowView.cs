using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UI;

public class FightWindowView : MonoBehaviour, IView
{
    [SerializeField]
    public TMP_Text _countMoneyText;

    [SerializeField]
    public TMP_Text _countHealthText;

    [SerializeField]
    public TMP_Text _countPowerText;

    [SerializeField]
    public TMP_Text _countPowerEnemyText;

    [SerializeField]
    public TMP_Text _crimeStatusPlayerText;

    [SerializeField]
    public TMP_Text _countKnifePowerEnemyText;


    [SerializeField]
    public Button _addMoneyButton;

    [SerializeField]
    public Button _minusMoneyButton;


    [SerializeField]
    public Button _addHealthButton;

    [SerializeField]
    public Button _minusHealthButton;


    [SerializeField]
    public Button _addPowerButton;

    [SerializeField]
    public Button _minusPowerButton;

    [SerializeField]
    public Button _addCrimeButton;

    [SerializeField]
    public Button _minusCrimeButton;

    //[SerializeField]
    //public Button _freeWayButton;

    [SerializeField]
    public Button _fightButton;

    [SerializeField]
    public Button _knifeFightButton;

     
    private int _allCountCrimePlayer;

    
    public void Init(UnityAction<DataType, int> changeAction, UnityAction fight, UnityAction Knifefight )
    {
        SubscribeButtons(changeAction, fight, Knifefight);
    }

    private void SubscribeButtons(UnityAction<DataType, int> changeAction, UnityAction fight, UnityAction Knifefight)
    {
        _addMoneyButton.onClick.AddListener(() => changeAction(DataType.Money, 1));
        _minusMoneyButton.onClick.AddListener(() => changeAction(DataType.Money, -1));

        _addHealthButton.onClick.AddListener(() => changeAction(DataType.Health, 1));
        _minusHealthButton.onClick.AddListener(() => changeAction(DataType.Health, -1));

        _addPowerButton.onClick.AddListener(() => changeAction(DataType.Power, 1));
        _minusPowerButton.onClick.AddListener(() => changeAction(DataType.Power, -1));

        _addCrimeButton.onClick.AddListener(() => changeAction(DataType.Crime, 1));
        _minusCrimeButton.onClick.AddListener(() => changeAction(DataType.Crime,1));


        _knifeFightButton.onClick.AddListener(Knifefight);
        //_freeWayButton.onClick.AddListener(FreeWay);



        _fightButton.onClick.AddListener(fight);
    }
    

    //private void FreeWay()
    //{
    //   if(_allCountCrimePlayer <=2)
    //   {
    //        _freeWayButton.GetComponent<Image>().color = Color.green;
    //        Debug.Log("FreeWay");
    //   }
    //   else
    //   {
    //        _freeWayButton.GetComponent<Image>().color = Color.black;
    //        Debug.Log("Can't FreeWay");
    //   }
    //}

    private void OnDestroy()
    {
        UnsubscribeButtons();
       
    }

    private void UnsubscribeButtons()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addCrimeButton.onClick.RemoveAllListeners();
        _minusCrimeButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();
        _knifeFightButton.onClick.RemoveAllListeners();
    }

    //private void ChangeCrime(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountCrimePlayer++;
    //    else
    //        _allCountCrimePlayer--;

    //    ChangeDataWindow(_allCountCrimePlayer, DataType.Crime);
    //}
   

    //private void ChangePower(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountPowerPlayer++;
    //    else
    //        _allCountPowerPlayer--;

    //    ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    //}

    //private void ChangeHealth(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountHealthPlayer++;
    //    else
    //        _allCountHealthPlayer--;

    //    ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    //}

    //private void ChangeMoney(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountMoneyPlayer++;
    //    else
    //        _allCountMoneyPlayer--;

    //    ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    //}

    //private void ChangeDataWindow(int countChangeData, DataType dataType)
    //{


    //    _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    //    _countKnifePowerEnemyText.text = $"Enemy Knife Power: {_enemy.KnifePower}";
    //}

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
