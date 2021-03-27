using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildersListItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public BuildManager.Brigade brigade;
    public int id;
    private void Update()
    {
        if (GetComponentInParent<BuildersPage>().selected == this)
        {
            GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            GetComponent<Image>().color = new Color(0.745283f, 0.745283f, 0.745283f);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponentInParent<BuildersPage>().selected = this;
        GetComponentInParent<BuildersPage>().UpdateBuildersInList();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
