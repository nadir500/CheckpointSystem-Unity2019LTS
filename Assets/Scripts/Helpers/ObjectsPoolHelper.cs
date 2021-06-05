using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectsPoolHelper
{
    //we use this class to manage and objectpool method for more efficiency to the memory resources
    //plus managing the behavior of those instantiated objects in the memory 
    private bool _isDoneLoading;
    private GameObject _prefab;
    private GameObject[] _pool;
    private List<GameObject> _loosePool;
    private int _poolSize;
    private int _maxLooseSize;
    private bool _expandable;
    private StringBuilder stringBuilder;

    public GameObject[] Pool
    {
        get { return _pool; }
    }

    public List<GameObject> LoosePool
    {
        get { return _loosePool; }
    }

    public ObjectsPoolHelper(GameObject prefab, int poolSize, bool expandable = true, string parent = "")
    {
        _prefab = prefab;
        _poolSize = poolSize;
        _maxLooseSize = _poolSize / 4;
        var defaultPos = new Vector3();
        Transform parentTrans = null;
        if (!string.IsNullOrEmpty(parent))
        {
            parentTrans = GameObject.Find(parent).transform;
        }

        _pool = new GameObject[_poolSize];
        _loosePool = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            var gm = (GameObject) GameObject.Instantiate(_prefab, defaultPos, Quaternion.identity);
            gm.SetActive(false);
            _pool[i] = gm;
            if (parentTrans != null)
            {
                gm.transform.parent = parentTrans;
            }
        }

        _expandable = expandable;
        _isDoneLoading = true;
    }

    public GameObject RetrieveInstance(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            stringBuilder = new StringBuilder();
            var gm = _pool[i];
            if (gm != null && !gm.activeSelf)
            {
                gm.transform.position = position;
                gm.transform.rotation = rotation;
                gm.SetActive(true);
                stringBuilder.Append(" ");
                stringBuilder.Append(i);
                gm.name += stringBuilder;
                return gm;
            }
        }

        if (_expandable)
        {
            var newGm = (GameObject) GameObject.Instantiate(_prefab, position, rotation);

            _loosePool.Add(newGm);
            if (_loosePool.Count >= _maxLooseSize)
            {
                var temp = _pool.ToList();
                temp.AddRange(_loosePool);
                _pool = temp.ToArray();
                _loosePool.Clear();
            }

            return newGm;
        }

        return null;
    }

    public void DeactivateAll()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Debug.Log("pool size " + _poolSize + " and game object car " + _pool[i].name);
            _pool[i].SetActive(false);
        }
    }

    public void ActivateAll()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Debug.Log("pool size " + _poolSize + " and game object car " + _pool[i].name);
            _pool[i].SetActive(true);
        }
    }

    public bool IsDoneLoading()
    {
        return _isDoneLoading;
    }
}