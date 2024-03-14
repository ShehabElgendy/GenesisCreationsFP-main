using System;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField]
    private string id = Guid.NewGuid().ToString();

    public string ID => id;

    [ContextMenu("Generate ID")]
    private void Generate()
    {
        id = Guid.NewGuid().ToString();
    }
}
