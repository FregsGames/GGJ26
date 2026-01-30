using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IController
{
    public UniTask Prepare();
    public UniTask Setup();
}
