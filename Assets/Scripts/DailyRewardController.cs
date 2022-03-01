using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardController:BaseController
{
    private readonly RewardView _rewardView;
    private readonly ProfilePlayer _profile;
    private List<SlotRewardView> _slots;

    private bool _rewardReceived = false;

    public DailyRewardController(RewardView rewardView, CurrencyWindow currencyWindow, ProfilePlayer profile)
    {
        _rewardView = rewardView;
        _profile = profile;
        currencyWindow.Init(profile.RewardData.Diamond, profile.RewardData.Wood);
        InitSlots();
        RefreshUi();
        _rewardView.StartCoroutine(UpdateCoroutine());
        SubscribeButtons();
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            Update();
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        RefreshRewardState();
        RefreshUi();
    }

    private void RefreshRewardState()
    {
        _rewardReceived = false;
        if (_profile.RewardData.LastRewardTimeDaily.Value.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _profile.RewardData.LastRewardTimeDaily.Value.Value;
            if (timeSpan.Seconds > _rewardView.TimeDeadlineDaily)
            {
                _profile.RewardData.LastRewardTimeDaily.Value = null;
                _profile.RewardData.CurrentActiveSlotDaily.Value = 0;
            }
            else if(timeSpan.Seconds < _rewardView.TimeCooldownDaily)
            {
                _rewardReceived = true;
            }
        }
    }

    private void RefreshUi()
    {
        _rewardView.GetRewardButtonDaily.interactable = !_rewardReceived;

        for (var i = 0; i < _rewardView.DailyRewards.Count; i++)
        {
            _slots[i].SetData(_rewardView.DailyRewards[i], i+1, i <= _profile.RewardData.CurrentActiveSlotDaily.Value);
        }

        DateTime nextDailyBonusTime =
            !_profile.RewardData.LastRewardTimeDaily.Value.HasValue
                ? DateTime.MinValue
                : _profile.RewardData.LastRewardTimeDaily.Value.Value.AddSeconds(_rewardView.TimeCooldownDaily);
        var delta = nextDailyBonusTime - DateTime.UtcNow;
        if (delta.TotalSeconds < 0)
            delta = new TimeSpan(0);

        _rewardView.RewardTimerDaily.text = delta.ToString();

        _rewardView.TimeLineDaily.fillAmount = (_rewardView.TimeCooldownDaily - (float)delta.TotalSeconds)/_rewardView.TimeCooldownDaily;
    }

    private void InitSlots()
    {
        _slots = new List<SlotRewardView>();
        for (int i = 0; i < _rewardView.DailyRewards.Count; i++)
        {
            var reward = _rewardView.DailyRewards[i];
            var slotInstance = GameObject.Instantiate(_rewardView.SlotPrefab, _rewardView.SlotsParent, false);
            slotInstance.SetData(reward, i+1, false);
            _slots.Add(slotInstance);
        }
    }

    private void SubscribeButtons()
    {
        _rewardView.GetRewardButtonDaily.onClick.AddListener(ClaimReward);
        _rewardView.ResetButton.onClick.AddListener(ResetReward);
    }

    private void ResetReward()
    {
        _profile.RewardData.LastRewardTimeDaily = null;
        _profile.RewardData.CurrentActiveSlotDaily.Value = 0;
    }

    private void ClaimReward()
    {
        if (_rewardReceived)
            return;
        var reward = _rewardView.DailyRewards[_profile.RewardData.CurrentActiveSlotDaily.Value];
        switch (reward.Type)
        {
            case RewardType.None:
                break;
            case RewardType.Wood:
                _profile.RewardData.Wood.Value += reward.Count;
                
                break;
            case RewardType.Diamond:
                _profile.RewardData.Diamond.Value += reward.Count;
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _profile.RewardData.LastRewardTimeDaily.Value = DateTime.UtcNow;
        _profile.RewardData.CurrentActiveSlotDaily.Value = (_profile.RewardData.CurrentActiveSlotDaily.Value + 1) % _rewardView.DailyRewards.Count;
        RefreshRewardState();
    }
}
