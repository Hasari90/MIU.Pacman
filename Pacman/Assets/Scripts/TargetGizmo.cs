using UnityEngine;
using System.Collections;
using System;

public class TargetGizmo : MonoBehaviour {


    public GameObject Ghost;

    void Start()
    {

    }
    void Update()
    {
        if (Ghost.GetComponent<AI>().targetTile != null)
        {
            Vector3 pos = new Vector3(Ghost.GetComponent<AI>().targetTile.x, Ghost.GetComponent<AI>().targetTile.y, 0f);
            transform.position = pos;
        }
    }
}
