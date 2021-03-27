using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetBrigadeButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public int brigadeID;
    public PlayerUI ui;
    public BuildManager buildManager;
    public Image image;
    private void Update()
    {
        var build = buildManager.brigades.Find(x => x.brigadeID == brigadeID);
        if (ui.lastBuild != null && build != null && ui.lastBuild == build.builded)
        {
            image.color = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            image.color = new Color(0.745283f, 0.745283f, 0.745283f);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (ui.lastBuild != null)
        {
            var ButtonBrigade = buildManager.brigades.Find(x => x.brigadeID == brigadeID); 

            var g = ui.lastBuild.createdBuilds.building.GetComponent<HouseBuild>();
            if (g == null) { return; }

            var HouseBrigade = g.buildBrigade != -1 ? buildManager.brigades.Find(x => x.brigadeID == g.buildBrigade) : null;

           

            if (HouseBrigade == null)
            {
                print("HouseBrigade == null");
                if (ButtonBrigade.builded == null)
                {
                    ButtonBrigade.builded = g.builded;
                    ButtonBrigade.builded.GetComponent<HouseBuild>().buildBrigade = brigadeID;
                }
                else
                {
                    ButtonBrigade.builded.GetComponent<HouseBuild>().buildBrigade = -1;
                    ButtonBrigade.builded = g.builded;
                    ButtonBrigade.builded.GetComponent<HouseBuild>().buildBrigade = ButtonBrigade.brigadeID;
                }
            }
            else
            {
                if (HouseBrigade == ButtonBrigade) return;

                if (ButtonBrigade.builded == null)
                {
                    print("else ButtonBrigade.builded == null");

                    ButtonBrigade.builded = HouseBrigade.builded;
                    ButtonBrigade.builded.GetComponent<HouseBuild>().buildBrigade = brigadeID;
                    HouseBrigade.builded = null;

                }
                else
                {
                    print("else ButtonBrigade.builded != null");


                    Builded tmp = ButtonBrigade.builded;
                    ButtonBrigade.builded.GetComponent<HouseBuild>().buildBrigade = HouseBrigade.brigadeID;
                    HouseBrigade.builded.GetComponent<HouseBuild>().buildBrigade = ButtonBrigade.brigadeID;

                    ButtonBrigade.builded = HouseBrigade.builded;
                    HouseBrigade.builded = tmp;
                }
            }
        }
    }
}
