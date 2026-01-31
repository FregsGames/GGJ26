using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sRenderer;

    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private float animationDelay;

    private List<Sprite> currentSprites;
    private float timeSinceLastUpdate = 0.0f;
    private int index = 0;

    private bool doingOnce;


    public void Setup()
    {
        sRenderer.sprite = sprites[0];
        index = 0;
        currentSprites = sprites;
    }

    public async void DoOnce(List<Sprite> animation)
    {
        doingOnce = true;

        foreach (Sprite s in animation)
        {
            sRenderer.sprite = s;
            await UniTask.Delay(Mathf.RoundToInt(1000* animationDelay));
        }

        doingOnce = false;
    }

    public void Tick(NavMeshAgent navMesh, Transform player)
    {
        if (currentSprites == null || doingOnce)
            return;

        if((player.position - transform.position).normalized.x < 0 && sRenderer.transform.localScale.x > 0)
        {
            sRenderer.transform.localScale = new Vector3(-1, 1, 1);
        }

        if ((player.position - transform.position).normalized.x > 0 && sRenderer.transform.localScale.x < 0)
        {
            sRenderer.transform.localScale = new Vector3(1, 1, 1);
        }

        if (navMesh.velocity.magnitude != 0)
        {
            
            if (timeSinceLastUpdate >= animationDelay)
            {
                index++;

                if (index >= currentSprites.Count)
                {
                    index = 0;
                    currentSprites = sprites;
                }

                sRenderer.sprite = currentSprites[index];
                timeSinceLastUpdate = 0.0f;
            }
            else
            {
                timeSinceLastUpdate += Time.deltaTime;
            }
        }
        else
        {
            sRenderer.sprite = currentSprites[0];
        }
    }
}
