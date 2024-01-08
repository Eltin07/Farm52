using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaterJug : MonoBehaviour
{
    public Image _fillImage;
    private float width;
    private float maxHeight;

    private const int MAX_WATER = 100;
    private int WaterLevel;

    void Awake() => Card.OnCardPlayed += UpdateWaterLevel;
    void OnDestroy() => Card.OnCardPlayed -= UpdateWaterLevel;

    void Start()
    {
        width = _fillImage.rectTransform.rect.size.x;
        maxHeight = _fillImage.rectTransform.rect.size.y;
    }

    private void UpdateWaterLevel()
    {
        WaterLevel = GameManager.Instance.ScoreManager.WaterLevel;
        float fillPercent = WaterLevel / MAX_WATER;

        _fillImage.rectTransform.sizeDelta = new Vector2(width, fillPercent * maxHeight);
    }
}
