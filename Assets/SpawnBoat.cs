using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnBoat : MonoBehaviour
{
    public float speed;
    public int humansCount;
    public GameObject particles;
    public Transform spawn;
    private void Start()
    {
        humansCount = Random.Range(5, 15 + Mathf.FloorToInt(Player.player.maxLivePoints / 15f)) ;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.LookAt(new Vector3(0, transform.position.y, 0));
        RaycastHit hit;
        int spawnedCount = FindObjectsOfType<Bot>().ToList().Count;
        Debug.DrawRay(transform.position - (transform.right/2), transform.forward);
        if (Physics.Raycast(transform.position - (transform.right/2), transform.forward, out hit, 1f))
        {
            if (hit.collider != null)
            {
                
                Nofications.AddNof($"{ Lang.Find("На остров прибыло", 6)} " + humansCount + $" {Lang.Find("человек", 6)}");
                for (int i = 0; i < 5 - spawnedCount; i++)
                {
                    Player.player.humans.Add(new Player.Human(spawn.position + new Vector3(Random.Range(-1f,1f), 0 , Random.Range(-1f, 1f))));
                    humansCount--;
                }
                for (int i = 0; i < humansCount; i++)
                {
                    Player.player.humans.Add(new Player.Human());

                }
                Destroy(Instantiate(particles, transform.position + new Vector3(0, 1, 0), Quaternion.identity),2);
                Destroy(gameObject);
            }
        }
       
    }


}
