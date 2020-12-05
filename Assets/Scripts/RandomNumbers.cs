using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumbers
{

    public static int MAX_NUMBER = 10;

    private enum Numbers
    {
        Zero,
        Un,
        Dos,
        Tres,
        Quatre,
        Cinc,
        Sis,
        Set,
        Vuit,
        Nou,
        Deu
    }

    public static void GenerateRandomNumbers(out string correctNumberText, out int correctNumber)
    {
        Numbers randomN = (Numbers)Random.Range(0, 10);
        correctNumber = (int)randomN;
        correctNumberText = randomN.ToString();
    }
}
