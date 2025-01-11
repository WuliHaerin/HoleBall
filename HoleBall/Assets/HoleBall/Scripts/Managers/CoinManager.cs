using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class CoinManager : MonoBehaviour
  {
    // How much coins will be added for level finish
    public int coinsReward = 3000;
    private static int reward;

    private const string CoinKey = "Coins";

    public static int GetLevelReward() { return (LevelsManager.GetCurrentLevelIndex() + 1) * reward; }
    public static int GetCoinsCount() { return PlayerPrefs.GetInt(CoinKey, 0); }
    public static bool IsCoinsEnough(int coins) { return GetCoinsCount() >= coins; }

    public static void DecreaseCoinsCount(int coins)
    {
      ChangeCoinsCount(-coins);
    }

    public static void IncreaseCoinsCount(int coins)
    {
      ChangeCoinsCount(coins);
    }

    private void Awake()
    {
      reward = coinsReward;
      Events.LevelFinished += OnLevelFinished;
    }

    private void OnDestroy()
    {
      Events.LevelFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
      IncreaseCoinsCount(GetLevelReward());
    }

    private static void ChangeCoinsCount(int coins)
    {
      PlayerPrefs.SetInt(CoinKey, GetCoinsCount() + coins);
      Events.CoinCountChanged.Call();
    }
  }
}
