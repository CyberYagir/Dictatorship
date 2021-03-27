using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageKeys : MonoBehaviour
{
    public string message1, message2, full;
    public TMP_Text text;
    public GameObject exit;
    int i = 0;
    public void Init()
    {
        EventSystem.current.SetSelectedGameObject(null);
        var list = GetComponentInParent<PoliticsPanel>().xAxis.ToList();
        if (Player.player.politics.x == 0)
        {
            message1 = Lang.Find("Мы не знаем что нам делать с нашей экономикой..", 0);
        }
        else
        {
            float rangeL = 0.1f;
            float rangeR = 0.1f;
            var polit = list.Find(x => x.axes >= Player.player.politics.x - rangeL && x.axes <= Player.player.politics.x + rangeR);
            int g = 0;
            while (polit == null)
            {
                g++;
                if (g > 20) break;
                if (Player.player.politics.x < 0)
                {
                    rangeL += 0.1f;
                }
                else
                {
                    rangeR += 0.1f;
                }
                polit = list.Find(x => x.axes >= Player.player.politics.x - rangeL && x.axes <= Player.player.politics.x + rangeR);
            }
            message1 = polit.sentenses[Lang.l.lang];
        }
        var regim = GetComponentInParent<PoliticsPanel>().ctypes.ToList().Find(x => x.gameObject.name == Player.player.regim);
        message2 = regim != null ? regim.allPhrases[Random.Range(0, regim.allPhrases.Count)].phrase[Lang.l.lang] : Lang.Find("Мы ещё не определили направление, мы центристы...", 0);
        text.text = "";
        exit.SetActive(false);
        i = 0;
        full = "" + message1 + "\n" + message2;
    }
    

    private void Update()
    {
        exit.SetActive(i >= full.Length);
        if (i >= full.Length) return;

        if (Input.anyKeyDown)
        {
            text.text += full[i];
            i++;
        }
    }
}
