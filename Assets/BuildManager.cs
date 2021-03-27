using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public List<Brigade> brigades = new List<Brigade>();

    float time;
    [System.Serializable]
    public class Brigade {
        public List<Player.Human> humans = new List<Player.Human>();
        public Builded builded;
        public int brigadeID;

        public Brigade()
        {
            try
            {
                brigadeID = Random.Range(1000, 9999);
            }
            catch (System.Exception)
            {
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 1)
        {
            for (int i = 0; i < brigades.Count; i++)
            {
                var inbuild = FindObjectsOfType<HouseBuild>().ToList().FindAll(x => x.buildBrigade == -1);
                for (int k = 0; k < inbuild.Count; k++)
                {
                    if (brigades[i].builded == null)
                    {
                        brigades[i].builded = inbuild[k].GetComponent<Builded>();
                        inbuild[k].buildBrigade = brigades[i].brigadeID;
                    }
                }
                if (brigades[i].builded != null)
                {
                    if (brigades[i].humans.Count != 0)
                    {
                        var pl = FindObjectOfType<Player>();
                        var onePay = brigades[i].builded.GetComponent<HouseBuild>().GetMoneyForBuild(5);

                        for (int j = 0; j < brigades[i].builded.createdBuilds.requiredToBuild.Count; j++)
                        {
                            if (brigades[i].builded.createdBuilds.requiredToBuild[j].resIn < brigades[i].builded.createdBuilds.requiredToBuild[j].resMax)
                            {
                                if (pl.SubMoney((int)onePay))
                                {
                                    if (pl.SubResources(brigades[i].builded.createdBuilds.requiredToBuild[j].resName, 5 * brigades[i].humans.Count))
                                    {
                                        brigades[i].builded.createdBuilds.requiredToBuild[j].resIn += 5 * brigades[i].humans.Count;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            time = 0;
        }
    }

}
