using UnityEngine;

using LightDev;
using LightDev.Core;
using DG.Tweening;

namespace HoleBall
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(Collider))]
  public class NonPoolableObstacle : Base, IObstacle
  {
    private void Awake()
    {
      gameObject.layer = GameLayers.ObstacleOutHole;
      GetComponent<Rigidbody>().isKinematic = false;
      GetComponent<Collider>().isTrigger = false;
      
      Events.GamePreReset += OnGamePreReset;
    }

    private void Update()
    {
      if (GetPositionZ() + 20 < Player.GetInstance().GetPositionZ())
      {
        DestroyObstacle();
      }
    }

    private void OnGamePreReset()
    {
      DestroyObstacle();
    }

    private void DestroyObstacle()
    {
      Events.GamePreReset -= OnGamePreReset;
      Destroy(gameObject);
    }

    public void OnHitHoleLayerChanger(HoleLayerChanger layerChanger)
    {
      GetComponent<Rigidbody>().isKinematic = true;
      transform.parent = layerChanger.transform;
      gameObject.layer = GameLayers.ObstacleInHole;

      Sequence(
        MoveLocal(new Vector3(-GetLocalPositionX(), -5, -GetLocalPositionZ()), 0.2f).SetEase(Ease.InSine),
        OnFinish(() => DestroyObstacle())
      );
    }
  }
}
