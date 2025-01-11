using LightDev;
using LightDev.Pool;

namespace HoleBall
{
  public class Road : PoolableElement
  {
    public override void Subscribe()
    {
      Events.GamePreReset += OnGamePreReset;
    }

    public override void Unsubscribe()
    {
      Events.GamePreReset -= OnGamePreReset;
    }

    private void OnGamePreReset()
    {
      PoolsManager.ReturnElement(this);
    }

    private void Update()
    {
      if(GetPositionZ() + GenerationManager.GetRoadLength() * 2 < Player.GetInstance().GetPositionZ())
      {
        PoolsManager.ReturnElement(this);
      }
    }
  }
}
