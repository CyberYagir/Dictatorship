using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanDebug : MonoBehaviour
{
    public TMP_Text text;



    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            float happ = 0;
            for (int i = 0; i < Player.player.humans.Count; i++)
            {
                if (Player.player.humans[i].happiness < 0) Player.player.humans[i].happiness = 0;
                happ += Player.player.humans[i].happiness;
            }
            text.text = "Dop: " + Player.player.dopLivePoints + "\n";
            text.text += "Hap: " + (happ / (Player.player.humans.Count)) + "%\n";
            for (int i = 0; i < Player.player.humans.Count; i++)
            {
                text.text += Player.player.humans[i].name + "\n" + "P: " + Player.player.humans[i].livePoints + "/" + Player.player.humans[i].happiness.ToString("000.0") + "\n";
            }
        }
    }
}
