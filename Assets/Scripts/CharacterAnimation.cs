using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour, ITickeable, IController
{
    public enum Animations { Idle, Run }

    [SerializeField]
    private SpriteRenderer sRenderer;

    [SerializeField]
    private List<Sprite> idleSprites;
    [SerializeField]
    private List<Sprite> moveSprites;

    [SerializeField]
    private float[] animationDelays;

    [SerializeField]
    private PlayerController playerController;

    private List<Sprite> currentSprites;
    private float timeSinceLastUpdate = 0.0f;
    private int index = 0;

    public float AnimationDelay { get; set; }

    public UniTask Prepare()
    {
        sRenderer.sprite = idleSprites[0];
        index = 0;
        currentSprites = idleSprites;
        AnimationDelay = animationDelays[0];
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void ChangeAnimation(Animations a)
    {
        switch (a)
        {
            case Animations.Idle:
                sRenderer.sprite = idleSprites[0];
                index = 0;
                currentSprites = idleSprites;
                AnimationDelay = animationDelays[0];
                break;
            case Animations.Run:
                sRenderer.sprite = moveSprites[0];
                index = 0;
                currentSprites = moveSprites;
                AnimationDelay = animationDelays[1];
                break;
            default:
                break;
        }
    }

    public void Tick()
    {
        if (currentSprites == null)
            return;

        if (playerController.Velocity.magnitude != 0 && currentSprites == idleSprites)
        {
            ChangeAnimation(Animations.Run);
        }
        else if (playerController.Velocity.magnitude == 0 && currentSprites == moveSprites)
        {
            ChangeAnimation(Animations.Idle);
        }

        if (timeSinceLastUpdate >= AnimationDelay)
        {
            index++;

            if (index >= currentSprites.Count)
            {
                index = 0;
            }

            sRenderer.sprite = currentSprites[index];
            timeSinceLastUpdate = 0.0f;

            if (currentSprites != idleSprites)
            {
                if (index == 0)
                {
                    //AudioController.Instance.PlayRandom(step);
                }
            }
        }
        else
        {
            timeSinceLastUpdate += Time.deltaTime;
        }
    }
}
