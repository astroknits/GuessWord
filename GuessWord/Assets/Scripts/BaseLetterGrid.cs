using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class BaseLetterGrid : Object
{
    // Prefab for individual letters
    internal GameObject m_LetterBox; // set in editor
    // Parent empty GameObject for grouping the letter boxes
    internal GameObject m_LetterBoxParent;
    // Quad defining region of grid on screen
    internal GameObject m_GridQuad; // set in editor

    internal int m_MaxRows;
    internal int m_MaxCols;
    internal float m_GridMargin;
    internal float m_CellPadding;
    internal float m_ZOffset = 0.8f;

    internal BaseLetterGrid(GameObject letterBox,
                      GameObject letterBoxParent,
                      GameObject gridQuad,
                      int maxCols, // max number of cols in the grid
                      int maxRows, // max number of rows in the grid
                      float gridMargin,
                      float cellPadding)
    {
        m_LetterBox = letterBox;
        m_LetterBoxParent = letterBoxParent;
        m_GridQuad = gridQuad;
        m_MaxRows = maxRows;
        m_MaxCols = maxCols;
        m_GridMargin = gridMargin;
        m_CellPadding = cellPadding;
    }

    internal void Destroy()
    {
        foreach (var gameObject in m_LetterBoxParent.GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(gameObject.gameObject);
        }
    }

    internal Vector3 GetUpperLeftCorner()
    {
        Vector3 gridPosition = m_GridQuad.transform.position;
        Vector3 gridScale = m_GridQuad.transform.localScale;
        return new Vector3(
            gridPosition.x - gridScale.x / 2.0f,
            gridPosition.y + gridScale.y / 2.0f,
            gridPosition.z);
    }

    internal float GetCellSize()
    {
        Vector3 gridScale = m_GridQuad.transform.localScale;
        float cellWidth = (gridScale.x - 2.0f * m_GridMargin - (m_MaxCols - 1.0f) * m_CellPadding) / m_MaxCols;
        float cellHeight = (gridScale.y - 2.0f * m_GridMargin - (m_MaxRows - 1.0f) * m_CellPadding) / m_MaxRows;
        return Mathf.Min(cellWidth, cellHeight);
    }
}