using System.Linq;
using UnityEngine;
using UnityEditor;

namespace HoleBall
{
  [CustomEditor(typeof(ObstaclesHolder))]
  public class ObstacleHolderEditor : Editor
  {
    private GameObject visualPrefab;

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      ShowFillButton();
      ShowVisualization();
    }

    private void ShowFillButton()
    {
      GUILayout.Space(6);
      if (GUILayout.Button("Fill obstacles positions", GUILayout.Width(160), GUILayout.Height(30)))
      {
        ((ObstaclesHolder)target).positions = GetLocalPositionsFromTransforms(GetChildTransforms());
        ((ObstaclesHolder)target).rotations = GetLocalRotationsFromTransforms(GetChildTransforms());
        serializedObject.ApplyModifiedProperties();
      }
    }

    private void ShowVisualization()
    {
      GUILayout.Space(14);
      EditorGUILayout.LabelField("Visualization", EditorStyles.boldLabel);
      visualPrefab = EditorGUILayout.ObjectField("Visualize object", visualPrefab, typeof(GameObject), true) as GameObject;
      ShowRemoveChildrenButton();
      ShowVisualizeButton();
    }

    private void ShowRemoveChildrenButton()
    {
      GUILayout.Space(5);
      if (GUILayout.Button("Remove child objects", GUILayout.Width(160), GUILayout.Height(30)))
      {
        var transforms = GetChildTransforms();
        for (int i = transforms.Length - 1; i >= 0; i--)
        {
          DestroyImmediate(transforms[i].gameObject);
        }
      }
    }

    private void ShowVisualizeButton()
    {
      GUILayout.Space(5);
      if (GUILayout.Button("Visualize obstacles", GUILayout.Width(160), GUILayout.Height(30)))
      {
        if (((ObstaclesHolder)target).positions != null && visualPrefab != null)
        {
          Transform parent = ((ObstaclesHolder)target).transform;
          Vector3 parentPosition = parent.position;
          Quaternion parentRotation = parent.rotation;
          for (int i = 0; i < ((ObstaclesHolder)target).positions.Length; i++)
          {
            var obj = Instantiate(visualPrefab);
            obj.transform.parent = parent;
            obj.transform.localPosition = ((ObstaclesHolder)target).positions[i];
            obj.transform.localEulerAngles = ((ObstaclesHolder)target).rotations[i];
          }
        }
      }
    }

    private Transform[] GetChildTransforms()
    {
      return ((ObstaclesHolder)target).GetComponentsInChildren<Transform>().Where(
        go => go.gameObject != ((ObstaclesHolder)target).gameObject
      ).ToArray();
    }

    private Vector3[] GetLocalPositionsFromTransforms(Transform[] transforms)
    {
      return transforms.Select((t) => t.localPosition).ToArray(); ;
    }

    private Vector3[] GetLocalRotationsFromTransforms(Transform[] transforms)
    {
      return transforms.Select((t) => t.localEulerAngles).ToArray(); ;
    }
  }
}
