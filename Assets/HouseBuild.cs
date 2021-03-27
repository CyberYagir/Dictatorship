using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBuild : MonoBehaviour
{
    public List<GameObject> lesa;
    public int percents;
    public GameObject mesh;
    public Builded builded;
    public int buildBrigade = -1;

    private void Start()
    {
        mesh.GetComponent<Renderer>().material = FindObjectOfType<Manager>().normal;
        mesh.SetActive(false);
    }



    public float GetMoneyForBuild(int countAdd)
    {
        float p = 0;
        float all = 0;
        for (int i = 0; i < builded.createdBuilds.requiredToBuild.Count; i++)
        {
            all += builded.createdBuilds.requiredToBuild[i].resMax;
        }
        p = (countAdd / all);

        return (int)(p * builded.createdBuilds.build.cost);
    }
    private void Update()
    {
        percents = 0;
        float all = 0, inall = 0;
        for (int i = 0; i < builded.createdBuilds.requiredToBuild.Count; i++)
        {
            inall += builded.createdBuilds.requiredToBuild[i].resIn;
            all += builded.createdBuilds.requiredToBuild[i].resMax;
        }
        percents = (int)((inall / all) * 100f);

        for (int i = 0; i < lesa.Count; i++)
        {
            if(i <= lesa.Count * (percents / 100f))
            {
                lesa[i].SetActive(true);
            }
            else
            {
                lesa[i].SetActive(false);
            }
        }
        
        if (percents >= 100)
        {
            for (int i = 0; i < lesa.Count; i++)
            {
                lesa[i].SetActive(false);
            }
            var brigade = FindObjectOfType<BuildManager>().brigades.Find(x => x.brigadeID == buildBrigade);
            if (brigade != null)
            {
                brigade.builded = null;
            }
            if (builded.createdBuilds.build.names[0] == "Халупа") Quests.q.buidedXalupa = true;
            Nofications.AddNof(Lang.Find("Здание",6) +  transform.name.Split('-')[0] + " (" + transform.name.Split('-')[1] + ") " + Lang.Find("построено",6));
            if (GetComponent<Port>() != null)
            {
                GetComponent<Port>().enabled = true;
                FindObjectOfType<WorldPolitics>().FindPorts();
            }
            mesh.SetActive(true);
            Destroy(this);
        }
    }

}
