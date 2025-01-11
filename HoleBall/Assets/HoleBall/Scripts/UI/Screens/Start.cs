using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace HoleBall.UI
{
  public class Start : CanvasElement
  {
    public Base logo;
    public Base finger;

    public override void Subscribe()
    {
      Events.GamePostReset += Show;
      Events.CloseStore += Show;
      Events.GameStart += Hide;
    }

    public override void Unsubscribe()
    {
      Events.GamePostReset -= Show;
      Events.CloseStore -= Show;
      Events.GameStart -= Hide;
    }

    protected override void OnStartShowing()
    {
      AnimateLogo();
      AnimateFinger();
    }

    private void AnimateLogo()
    {
      logo.SetPositionY(300);
      logo.Sequence(
        logo.MoveY(-274, 0.5f).SetEase(Ease.OutBack)
      );
    }

    private void AnimateFinger()
    {
      finger.SetPositionX(-140);
      finger.Sequence(
        finger.MoveX(210, 1.3f).SetEase(Ease.InOutQuad)
      ).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnShowStore()
    {
      Events.ShowStore.Call();
      Hide();
    }
  }
}
