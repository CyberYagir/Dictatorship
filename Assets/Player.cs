using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public static Player player;

    public static List<string> first_names = new List<string>(), last_names = new List<string>();
    public int years, mounths, day;
    [Header("Economic")]
    public int money;
    public int moneySpent;
    public int moneyAll;
    public int moneyAdded;
    public int moneyPerCycle;
    public int yourYear;
    public List<int> moneyPerYearStats;
    public int localYear = 0;
    public int localYearNum;
    [Header("Humans")]
    public int humansDeadAll;
    public int humansAll;
    public int humansDead;
    public float humansHappyness;
    public int humanHungryDead;
    public int minEatCost;
    public List<int> humansPerYears;
    public int dopLivePoints;
    [Space]
    public List<Resource> resources;
    public List<PlayerResource> playerResources;
    public Vector2 politics;
    public int watts, wattsEated;
    public string regim = "Центризм";
    public int minLivePoints, maxLivePoints;
    Manager manager;
    [Space]
    public float dayLength = 0.3f;
    public PlayerEvents playerEvents;
    public List<string> humansPotrebs;

    private void Update()
    {
        if(dayLength <= 0)
        {
            dayLength = 0.3f;
        }
    }
    public void AddResources(string name, int count)
    {
        var g = playerResources.Find(x => x.resource.name == name);
        if (g != null)
        {
            g.cargo += count;
            g.created += count;
        }
        else
        {
            playerResources.Add(new PlayerResource() { resource = resources.Find(x => x.name == name), cargo = count, created = count, eated = 0 });
        }
    }

    public bool SubResources(string name, int count)
    {
        var g = playerResources.Find(x => x.resource.name == name);
        if (g != null)
        {
            if (g.cargo >= count)
            {
                g.cargo -= count;
                return true;
            }
        }
        return false;
    }
    public bool SubResources(string name, int count, bool boolonly)
    {
        var g = playerResources.Find(x => x.resource.name == name);
        if (g != null)
        {
            if (g.cargo >= count)
            {
                return true;
            }
        }
        return false;
    }
    public void AddMoney(int n)
    {
        moneyAll += n;
        money += n;
        moneyPerCycle += n;
        moneyAdded += n;
    }
    public bool SubMoney(int n)
    {
        if (money - n > 0)
        {
            money -= n;
            moneySpent -= n;
            return true;
        }
        else return false;
    }


    public List<Human> humans = new List<Human>();
    public List<PlayerResource> storage;
    public GameObject human;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        player = this;
        TextAsset txt1 = (TextAsset)Resources.Load("first-names", typeof(TextAsset));
        TextAsset txt2 = (TextAsset)Resources.Load("last-names", typeof(TextAsset));
        first_names = txt1.text.Split('\n').ToList();
        last_names = txt2.text.Split('\n').ToList();
        years = Random.Range(1900, 1910);
        mounths = Random.Range(1, 8);
        money = 50000;
        yourYear = PlayerPrefs.GetInt("Years", Random.Range(20,25));
        moneyAll = money;
        localYearNum = years;
        StartCoroutine(timeLoop());
        moneyPerYearStats = new List<int>(new int[12]);
        humansPerYears = new List<int>(new int[12]);
        moneyPerYearStats[0] = money;
    }


    IEnumerator timeLoop()
    {
        while (true)
        {
            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(dayLength);
                day++;
            }
            if (day >= 30)
            {
                int max = -999;
                int min = 999;
                humansPerYears[localYear] = humans.Count;
                mounths++;
                FindObjectOfType<WorldPolitics>().CheckTenders();
                List<Human> homenull = new List<Human>();
                List<Human> worknull = new List<Human>();

                var allbuilds = FindObjectsOfType<Builded>().ToList().FindAll(x=>x.GetComponent<HouseBuild>() == null);
                var farm = allbuilds.FindAll(x => x.buildName == "Farm");
                var mobsfarm = allbuilds.FindAll(x => x.buildName == "MobsFarm");
                var pomp = allbuilds.FindAll(x => x.buildName == "WarterPump");

                int farmsForPeoples = (int)((humans.Count + 60f) / 60f);
                int mobsfarmForPeoples = (int)((humans.Count + 80f) / 80f);
                int pompForPeoples = (int)((humans.Count + 60f) / 60f);

                FindObjectOfType<PlayerUI>().buildUI.GetComponent<BuildUI>().lastHumans = new List<Human>();

                int addPoints = 0; 
                var humanBuildings = FindObjectsOfType<HumansBuild>().ToList();

                var electro = FindObjectsOfType<Electro>();

                var allWatts = 0;
                for (int i = 0; i < electro.Length; i++)
                {
                    if (electro[i].GetComponent<Builded>().createdBuilds.humans.Count != 0)
                    {
                        allWatts += electro[i].generateWatts;
                    }
                }
                watts = allWatts;
                for (int i = 0; i < humanBuildings.Count; i++)
                {
                    addPoints += humanBuildings[i].humanPoints;
                }
                for (int i = 0; i < humans.Count; i++)
                {
                    humans[i].hungry -= Random.Range(40, 80);
                    if (SubMoney(minEatCost))
                    {
                        humans[i].hungry = 100;
                    }

                    if (humans[i].hungry < 0 || humans[i].old >= humans[i].death)
                    {
                        if (humans[i].work != null)
                        {
                            humans[i].work.humans.RemoveAll(x=>x.ID == humans[i].ID);
                        }
                        if (humans[i].builder)
                        {
                            var brigade = FindObjectOfType<BuildManager>().brigades.Find(x => x.humans.Find(y => y.ID == humans[i].ID) != null);
                            if (brigade != null)
                            {
                                brigade.humans.RemoveAll(x => x.ID == humans[i].ID);
                            }
                            else
                            {
                                humans[i].builder = false;
                            }
                        }
                        if (humans[i].home != null)
                        {
                            humans[i].home.humans.RemoveAll(x => x.ID == humans[i].ID);
                        }
                        humans[i] = null;
                        continue;
                    }

                    humans[i].livePoints = 0;
                    humans[i].livePoints += addPoints;
                    if (humans[i].hungry <= 10)
                    {
                        humans[i].livePoints -= 10;
                    } 
                    else if(humans[i].hungry <= 30)
                    {
                        humans[i].livePoints -= 5;
                    }else if (humans[i].hungry <= 50)
                    {
                        humans[i].livePoints += 1;
                    }
                    else if (humans[i].hungry > 50)
                    {
                        humans[i].livePoints += 4;
                    }

                    if (humans[i].home != null)
                    {
                        humans[i].livePoints += 10 * humans[i].home.level;
                    }
                    else
                    {
                        humans[i].livePoints -= 8;
                    }


                    if (humans[i].work != null || humans[i].builder)
                    {
                        if (!humans[i].builder)
                        {
                            humans[i].livePoints += 10 * humans[i].work.level;
                        }
                        else
                        {
                            humans[i].livePoints += 8;
                        }
                    }
                    else
                    {
                        humans[i].livePoints -= 5;
                    }
                    humans[i].livePoints += (int)(humans[i].livePoints * Random.Range(-0.15f, 0.15f));

                    if (humans[i].livePoints < 0)
                    {
                        humans[i].livePoints = 0;
                    }

                    if (max < humans[i].livePoints)
                    {
                        max = humans[i].livePoints;
                    }
                    if (min > humans[i].livePoints)
                    {
                        min = humans[i].livePoints;
                    }
                    if (humans[i].home == null)
                    {
                        homenull.Add(humans[i]);
                    }
                    if (humans[i].work == null)
                    {
                        if (!humans[i].builder)
                            worknull.Add(humans[i]);
                    }
                }
                humansPotrebs = new List<string>();
                humans.RemoveAll(x => x == null);
                max = ((max + dopLivePoints == 0 ? max + dopLivePoints - 1 : max + dopLivePoints));
                maxLivePoints = max;
                minLivePoints = min;
                if (farm.Count < farmsForPeoples) { max += (int)(max * 0.1f) + 2; humansPotrebs.Add(Lang.Find("Нужна Ферма", 7)); }// else max -= (int)(max * 0.15f);
                if (mobsfarm.Count < mobsfarmForPeoples) { max += (int)(max * 0.15f) + 5; humansPotrebs.Add(Lang.Find("Нужна Хозяйство", 7)); }// else max -= (int)(max * 0.2f);
                if (pomp.Count < pompForPeoples) { max += (int)(max * 0.15f) + 2; humansPotrebs.Add(Lang.Find("Нужна Вода", 7)); }// else max -= (int)(max * 0.2f);

                print("max: " + max);
                for (int i = 0; i < humans.Count; i++)
                {
                    if (humans[i].work != null)
                    {
                        if (humans[i].work.building == null) humans[i].work = null;
                    }
                    if (humans[i].home != null)
                    {
                        if (humans[i].home.building == null) humans[i].home = null;
                    }
                    if (farm.Count > farmsForPeoples) 
                        if (Random.Range(1, 4) == 2) 
                            player.AddMoney((int)(minEatCost * Random.Range(0.5f, 0.8f)));
                    if (mobsfarm.Count > mobsfarmForPeoples)
                        if (Random.Range(1, 4) == 2)
                            player.AddMoney((int)(minEatCost * Random.Range(0.5f, 0.8f)));
                    if (pomp.Count > pompForPeoples)
                        if (Random.Range(1, 4) == 2)
                            player.AddMoney((int)(minEatCost * Random.Range(0.5f, 0.8f)));

                    if (humans[i].home != null && humans[i].work != null)
                    {
                        if (Nalogs.n.tax_housing > humans[i].work.salary * 0.5f)
                        {
                            humans[i].livePoints -= 5;
                            player.AddMoney((int)(humans[i].work.salary * 0.5f));
                        }
                        else
                        {
                            player.AddMoney(Nalogs.n.tax_housing);
                        }
                    }
                    humans[i].happiness = (int)(((float)humans[i].livePoints / (float)max) * 100f);
                    
                    if (humans[i].home != null)
                    {
                        if (humans[i].spawned != null)
                        {
                            Destroy(humans[i].spawned);
                        }
                    }
                    if (humans[i].hungry == 100)
                    {
                        if (humans.Count > 20)
                        {
                            if (Random.Range(0, 4) == 2)
                            {
                                money += Random.Range(30, 150);
                            }
                        }
                    }
                }

                manager.createdBuilds = manager.createdBuilds.OrderBy(u => u.salary).ToList();
                manager.createdBuilds.Reverse();
                for (int i = 0; i < manager.createdBuilds.Count; i++)
                {
                    if (manager.createdBuilds[i].building.GetComponent<HouseBuild>() == null)
                    {
                        if (manager.createdBuilds[i].humans.Count < manager.createdBuilds[i].maxPeopleIn)
                        {
                            if (manager.createdBuilds[i].type == CreatedBuilds.bType.House)
                            {
                                while (manager.createdBuilds[i].humans.Count < manager.createdBuilds[i].maxPeopleIn)
                                {
                                    if (homenull.Count == 0) break;
                                    manager.createdBuilds[i].humans.Add(homenull[0]);
                                    homenull[0].home = manager.createdBuilds[i];
                                    homenull.RemoveAt(0);
                                }
                            }
                            else if (manager.createdBuilds[i].type == CreatedBuilds.bType.Factory)
                            {
                                while (manager.createdBuilds[i].humans.Count < manager.createdBuilds[i].maxPeopleIn)
                                {
                                    if (worknull.Count == 0) break;
                                    manager.createdBuilds[i].humans.Add(worknull[0]);
                                    worknull[0].work = manager.createdBuilds[i];
                                    worknull.RemoveAt(0);
                                }
                            }
                        }
                        if (manager.createdBuilds[i].type == CreatedBuilds.bType.Factory)
                        {
                            if (manager.createdBuilds[i].building.GetComponent<Builded>().size * Nalogs.n.tax_land < manager.createdBuilds[i].salary * manager.createdBuilds[i].humans.Count * 0.5f)
                                player.AddMoney(manager.createdBuilds[i].building.GetComponent<Builded>().size * Nalogs.n.tax_land);

                            var fac = manager.createdBuilds[i].building.GetComponent<Factory>();
                            if (fac != null)
                            {
                                
                                if (fac.watts <= allWatts || fac.watts == 0)
                                {
                                    fac.isElectro = true;
                                    fac.GenRes();
                                    wattsEated += fac.watts;
                                    allWatts -= fac.watts;
                                }
                                else
                                {
                                    fac.isElectro = false;
                                }
                            }
                        }
                        if (manager.createdBuilds[i].type == CreatedBuilds.bType.House)
                        {
                            player.AddMoney(Nalogs.n.tax_land);
                        }
                    }
                }
                day = 1;
            }
            if (mounths == 13)
            {
                bool allDeleted = false;

                //dopLivePoints++;
                while (allDeleted)
                {
                    allDeleted = false;
                    for (int i = 0; i < humans.Count; i++)
                    {
                        humans[i].old++;
                        if (humans[i].old >= humans[i].death || humans[i].hungry <= 0)
                        {
                            if (humans[i].spawned != null)
                            {
                                Destroy(humans[i].spawned.gameObject);
                            }
                        }
                        allDeleted = true;
                        humans.Remove(humans[i]);
                        break;
                    }
                }

                years++;
                dopLivePoints += Random.Range(0, 4) == 2 ? (int)Random.Range(maxLivePoints * 0.1f, maxLivePoints * 0.3f) : 0;
                yourYear++;
                localYear++;
                playerEvents.Check();
                if (localYear == 12)
                {
                    localYear = 0;
                    moneyPerYearStats = new List<int>(new int[12]);
                    humansPerYears = new List<int>(new int[12]);
                    moneyPerCycle = 0;
                }
                localYearNum = years;
                moneyPerYearStats[localYear] = money;
                humansPerYears[localYear] = humans.Count;
                mounths = 1;
            }
        }
    }

    [System.Serializable]
    public class Human
    {
        public string name;
        public int old;
        public int death;
        public int hungry = 100;
        public int livePoints;

        public enum Gender { Male, Female, Attack_Helicopter };
        public Gender gender;
        public enum Education { Early, Primary, Lower_secondary, Upper_secondary, Post_secondary, Short_cycle, Bachelor, Master, Doctoral };
        public Education education;
        public CreatedBuilds work;
        public bool builder;
        public CreatedBuilds home;
        public enum Claster { Slavic, China, Japan, USA, England, India, France };
        public Claster country;
        [Range(0, 100)]
        public int happiness;
        public GameObject spawned;
        public int ID; 


        public Human()
        {
            if (first_names.Count != 0 && last_names.Count != 0)
            {
                Cfg();
            }
        }
        public Human(Vector3 pos)
        {
            if (first_names.Count != 0 && last_names.Count != 0)
            {
                if (player != null)
                {
                    Cfg();
                    spawned = Instantiate(player.human, pos, Quaternion.identity);
                    spawned.GetComponent<Bot>().human = this;
                }
            }
        }

        public void Cfg()
        {
            try
            {
                ID = Random.Range(10000, 99999);
                name = first_names[Random.Range(0, first_names.Count)].Trim().ToLower() + " " + last_names[Random.Range(0, last_names.Count)].Trim().ToLower();
                old = Random.Range(18, 80);
                while (old > death)
                {
                    death = Random.Range(18, 110);
                }
                gender = (Gender)Random.Range(0, 2);
                education = (Education)Random.Range(0, 9);
                country = (Claster)Random.Range(0, 7);
                happiness = Random.Range(10, 30);
            }
            catch (System.Exception)
            {
            }
            
        }
    }
}


