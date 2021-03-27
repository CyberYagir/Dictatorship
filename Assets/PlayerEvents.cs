using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public int unhappyYears;
    public int heath = 100, years = 0;
    public GameObject panel;
    public List<GameObject> all;
    public List<GameObject> deadpages;
    public List<GameObject> unhappy;

    public bool dead;

    public void CloseAll()
    {
        for (int i = 0; i < all.Count; i++)
        {
            all[i].active = false;
        }
    }

    public void Check()
    {
	years++;
        if (Player.player.humans.Count != 0)
        {
            if (Player.player.humansHappyness < 25)
            {
                unhappyYears++;
                if (Random.Range(unhappyYears - 1, unhappyYears + 7) == unhappyYears && years > 6)
                {
                    CloseAll();
                    panel.SetActive(true);
                    unhappy[Random.Range(0, unhappy.Count)].active = true;
                    dead = true;
                }
            }
            else
            {
                unhappyYears = 0;
            }
        }
        if (Player.player.yourYear > 75)
        {
            if (Random.Range(0, 4) == 2)
                heath -= Random.Range(25, 35);
        }

        if (heath <= 0)
        {
            CloseAll();
            panel.SetActive(true);
            deadpages[Random.Range(0, deadpages.Count)].active = true;
            dead = true;
        }
    }
}
