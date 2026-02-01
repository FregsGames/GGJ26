using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TooltipController : MySerializedSingleton<TooltipController>
{
    [SerializeField]
    private RectTransform container;
    [SerializeField]
    private TextMeshProUGUI titleTMP;
    [SerializeField]
    private TextMeshProUGUI contentTMP;

    public void Start()
    {
        container.gameObject.SetActive(false);
    }
    public void ShowTooltip(string title, string info, RectTransform r)
    {
        titleTMP.text = title;
        contentTMP.text = info;

        var pos = GameManager.Instance.Cam.WorldToScreenPoint(r.position);

        bool leftScreen = pos.x <= Screen.width / 2f;
        bool bottomScreen = pos.y <= Screen.height / 2f;

        container.pivot = new Vector2(leftScreen ? 0 : 1, bottomScreen ? 0 : 1);

        Vector3 offset = new Vector2(leftScreen ? r.rect.width / 2f : -r.rect.width / 2f, bottomScreen ? r.rect.height / 2f : -r.rect.height / 2f);

        container.transform.position = pos + offset;

        container.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        container.gameObject.SetActive(false);
    }
}
