using UnityEngine;

namespace HoleBall
{
  [CreateAssetMenu(fileName = "Skins", menuName = "HoleBall/Skins", order = 1)]
  public class SkinsSettings : ScriptableObject
  {
    public int defalutBallSkinIndex;
    public int defaultHoleSkinIndex;

    [Space]
    public SkinInfo[] ballSkins;
    public SkinInfo[] holeSkins;
  }
}
