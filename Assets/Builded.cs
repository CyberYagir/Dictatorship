using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Builded : MonoBehaviour
{
    public CreatedBuilds createdBuilds;
    public int size;
    public bool destroy;
    Vector3 localpos;
    public string buildName;

    private void Start()
    {
        size = GetComponent<PlaceBuild>().grid.Count;
    }
    private void OnMouseUp()
    {
        if (!destroy)
        {
            if (FindObjectsOfType<PanelUI>().ToList().Find(x => x.over == true && x.gameObject.active) != null) return;
            FindObjectOfType<PlayerUI>().lastBuild = this;
            FindObjectOfType<PlayerUI>().buildUI.SetActive(true);
            FindObjectOfType<BuildUI>().salary.text = FindObjectOfType<PlayerUI>().lastBuild.createdBuilds.salary.ToString();
            FindObjectOfType<PlayerUI>().treeUI.SetActive(false);
        }
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
        Rescan.r.Scan();
        StopAllCoroutines();
    }
    public void DestroyBuild()
    {
        localpos = transform.position;
        var grid = FindObjectOfType<GridGen>();
        FindObjectOfType<PlayerUI>().buildUI.SetActive(false);
        var colls = GetComponents<Collider>();
        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].enabled = false;
        }
        FindObjectOfType<Manager>().DestroyBuild(gameObject);
        var chunk = grid.chunks[(int)((transform.position.x) - grid.transform.position.x), (int)((transform.position.z) - grid.transform.position.z)];
        if (chunk.objectInchunk == gameObject)
        {
            var mn = FindObjectOfType<Manager>();
            mn.createdBuilds.Remove(createdBuilds);
            for (int i = 0; i < Player.player.humans.Count; i++)
            {
                if (Player.player.humans[i].home != null)
                    if (Player.player.humans[i].home.building == gameObject)
                    {
                        Player.player.humans[i].home = null;
                    }

                if (Player.player.humans[i].work != null)
                    if (Player.player.humans[i].work.building == gameObject)
                    {
                        Player.player.humans[i].work = null;
                    }
            }

            StartCoroutine(loop());
            Destroy(gameObject, 2f);
        }
    }



}


