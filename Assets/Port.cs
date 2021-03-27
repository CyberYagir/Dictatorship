using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Port : MonoBehaviour
{
    public Transform dockPoint;
    public int portAdress;
    public GameObject ship;
    public Transform backSpawn;
    public enum type {To, Back, None};
    public type shipMove;
    public float shipSpeed;
    public bool inDock;

    public WorldPolitics.Ship shipClass = null;
    private void Start()
    {
        portAdress = int.Parse(transform.name.Split('-')[1]);
    }
    //private void OnMouseUp()
    //{
    //    if (this.enabled)
    //    {
    //        if (FindObjectsOfType<PanelUI>().ToList().Find(x => x.over == true && x.gameObject.active) != null) return;
    //        FindObjectOfType<PlayerUI>().portUI.gameObject.SetActive(true);
    //    }
    //}
    private void Update()
    {
        if (shipMove == type.None)
        {
            ship.SetActive(false);
        }
        else
        if (shipMove == type.Back)
        {
            ship.active = true;
            ship.transform.LookAt(new Vector3(dockPoint.position.x, ship.transform.position.y, dockPoint.position.z));
            var pos = new Vector3(dockPoint.transform.position.x, ship.transform.position.y, dockPoint.transform.position.z);
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, pos, shipSpeed * Time.deltaTime);
            //print(Vector3.Distance(pos, ship.transform.position));
            inDock = Vector3.Distance(pos, ship.transform.position) < 1f;
            //if (inDock)
            //{
            //    if (shipClass != null)
            //    {
            //        shipClass.end = false;
            //    }
            //}
        }
        else
        if (shipMove == type.To)
        {
            ship.active = true;
            ship.transform.LookAt(new Vector3(backSpawn.position.x, ship.transform.position.y, backSpawn.position.z));
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, new Vector3(backSpawn.transform.position.x, transform.position.y, backSpawn.transform.position.z), shipSpeed * Time.deltaTime);
            if (Vector3.Distance(backSpawn.transform.position, ship.transform.position) < 1f)
            {
                shipMove = type.None;
            }
            inDock = false;
        }
    }
}
