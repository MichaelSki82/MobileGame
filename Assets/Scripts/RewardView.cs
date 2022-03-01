using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour,IView
{
   

   


    #region Fields
    [Header("Time settings")]
    [SerializeField]
    public int TimeCooldownDaily = 86400;
    [SerializeField]
    public int TimeDeadlineDaily = 172800;

    [SerializeField]
    public int TimeCooldownWeekly = 604800;
    [SerializeField]
    public int TimeDeadlineWeekly = 172800;

    [Space]
    [Header("RewardSettings")]
    public List<Reward> DailyRewards;
    public List<Reward> WeeklyRewards;


    [Header("UI")]
    [SerializeField]
    public TMP_Text RewardTimerDaily;
    [SerializeField]
    public TMP_Text RewardTimerWeekly;

    [SerializeField]
    public Transform SlotsParent;
    [SerializeField]
    public SlotRewardView SlotPrefab;
    [SerializeField]
    public SlotRewardWeeklyView SlotPrefabWeekly;
    [SerializeField]
    public Button ResetButton;
    [SerializeField]
    public Button ExitButton;
    [SerializeField]
    public Button GetRewardButtonDaily;

    [SerializeField]
    public Button GetRewardButtonWeekly;

    [SerializeField]
    public Image TimeLineDaily;

    [SerializeField]
    public Image TimeLineWeekly;




    #endregion

   

    //public int CurrentActiveSlotWeekly
    //{
    //    get => PlayerPrefs.GetInt(ActiveSlotKeyWeekly);
    //    set => PlayerPrefs.SetInt(ActiveSlotKeyWeekly, value);
    //}


      

    //public DateTime? LastRewardTimeWeekly
    //{
    //    get
    //    {
    //        var data = PlayerPrefs.GetString(LastTimeKeyWeekly);
    //        if (string.IsNullOrEmpty(data))
    //            return null;
    //        return DateTime.Parse(data);
    //    }
    //    set
    //    {
    //        if (value != null)
    //            PlayerPrefs.SetString(LastTimeKeyWeekly, value.ToString());
    //        else
    //            PlayerPrefs.DeleteKey(LastTimeKeyWeekly);
    //    }
    //}

    public void Hide()
    {
        throw new NotImplementedException();
    }

    public void Show()
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        GetRewardButtonDaily.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
        GetRewardButtonWeekly.onClick.RemoveAllListeners();
    }

}
