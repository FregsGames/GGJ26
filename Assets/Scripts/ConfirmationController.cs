using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmationController : MySerializedSingleton<ConfirmationController>
{
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private TextMeshProUGUI titleTMP;
    [SerializeField]
    private TextMeshProUGUI positiveTMP;
    [SerializeField]
    private TextMeshProUGUI negativeTMP;
    [SerializeField]
    private Button positiveButton;
    [SerializeField]
    private Button negativeButton;

    private bool answerReceived;
    private bool positiveAnswer;
    private UnityAction answerPressedPositive;
    private UnityAction answerPressedNegative;

    private CancellationTokenSource timerToken;


    public void Start()
    {
        answerPressedPositive = new UnityAction(() => ResolveAnswer(true));
        answerPressedNegative = new UnityAction(() => ResolveAnswer(false));

        container.SetActive(false);
    }

    public async UniTask<bool> AskForConfirmation(string text, string positive, string negative)
    {
        titleTMP.text = text;

        positiveTMP.text = positive;

        if (string.IsNullOrEmpty(negative))
        {
            negativeButton.gameObject.SetActive(false);
        }
        else
        {
            negativeButton.gameObject.SetActive(true);
            negativeTMP.text = negative;
        }

        answerReceived = false;

        container.SetActive(true);

        var token = this.GetCancellationTokenOnDestroy();

        positiveButton.onClick.AddListener(answerPressedPositive);
        negativeButton.onClick.AddListener(answerPressedNegative);

        BlockController.Instance.Block(container.transform);

        Time.timeScale = 0.0f;
        await UniTask.WaitUntil(() => answerReceived, cancellationToken: token);
        Time.timeScale = 1.0f;

        if (this == null || gameObject == null || token == null || token.IsCancellationRequested)
            return false;

        BlockController.Instance.Unblock(container.transform);

        container.SetActive(false);

        positiveButton.onClick.RemoveListener(answerPressedPositive);
        negativeButton.onClick.RemoveListener(answerPressedNegative);

        return positiveAnswer;
    }

    private void OnDestroy()
    {
        if (timerToken != null)
        {
            timerToken.Cancel();
        }
    }

    private void ResolveAnswer(bool positive)
    {
        if (timerToken != null)
        {
            timerToken.Cancel();
        }

        answerReceived = true;
        positiveAnswer = positive;
    }
}
