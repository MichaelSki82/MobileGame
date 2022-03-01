using System;
using Tools;

public class RewardData
{
    private const string WoodKey = "Wood";
    private const string DiamondKey = "Diamond";
    private const string LastTimeKeyDaily = "LastRewardTimeDaily";
    private const string ActiveSlotKeyDaily = "ActiveSlotDaily";
    private const string LastTimeKeyWeekly = "LastRewardTimeWeekly";
    private const string ActiveSlotKeyWeekly = "ActiveSlotWeekly";

    public RewardData()
    {
        Wood = new PrefsSubscriptionProperty<int>(WoodKey, int.Parse);
        Diamond = new PrefsSubscriptionProperty<int>(DiamondKey, int.Parse);
        CurrentActiveSlotDaily = new PrefsSubscriptionProperty<int>(ActiveSlotKeyDaily, int.Parse);
        CurrentActiveSlotWeekly = new PrefsSubscriptionProperty<int>(ActiveSlotKeyWeekly, int.Parse);
        LastRewardTimeDaily = new PrefsSubscriptionProperty<DateTime?>(LastTimeKeyDaily, NullableDateTimeConverter());
        LastRewardTimeWeekly = new PrefsSubscriptionProperty<DateTime?>(LastTimeKeyWeekly, NullableDateTimeConverter());
    }

    public SubscriptionProperty<int> Wood;
    public SubscriptionProperty<int> Diamond;
    public SubscriptionProperty<int> CurrentActiveSlotDaily;
    public SubscriptionProperty<DateTime?> LastRewardTimeDaily;
    public SubscriptionProperty<int> CurrentActiveSlotWeekly;
    public SubscriptionProperty<DateTime?> LastRewardTimeWeekly;

    private static Func<string, DateTime?> NullableDateTimeConverter()//класс наподобие Action(экшн ничего не возвращает) но с возвращаемым знаечением
    {
        return (v) =>
        {
            if (DateTime.TryParse(v, out var value))
                return value;
            return null;
        };
    }
}