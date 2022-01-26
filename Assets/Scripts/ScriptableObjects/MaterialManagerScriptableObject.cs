using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Lighthouse2D/ MaterialManagerScriptableObject", order = 1)]
public class MaterialManagerScriptableObject : ScriptableObject
{
    public List<Material> materials;

    public bool materialsEffectEnable;

    private void OnValidate()
    {
        foreach (Material m in materials)
        {
            m.SetInt("_Enable", materialsEffectEnable ? 1 : 0);
        }
    }

    public void EnableMaterialsEffect()
    {
        foreach (Material m in materials)
        {
            m.SetInt("_Enable", 1);
        }
    }
}