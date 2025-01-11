using UnityEngine;

using LightDev;
using LightDev.Core;

namespace HoleBall
{
  public class Hole : Base
  {
    [Header("Limitations")]
    public Vector2 center;
    public Vector2 extent;

    private void Awake()
    {
      Events.GamePreReset += OnGamePreReset;
    }

    private void OnDestroy()
    {
      Events.GamePreReset -= OnGamePreReset;
    }

    private void OnGamePreReset()
    {
      SetLocalPosition(Vector3.zero);
    }

    private void UpdatePosition()
    {
      Vector3 localPosition = GetLocalPosition();
      localPosition.x += InputManager.GetHorizontal();
      localPosition.z += InputManager.GetVertical();

      if (localPosition.x > center.x + extent.x)
      {
        localPosition.x = center.x + extent.x;
      }
      else if (localPosition.x < center.x - extent.x)
      {
        localPosition.x = center.x - extent.x;
      }

      if (localPosition.z > center.y + extent.y)
      {
        localPosition.z = center.y + extent.y;
      }
      else if (localPosition.z < center.y - extent.y)
      {
        localPosition.z = center.y - extent.y;
      }

      SetLocalPosition(localPosition);
    }

    private void Update()
    {
      UpdatePosition();
    }
  }
}
