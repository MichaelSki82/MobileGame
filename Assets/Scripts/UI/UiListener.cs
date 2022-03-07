using TMPro;

public class UiListener : IEnemy
{
    private readonly TMP_Text _countPower;
    private readonly TMP_Text _countMoney;
    private readonly TMP_Text _countHealth;
    private readonly TMP_Text _crimeStatusPlayerText;

    public UiListener(TMP_Text countPower, TMP_Text countMoney, TMP_Text countHealth, TMP_Text crimeStatusPlayerText)
    {
        _countPower = countPower;
        _countMoney = countMoney;
        _countHealth = countHealth;
        _crimeStatusPlayerText = crimeStatusPlayerText;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoney.text = $"Player money: {dataPlayer.CountMoney}";
                break;

            case DataType.Health:
                _countHealth.text = $"Player health: {dataPlayer.CountHealth}";
                break;

            case DataType.Power:
                _countPower.text = $"Player power: {dataPlayer.CountPower}";
                break;
            case DataType.Crime:
                _crimeStatusPlayerText.text = $"Player crime: {dataPlayer.CountCrime}";
                break;
        }
    }
}