using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortTenderButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text nameT;
    public TMP_Text valT;
    public PortsPanel portsPanel;
    public int tenderid;
    public GameObject import, export;

    private void Start()
    {
        portsPanel = GetComponentInParent<PortsPanel>();
        import.active = portsPanel.world.tenders[tenderid].import;
        export.active = !portsPanel.world.tenders[tenderid].import;
    }

    private void Update()
    {
        if (portsPanel.lastTender == gameObject)
        {
            GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        portsPanel.lastTender = gameObject;
        portsPanel.lastTenderClass = portsPanel.world.tenders[tenderid];
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
