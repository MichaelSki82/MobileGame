using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyRewardController
{
    private readonly RewardView _rewardView;
    private List<SlotRewardWeeklyView> _slots;

    private bool _rewardReceived = false;
    private readonly ProfilePlayer _profile;

    public WeeklyRewardController(RewardView rewardView, CurrencyWindow currencyWindow, ProfilePlayer profile)
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
        if (_profile.RewardData.LastRewardTimeWeekly.Value.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _profile.RewardData.LastRewardTimeWeekly.Value.Value;
            if (timeSpan.Seconds > _rewardView.TimeDeadlineWeekly)
            {
                _profile.RewardData.LastRewardTimeWeekly.Value = null;
                _profile.RewardData.CurrentActiveSlotWeekly.Value = 0;
            }
            else if (timeSpan.Seconds < _rewardView.TimeCooldownWeekly)
            {
                _rewardReceived = true;
            }
        }
    }

    private void RefreshUi()
    {
        _rewardView.GetRewardButtonWeekly.interactable = !_rewardReceived;

        for (var i = 0; i < _rewardView.WeeklyRewards.Count; i++)
        {
            _slots[i].SetData(_rewardView.WeeklyRewards[i], i + 1, i <= _profile.RewardData.CurrentActiveSlotWeekly.Value);
        }

        DateTime nextDailyBonusTime =
            !_profile.RewardData.LastRewardTimeWeekly.Value.HasValue
                ? DateTime.MinValue
                : _profile.RewardData.LastRewardTimeWeekly.Value.Value.AddSeconds(_rewardView.TimeCooldownWeekly);
        var delta = nextDailyBonusTime - DateTime.UtcNow;
        if (delta.TotalSeconds < 0)
            delta = new TimeSpan(0);

        _rewardView.RewardTimerWeekly.text = delta.ToString();

        _rewardView.TimeLineWeekly.fillAmount = (_rewardView.TimeCooldownWeekly - (float)delta.TotalSeconds) / _rewardView.TimeCooldownWeekly;
    }

    private void InitSlots()
    {
        _slots = new List<SlotRewardWeeklyView>();
        for (int i = 0; i < _rewardView.WeeklyRewards.Count; i++)
        {
            var reward = _rewardView.WeeklyRewards[i];
            var slotInstance = GameObject.Instantiate(_rewardView.SlotPrefabWeekly, _rewardView.SlotsParent, false);
            slotInstance.SetData(reward, i + 1, false);
            _slots.Add(slotInstance);
        }
    }

    private void SubscribeButtons()
    {
        _rewardView.GetRewardButtonWeekly.onClick.AddListener(ClaimReward);
        _rewardView.ResetButton.onClick.AddListener(ResetReward);
    }

    private void ResetReward()
    {
        _profile.RewardData.LastRewardTimeWeekly = null;
        _profile.RewardData.CurrentActiveSlotWeekly.Value = 0;
    }

    private void ClaimReward()
    {
        if (_rewardReceived)
            return;
        var reward = _rewardView.WeeklyRewards[_profile.RewardData.CurrentActiveSlotWeekly.Value];
        switch (reward.Type)
        {
            case RewardType.None:
                break;
            case RewardType.Wood:
                _profile.RewardData.Wood.Value += reward.Count;
                break;
            case RewardType.Diamond:
                _profile.RewardData.Wood.Value += reward.Count;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _profile.RewardData.LastRewardTimeWeekly.Value = DateTime.UtcNow;
        _profile.RewardData.CurrentActiveSlotWeekly.Value = (_profile.RewardData.CurrentActiveSlotWeekly.Value + 1) % _rewardView.WeeklyRewards.Count;
        RefreshRewardState();
    }
}



