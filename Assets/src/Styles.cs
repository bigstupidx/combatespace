using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

[Serializable]
public class Styles {

    public int cabezas;
    public int peinados;
    public int pelos;
    public int piel;
    public int narices;
    public int barbas;
    public int cejas;
    public int guantes;
    public int pantalon;
    public int botas;
    public int tatoo;

    public void Parse(string content)
    {
        if (content == "") return;

        Debug.Log("content: " + content);

         string[] allData = Regex.Split(content, "-");

         if(allData.Length>0)
         {
             cabezas = int.Parse(allData[0]);
             peinados = int.Parse(allData[1]);
             pelos = int.Parse(allData[2]);
             piel = int.Parse(allData[3]);
             narices = int.Parse(allData[4]);
             barbas = int.Parse(allData[5]);
             cejas = int.Parse(allData[6]);
             guantes = int.Parse(allData[7]);
             pantalon = int.Parse(allData[8]);
             botas = int.Parse(allData[9]);
             tatoo = int.Parse(allData[10]);
         }
    }

}
