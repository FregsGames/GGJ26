using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour, ITickeable, IController
{
    public enum Animations { Idle, MoveForward, MoveBack, MoveSide }

    [SerializeField]
    private SpriteRenderer sRenderer;
    [SerializeField]
    private SpriteRenderer maskSRenderer;

    [SerializeField]
    private List<Sprite> idleSprites;
    [SerializeField]
    private List<Sprite> frontSprites;
    [SerializeField]
    private List<Sprite> sideSprites;
    [SerializeField]
    private List<Sprite> backSprites;

    [SerializeField]
    private float[] animationDelays;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private MaskController maskController;

    private List<Sprite> currentSprites;
    private float timeSinceLastUpdate = 0.0f;
    private int index = 0;

    private Animations currentAnimation;

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

    public void UpdateMask()
    {
        switch (currentAnimation)
        {
            case Animations.Idle:
                maskSRenderer.sprite = maskController.CurrentSprites[0];
                maskSRenderer.sortingOrder = 3;
                break;
            case Animations.MoveForward:
                maskSRenderer.sprite = maskController.CurrentSprites[0];
                maskSRenderer.sortingOrder =3;
                break;
            case Animations.MoveBack:
                maskSRenderer.sprite = maskController.CurrentSprites[1];
                maskSRenderer.sortingOrder = 1;
                break;
            case Animations.MoveSide:
                maskSRenderer.sprite = maskController.CurrentSprites[2];
                maskSRenderer.sortingOrder = 3;
                break;
            default:
                break;
        }
    }

    public void ChangeAnimation(Animations a)
    {
        currentAnimation = a;
        UpdateMask();

        switch (a)
        {
            case Animations.Idle:
                sRenderer.sprite = idleSprites[0];
                index = 0;
                currentSprites = idleSprites;
                AnimationDelay = animationDelays[0];
                break;
            case Animations.MoveForward:
                sRenderer.sprite = frontSprites[0];
                index = 0;
                currentSprites = frontSprites;
                AnimationDelay = animationDelays[1];
                break;
            case Animations.MoveBack:
                sRenderer.sprite = backSprites[0];
                index = 0;
                currentSprites = backSprites;
                AnimationDelay = animationDelays[2];
                break;
            case Animations.MoveSide:
                sRenderer.sprite = sideSprites[0];
                index = 0;
                currentSprites = sideSprites;
                AnimationDelay = animationDelays[3];
                break;
            default:
                break;
        }
    }

    public void Tick()
    {
        if (currentSprites == null)
            return;

        if (playerController.Velocity.magnitude == 0)
            return;

        if (playerController.Velocity.magnitude != 0 && currentSprites == idleSprites)
        {
            if (playerController.Velocity.x != 0)
            {
                ChangeAnimation(Animations.MoveSide);
            }
            else if (playerController.Velocity.y != 0)
            {
                MoveFrontBack();
            }
        }
        else if (playerController.Velocity.magnitude != 0)
        {
            if (currentSprites == sideSprites && playerController.Velocity.x == 0)
            {
                MoveFrontBack();
            }
            else if (currentSprites != sideSprites && playerController.Velocity.y == 0)
            {
                ChangeAnimation(Animations.MoveSide);
            }
            else if (currentSprites == frontSprites && playerController.Velocity.y > 0)
            {
                MoveFrontBack();
            }
            else if (currentSprites == backSprites && playerController.Velocity.y < 0)
            {
                MoveFrontBack();
            }
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

    private void MoveFrontBack()
    {
        if (playerController.Velocity.y > 0)
        {
            ChangeAnimation(Animations.MoveBack);
        }
        else
        {
            ChangeAnimation(Animations.MoveForward);
        }
    }
}
