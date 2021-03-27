using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool over;

    public void N()
    {
        over = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
    }
}
