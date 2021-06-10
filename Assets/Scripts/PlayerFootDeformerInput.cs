using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootDeformerInput : MonoBehaviour
{
    [SerializeField] private float force = 10.0f;
    [SerializeField] private float forceOffset = 0.1f;

    private Vector3 lastPos; 
    // Start is called before the first frame update
    void Start()
    {
        lastPos = this.transform.position;
    }

    private void LateUpdate()
    {
          // currentPos = this.transform.position;
          // float velocity = ((currentPos - prevPos).magnitude) / Time.deltaTime;
          // Debug.Log("velocity " + velocity);
          

// At each frame:

    }

    // Update is called once per frame
    void Update()
    {
        var displacement = transform.position - lastPos;
        lastPos = transform.position; 
        // Debug.Log("feet mag " + displacement.magnitude);
        Ray _ray;
        RaycastHit _raycastHit;
        _ray = new Ray(this.transform.position, Vector3.down);
 
        if (Physics.Raycast(_ray, out _raycastHit, 0.7f))
        {
            // Debug.Log("raycast hit " + _raycastHit.collider.name);
            Debug.DrawLine(_ray.origin, _raycastHit.point,Color.magenta);
            
            // Debug.Log("feet mag " + this.transform.position.);
            
            MeshDeformer deformer = _raycastHit.collider.GetComponent<MeshDeformer>();
            if (deformer && displacement.magnitude >=0.02f)
            {
                Vector3 point = _raycastHit.point;
                point += _raycastHit.normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        }
    }
}