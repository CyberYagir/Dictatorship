using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Manager : MonoBehaviour
{
    public Texture2D normal_cursor, active_cursor;
    public List<Build> builds = new List<Build>();
    public Material yes, no, normal;
    public Camera playerCamera;
    public Player player;
    public Transform[] shipSpawn;
    public GameObject humanShip;
    public List<CreatedBuilds> createdBuilds;
    public AudioSource startBuild, destroyBuild;

    public void StartBuild(GameObject gm)
    {
        startBuild.gameObject.transform.position = gm.transform.position;
        if (!startBuild.isPlaying)
        {
            startBuild.PlayOneShot(startBuild.clip);
        }
    }
    public void DestroyBuild(GameObject gm)
    {
        startBuild.Stop();
        destroyBuild.gameObject.transform.position = gm.transform.position;
        if (!destroyBuild.isPlaying)
        {
            destroyBuild.PlayOneShot(destroyBuild.clip);
        }
    }
    private void Start()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Grap", 0));
        (FindObjectOfType<Volume>().profile.components[1] as MotionBlur).intensity.value = PlayerPrefs.GetInt("Blur", 1) == 0 ? 0 : 0.05f;
        (FindObjectOfType<Volume>().profile.components[2] as HDShadowSettings).maxShadowDistance.value = PlayerPrefs.GetInt("Shadow", 150);


        if (FindObjectOfType<Player>() != null)
        {
            player = FindObjectOfType<Player>();
            playerCamera = Camera.main;
            //data = AstarPath.active.data;
            //graph = data.AddGraph(typeof(GridGraph)) as GridGraph;
            StartCoroutine(loop());
        }
    }
    private void Update()
    {
        //if (graph != null)
        //{
        //    var pos = new Vector3(playerCamera.transform.position.x, 0.5f, playerCamera.transform.position.z);
        //    graph.center = new Vector3(0, 0.5f, 0);
        //    graph.SetDimensions(115, 115, 1f);
        //}
    }

    
    IEnumerator loop()
    {
        while (true)
        {
            float min = 60f - Mathf.FloorToInt(Player.player.maxLivePoints / 15f);
            if (min < 15)
            {
                min = 15;
            }
            float max = 120f - Mathf.FloorToInt(Player.player.maxLivePoints / 10f);
            if (max < 30f)
            {
                max = 30;
            }
            Instantiate(humanShip, shipSpawn[Random.Range(0, shipSpawn.Length)].position + new Vector3(0, 0.4f,0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
}
[System.Serializable]
public class ResourceBuild {
    public string resName;
    public int resIn, resMax;
}

[System.Serializable]
public class CreatedBuilds
{
    public Build build;
    public GameObject building;
    public int level;
    public enum bType { House, Factory };
    public bType type;
    public int maxPeopleIn;
    public List<Player.Human> humans = new  List<Player.Human>();

    [Space]
    public List<ResourceBuild> requiredToBuild;
    [Space]
    public int salary;

    public List<ResourceBuild> requireResources;
    public List<ResourceBuild> finalResources;

    public int exportCost; 
}
[System.Serializable]
public class Build
{
    public List<string> names = new List<string>(new string[2]);
    public GameObject prefab;
    public Sprite icon;
    public int cost;
    public List<string> descs = new List<string>(new string[2]); 
    public bool opened;
    public int year;
    [Space]
    public string[] requireBuildsNames;
}

[System.Serializable]
public class Resource
{
    public string name;
    public int rarity = 1;
    public int minCost, maxCost;
}

[System.Serializable]
public class PlayerResource
{
    public Resource resource;
    public int cargo, eated, created;
}