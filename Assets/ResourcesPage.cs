using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesPage : MonoBehaviour
{
    public Player player;
    public Transform item, holder;
    public TMP_Text empty;


    public void UpadteRes()
    {
        player = FindObjectOfType<Player>();
        foreach (Transform item in holder)
        {
            Destroy(item.gameObject);
        }
        empty.gameObject.SetActive(player.playerResources.Count == 0);
        for (int i = 0; i < player.playerResources.Count; i++)
        {
            var g = Instantiate(item.gameObject, holder);
            var r =g.GetComponent<PlayerResourceItem>();
            r.name.text = Lang.Find(player.playerResources[i].resource.name, 1);
            r.cargo.text = player.playerResources[i].cargo.ToString();
            r.created.text = player.playerResources[i].created.ToString();
            r.eated.text = player.playerResources[i].eated.ToString();
            g.SetActive(true);
        }
    }
}
