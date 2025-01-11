using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class HoleLayerChanger : MonoBehaviour
  {
    private void OnTriggerEnter(Collider collider)
    {
      Events.ObstacleHitLayerChanger.Call();
      collider.GetComponent<IObstacle>().OnHitHoleLayerChanger(this);
    }
  }
}
