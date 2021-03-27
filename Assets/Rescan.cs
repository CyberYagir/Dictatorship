using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescan : MonoBehaviour
{
    public static Rescan r;
    public MeshCollider water;
    void Start()
    {
        r = this;
    }

    public void Scan()
    {
        water.enabled = false;
        FindObjectOfType<AstarPath>().threadCount = Pathfinding.ThreadCount.AutomaticHighLoad;
        FindObjectOfType<AstarPath>().Scan();
        water.enabled = true;
    }
}
