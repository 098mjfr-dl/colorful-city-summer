using DG.Tweening;
using UnityEngine;

public class ColorfulChange : MonoBehaviour
{
    public enum ColorChangeMode
    {
        随机 = 0
    }
    public bool 主开关 = true;

    public Material[] 材质球;

    public float 渐变时间 = 0.5f;

    public ColorChangeMode 渐变效果;

    public bool 启用雾气渐变效果;

    public bool 启用摄像机渐变效果;

    public float 摄像机摄像机渐变时间 = 0.5f;

    private Color previousBackgroundColor;

    private void Start()
    {
        if (启用雾气渐变效果)
        {
            RenderSettings.fog = true;
        }
        if (启用摄像机渐变效果)
        {
            previousBackgroundColor = Camera.main.backgroundColor;
        }
    }
    public void Change()
    {
        if (主开关)
        {
            for (int i = 0; i < 材质球.Length; i++)
            {
                Color targetColor = GetTargetColor();
                材质球[i].DOColor(targetColor, 渐变时间);
            }
            if (启用雾气渐变效果)
            {
                RenderSettings.fogColor = Random.ColorHSV();
            }
            if (启用摄像机渐变效果)
            {
                Color endValue = Random.ColorHSV();
                Camera.main.DOColor(endValue, 摄像机摄像机渐变时间);
            }
        }
    }
    public void ToggleMainSwitch(bool state)
    {
        主开关 = state;
    }
    private Color GetTargetColor()
    {
        if (渐变效果 == ColorChangeMode.随机)
        {
            return Random.ColorHSV();
        }
        return Color.white;
    }
}