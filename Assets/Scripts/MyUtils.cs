using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public static class MyUtils
{
    public static string Localize(this string textToTranslate)
    {
        return LocalizationManager.Localize(textToTranslate);
    }

    public static void DestroyChildren(Transform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(t.GetChild(i).gameObject);
        }
    }
}
