using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetPolitics : MonoBehaviour
{
    public Image point;
    public TMP_Text text;
    public GraphicRaycaster raycaster;
    private void Update()
    {
        if (!GetComponentInParent<PoliticsPanel>().lockPanel.active)
        {
            text.transform.position = Input.mousePosition;
            var g = new List<RaycastResult>();
            var p = new PointerEventData(FindObjectOfType<EventSystem>());
            p.position = Input.mousePosition;
            raycaster.Raycast(p, g);
            if (g.Count == 0 || g[0].gameObject.GetComponent<ButtonEffects>())
            {
                text.text = "";
            }
            else
            {
                if (g[0].gameObject.GetComponent<PiliticsCountryType>() != null)
                    text.text = g[0].gameObject.GetComponent<PiliticsCountryType>().names[Lang.l.lang];
                else
                    text.text = "";
            }
        }
        else
        {
            text.text = "";
        }
    }
}
