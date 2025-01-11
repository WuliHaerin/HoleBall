using UnityEngine;

using LightDev;
using LightDev.Pool;

namespace HoleBall
{
  public class RoadGenerator : MonoBehaviour
  {
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
      playerLastPosition = Player.GetInstance().GetPositionZ() + GenerationManager.GetStartRoadPosition();
      generationPosition = GenerationManager.GetStartRoadPosition();

      Generate();
      Generate();
    }

    private bool IsNeedGeneration()
    {
      return Player.GetInstance().GetPositionZ() > playerLastPosition + GenerationManager.GetRoadLength();
    }

    private void Generate()
    {
      PoolsManager.RetrieveElement<Road>(
        (element) => element.SetPosition(new Vector3(0, 0, generationPosition))
      );

      generationPosition += GenerationManager.GetRoadLength();
    }

    private void Update()
    {
      if(IsNeedGeneration())
      {
        playerLastPosition = Player.GetInstance().GetPositionZ();
        Generate();
      }
    }
  }
}
