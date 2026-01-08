using DG.Tweening;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
#endif

public class ColorfulChange : MonoBehaviour
{
    public enum ColorChangeMode { 随机 = 0 }
    
    public bool 主开关 = true;
    public Material[] 材质球;
    public float 渐变时间 = 0.5f;
    public ColorChangeMode 渐变效果;
    public bool 启用雾气渐变效果;
    public bool 启用摄像机渐变效果;
    public float 摄像机渐变时间 = 0.5f;

    private void Start()
    {
        if (启用雾气渐变效果) RenderSettings.fog = true;
    }

    public void Change()
    {
        if (!主开关) return;

        for (int i = 0; i < 材质球.Length; i++)
        {
            if (材质球[i] != null)
                材质球[i].DOColor(GetTargetColor(), 渐变时间);
        }

        if (启用雾气渐变效果)
        {
            DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, Random.ColorHSV(), 渐变时间);
        }

        if (启用摄像机渐变效果 && Camera.main != null)
        {
            Camera.main.DOColor(Random.ColorHSV(), 摄像机渐变时间);
        }
    }

    private Color GetTargetColor()
    {
        return 渐变效果 == ColorChangeMode.随机 ? Random.ColorHSV() : Color.white;
    }

    // --- 编辑器功能部分 ---
#if UNITY_EDITOR
    [ContextMenu("从文件夹导入材质球")]
    public void ImportMaterials()
    {
        string path = EditorUtility.OpenFolderPanel("选择材质文件夹", "Assets", "");
        if (string.IsNullOrEmpty(path)) return;

        if (path.StartsWith(Application.dataPath))
        {
            path = "Assets" + path.Substring(Application.dataPath.Length);
        }
        else
        {
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { path });
        List<Material> mats = new List<Material>();

        foreach (string guid in guids)
        {
            Material m = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(guid));
            if (m != null) mats.Add(m);
        }

        材质球 = mats.ToArray();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}