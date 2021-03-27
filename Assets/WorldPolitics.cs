using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldPolitics : MonoBehaviour
{
    public List<WorldCountry> worldCountries = new List<WorldCountry>();
    public List<WorldTender> tenders;

    public List<Port> ports = new List<Port>();
    public PortsPanel portsPanel;
    public int maxTenders = 12;
    public Player p;
    public AudioSource portSell, tenderEnd;
    public int moneyAll = 0;


    public void FindPorts()
    {
        ports = FindObjectsOfType<Port>().ToList().FindAll(x=>x.enabled == true);
        portsPanel.UpdatePorts();
    }
    public void CheckTenders()
    {
        tenders = tenders.OrderBy(u => u.cost).ToList();
        for (int i = 0; i < tenders.Count; i++)
        {
            //print(tenders[i].tendermounth);
            if (tenders[i].tendermounth-1 <= 0)
            {
                tenders.RemoveAt(i);
                CheckTenders();
                return;
            }
        }
        for (int i = 0; i < tenders.Count; i++)
        {
            tenders[i].tendermounth -= 1;
        }
        while (tenders.Count < maxTenders)
        {
            var tender = new WorldTender(p.resources[Random.Range(0, p.resources.Count)], Random.Range(1, 3), Random.Range(20, 100)) { countryID = Random.Range(0, worldCountries.Count), import = Random.Range(0, 3) == 0 ? true : false, tendermounth = Random.Range(36, 80)};
            tender.rating = Random.Range(1, 21);
            tender.oponentID = Random.Range(0, worldCountries.Count);
            while (tender.countryID == tender.oponentID)
                tender.oponentID = Random.Range(0, worldCountries.Count);

            tenders.Add(tender);
        }
        tenders = tenders.OrderBy(u => u.cost).ToList();
        if (portsPanel.lastPort != null)
        {
            portsPanel.UpdateTenders();
        }
        for (int i = 0; i < ports.Count; i++)
        {
            if (ports[i].shipClass != null) {
                if (ports[i].shipMove == Port.type.None)
                {
                    ports[i].shipClass.mounths -= 1;
                    ports[i].shipClass.worldTender.mounths -= 1;
                    if (ports[i].shipClass.mounths <= 0)
                    {
                        ports[i].shipMove = Port.type.Back;
                    }
                }
                if (ports[i].shipMove == Port.type.Back)
                {
                    if (ports[i].inDock)
                    {
                        int income = ports[i].shipClass.worldTender.cost * Random.Range(1, 3);
                        Player.player.AddMoney(income);
                        if (Nalogs.n.tax_income < income / 2)
                        {
                            Player.player.AddMoney((int)Nalogs.n.tax_income);
                        }
                        else
                        {
                            Player.player.AddMoney((income / 2));
                        }
                        worldCountries[ports[i].shipClass.worldTender.countryID].rating += ports[i].shipClass.worldTender.rating;
                        worldCountries[ports[i].shipClass.worldTender.oponentID].rating -= ports[i].shipClass.worldTender.rating;

                        Nofications.AddNof(ports[i].name + $" {Lang.Find("продал",6)} " + Lang.Find(ports[i].shipClass.worldTender.resource.name, 1) + $" {Lang.Find("на",6)} " + income + "$");
                        ShipSellPlay(ports[i].gameObject);
                        worldCountries[ports[i].shipClass.worldTender.countryID].rating -= ports[i].shipClass.worldTender.rating;
                        worldCountries[ports[i].shipClass.worldTender.countryID].rating += ports[i].shipClass.worldTender.rating;

                        if (ports[i].shipClass.worldTender.mounthsFull <= ports[i].shipClass.worldTender.tendermounth)
                        {
                            if (!ports[i].shipClass.worldTender.import)
                            {
                                if (!ports[i].shipClass.end)
                                {
                                    if (Player.player.SubResources(ports[i].shipClass.worldTender.resource.name, ports[i].shipClass.worldTender.resCount))
                                    {
                                        ports[i].shipClass.mounths = ports[i].shipClass.worldTender.mounthsFull;
                                        ports[i].shipMove = Port.type.To;
                                    }
                                    else
                                    {
                                        print("TenderEnd");
                                        TenderEndPlay(ports[i].gameObject);
                                        ports[i].shipClass = null;
                                    }
                                }
                                else
                                {
                                    ports[i].shipClass = null;
                                }
                            }
                            else
                            {
                                Player.player.AddResources(ports[i].shipClass.worldTender.resource.name, ports[i].shipClass.worldTender.resCount);
                                ShipSellPlay(ports[i].gameObject);
                                worldCountries[ports[i].shipClass.worldTender.countryID].rating += ports[i].shipClass.worldTender.rating;
                                worldCountries[ports[i].shipClass.worldTender.oponentID].rating -= ports[i].shipClass.worldTender.rating;
                                if (!ports[i].shipClass.end)
                                {
                                    if (Player.player.SubMoney(ports[i].shipClass.worldTender.cost))
                                    {
                                        ports[i].shipClass.mounths = ports[i].shipClass.worldTender.mounthsFull;
                                        ports[i].shipMove = Port.type.To;
                                    }
                                    else
                                    {
                                        TenderEndPlay(ports[i].gameObject);
                                        ports[i].shipClass = null;
                                    }
                                }
                                else
                                {
                                    ports[i].shipClass = null;
                                }
                            }
                        }
                        else
                        {
                            TenderEndPlay(ports[i].gameObject);
                            ports[i].shipClass = null;
                        }
                    }
                }
            }
        }
    }

    public void TenderEndPlay(GameObject gm)
    {
        if (!tenderEnd.isPlaying)
        {
            tenderEnd.transform.position = gm.transform.position;
            tenderEnd.PlayOneShot(tenderEnd.clip);
        }
    }
    public void ShipSellPlay(GameObject gm)
    {
        if (!portSell.isPlaying)
        {
            if (Random.Range(0, 2) == 1)
            {
                portSell.transform.position = gm.transform.position;
                portSell.PlayOneShot(portSell.clip);
            }
        }
    }
    [System.Serializable]
    public class WorldCountry
    {
        public string name;
        public int rating;
    }

    [System.Serializable]
    public class WorldTender {
        public int countryID;
        public int oponentID;
        public int rating;
        public Resource resource;
        public int cost;
        public int resCount;
        public bool import;
        public int mounths, mounthsFull;
        public int tendermounth;


        public WorldTender(Resource resource, int mounthsFull, int resc)
        {
            this.resCount = resc;
            this.resource = resource;
            cost = this.resCount * Random.Range(resource.minCost, resource.maxCost);
            this.mounths = mounthsFull;
            this.mounthsFull = mounthsFull;
        }
    }
    public class Ship
    {
        public WorldTender worldTender;
        public int mounths;
        public Port port;
        public bool end;

        public Ship(WorldTender tender, Port _port)
        {
            worldTender = tender;
            port = _port;
            mounths = tender.mounthsFull;
        }
    }
}
