using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class CloseMenu : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject close;
    public void OnPointerClick(PointerEventData eventData)
    {
        close.SetActive(false);
        Cursor.SetCursor(FindObjectOfType<Manager>().normal_cursor, new Vector2(64, 64), CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(FindObjectOfType<Manager>().active_cursor, new Vector2(64, 64), CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(FindObjectOfType<Manager>().normal_cursor, new Vector2(64, 64), CursorMode.Auto);
    }
}
