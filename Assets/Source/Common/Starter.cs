using System.Collections.Generic;
using UnityEngine;

public abstract class Starter : MonoBehaviour
{
    private List<IInitable> _initables;
    private List<IDeinitable> _deinitables;

    private void Awake()
    {
        _initables = new List<IInitable>();
        _deinitables = new List<IDeinitable>();
    }

    private void Start()
    {
        OnStart();
        Init();
    }

    protected abstract void OnStart();

    protected T Register<T>(T service)
    {
        if (service is IInitable)
        {
            _initables.Add(service as IInitable);
        }
        if (service is IDeinitable)
        {
            _deinitables.Add(service as IDeinitable);
        }

        return service;
    }

    private void OnDestroy()
    {
        Deinit();
    }

    private void Init()
    {
        for (int i = 0; i < _initables.Count; i++)
        {
            _initables[i].Init();
        }
    }

    private void Deinit()
    {
        for (int i = 0; i < _deinitables.Count; i++)
        {
            _deinitables[i].Deinit();
        }
    }
}
