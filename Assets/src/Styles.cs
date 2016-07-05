using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

[Serializable]
public class Styles {

    public int style1;
    public int style2;
    public int style3;
    public int style4;

    public void Parse(string content)
    {
        if (content == "") return;

        Debug.Log("content: " + content);

         string[] allData = Regex.Split(content, "-");

         if(allData.Length>0)
         {
             style1 = int.Parse(allData[0]);
             style2 = int.Parse(allData[1]);
             style3 = int.Parse(allData[2]);
             style4 = int.Parse(allData[3]);
         }
    }

}
