using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nofications : MonoBehaviour
{
    public List<string> nofications;
    public static Nofications n;
    public TMP_Text text;
    public Animator animator;
    public void Start()
    {
        n = this;
        StartCoroutine(loop());
    }

    IEnumerator loop()
    {
        while (true)
        {
            if (nofications.Count == 0)
            {
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                text.text = nofications[0];
                nofications.RemoveAt(0);
                animator.Play("Show");
                yield return new WaitForSeconds(0.5f);
                yield return new WaitForSeconds(3f);
                animator.Play("Hide");
                yield return new WaitForSeconds(0.6f);
            }
        }
    }

    public static void AddNof(string str)
    {
        n.nofications.Add(str);
    }

}
