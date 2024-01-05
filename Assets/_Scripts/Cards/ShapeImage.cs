using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShapeImage : MonoBehaviour
{
    private Card _card;
    private RectTransform _imageContainer;
    private float height;
    private float width;

    public float ratioXY;

    private Image _image;
    private RectTransform _rt;

    void Start()
    {
        _card = gameObject.GetComponentInParent<Card>();
        _image = gameObject.GetComponent<Image>();

        _image.sprite = _card.Shape.GetComponent<Shape>().DisplayImage;
        _image.SetNativeSize();
        ratioXY = _image.rectTransform.rect.width / _image.rectTransform.rect.height;

        //Get Container Specs
        _imageContainer = transform.parent.gameObject.GetComponent<RectTransform>();
        height = _imageContainer.rect.height;
        width = _imageContainer.rect.width;

        float sizeBase = Mathf.Min(height, width);

        float newWidth;
        float newHeight;

        if (ratioXY < 1)
        {
            newHeight = sizeBase;
            newWidth = sizeBase * ratioXY;
        }
        else
        {
            newWidth = sizeBase;
            newHeight = sizeBase / ratioXY;
        }

        _image.rectTransform.sizeDelta = new Vector2(newWidth,newHeight);            
    }

    void Rotate()
    {
        
    }
}
