using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGen : MonoBehaviour
{
    public Vector2 size;
    public Chunk[,] chunks;
    public LayerMask mask;
    public class Chunk{
        public GameObject objectInchunk;
    }

    public void Gen()
    {
        chunks = new Chunk[(int)size.x, (int)size.y];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + new Vector3(i, 20, j), Vector3.down, out hit, 99f, mask))
                {
                    if (hit.point.y <= 2)
                    {
                        chunks[i, j] = new Chunk() { objectInchunk = null };
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(size.x/2, 0, size.y/2), new Vector3(size.x, 10, size.y));
    }
}
