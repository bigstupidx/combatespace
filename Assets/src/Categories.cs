using UnityEngine;
using System.Collections;

public static class Categories {

    public static string GetCategorieByScore(int score)
    {
        if(score<33)
            return "COBRE";
        else if(score<66)
            return "BRONCE";
        else
            return "ORO";    
    }
    public static int GetCategorieIdScore(int score)
    {
        if (score < 33)
            return 1;
        else if (score < 66)
            return 2;
        else
            return 3;
    }
}
