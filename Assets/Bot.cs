using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public AIDestinationSetter path;
    public GameObject point;
    public Player.Human human;
    private void OnMouseDown()
    {
        if (FindObjectsOfType<PanelUI>().ToList().Find(x => x.over == true && x.gameObject.active) != null) return;
        FindObjectOfType<PlayerUI>().lastHuman = human;
        FindObjectOfType<PlayerUI>().humanUI.SetActive(true);
    }

    private void Start()
    {
        point = new GameObject() { name = transform.name + "_Point"};
        Set();
        StartCoroutine(loop());
    }

    IEnumerator loop()
    {
        while (true)
        {
            Set();
            if (transform.position.y < -20)
            {
                transform.position = FindObjectOfType<PlayerCamera>().lastPos;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    public void Set()
    {
        RaycastHit hit;
        var pos = new Vector3(Random.Range(-50, 50), 20, Random.Range(-50, 50));
        while (!Physics.Raycast(pos, Vector3.down, out hit))
        {
            pos = new Vector3(Random.Range(-50, 50), 20, Random.Range(-50, 50));
        }
        point.transform.position = hit.point;
        path.target = point.transform;
    
    }
    private void OnDestroy()
    {
        Destroy(point.gameObject);
    }
}
