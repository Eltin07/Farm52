using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int YellowScore;
    public int BlueScore;
    public int RedScore;

    public int WaterLevel;
    public int DeadPlants;

    public void AddHarvestScore(Crop crop)
    {
        int count = crop.Slots.Count;
        switch(crop.Plant.PlantType)
        {
            case PlantType.Yellow:
                YellowScore += count;
                break;
            case PlantType.Blue:
                WaterLevel += count;
                break;
            case PlantType.Red:
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
