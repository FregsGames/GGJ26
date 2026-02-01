using Cysharp.Threading.Tasks;
using NavMeshPlus.Components;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, IController
{
    [SerializeField]
    private List<MapComponent> components;
    [SerializeField]
    private SpriteRenderer sRenderer;
    [SerializeField]
    private int size;
    [SerializeField]
    private int baseElementDensity;
    [SerializeField]
    private NavMeshSurface nav;
    [SerializeField]
    private BoxCollider2D confiner;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private PlayerController playerController;

    public async UniTask Prepare()
    {
        sRenderer.size = Vector2.one * size;
        foreach (var component in components)
        {
            if (component.justOne)
            {
                var pos = new Vector3(UnityEngine.Random.Range(-size * 0.75f / 2f, size * 0.75f / 2f), UnityEngine.Random.Range(-size * 0.75f / 2f, size * 0.75f / 2f), 0);

                while (Vector3.Distance(pos, Vector3.zero) < 2)
                {
                    pos = new Vector3(UnityEngine.Random.Range(-size * 0.75f / 2f, size * 0.75f / 2f), UnityEngine.Random.Range(-size * 0.75f / 2f, size * 0.75f / 2f), 0);
                }

                var c = Instantiate(component.prefab, sRenderer.transform);
                c.transform.position = pos;
            }
            else
            {
                var amount = baseElementDensity * size * component.densityMultiplier;

                for (int i = 0; i < amount; i++)
                {
                    var pos = new Vector3(UnityEngine.Random.Range(-size / 2f, size / 2f), UnityEngine.Random.Range(-size / 2f, size / 2f), 0);

                    while (Vector3.Distance(pos, Vector3.zero) < 2)
                    {
                        pos = new Vector3(UnityEngine.Random.Range(-size / 2f, size / 2f), UnityEngine.Random.Range(-size / 2f, size / 2f), 0);
                    }

                    var c = Instantiate(component.prefab, sRenderer.transform);
                    c.transform.position = pos;
                }
            }

        }
        playerController.bounds = size * .75f * 0.5f;
        confiner.size = Vector2.one * size * .75f;
        var confinerComponent = (CinemachineConfiner2D)cam.AddComponent(typeof(CinemachineConfiner2D));
        confinerComponent.BoundingShape2D = confiner;

        await UniTask.Yield();
        await nav.BuildNavMeshAsync();

        
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}


[Serializable]
public class MapComponent
{
    public GameObject prefab;
    public float densityMultiplier;
    public bool justOne;
}