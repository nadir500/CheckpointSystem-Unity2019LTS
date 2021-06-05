using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ShootTestManager : MonoBehaviour
{
    [SerializeField] private DecalSystemManager _decalSystemManager;
    [SerializeField] private Material _decalMaterial;
    private DecalProjector[] _decalProjectors;

    private void Start()
    {
        _decalProjectors = _decalSystemManager.InitializeDecals("_BloodGroup_Decal", 5, false);
        _decalSystemManager.SetDecalValues(_decalProjectors, new Vector3(1, 1, 1), _decalMaterial, 100, 1);
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            ActivateDecal();
        }
    }

    private void ActivateDecal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            _decalSystemManager.ShowDecal(_decalProjectors,hit.point, hit.normal,DecalMode.FaceDirection,1);
        }
    }
}