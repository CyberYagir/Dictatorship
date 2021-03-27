using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceBuild : MonoBehaviour
{
    public LayerMask layerMask;
    public List<Transform> grid;
    public GridGen gridGen;
    Quaternion newRot;
    Vector3 elers;
    public HouseBuild houseBuild;
    bool can;
    Collider[] colliders;
    private void Start()
    {
        gridGen = FindObjectOfType<GridGen>();
        transform.name = GetComponent<Builded>().createdBuilds.build.names[Lang.l.lang] + "-" + Random.Range(1000, 9999);
        colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    public void Update()
    {
        //Сделать леса и постройку здания.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool error = false;

        foreach (Transform item in grid)
        {
            if (Mathf.FloorToInt(item.position.x - gridGen.transform.position.x) >= 0 && Mathf.FloorToInt(item.position.x - gridGen.transform.position.x) < gridGen.size.x)
            {
                if (Mathf.FloorToInt(item.position.z - gridGen.transform.position.z) >= 0 && Mathf.FloorToInt(item.position.z - gridGen.transform.position.z) < gridGen.size.y)
                {
                    var chk = gridGen.chunks[Mathf.FloorToInt(item.position.x - gridGen.transform.position.x), Mathf.FloorToInt(item.position.z - gridGen.transform.position.z)];

                    if (chk != null)
                    {
                        if (chk.objectInchunk != null) error = true;
                    }
                    else
                    {
                        error = true;
                    }
                }
            }
        }
        if (GetComponent<Port>() != null)
        {
            RaycastHit h;
            if (Physics.Raycast(GetComponent<Port>().dockPoint.position, Vector3.down, out h))
            {
                if (h.collider != null)
                {
                    if (h.transform.tag != "Water")
                    {
                        error = true;
                    }
                }
            }
            else
            {
                error = true;
            }
        }
        if (GetComponent<MineScript>() != null)
        {
            if (!GetComponent<MineScript>().can)
            {
                error = true;
            }
        }
        if (error)
        {
            GetComponentInChildren<Renderer>().material = FindObjectOfType<Manager>().no;
            can = false;
        }
        else
        {
            GetComponentInChildren<Renderer>().material = FindObjectOfType<Manager>().yes;
            can = true;
        }
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            //transform.position = new Vector3((int)hit.point.x, float.Parse(hit.point.y.ToString("0.0")), (int)hit.point.z);
            transform.position = Vector3.Lerp(transform.position, new Vector3((int)hit.point.x, float.Parse(hit.point.y.ToString("0.0")), (int)hit.point.z), 25f * Time.deltaTime);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.4f);
        if (Input.GetKeyDown(KeyCode.R))
        {
            elers += new Vector3(0, 90, 0);
            newRot = Quaternion.Euler(elers);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (can)
            {
                if (FindObjectsOfType<PanelUI>().ToList().Find(x => x.over == true) == null)
                {
                    Nofications.AddNof(Lang.Find("Тендер здания",6) + " " + transform.name.Split('-')[0] + " (" + transform.name.Split('-')[1] + ") " +  Lang.Find("создан",6));
                    Place();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (FindObjectsOfType<PanelUI>().ToList().Find(x => x.over == true) == null)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Place()
    {
        foreach (Transform item in grid)
        {
            var chk = gridGen.chunks[Mathf.FloorToInt(item.position.x - gridGen.transform.position.x), Mathf.FloorToInt(item.position.z - gridGen.transform.position.z)];
            chk.objectInchunk = gameObject;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
        transform.localEulerAngles = elers;
        GetComponent<Builded>().createdBuilds.building = gameObject;
        if (GetComponent<Builded>().createdBuilds.build.names[0] == "Халупа")
        {
            Quests.q.placeXalupa = true;
        }
        FindObjectOfType<Manager>().StartBuild(gameObject);
        FindObjectOfType<Manager>().createdBuilds.Add(GetComponent<Builded>().createdBuilds);
        houseBuild.enabled = true;
        Rescan.r.Scan();
        Destroy(this);
    }

}
