using Assets.SimpleLocalization.Scripts;

public static class MyUtils
{
    public static string Localize(this string textToTranslate)
    {
        return LocalizationManager.Localize(textToTranslate);
    }
}
