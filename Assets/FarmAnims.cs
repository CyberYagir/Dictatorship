using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmAnims : MonoBehaviour
{
    public Player manager;
    public List<GameObject> objects;
    public HouseBuild houseBuild;

    private void Start()
    {
        manager = FindObjectOfType<Player>();
    }
    private void FixedUpdate()
    {
        if (houseBuild == null)
        {
            float proc = manager.day / 30f;

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].active = i < (int)(objects.Count * proc);
            }
        }
    }

}
