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
    // internal float zOffset = 0.2f;

    internal LetterCell(GameObject letterCellParent)
    {
        LetterCellParent = letterCellParent;
    }

    internal void ConfigureCell(Vector3 centre, float cellSize, GameObject prefab)
    {
        Centre = centre;
        CellSize = cellSize;
        Cube = Instantiate(prefab, centre, Quaternion.identity);
        Cube.transform.localScale *= cellSize;
        Cube.transform.parent = LetterCellParent.transform;
    }

    internal void SetValue(char letter, int i, int j)
    {
        Value = letter;
        var target = Cube.transform;
        RenderLetter(i, j);
    }

    internal void SetBoxColor(Color color)
    {
        CellColor = color;
        Cube.GetComponent<Renderer>().material.color = color;
    }

    internal void RenderLetter(int i, int j, int fontSize=5)
    {
        GameObject text = new GameObject($"Guess {j} Letter {i}: {Value.ToString()}");
        text.transform.parent = LetterCellParent.transform;
        AddTextMesh(text, fontSize);
        text.transform.position = Cube.transform.position - new Vector3(
            0, 0, CellSize / 2.0f);
    }

    internal void AddTextMesh(GameObject text, int fontSize)
    {
        TextMeshPro t = text.AddComponent<TextMeshPro>();
        t.text = Value.ToString();
        t.color = Color.black;
        t.fontSize = fontSize;
        t.alignment = TextAlignmentOptions.Center;
    }
}