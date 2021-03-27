using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public Transform point;
    public LayerMask layerMask;
    public bool can;
    void Update()
    {
        RaycastHit hit;
        can = (Physics.Raycast(point.position, point.forward, out hit, 2f, layerMask, QueryTriggerInteraction.Ignore));
        if (GetComponent<PlaceBuild>() == null)
        {
            Destroy(this);
        }    
    }
}
