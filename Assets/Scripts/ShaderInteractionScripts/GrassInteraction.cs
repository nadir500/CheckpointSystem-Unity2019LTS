using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GrassInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public Material mat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Shader.SetGlobalVector("_PlayerPosition", transform.position);
       // mat.SetVector("_PlayerPos",transform.position);
    }
}
