using UnityEditor;

namespace HoleBall
{
  public class HoleBallEditor
  {
    [MenuItem("HoleBall/Skin Settings", false, 0)]
    private static void FindSkinSettings()
    {
      SelectUtil(nameof(SkinsSettings));
    }

    [MenuItem("HoleBall/Levels Settings", false, 1)]
    private static void FindLevelsSettings()
    {
      SelectUtil(nameof(GenerationSettings));
    }

    private static void SelectUtil(string className)
    {
      foreach (string guid in AssetDatabase.FindAssets($"t:{className}"))
      {
        Selection.activeInstanceID = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(guid)).GetInstanceID();
      }
    }
  }
}
