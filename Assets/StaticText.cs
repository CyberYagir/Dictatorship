using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticText : MonoBehaviour
{
    public TMP_Text text;
    [TextArea]
    public List<string> texts;
    public bool update = true;
    public void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (update)
            text.text = texts[Lang.l.lang];
    }

}
