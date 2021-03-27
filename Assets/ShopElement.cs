using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public bool over;
    public Build build;
    public Image image;



    private void Start()
    {
        image.sprite = build.icon;
        if (build.opened)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<Shop>().infoText.text = build.names[Lang.l.lang] + " [<color=red>" + build.cost + "$</color>]\n" + build.descs[Lang.l.lang];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<Shop>().infoText.text = "";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (build.opened)
        {
            print("Up");
            var f = FindObjectOfType<PlaceBuild>();
            if (f != null) Destroy(f.gameObject);
            var b = Instantiate(build.prefab.gameObject);
            b.GetComponent<Builded>().createdBuilds.build = build;
            FindObjectOfType<Shop>().gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("Click");
    }
}
