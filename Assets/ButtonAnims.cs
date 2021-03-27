using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonAnims : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;
    public TMP_Text text;
    bool over;
    private void Update()
    {
        text.gameObject.SetActive(over);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        over = true;
        Cursor.SetCursor(FindObjectOfType<Manager>().active_cursor, new Vector2(64, 64), CursorMode.Auto);

        animator.Play("ButtonAnim");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
        Cursor.SetCursor(FindObjectOfType<Manager>().normal_cursor, new Vector2(64, 64), CursorMode.Auto);
    }
}
