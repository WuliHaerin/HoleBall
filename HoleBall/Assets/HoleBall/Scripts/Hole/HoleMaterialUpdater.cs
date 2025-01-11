using UnityEngine;

namespace HoleBall
{
  [ExecuteInEditMode]
  public class HoleMaterialUpdater : MonoBehaviour
  {
    public float radius = 1.5f;
    public Material[] materials;

    private readonly int holePositionID = Shader.PropertyToID("_HolePosition");
    private readonly int holeRadiusID = Shader.PropertyToID("_HoleRadius");

    private void UpdateMaterials()
    {
      if(materials == null) return;

      foreach (Material material in materials)
      {
        material.SetVector(holePositionID, transform.position);
        material.SetFloat(holeRadiusID, radius);
      }
    }

    private void Update()
    {
      if(!Application.isPlaying)
      {
        UpdateMaterials();
      }
    }

    private void LateUpdate()
    {
      UpdateMaterials();
    }
  }
}
