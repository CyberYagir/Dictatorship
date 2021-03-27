using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class SpawnTrees : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject> spawned;
    public int count;
    public GameObject treesHolder;
    private void Start()
    {
        treesHolder = new GameObject();
        var gen = FindObjectOfType<GridGen>();
        gen.Gen();
        for (int i = 0; i < count; i++)
        {
            RaycastHit hit;
            var pos = new Vector3((int)Random.Range(0, gen.size.x), 99f, (int)Random.Range(0, gen.size.y));

            if (Physics.Raycast(gen.transform.position + pos, Vector3.down, out hit))
            {
                if (hit.transform.tag != "Water" && hit.transform.tag != "Sand" && hit.transform.tag != "Tree")
                {
                    if (gen.chunks[(int)pos.x, (int)pos.z] != null)
                    {
                        if (gen.chunks[(int)pos.x, (int)pos.z].objectInchunk == null)
                        {
                            gen.chunks[(int)pos.x, (int)pos.z].objectInchunk = Instantiate(prefabs[Random.Range(0, prefabs.Length)].gameObject, new Vector3(gen.transform.position.x, 0, gen.transform.position.z) + new Vector3(pos.x, hit.point.y + 0.1f, pos.z), Quaternion.Euler(0, Random.Range(0,360), 0), treesHolder.transform);
                        }
                    }
                }
            }
        }
        Destroy(GameObject.FindGameObjectWithTag("Sand"));
        //Destroy(GameObject.FindGameObjectWithTag("Water").GetComponent<MeshCollider>());

        FindObjectOfType<Rescan>().Scan();
    }

}
