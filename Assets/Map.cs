using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Map : MonoBehaviour, IPointerDownHandler
{
    public RectTransform rect, map;
    public float del;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private void Update()
    {
        var pos = new Vector2(Player.player.transform.position.x, Player.player.transform.position.z);
        rect.localPosition = (Vector2)(map.sizeDelta / 2f) + pos * del; // * (1920f/Screen.width);


        rect.localEulerAngles = new Vector3(0, 0, -Player.player.transform.localEulerAngles.y);
        rect.localScale = Vector3.one * (Player.player.transform.position.y / 10f);
    }
}
