using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortSelectPort : MonoBehaviour
{
    public Port port;
    public PortsPanel portsPanel;
    public TMP_Text nameT, isTenderT;
    private void Start()
    {
        portsPanel = GetComponentInParent<PortsPanel>();

    }
    private void Update()
    {
        nameT.text = $"{Lang.Find("Порт", 0)} #" + port.transform.name.Split('-')[1];
        isTenderT.text = port.shipClass != null ? "T" : "П";
        if (portsPanel.lastPort == port)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            nameT.color = new Color(0.682f, 0.603f, 0.176f);
            isTenderT.color = new Color(0.682f, 0.603f, 0.176f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f);
            nameT.color = new Color(0.433f, 0.394f, 0.133f);
            isTenderT.color = new Color(0.433f, 0.394f, 0.133f);
        }
    }
    public void Click()
    {
        portsPanel.lastPort = port;
    }
}
