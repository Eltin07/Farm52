using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int YellowScore;
    public int BlueScore;
    public int RedScore;

    public void AddHarvestScore(int count, CardColor color)
    {
        switch(color)
        {
            case CardColor.Yellow:
                YellowScore += count;
                break;
            case CardColor.Blue:
                BlueScore += count;
                break;
            case CardColor.Red:
                RedScore += count;
                break;
            default:
                break;
        }
    }
}
