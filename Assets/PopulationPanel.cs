using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulationPanel : MonoBehaviour
{
    public TMP_Text text, needsText;
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public void FixedUpdate()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        needsText.text = Lang.Find("Потребности", 7) + ":\n";
        for (int i = 0; i < Player.player.humansPotrebs.Count; i++)
        {
            needsText.text += Player.player.humansPotrebs[i] + "\n";
        }
        if (player.humans.Count != 0)
        {
            int sum = 0;
            int hung = 0;
            int work = 0, homes = 0;
            int male = 0, famale = 0;
            for (int i = 0; i < player.humans.Count; i++)
            {
                sum += player.humans[i].old;
                hung += player.humans[i].hungry;
                if (player.humans[i].work != null)
                    work += 1;
                if (player.humans[i].home != null)
                    homes += 1;
                if (player.humans[i].gender == Player.Human.Gender.Male)
                {
                    male++;
                }
                if (player.humans[i].gender == Player.Human.Gender.Female)
                {
                    famale++;
                }
            }


            text.text = Lang.Find("Население", 0) + ": " + "\n" +
                 $"{Lang.Find("Всего", 0)}: " + player.humans.Count + "\n" +
                 $"{Lang.Find("Баб", 0)}: " + ((famale / ((float)player.humans.Count)) * 100f).ToString("00.00") + "%\n" +
                 $"{Lang.Find("Мужиков", 0)}: " + ((male / ((float)player.humans.Count)) * 100f).ToString("00.00") + "%\n" +
                 $"\n{Lang.Find("Ср. возраст", 0)}: " + ((float)sum / (player.humans.Count)).ToString("000") + "\n" +
                 $"{Lang.Find("Голод", 0)}: " + Mathf.Abs((((float)hung / (player.humans.Count))-100f)) + "%\n" +
                 $"{Lang.Find("Занятость", 0)}: " + (int)(((float)work / (player.humans.Count)) * 100f) + "%\n" +
                 $"{Lang.Find("Есть жильё", 0)}: " + (int)(((float)homes / (player.humans.Count)) * 100f) + "%\n\n" +
                 $"{Lang.Find("Поддержка", 0)}: " + player.humansHappyness + "%\n" +
                 $"{Lang.Find("Уровень жизни", 0)}: \n" +
                 $"    {Lang.Find("Макс", 0)}: " + player.maxLivePoints + " о.\n" +
                 $"    {Lang.Find("Мин", 0)}: " + player.minLivePoints + " о.\n";
        }
        else
        {
            text.text = Lang.Find("Население", 0) + ": " + "\n" +
                    $"{Lang.Find("Всего", 0)}: " + 0 + "\n" +
                    $"{Lang.Find("Баб",0 )}: " + 0 + "%\n" +
                    $"{Lang.Find("Мужиков", 0)}: " + 0 + "%\n" +
                    $"\n{Lang.Find("Ср. возраст", 0)}: " + 0 + "\n" +
                    $"{Lang.Find("Голод", 0)}: " + 0 + "%\n" +
                    $"{Lang.Find("Занятость", 0)}: " + 0 + "%\n" +
                    $"{Lang.Find("Есть жильё", 0)}: " + 0 + "%\n\n" +
                    $"{Lang.Find("Поддержка", 0)}: " + 0 + "%";
        }
    }
}
