using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildersPage : MonoBehaviour
{
    public BuildManager buildManager;
    public Transform brigadesItem, brigadesHolder;
    public BuildersListItem selected;

    public Transform brigadeHumanItem, brigadeHumanHolder;
    public Transform allHumanItem, allHumanHolder;

    public void UpdateBuildersList()
    {
        foreach (Transform item in brigadeHumanHolder)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in allHumanHolder)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in brigadesHolder)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < buildManager.brigades.Count; i++)
        {
            var g = Instantiate(brigadesItem, brigadesHolder);
            g.GetComponentInChildren<TMP_Text>().text = Lang.Find("Бригада", 0) + " #" + i;
            g.GetComponent<BuildersListItem>().brigade = buildManager.brigades[i];
            g.GetComponent<BuildersListItem>().id = i;
            g.gameObject.SetActive(true);
        }
    }

    public void UpdateBuildersInList()
    {
        foreach (Transform item in brigadeHumanHolder)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in allHumanHolder)
        {
            Destroy(item.gameObject);
        }
        if (selected != null)
        {
            var mn = FindObjectOfType<Player>();
            for (int i = 0; i < mn.humans.Count; i++)
            {
                if (!mn.humans[i].builder && mn.humans[i].work == null)
                {
                    var g = Instantiate(allHumanItem, allHumanHolder);
                    g.GetComponent<BuildersRightListItem>().human = mn.humans[i];
                    g.GetComponent<BuildersRightListItem>().text.text = mn.humans[i].name;
                    g.gameObject.SetActive(true);
                }
            }
            for (int p = 0; p < buildManager.brigades[selected.id].humans.Count; p++)
            {
                var g = Instantiate(brigadeHumanItem, brigadeHumanHolder);
                g.GetComponent<BuildersRightListItem>().human = buildManager.brigades[selected.id].humans[p];
                g.GetComponent<BuildersRightListItem>().text.text = buildManager.brigades[selected.id].humans[p].name;
                g.gameObject.SetActive(true);
            }
        }
    }

    public void AddBuildersBrigade()
    {
        FindObjectOfType<BuildManager>().brigades.Add(new BuildManager.Brigade());
        UpdateBuildersList();
    }

    public void RemoveBrigade()
    {
        if (selected != null)
            FindObjectOfType<BuildManager>().brigades.RemoveAt(selected.id);
        UpdateBuildersList();
    }
}
