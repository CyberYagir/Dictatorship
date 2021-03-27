using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] spawnPoints;
    public Builded builded;
    private void Start()
    {
        StartCoroutine(loop());
    }

    IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (GetComponent<HouseBuild>() == null)
            {
                if (builded.createdBuilds.humans.Count != 0) { 
                var find = GameObject.FindGameObjectsWithTag("Car");
                var ends = GameObject.FindGameObjectsWithTag("CarEnd").ToList().FindAll(x=>x.GetComponentInParent<PlaceBuild>() == null);
                    if (ends.Count != 0)
                    {
                        if (find.Length < 5)
                        {
                            var t = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            var car = Instantiate(carPrefab, t.position, transform.rotation);
                            car.GetComponent<BotCar>().path.target = ends[Random.Range(0, ends.Count)].transform;
                        }
                    }
                }
            }
        }
    }
}
