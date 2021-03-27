using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PoliticsPanel : MonoBehaviour
{
    public TMP_Text countries, ratings, types;
    public WorldPolitics manager;
    public Rating[] ratingsList;
    public Phrase[] xAxis;
    public PiliticsCountryType[] ctypes;
    public GameObject lockPanel;
    public int lastYear;
    [Space]
    public TMP_Text viruchkaNalog;
    public Slider vurichkaSlider;
    public int humanCost;
    [HideInInspector]
    public float time;
    public int humanAddCount;
    public TMP_Text buttonAddButton;

    private void Awake()
    {
        UpdateStats();
    }
    private void Start()
    {
        viruchkaNalog.text = $"{Lang.Find("Налог с выручки", 0)}: {vurichkaSlider.value}%";
    }
    public void ChangeHousing(TMP_InputField g)
    {
        Nalogs.n.tax_housing = g.text == "" ? 0 : int.Parse(g.text);
    }
    public void ChangeShip(TMP_InputField g)
    {
        Nalogs.n.tax_ship = g.text == "" ? 0 : int.Parse(g.text);
    }
    public void ChangeLand(TMP_InputField g)
    {
        Nalogs.n.tax_land = g.text == "" ? 0 : int.Parse(g.text);
    }
    public void ChangeIncome()
    {
        viruchkaNalog.text = $"{Lang.Find("Налог с выручки", 0)}: {vurichkaSlider.value}%";
        Nalogs.n.tax_income = vurichkaSlider.value / 100f;
    }

    public void Propaganda()
    {
        Player.player.dopLivePoints -= Random.Range(0, 3);
        lastYear = Player.player.years;
    }
    public void TalkToPeoples()
    {
        lastYear = Player.player.years;
    }
    public void AddHuman()
    {
        if (Player.player.SubMoney(humanCost))
        {
            humanCost += (int)(humanCost * 0.2f);
            humanCost += Random.Range(-5, 10);
            if (humanCost == 0)
            {
                humanCost += 20;
            }
            Player.player.humans.Add(new Player.Human());
            humanAddCount++;
        }
    }

    [System.Serializable]
    public struct Rating
    {
        public string name;
        public Vector2 range;
    }

    [System.Serializable]
    public class Phrase
    {
        [TextArea]
        public string[] sentenses = new string[2];
        public float axes;
    }

    public void UpdateStats()
    {
        buttonAddButton.text = Lang.Find("Добавить специалиста", 0) + " " + humanCost + "$";
        countries.text = Lang.Find("Страна", 0) + "\n";
        ratings.text = Lang.Find("Рейтинг", 0) + "\n";
        types.text = Lang.Find("Тип", 0) + "\n";

        for (int i = 0; i < manager.worldCountries.Count; i++)
        {
            countries.text += Lang.Find(manager.worldCountries[i].name, 2) + "\n";
            ratings.text += manager.worldCountries[i].rating + "\n";
            for (int a = 0; a < ratingsList.Length; a++)
            {
                if (manager.worldCountries[i].rating >= ratingsList[a].range.x && manager.worldCountries[i].rating <= ratingsList[a].range.y)
                {
                    types.text += " (" + Lang.Find(ratingsList[a].name, 3) + ")\n";
                    break;
                }
            }
        }
    }
    void FixedUpdate()
    {
        lockPanel.SetActive(lastYear == Player.player.years);
    }
}
