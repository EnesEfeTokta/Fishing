#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class DBSave
{
    public DBSave(ScriptableObject scriptableObject)
    {
        #if UNITY_EDITOR
        EditorUtility.SetDirty(scriptableObject);
        #endif
    }
}
