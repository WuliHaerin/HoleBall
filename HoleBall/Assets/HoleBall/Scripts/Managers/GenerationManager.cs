using UnityEngine;

namespace HoleBall
{
  public class GenerationManager : MonoBehaviour
  {
    public GenerationSettings generationSettings;
    private static GenerationSettings settings;

    public static int GetStartRoadPosition() { return settings.startRoadPosition; }
    public static int GetRoadLength() { return settings.roadLength; }

    public static int GetStartObstaclePosition() { return settings.startObstaclesPosition; }
    public static int GetDistanceBetweenObstacles() { return settings.distanceBetweenObstacles; }

    public static LevelInfo[] GetLevels() { return settings.levels; }

    private void Awake()
    {
      settings = generationSettings;
    }
  }
}
