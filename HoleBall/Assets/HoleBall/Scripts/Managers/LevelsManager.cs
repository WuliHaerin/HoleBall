using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using LightDev;

namespace HoleBall
{
  public class LevelsManager : MonoBehaviour
  {
    private static int currentLevelIndex;

    private const string levelIndexKey = "level";

    public static int GetCurrentLevelIndex()
    {
      return currentLevelIndex % GetLevelsCount();
    }

    public static int GetLevelLength()
    {
      int distance = GenerationManager.GetLevels()[GetCurrentLevelIndex()].obstaclesPrefabs.Length;
      distance *= GenerationManager.GetDistanceBetweenObstacles();
      distance += GenerationManager.GetStartObstaclePosition();
      return distance;
    }

    public static float GetLevelProgress()
    {
      return Player.GetInstance().GetPositionZ() / GetLevelLength();
    }

    public static int GetLevelsCount()
    {
      return GenerationManager.GetLevels().Length;
    }

    private void Awake()
    {
      Events.GamePreReset += OnGamePreReset;
      Events.LevelFinished += OnLevelFinished;
    }

    private void OnDestroy()
    {
      Events.GamePreReset -= OnGamePreReset;
      Events.LevelFinished -= OnLevelFinished;
    }

    private void OnGamePreReset()
    {
      currentLevelIndex = PlayerPrefs.GetInt(levelIndexKey, 0);
    }

    private void OnLevelFinished()
    {
      PlayerPrefs.SetInt(levelIndexKey, (currentLevelIndex + 1));
    }

#if UNITY_EDITOR
    [MenuItem("HoleBall/Levels/Level 1", false, 2)]
    private static void ClearLevels1()
    {
      PlayerPrefs.SetInt(levelIndexKey, 0);
    }
    [MenuItem("HoleBall/Levels/Level 2", false, 2)]
    private static void ClearLevels2()
    {
      PlayerPrefs.SetInt(levelIndexKey, 1);
    }
    [MenuItem("HoleBall/Levels/Level 3", false, 2)]
    private static void ClearLevels3()
    {
      PlayerPrefs.SetInt(levelIndexKey, 2);
    }
    [MenuItem("HoleBall/Levels/Level 4", false, 2)]
    private static void ClearLevels4()
    {
      PlayerPrefs.SetInt(levelIndexKey, 3);
    }
    [MenuItem("HoleBall/Levels/Level 5", false, 2)]
    private static void ClearLevels5()
    {
      PlayerPrefs.SetInt(levelIndexKey, 4);
    }
#endif
  }
}
