using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

[Serializable]
public class Styles {

    public string style;

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
    public int pantalon_tela;
    public int guantes_tela;

    public void Parse(string style)
    {
        this.style = style;
        if (style == "") return;

     //   Debug.Log("content: " + style);

        string[] allData = Regex.Split(style, "-");

         if(allData.Length>9)
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
             //if(allData.Length>10)
             //   pantalon_tela = int.Parse(allData[11]);
             //if (allData.Length > 11)
             //    guantes_tela = int.Parse(allData[12]);
         }
    }
    public void Save()
    {
        style = cabezas + "-";
        style += peinados + "-";
        style += pelos + "-";
        style += piel + "-";
        style += narices + "-";
        style += barbas + "-";
        style += cejas + "-";
        style += guantes + "-";
        style += pantalon + "-";
        style += botas + "-";
        style += tatoo + "-";
        style += pantalon_tela + "-";
        style += guantes_tela;

        PlayerPrefs.SetString("styles", style);

        if(SocialManager.Instance.userData.logged)
            Events.OnSaveStyles(style);
    }


}
