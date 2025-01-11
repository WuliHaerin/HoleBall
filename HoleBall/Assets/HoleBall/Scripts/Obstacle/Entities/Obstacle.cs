using UnityEngine;

using LightDev;
using LightDev.Pool;
using DG.Tweening;

namespace HoleBall
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(Collider))]
  public class Obstacle : PoolableElement, IObstacle
  {
    private Rigidbody rb;

    private void Awake()
    {
      rb = GetComponent<Rigidbody>();
      GetComponent<Collider>().isTrigger = false;
      DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
      if (GetPositionZ() + 20 < Player.GetInstance().GetPositionZ())
      {
        PoolsManager.ReturnElement(this);
      }
    }

    public override void Subscribe()
    {
      Events.GamePreReset += OnGamePreReset;
    }

    public override void Unsubscribe()
    {
      Events.GamePreReset -= OnGamePreReset;
    }

    public override void OnRetrieved()
    {
      rb.isKinematic = false;
      rb.angularVelocity = Vector3.zero;
      rb.velocity = Vector3.zero;

      gameObject.layer = GameLayers.ObstacleOutHole;

      SetScale(1);
      SetRotation(Quaternion.identity);
    }

    public void OnHitHoleLayerChanger(HoleLayerChanger layerChanger)
    {
      rb.isKinematic = true;
      transform.parent = layerChanger.transform;
      gameObject.layer = GameLayers.ObstacleInHole;

      Sequence(
        MoveLocal(new Vector3(-GetLocalPositionX(), -5, -GetLocalPositionZ()), 0.2f).SetEase(Ease.InSine),
        OnFinish(() => PoolsManager.ReturnElement(this))
      );
    }

    private void OnGamePreReset()
    {
      PoolsManager.ReturnElement(this);
    }
  }
}
