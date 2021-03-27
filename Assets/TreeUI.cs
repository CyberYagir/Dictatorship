using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TreeUI : MonoBehaviour
{
    public bool dead;
    Vector3 localpos;
    public LayerMask mask;
    bool rescan;
    public AudioClip destroyTree;
    private void Start()
    {

        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position + new Vector3(0,20,0), Vector3.down, out hit, 99f, mask, QueryTriggerInteraction.Ignore)){
            transform.parent.position = hit.point;
        }
        localpos = transform.position;
    }
    IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            transform.Translate(Vector3.down * 0.05f);
            transform.position = new Vector3(localpos.x + Random.Range(-0.05f, 0.05f), transform.position.y, localpos.z + Random.Range(-0.05f, 0.05f));
        }
    }
    private void OnDestroy()
    {
        if (rescan)
        {
            Rescan.r.Scan();
        }
        StopAllCoroutines();
    }
    private void OnMouseDown()
    {
        if (FindObjectsOfType<PanelUI>().ToList().Find(x => x.over == true && x.gameObject.active) != null) return;

        var grid = FindObjectOfType<GridGen>();
        var g = FindObjectOfType<PlayerUI>();
        if (g.lastTree != null)
        {
            var lines = g.lastTree.GetComponentsInChildren<LineRenderer>();
            for (int i = 0; i < lines.Length; i++)
            {
                Destroy(lines[i].gameObject);
            }
        }
        g.lastTree = transform.parent.parent.gameObject;
        g.treeUI.SetActive(true);
        g.treeCount = 0;
        for (int i = -3; i < 3; i++)
        {
            for (int j = -3; j < 3; j++)
            {
                var chunk = grid.chunks[(int)((transform.parent.parent.position.x + i) - grid.transform.position.x), (int)((transform.parent.parent.position.z + j) - grid.transform.position.z)];
                if (chunk != null)
                {
                    var obj = chunk.objectInchunk;
                    if (obj != null)
                    {
                        if (obj.tag == "Tree")
                        {
                            g.treeCount++;
                            var line = Instantiate(g.treeLine, transform.parent.parent);
                            line.GetComponent<LineRenderer>().SetPosition(0, transform.parent.parent.position);
                            line.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                        }
                    }
                }
            }
        }
    }

    public void DestroyTrees()
    {
        rescan = true;
        var res = FindObjectOfType<Player>();
        var grid = FindObjectOfType<GridGen>();
        var g = FindObjectOfType<PlayerUI>();
        if (g.lastTree != null)
        {
            var lines = g.lastTree.GetComponentsInChildren<LineRenderer>();
            for (int i = 0; i < lines.Length; i++)
            {
                Destroy(lines[i].gameObject);
            }
        }
        GameObject sound = new GameObject();
        sound.transform.position = transform.position;
        var audio =  sound.AddComponent<AudioSource>();
        audio.volume = 0.5f;
        audio.spatialBlend = 1f;
        audio.minDistance = 1;
        audio.maxDistance = 6;
        audio.PlayOneShot(destroyTree);
        Destroy(audio, 5f);

        for (int i = -3; i < 3; i++)
        {
            for (int j = -3; j < 3; j++)
            {
                var chunk = grid.chunks[(int)((transform.parent.parent.position.x + i) - grid.transform.position.x), (int)((transform.parent.parent.position.z + j) - grid.transform.position.z)];
                if (chunk != null)
                {
                    var obj = chunk.objectInchunk;
                    if (obj != null)
                    {
                        if (obj.tag == "Tree")
                        {
                            grid.chunks[(int)((transform.parent.parent.position.x + i) - grid.transform.position.x), (int)((transform.parent.parent.position.z + j) - grid.transform.position.z)].objectInchunk = null;
                            var colls = obj.GetComponentsInChildren<Collider>();
                            for (int k = 0; k < colls.Length; k++)
                            {
                                Destroy(colls[k]);
                            }
                            var trees = obj.GetComponentsInChildren<TreeUI>();

                            for (int k = 0; k < trees.Length; k++)
                            {
                                res.AddResources("Дерево", Random.Range(5, 15));
                                trees[k].StartCoroutine(trees[k].loop());
                                trees[k].dead = true;
                            }
                            Destroy(obj.gameObject, 2f);
                            Quests.q.treeCut = true;
                        }
                    }
                }
            }
        }
        FindObjectOfType<PlayerUI>().treeUI.SetActive(false);
    }
}