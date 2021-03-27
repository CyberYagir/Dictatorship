using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public static Quests q;

    public bool treeCut;
    public bool placeXalupa;
    public bool buidedXalupa;
    public bool startTender;
    [Space]
    public TMP_Text cutTreeT;
    public TMP_Text paceXalupaT;
    public TMP_Text buildedXalupaT;
    public TMP_Text buildedPortT;

    private void Start()
    {
        q = this;        
    }

    private void Update()
    {
        string vip = Lang.l.lang == 0 ? "Выполнено" : "Finished:", not = Lang.l.lang == 0 ? "Не выполнено" : "Not Finished";
        cutTreeT.text = (Lang.l.lang == 0 ? "Срубите деревья: " : "Destroy trees: ") + (treeCut == true ? vip : not);
        paceXalupaT.text = (Lang.l.lang == 0 ? "Постройте \"Халупа\": " : "Build \"House\": ") + (placeXalupa == true ? vip : not);
        buildedXalupaT.text = (Lang.l.lang == 0 ? "Достройте здание: " : "Finish the building: ") + (buidedXalupa == true ? vip : not);
        buildedPortT.text = (Lang.l.lang == 0 ? "Оформите первый тендер: " : "Submit your first tender: ") + (startTender == true ? vip : not);
    }
}
