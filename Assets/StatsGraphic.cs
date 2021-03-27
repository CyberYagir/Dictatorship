using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsGraphic : MonoBehaviour
{
    public List<RectTransform> cols;
    public List<TMP_Text> texts;
    public Player pl;
    public TMP_Text maxT, midT;

    public bool moneys;
    private void Start()
    {
        pl = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (moneys)
        {
            float max = -9999999;
            for (int i = 0; i < pl.moneyPerYearStats.Count; i++)
            {
                if (pl.moneyPerYearStats[i] > max)
                {
                    max = pl.moneyPerYearStats[i];
                }
            }
            maxT.text = max.ToString();
            midT.text = (max / 2).ToString();
            for (int i = 0; i < pl.moneyPerYearStats.Count; i++)
            {
                cols[i].localScale = new Vector3(1, (float)(pl.moneyPerYearStats[i] / (max + 1)), 1);
                texts[i].text = (pl.localYearNum + i).ToString();
            }
        }
        else
        {
            float max = -9999999;
            for (int i = 0; i < pl.humansPerYears.Count; i++)
            {
                if (pl.humansPerYears[i] > max)
                {
                    max = pl.humansPerYears[i];
                }
            }
            maxT.text = max.ToString();
            midT.text = (max / 2).ToString();
            for (int i = 0; i < pl.humansPerYears.Count; i++)
            {
                if (max != 0)
                    cols[i].localScale = new Vector3(1, (float)(pl.humansPerYears[i] / (max)), 1);
                else
                    cols[i].localScale = new Vector3(1, 0, 1);
                texts[i].text = (pl.localYearNum + i).ToString();
            }
        }
    }
}
