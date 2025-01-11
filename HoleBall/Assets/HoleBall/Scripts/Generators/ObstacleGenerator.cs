using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class ObstacleGenerator : MonoBehaviour
  {
    private GameObject[] currentLevelObstaclesPrefabs;

    private int generationIndex;
    private int generationPosition;
    private float playerLastPosition;

    private void Awake()
    {
      Events.GamePostReset += OnGamePostReset;
    }

    private void OnDestroy()
    {
      Events.GamePostReset -= OnGamePostReset;
    }

    private void OnGamePostReset()
    {
      currentLevelObstaclesPrefabs = GenerationManager.GetLevels()[LevelsManager.GetCurrentLevelIndex()].obstaclesPrefabs;

      generationIndex = 0;
      generationPosition = GenerationManager.GetStartObstaclePosition();
      playerLastPosition = Player.GetInstance().GetPositionZ();

      Generate();
    }

    private void Generate()
    {
      Instantiate(currentLevelObstaclesPrefabs[generationIndex], new Vector3(0, 0, generationPosition), Quaternion.identity);
      generationPosition += GenerationManager.GetDistanceBetweenObstacles();
      playerLastPosition = Player.GetInstance().GetPositionZ();
      generationIndex++;
    }

    private bool IsNeedGeneration()
    {
      if (generationIndex == currentLevelObstaclesPrefabs.Length) return false;
      if (Player.GetInstance().GetPositionZ() < playerLastPosition + GenerationManager.GetDistanceBetweenObstacles()) return false;

      return true;
    }

    private void Update()
    {
      if(IsNeedGeneration())
      {
        Generate();
      }
    }
  }
}
