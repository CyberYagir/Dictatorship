using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    public TMP_Text stats;
    public Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        stats.text = $"{Lang.Find("Ресурсы", 0)}: \n" +
            $"{Lang.Find("Казна", 0)}: " + player.money.ToString() + "$\n" +
            $"{Lang.Find("Потрачено", 0)}: " + player.moneySpent.ToString() + "$\n" +
            $"{Lang.Find("Заработано", 0)}: " + player.moneyAdded.ToString() + "$\n" +
            $"     {Lang.Find("За 12 лет", 0)}: " + player.moneyPerCycle.ToString() + "$\n" +
            $"{Lang.Find("Энергии", 0)}: " + player.watts + $" {Lang.Find("Ватт", 0)}\n" +
            $"{Lang.Find("Потреблено", 0)}: " + player.wattsEated + $" {Lang.Find("Ватт", 0)}\n"; 
    }
}
