using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridCellSize : MonoBehaviour
{
    public Vector2 gridSize;
    public float width;
    public float height;
    // Use this for initialization
    void Start()
    {
        width = this.gameObject.GetComponent<RectTransform>().rect.width;
        height = this.gameObject.GetComponent<RectTransform>().rect.height;
        float gridSpacing = GetComponent<GridLayoutGroup>().spacing.x;
        Vector2 newSize = new Vector2((width / gridSize.x) - (gridSpacing * 0.5f), (width / gridSize.x) - (gridSpacing * 0.5f));
        this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
