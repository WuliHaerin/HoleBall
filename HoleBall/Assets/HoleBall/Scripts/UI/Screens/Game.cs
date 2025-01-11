using UnityEngine;
using UnityEngine.UI;

using LightDev;
using LightDev.UI;

namespace HoleBall.UI
{
  public class Game : CanvasElement
  {
    [Header("References")]
    public BaseText currentLevelText;
    public BaseText nextLevelText;

    [Space]
    public Image progressBar;

    public override void Subscribe()
    {
      Events.GameStart += Show;
      Events.GameSucceed += Hide;
      Events.GamePreReset += Hide;
    }

    public override void Unsubscribe()
    {
      Events.GameStart -= Show;
      Events.GameSucceed -= Hide;
      Events.GamePreReset -= Hide;
    }

    protected override void OnStartShowing()
    {
      currentLevelText.SetText(LevelsManager.GetCurrentLevelIndex() + 1);
      nextLevelText.SetText(LevelsManager.GetCurrentLevelIndex() + 2);
      progressBar.fillAmount = 0;
    }

    private void Update()
    {
      progressBar.fillAmount = LevelsManager.GetLevelProgress();
    }
  }
}
