using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RowMaker)), CanEditMultipleObjects]
public class RowMakerEditor : Editor
{
    //SerializedProperty copies;
    [SerializeField]
    public RowMaker rowMaker;

    void OnEnable()
    {
        rowMaker = (RowMaker)target;
        rowMaker.clean();
    }

    public override void OnInspectorGUI()
    {
        rowMaker.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", rowMaker.prefab, typeof(GameObject), false);

        rowMaker.copies = EditorGUILayout.IntSlider("Copies", rowMaker.copies, 0, 50);
        rowMaker.spacing = EditorGUILayout.FloatField("Spacing", rowMaker.spacing);
        rowMaker.angle = EditorGUILayout.Slider("Angle", rowMaker.angle, 0, 360);
        rowMaker.rotateObjects = EditorGUILayout.Toggle("Rotate Objects", rowMaker.rotateObjects);
        //rowMaker.twist = EditorGUILayout.Slider("Twist", rowMaker.twist, 0, 1);

        // update the preview whenever variable in RowMaker is changed
        rowMaker.preview();

        //if (EditorApplication.isPlayingOrWillChangePlaymode)
            //rowMaker.cleanCopies();
    }
}