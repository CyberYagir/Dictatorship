using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildersRightListItem : MonoBehaviour
{
    public Player.Human human;
    public TMP_Text text;
    public void AddHuman()
    {
        var s = GetComponentInParent<BuildersPage>().selected;
        if (s != null)
        {
            GetComponentInParent<BuildersPage>().buildManager.brigades[s.id].humans.Add(human);
            human.builder = true;
            GetComponentInParent<BuildersPage>().UpdateBuildersInList();
        }
    }

    public void RemoveHuman()
    {
        var s = GetComponentInParent<BuildersPage>().selected;
        if (s != null)
        {
            GetComponentInParent<BuildersPage>().buildManager.brigades[s.id].humans.Remove(human);
            human.builder = false;
            GetComponentInParent<BuildersPage>().UpdateBuildersInList();
        }
    }
}
