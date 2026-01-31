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


    public void Setup()
    {
        sRenderer.sprite = sprites[0];
        index = 0;
        currentSprites = sprites;
    }

    public void Tick(NavMeshAgent navMesh)
    {
        if (currentSprites == null)
            return;

        if(navMesh.velocity.magnitude != 0)
        {
            sRenderer.flipX = navMesh.velocity.x < 0;

            if (timeSinceLastUpdate >= animationDelay)
            {
                index++;

                if (index >= currentSprites.Count)
                {
                    index = 0;
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
