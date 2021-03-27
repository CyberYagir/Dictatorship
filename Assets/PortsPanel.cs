using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortsPanel : MonoBehaviour
{
    public Transform portItem, portHolder;

    public Transform tenderItem, tenderHolder;

    public WorldPolitics world;

    public GameObject lastTender;
    public WorldPolitics.WorldTender lastTenderClass;

    public TMP_Text tenderInfo, shipInfo;

    public Port lastPort;
    public GameObject startButtom, endButton;

    public void EndTender()
    {
        if (lastPort != null)
        {
            if (lastPort.shipClass != null)
            {
                lastPort.shipClass.end = true;
            }
        }
    }

    public void StartTender()
    {
        lastPort.shipClass = new WorldPolitics.Ship(world.tenders[lastTender.GetComponent<PortTenderButton>().tenderid], lastPort);
        if (lastPort.shipMove == Port.type.Back)
        {
            if (lastPort.inDock)
            {
                if (lastPort.shipClass.worldTender.mounthsFull <= lastPort.shipClass.worldTender.tendermounth)
                {
                    if (!lastPort.shipClass.worldTender.import)
                    {
                        if (Player.player.SubResources(lastPort.shipClass.worldTender.resource.name, lastPort.shipClass.worldTender.resCount))
                        {
                            lastPort.shipClass.end = false;
                            lastPort.shipMove = Port.type.To;
                            Quests.q.startTender = true;
                        }
                        else
                        {
                            lastPort.shipClass = null;
                        }
                    }
                    else
                    {
                        lastPort.shipClass.end = false;
                        lastPort.shipMove = Port.type.To;
                    }
                }
                else
                {
                    lastPort.shipClass = null;
                }
            }
        }
    }

    public void UpdatePort()
    {
        if (lastPort)
        {
            if (lastPort.shipClass != null)
            {
                if (!lastPort.shipClass.end && lastPort.shipMove == Port.type.None)
                {
                    endButton.SetActive(true);
                }
                else
                {
                    endButton.SetActive(false);
                }
            }
            startButtom.SetActive(lastPort.shipClass == null && lastTender != null);

            shipInfo.text = $"{Lang.Find("Порт", 0)}: " + lastPort.portAdress + "\n\n";
            if (lastPort.shipClass != null)
            {
                shipInfo.text += $"{Lang.Find("Тендер на", 6)}: " + Lang.Find(lastPort.shipClass.worldTender.resource.name, 1) + "\n" +
                $"{Lang.Find("Месяцев осталось", 0)}: " + lastPort.shipClass.mounths + " М.";
            }
            else
            {
                shipInfo.text += $"\n{Lang.Find("Простой", 0)}";
            }
        }
        else
        {
            startButtom.SetActive(false);
            endButton.SetActive(false);
            shipInfo.text = "";
        }
        if (lastTender)
        {
            tenderInfo.text = $"{Lang.Find("Тендер", 0)}:\n{Lang.Find("Страна", 0)}: " + Lang.Find(world.worldCountries[lastTenderClass.countryID].name, 2) + "\n\n" +
            $"{Lang.Find("Отношения", 0)}:\n" +
            Lang.Find(world.worldCountries[lastTenderClass.countryID].name, 2) + ": " + "+" + lastTenderClass.rating + "\n" +
            Lang.Find(world.worldCountries[lastTenderClass.oponentID].name, 2) + ": " + "-" + lastTenderClass.rating + "\n" +
            $"\n\n{Lang.Find("Круиз", 0)}: \n" +
            $"{Lang.Find("Время пути", 0)}: " + lastTenderClass.mounthsFull + " м.\n" +
            $"{Lang.Find("Время конца", 0)}: " + lastTenderClass.tendermounth + " м.\n" +
            $"{Lang.Find("Цена", 0)}: " + lastTenderClass.cost + "\n" + 
            $"{Lang.Find("Ресурс", 0)}: " + Lang.Find(lastTenderClass.resource.name,1) + "\n" +
            $"{Lang.Find("Кол-во", 0)}: " + lastTenderClass.resCount + "\n" +
            $"{Lang.Find("Тип", 0)}: " + (lastTenderClass.import == true ? Lang.Find("Импорт", 0) : Lang.Find("Экспорт", 0));
        }
        else
        {
            tenderInfo.text = "";
        }
    }
    public void UpdatePorts()
    {
        foreach (Transform item in portHolder)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < world.ports.Count; i++)
        {
            var g = Instantiate(portItem.gameObject, portHolder);
            g.GetComponent<PortSelectPort>().port = world.ports[i];
            g.gameObject.SetActive(true);
        }
    }
    public void UpdateTenders()
    {
        foreach (Transform item in tenderHolder)
        {
            Destroy(item.gameObject);
        }

        for (int i = world.tenders.Count - 1; i > 0; i--)
        {
            var g = Instantiate(tenderItem.gameObject, tenderHolder);
            g.GetComponent<PortTenderButton>().nameT.text = Lang.Find(world.tenders[i].resource.name, 1); 
            g.GetComponent<PortTenderButton>().valT.text = world.tenders[i].cost.ToString();
            g.GetComponent<PortTenderButton>().tenderid = i;
            g.gameObject.SetActive(true);
            if (lastTenderClass == world.tenders[i])
            {
                lastTender = g;
                lastTenderClass = world.tenders[i];
            }
        }
    }
}
