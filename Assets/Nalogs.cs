using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nalogs : MonoBehaviour
{
    public static Nalogs n;
    private void Start()
    {
        n = this;
    }
    public float tax_income;
    public int tax_land;
    public int tax_housing;
    public int tax_ship;
}
