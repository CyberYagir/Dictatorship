using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCar : MonoBehaviour
{
    public AIDestinationSetter path;
    public float minDist;
    public GameObject particle;
    float time;
    void Update()
    {
        if (path.target != null)
        {
            if (Vector3.Distance(path.target.position, transform.position) <= minDist)
            {
                Destroy(gameObject);
            }
        }
        time += Time.deltaTime;
        if (time > 5)
        {
            if (GetComponent<AIPath>().velocity.magnitude < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        Destroy(Instantiate(particle.gameObject, transform.position, Quaternion.identity), 2f);   
    }
}
