using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class SkinChanger : MonoBehaviour
  {
    public Material ballMaterial;
    public Material holeMaterial;

    private Texture startBallTexture;
    private Texture startHoleTexture;

    private readonly int TextureID = Shader.PropertyToID("_MainTex");

    private void Awake()
    {
      startBallTexture = ballMaterial.GetTexture(TextureID);
      startHoleTexture = ballMaterial.GetTexture(TextureID);

      Events.BallSkinSelected += OnBallSkinSelected;
      Events.HoleSkinSelected += OnHoleSkinSelected;
    }

    private void Start()
    {
      OnBallSkinSelected(SkinManager.GetBallCurrentSkin().index);
      OnHoleSkinSelected(SkinManager.GetHoleCurrentSkin().index);
    }

    private void OnDestroy()
    {
      Events.BallSkinSelected -= OnBallSkinSelected;
      Events.HoleSkinSelected -= OnHoleSkinSelected;

      ballMaterial.SetTexture(TextureID, startBallTexture);
      holeMaterial.SetTexture(TextureID, startHoleTexture);
    }

    private void OnBallSkinSelected(int index)
    {
      ballMaterial.SetTexture(TextureID, SkinManager.GetBallSkin(index).skinTexture);
    }

    private void OnHoleSkinSelected(int index)
    {
      holeMaterial.SetTexture(TextureID, SkinManager.GetHoleSkin(index).skinTexture);
    }
  }
}
