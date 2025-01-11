using System;
using System.Collections;

using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class ObstaclesHolder : MonoBehaviour
  {
    public ObstacleType obstaclesType;
    public Vector3[] positions;
    public Vector3[] rotations;

    public event Action onObstaclesSpawnFinished;
    public event Action<Transform> onObstacleSpawned;

    // How much obstacles can be generated in one frame, used for optimization
    private const int maxGeneratedObstaclesInOneFrame = 30;

    private void Start()
    {
      StartCoroutine(GenerateObstacles());
      Events.GamePreReset += OnGamePreReset;
    }

    private void OnGamePreReset()
    {
      DestroyObject();
    }

    private void DestroyObject()
    {
      Events.GamePreReset -= OnGamePreReset;
      Destroy(gameObject);
    }

    private IEnumerator GenerateObstacles()
    {
      for (int i = 0; i < positions.Length; i++)
      {
        if(i == maxGeneratedObstaclesInOneFrame)
        {
          yield return null;
        }
        var element = ObstacleFactory.GetObstacle(obstaclesType);
        element.SetPosition(transform.position + positions[i]);
        element.SetLocalRotation(rotations[i]);
        onObstacleSpawned?.Invoke(element.transform);
      }

      onObstaclesSpawnFinished?.Invoke();
    }
  }
}
