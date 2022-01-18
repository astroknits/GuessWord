using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal class LetterCell: Object
{
    internal char Value;
    internal Vector3 Centre;
    internal float CellSize;
    internal Color CellColor;
    internal GameObject Cube;
    internal GameObject LetterCellParent;
    internal TextMeshPro m_LetterText;
    // internal float zOffset = 0.2f;

    internal LetterCell(GameObject letterCellParent)
    {
        LetterCellParent = letterCellParent;
        Value = ' ';
    }

    internal void ConfigureCell(Vector3 centre, float cellSize, GameObject prefab)
    {
        Centre = centre;
        CellSize = cellSize;
        Cube = Instantiate(prefab, centre, Quaternion.identity);
        Cube.transform.localScale *= cellSize;
        Cube.transform.parent = LetterCellParent.transform;
    }

    internal void SetValue(char letter)
    {
        Value = letter;
    }

    internal void SetBoxColor(Color color)
    {
        CellColor = color;
        Cube.GetComponent<Renderer>().material.color = color;
    }

    internal void SetUpText(string label, int fontSize=5)
    {
        GameObject text = new GameObject(label);
        text.transform.parent = LetterCellParent.transform;
        AddTextMesh(text, fontSize);
        text.transform.position = Cube.transform.position - new Vector3(
            0, 0, CellSize / 2.0f);
    }

    internal void AddTextMesh(GameObject text, int fontSize)
    {
        m_LetterText = text.AddComponent<TextMeshPro>();
        m_LetterText.text = Value.ToString();
        m_LetterText.color = Color.black;
        m_LetterText.fontSize = fontSize;
        m_LetterText.alignment = TextAlignmentOptions.Center;
    }
}