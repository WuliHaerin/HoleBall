using LightDev.Core;

namespace HoleBall
{
  public class CameraHolder : Base
  {
    private void Update()
    {
      SetPosition(Player.GetInstance().GetPosition());
    }
  }
}
