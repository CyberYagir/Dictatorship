using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Outline))]
public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool over;
    public Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>() != null || GetComponent<ShopElement>() != null)
            Cursor.SetCursor(FindObjectOfType<Manager>().active_cursor, new Vector2(64, 64), CursorMode.Auto);
        over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(FindObjectOfType<Manager>().normal_cursor, new Vector2(64, 64), CursorMode.Auto);
        over = false;
    }
    private void Update()
    {
        if (over == true)
        {
            if (outline.effectDistance.x < 5)
            {
                outline.effectDistance += Vector2.one * Time.unscaledDeltaTime * 30;
            }
        }
        else
        {
            outline.effectDistance -= Vector2.one * Time.unscaledDeltaTime * 50;
            if (outline.effectDistance.x < 0)
            {
                outline.effectDistance = Vector2.zero;
            }
        }
    }
}
