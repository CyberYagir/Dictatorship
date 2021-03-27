using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public bool first = true;
    public int resource = 0, watts = 0;
    public bool isElectro;
    public int addMoney;

    Builded bd;
    private void Start()
    {
        bd = GetComponent<Builded>();
    }
    public void GenRes()
    {
        if (GetComponent<HouseBuild>() == null)
        {
            if (bd.createdBuilds.humans.Count >= 1)
            {
                Player.player.AddMoney(addMoney);
                bool canProd = true;
                for (int i = 0; i < bd.createdBuilds.requireResources.Count; i++)
                {
                    if (!Player.player.SubResources(bd.createdBuilds.requireResources[i].resName, bd.createdBuilds.requireResources[i].resIn, true))
                    {
                        canProd = false;
                        break;
                    }
                }
                if (canProd)
                {
                    bool canPay = Player.player.SubMoney(bd.createdBuilds.humans.Count * bd.createdBuilds.salary);
                    if (canPay)
                    {
                        if (!first)
                        {
                            if (bd.createdBuilds.finalResources.Count != 0)
                            {
                                var res = bd.createdBuilds.finalResources[resource];
                                Player.player.AddResources(res.resName, Random.Range(res.resIn, res.resMax));
                            }
                        }
                        for (int i = 0; i < bd.createdBuilds.requireResources.Count; i++)
                        {
                            Player.player.SubResources(bd.createdBuilds.requireResources[i].resName, bd.createdBuilds.requireResources[i].resIn);
                        }
                    }
                    first = false;
                }
            }
        }
    }
}
