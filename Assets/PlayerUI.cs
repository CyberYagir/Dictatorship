using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public TMP_Text date, money, happyness;
    public Player player;
    public GameObject treeUI, humanUI, portUI, buildUI, canvas;
    public GameObject treeLine;
    [HideInInspector]
    public GameObject lastTree;
    [HideInInspector]
    public int treeCount;
    [HideInInspector]
    public Player.Human lastHuman;
    public TMP_Text treeDestroy, playerY;
    [Space]
    public TMP_Text humanName;
    public TMP_Text humanStats;
    [Space]
    public Builded lastBuild;
    public GameObject politicsButton;
    public PoliticsPanel pPanel;
    public GameObject escape, eventPanel, quests;
    public Slider sound;

    public TMP_Text yourYears;
    private void Start()
    {
        quests.active = true;
        sound.value = PlayerPrefs.GetFloat("Sound", 0.5f);
        AudioListener.volume = PlayerPrefs.GetFloat("Sound", 0.5f);
    }
    public void ToMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeSound()
    {
        AudioListener.volume = sound.value;
        PlayerPrefs.SetFloat("Sound", sound.value);
    }
    private void Update()
    {
        {
            var obj = GameObject.FindGameObjectWithTag("Capitol");
            if (obj != null && obj.GetComponent<HouseBuild>() == null)
            {
                politicsButton.SetActive(true);
            }
            else politicsButton.SetActive(false);
        }
	if (Input.GetKeyDown(KeyCode.F1)){
		canvas.SetActive(!canvas.active);
	}
        yourYears.text = (Lang.l.lang == 0 ? "Возраст" : "Old") + ": " + player.yourYear;

        if (Input.GetKeyDown(KeyCode.Escape)) escape.active = !escape.active;

        if (escape.active || eventPanel.active || quests.active) Time.timeScale = 0;
        else Time.timeScale = 1;
        portUI.GetComponent<PortsPanel>().UpdatePort();
        pPanel.time += Time.deltaTime;
        if (pPanel.time > 5)
        {
            if (pPanel.humanAddCount != 0)
            {
                Nofications.AddNof(Lang.Find("На остров прибыло",6) + " " + pPanel.humanAddCount + " " + Lang.Find("человек",6));
                pPanel.humanAddCount = 0;
                pPanel.time = 0;
            }
        }


        date.text = player.day.ToString("00") + "." + player.mounths.ToString("00") + "." + player.years.ToString("00");
        money.text ="$ " + player.money.ToString("0000000");
        float happ = 0;
        for (int i = 0; i < player.humans.Count; i++)
        {
            happ += player.humans[i].happiness;
        }
        if (player.humans.Count != 0)
        {
            player.humansHappyness = (happ / (player.humans.Count));
        }
        else
        {
            player.humansHappyness = 0;
        }
        happyness.text = player.humansHappyness.ToString("000") + "%";
        if (lastTree != null)
        {
            treeDestroy.text = Lang.Find("Уничтожить деревья", 0) + ": " + treeCount + "\n" + Lang.Find("Цена", 0) + ": " + (treeCount * 4) + "$";
        }
        playerY.text = "X" + player.transform.position.y.ToString("00");
        if (humanUI.active)
        {
            humanName.text = lastHuman.name;
            var e = lastHuman.education;
            humanStats.text = Lang.Find("Параметры", 0) + ":\n" + Lang.Find("Голод", 0) + ": " + lastHuman.hungry + "%\n" + Lang.Find("Образов.", 0) + ": " + 
                (e == Player.Human.Education.Bachelor? Lang.Find("Бакалавр", 4) : 
                (e == Player.Human.Education.Doctoral ? Lang.Find("Доктор", 4) : 
                (e == Player.Human.Education.Early ? Lang.Find("Нулевое", 4) : 
                (e == Player.Human.Education.Lower_secondary ? Lang.Find("Базовое высшее", 4) : 
                (e == Player.Human.Education.Master ? Lang.Find("Магистр" , 4) : 
                (e == Player.Human.Education.Post_secondary ? Lang.Find("Два высших", 4) : 
                (e == Player.Human.Education.Primary ? Lang.Find("Школьное", 4) : 
                (e == Player.Human.Education.Short_cycle ? Lang.Find("Полное", 4) : 
                (e == Player.Human.Education.Upper_secondary ? Lang.Find("Два полных", 4) : ""))))))))) + " (" + ((int)e) + ")\n" + Lang.Find("Возраст", 0) + ": " + lastHuman.old + "\n" + Lang.Find("Страна", 0) + ": " + lastHuman.country.ToString() + "\n" + Lang.Find("Пол",0) + ": " + (lastHuman.gender == Player.Human.Gender.Male ? Lang.Find("Мужик", 0) : Lang.Find("Баба", 0)) + "\n\n" + Lang.Find("Работа", 0) + ": " + (lastHuman.work != null ? lastHuman.work.build.names[Lang.l.lang] : Lang.Find("Тунеядец", 0)) + "\n" + Lang.Find("Живёт", 0) + ": " + (lastHuman.home == null ? Lang.Find("Бомж", 0) : lastHuman.home.build.names[Lang.l.lang]);
        }
    }


    public void DeleteBuild()
    {
        if (lastBuild)
        {
            lastBuild.DestroyBuild();
        }
    }

    public void CutTrees()
    {
        if (lastTree != null)
        {
            if (Player.player.SubMoney((int)(treeCount * 4f)))
            {
                lastTree.GetComponentInChildren<TreeUI>().DestroyTrees();
            }
        }
    }
}
