using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    public TMP_Text name_, percents;
    public GameObject buildWindow, normalWindow;
    public TMP_Text buildText;
    public PlayerUI playerUI;
    public HouseBuild houseBuild;
    public BuildManager bm;
    [Space]
    public Transform humansHolder, humansItem;
    public TMP_Text humansCount;
    public GameObject factoryPanel;
    public TMP_Dropdown resDrop;
    public TMP_InputField salary;
    public TMP_Text allSalary, requireT;
    [Space]
    public Transform chBrigadeItem;
    public Transform chBrigadeHolder;
    [HideInInspector]
    public List<Player.Human> lastHumans;

    public void UpdateChangeBuilders()
    {
        foreach (Transform item in chBrigadeHolder)
        {
            Destroy(item.gameObject);
        }
        var builders = FindObjectOfType<BuildManager>().brigades;
        for (int i = 0; i < builders.Count; i++)
        {
            var g = Instantiate(chBrigadeItem, chBrigadeHolder);
            g.GetComponentInChildren<TMP_Text>().text = Lang.Find("Бригада", 0) + " #" + i + " (" + builders[i].brigadeID + ")";
            g.GetComponent<SetBrigadeButton>().brigadeID = builders[i].brigadeID;
            g.gameObject.SetActive(true);
        }
    }
    public void RemoveWorker(GameObject button)
    {
        print("Click");
        if (playerUI.lastBuild != null)
        {
            var hum = playerUI.lastBuild.createdBuilds.humans.Find(x => x.ID.ToString() == button.name);
            if (hum != null)
            {
                var Orginal = Player.player.humans.Find(x => x.ID == hum.ID);
                
                playerUI.lastBuild.createdBuilds.humans.RemoveAll(x => x.ID.ToString() == button.name);
                if (playerUI.lastBuild.createdBuilds.type == CreatedBuilds.bType.Factory)
                {
                    Orginal.work = null;
                }
                else
                {
                    Orginal.home = null;
                }
                lastHumans = new List<Player.Human>();
            }
            else
            {
                print("NotRemove");
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerUI.lastBuild != null)
        {
            houseBuild = playerUI.lastBuild.GetComponent<HouseBuild>();
            name_.text = playerUI.lastBuild.createdBuilds.build.names[Lang.l.lang] + "(" + playerUI.lastBuild.createdBuilds.building.transform.name.Split('-')[1] + ")";
            if (houseBuild != null)
            {
                factoryPanel.SetActive(false);
                buildWindow.SetActive(true);
                normalWindow.SetActive(false);
                percents.text = houseBuild.percents + "%";
                var brigade = bm.brigades.FindIndex(x => x.builded == playerUI.lastBuild);
                buildText.text = Lang.Find("Бригада", 0) + ": " + (brigade != -1 ? "#" + brigade + "(" + bm.brigades[brigade].brigadeID.ToString() + ")" : "-") + "\n";
                for (int i = 0; i < playerUI.lastBuild.createdBuilds.requiredToBuild.Count; i++)
                {
                    buildText.text += Lang.Find(playerUI.lastBuild.createdBuilds.requiredToBuild[i].resName, 1) + ": " + playerUI.lastBuild.createdBuilds.requiredToBuild[i].resIn + "/" + playerUI.lastBuild.createdBuilds.requiredToBuild[i].resMax + "\n";
                }
            }
            else
            {
                percents.text = " ";
                print(lastHumans != playerUI.lastBuild.createdBuilds.humans);
                if (lastHumans != playerUI.lastBuild.createdBuilds.humans)
                {
                    lastHumans = playerUI.lastBuild.createdBuilds.humans;
                    foreach (Transform item in humansHolder)
                    {
                        Destroy(item.gameObject);
                    }
                    for (int i = 0; i < playerUI.lastBuild.createdBuilds.humans.Count; i++)
                    {
                        var g = Instantiate(humansItem.gameObject, humansHolder);
                        g.GetComponentInChildren<TMP_Text>().text = playerUI.lastBuild.createdBuilds.humans[i].name;
                        g.name = playerUI.lastBuild.createdBuilds.humans[i].ID.ToString();
                        g.SetActive(true);
                    }
                }
                humansCount.text = Lang.Find("Товарищей", 0) + ": " + playerUI.lastBuild.createdBuilds.humans.Count + "/" + playerUI.lastBuild.createdBuilds.maxPeopleIn;
                factoryPanel.SetActive(playerUI.lastBuild.createdBuilds.type == CreatedBuilds.bType.Factory);
                if(playerUI.lastBuild.createdBuilds.type == CreatedBuilds.bType.Factory)
                {
                    if (playerUI.lastBuild.createdBuilds.requireResources.Count != 0)
                    {
                        requireT.text = Lang.Find("Нужно", 0) +":\n";
                        for (int i = 0; i < playerUI.lastBuild.createdBuilds.requireResources.Count; i++)
                        {
                            requireT.text += Lang.Find(playerUI.lastBuild.createdBuilds.requireResources[i].resName, 1) + $": " + playerUI.lastBuild.createdBuilds.requireResources[i].resIn + $" {Lang.Find("до", 6)} " + playerUI.lastBuild.createdBuilds.requireResources[i].resMax + "\n";
                        }
                    }
                    else
                    {
                        requireT.text = Lang.Find("Нужно", 0) + ":\n" + Lang.Find("Живая сила", 0) + ".\n";
                    }
                    if (playerUI.lastBuild.createdBuilds.finalResources.Count != 0)
                    {
                        resDrop.gameObject.SetActive(true);
                        resDrop.value = playerUI.lastBuild.createdBuilds.building.GetComponent<Factory>().resource;
                    }
                    else
                    {
                        resDrop.gameObject.SetActive(false);
                        resDrop.options = new List<TMP_Dropdown.OptionData>();
                    }
                    List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
                    for (int i = 0; i < playerUI.lastBuild.createdBuilds.finalResources.Count; i++)
                    {
                        options.Add(new TMP_Dropdown.OptionData(Lang.Find(playerUI.lastBuild.createdBuilds.finalResources[i].resName,1))); ;
                    }
                    resDrop.options = options;
                }
                allSalary.text = Lang.Find("Оклад месяца", 0) + ": " + (playerUI.lastBuild.createdBuilds.humans.Count * playerUI.lastBuild.createdBuilds.salary).ToString();

                buildWindow.SetActive(false);
                normalWindow.SetActive(true);
            }
        }
    }

    public void ChangeSalary()
    {
        if (playerUI.lastBuild != null)
        {
            var zarp = int.Parse(salary.text);
            if (zarp < 20) { zarp = 20; salary.text = "20"; }
            
            playerUI.lastBuild.createdBuilds.salary = zarp;
        }
    }
    public void ChangeResource()
    {
        if (playerUI.lastBuild != null)
        {
            var f = playerUI.lastBuild.createdBuilds.building.GetComponent<Factory>();
            if (f != null)
            {
                f.resource = resDrop.value;
            }
        }
    }
}
