using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int YellowScore;
    public int BlueScore;
    public int RedScore;

    public int DeadPlants;

    public void AddHarvestScore(Group crop)
    {
        int count = crop.Slots.Count;
        switch(crop.Color)
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

    public void AddDeadPlants()
    {
        DeadPlants++;

        if (DeadPlants >= 5)
            GameManager.Instance.UpdateGameState(GameState.GameOver);
    }

}
