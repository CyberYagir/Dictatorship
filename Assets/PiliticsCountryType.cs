using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[ExecuteInEditMode]
public class PiliticsCountryType : MonoBehaviour, IPointerDownHandler
{
    public Image image;

    [TextArea]
    public string[] names = new string[2];
    [TextArea]
    public string[] phrases;
    public List<Phrases> allPhrases;

    [System.Serializable]
    public class Phrases
    {
        [TextArea]
        public string[] phrase = new string[2];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<SetPolitics>().point.transform.position = eventData.position;
        FindObjectOfType<Player>().politics = FindObjectOfType<SetPolitics>().point.transform.localPosition/200;
        FindObjectOfType<Player>().regim = gameObject.name;
    }

    private void Start()
    {
        //int n = 0;
        //allPhrases = new List<Phrases>();
        //for (int i = 0; i < phrases.Length; i++)
        //{
        //    allPhrases.Add(new Phrases());
        //    allPhrases[n].phrase = new string[2];
        //    allPhrases[n].phrase[0] = phrases[i];
        //    n++;
        //}
        image = GetComponent<Image>();
    }

}
