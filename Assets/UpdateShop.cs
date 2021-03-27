using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShop : MonoBehaviour
{
    public List<int> ids = new List<int>();
    public Transform holder, item;

    public void UpdateShopUI()
    {
        foreach (Transform item in holder)
        {
            Destroy(item.gameObject);
        }
        var mn = FindObjectOfType<Manager>();
        for (int i = 0; i < ids.Count; i++)
        {
            bool alls = true;
            for (int h = 0; h < mn.builds[ids[i]].requireBuildsNames.Length; h++)
            {
                var find = FindObjectsOfType<Builded>().ToList().FindAll(x => x.buildName == mn.builds[ids[i]].requireBuildsNames[h]);
                if (find.Count == 0)
                {
                    alls = false;
                    break;
                }
            }

            var gm = Instantiate(item, holder);
            gm.GetComponent<ShopElement>().build = mn.builds[ids[i]];
            gm.GetComponent<ShopElement>().build.opened = alls == true && mn.builds[ids[i]].year <= Player.player.years;
            gm.gameObject.SetActive(true);
        }
    }
}
