using UnityEngine;
using LightDev.Core;

namespace HoleBall
{
  public class HoleHolder : Base
  {
    public float playerOffsetZ;

    private void Update()
    {
      SetPositionZ(Player.GetInstance().GetPositionZ() + playerOffsetZ);
    }
  }
}
