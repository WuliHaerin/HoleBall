using UnityEngine;

namespace HoleBall
{
  [CreateAssetMenu(fileName = "GenerationSettings", menuName = "HoleBall/GenerationSettings", order = 2)]
  public class GenerationSettings : ScriptableObject
  {
    [Header("Road")]
    public int startRoadPosition;
    public int roadLength;

    [Header("Obstacles")]
    public int startObstaclesPosition;
    public int distanceBetweenObstacles;
    
    public LevelInfo[] levels;

    private void OnValidate()
    {
      if(levels == null) return;
      for(int i = 0; i < levels.Length; i++)
      {
        levels[i].name = "Level " + (i + 1);
      }
    }
  }
}
