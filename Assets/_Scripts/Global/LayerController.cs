using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LayerController : MonoBehaviour
{
    public static LayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public int Ground;
    public int Fighter;
}

#region << CustomInspector >>

[CustomEditor(typeof(LayerController))]
public class LayerManagerEditor : Editor
{
    SerializedObject layerController;
    LayerController mainScript;

    SerializedProperty GroundLayer;
    SerializedProperty FighterLayer;

    private void OnEnable()
    {
        layerController = serializedObject;
        mainScript = (LayerController)target;

        GroundLayer = layerController.FindProperty(nameof(LayerController.Ground));
        FighterLayer = layerController.FindProperty(nameof(LayerController.Fighter));
    }

    public override void OnInspectorGUI()
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label("LAYERS", EditorStyles.boldLabel);
            LayerSelection();
        }

        layerController.ApplyModifiedProperties();
    }

    private void LayerSelection()
    {
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Ground", EditorStyles.helpBox);
                GroundLayer.intValue = EditorGUILayout.LayerField(GroundLayer.intValue);
            }

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Fighter", EditorStyles.helpBox);
                FighterLayer.intValue = EditorGUILayout.LayerField(FighterLayer.intValue);
            }
        }
    }
}
#endregion
