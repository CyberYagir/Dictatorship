using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventsManager : MonoBehaviour
{

    public List<WorldEnent> events;
    public GameObject panel;
    public TMP_Text headerT, textT;
    public Image img;
    public GameObject news;
    [System.Serializable]
    public class WorldEnent
    {
        public string header;
        public Sprite image;
        [TextArea]
        public string text;

        public int year, mounth, day;

        public string invokeName;
    }


    private void Update()
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (Player.player.day == events[i].day && Player.player.mounths == events[i].mounth && Player.player.years == events[i].year)
            {
                news.gameObject.SetActive(true);
                headerT.text = Lang.Find(events[i].header,5);
                textT.text = Lang.Find(events[i].text, 5);
                img.sprite = events[i].image;
                if (events[i].invokeName != "")
                {
                    Invoke(events[i].invokeName, 40f);
                }
            }
        }
    }

    public void SovokCreate()
    {
        if ((Player.player.regim.ToLower().Contains("коммунизм") || Player.player.regim.ToLower().Contains("соц")) && Player.player.regim.ToLower() != "национал-социализм")
        {
            var m = Random.Range(1000, 5000);
            Player.player.AddMoney(m);
            Nofications.AddNof($"{Lang.Find("Вы получили", 6)} " + m + $"$ {Lang.Find("от",6)} {Lang.Find("СССР",2)}");
        }
    }
    public void Hitler()
    {
        if (Player.player.regim.ToLower().Contains("нац") || Player.player.regim.ToLower().Contains("фашизм"))
        {
            var m = Random.Range(1500, 10000);
            Player.player.AddMoney(m);
            Nofications.AddNof($"{Lang.Find("Вы получили",6)} " + m + $"$ {Lang.Find("от",6)} {Lang.Find("Германия",2)}"); ;
        }
    }
}
