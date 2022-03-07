using UnityEngine;

public class Enemy : IEnemy
{
    //коэфициенты для расчета формулы урона 
    private const int KHealth = 3;
    private const float KPower = 1.5f;
    private const int _maxMoneyPlayer = 20;
    private const int _maxHealthPlayer = 30;
    private const int KKnifePower = 15;

    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _crimePlayer;

    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _healthPlayer = dataPlayer.CountHealth;
                break;

            case DataType.Money:
                _moneyPlayer = dataPlayer.CountMoney;
                break;

            case DataType.Power:
                _powerPlayer = dataPlayer.CountPower;
                break;
            case DataType.Crime:
                _crimePlayer = dataPlayer.CountCrime;
                break;
        }

        Debug.Log($"Update {_name}, change {dataType}");
    }

    public int Power
    {
        get
        {
            //var power = _moneyPlayer + _healthPlayer - _powerPlayer;
            var kMoney = _moneyPlayer > _maxMoneyPlayer ? 50 : 5;
            var kHealthFight = _healthPlayer > _maxHealthPlayer ? 1 : 5;
            var power = (int)(_healthPlayer / KHealth + kMoney - kHealthFight + _powerPlayer / KPower+_crimePlayer);
            return power;
        }
    }

    public int KnifePower
    {
        get
        {
            var power = _moneyPlayer - _powerPlayer + (_healthPlayer /  KKnifePower) +_crimePlayer ;
            return power;
        }
    }
}
