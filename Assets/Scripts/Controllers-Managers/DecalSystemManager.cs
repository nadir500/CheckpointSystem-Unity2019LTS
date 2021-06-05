using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public enum DecalMode
{
    FaceGround,
    FaceDirection
}

public class DecalSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject decalParent;

    [SerializeField] private GameObject decalPrefab;
    private DecalProjector[] _decalProjectors;

    /// <summary>
    /// initialize group of decals with object pooling in mind 
    /// </summary>
    /// <param name="decalGroupName">decal generated objects' parent</param>
    /// <param name="decalMaterial">decal material</param>
    /// <param name="poolSize"></param>
    /// <param name="expandable"></param>
    public DecalProjector[] InitializeDecals(string decalGroupName, int poolSize,
        bool expandable) //generate with object pooling 
    {
        GameObject parent = new GameObject(decalGroupName);
        _decalProjectors = new DecalProjector[poolSize];
        parent.transform.SetParent(decalParent.transform);
        ObjectsPoolHelper objectsPoolHelper = new ObjectsPoolHelper(decalPrefab, poolSize, expandable, parent.name);
        for (int i = 0; i < objectsPoolHelper.Pool.Length; i++)
        {
            _decalProjectors[i] = objectsPoolHelper.Pool[i].GetComponent<DecalProjector>();
            // _decalProjectors[i].;
        }

        return _decalProjectors;
    }

    /// <summary>
    /// after initialization you should set decals parameters for the decal effect 
    /// </summary>
    public void SetDecalValues(DecalProjector[] decalProjectors, Vector3 decalSize, Material decalMaterial,
        float decalDrawDistance, float decalFadeScale)
    {
        for (int i = 0; i < decalProjectors.Length; i++)
        {
            decalProjectors[i].size = decalSize;
            decalProjectors[i].material = decalMaterial;
            decalProjectors[i].drawDistance = decalDrawDistance;
            decalProjectors[i].fadeScale = decalFadeScale;
        }
    }

    public void ShowDecal(DecalProjector[] decalProjectors, Vector3 hitTargetPoint, Vector3 hitTargetNormal,
        DecalMode decalMode, float duration = 0)
    {
        Transform activeChild = null;
        if (decalMode == DecalMode.FaceDirection)
        {
            //first find the first inactive 
            for (int i = 0; i < decalProjectors.Length; i++)
            {
                if (!decalProjectors[i].gameObject.activeSelf)
                {
                    activeChild = decalProjectors[i].transform;
                    break;
                }
            }

            activeChild.position = hitTargetPoint;
            activeChild.rotation = Quaternion.FromToRotation(Vector3.forward, hitTargetNormal);
            activeChild.gameObject.SetActive(true);
            StartCoroutine(FadeInDecal(activeChild.GetComponent<DecalProjector>(), duration));
        }
    }

    IEnumerator FadeInDecal(DecalProjector decalProjector, float duration = 0)
    {
        float elapsedTime = 0.0f;
        float waitTime = 3.0f;
        float lerpValue = 0;
        yield return new WaitForSeconds(duration);
        while (lerpValue < 1 )
        {
            //do lerp  on opacity 
            lerpValue = Mathf.Lerp(1, 0, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        }

        decalProjector.gameObject.SetActive(false);
    }
}