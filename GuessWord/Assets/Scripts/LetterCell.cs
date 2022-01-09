using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class LetterCell: Object
{
    internal char Value;
    internal char Solution;
    internal Vector3 Centre;
    internal float CellSize;
    internal GameObject Cube;

    internal LetterCell(char solution)
    {
        Solution = solution;
    }

    internal void ConfigureCell(Vector3 centre, float cellSize, GameObject prefab)
    {
        Centre = centre;
        CellSize = cellSize;
        Cube = Instantiate(prefab, centre, Quaternion.identity);
        Cube.transform.localScale *= cellSize;
    }

    internal void SetValue(char letter)
    {
        Value = letter;
        var target = Cube.transform;
        target.transform.position -= new Vector3(0, 0, 0.2f);
        RenderLetter();
    }

    internal void RenderLetter()
    {
        int fontSize = 50;
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = Value.ToString();
        t.color = Color.gray;
        t.fontSize = fontSize;
        // fudge factor to get reasonable text size:
        t.transform.localScale *= CellSize/8.125f;
        t.font.GetCharacterInfo(Value, out CharacterInfo info);
        /*
        t.transform.position = Cube.transform.position - new Vector3(
            info.glyphWidth / 2.0f + CellSize/4.0f,
            -1.0f * info.glyphHeight / 2.0f + CellSize/4.0f,
            .2f);
            */
        t.transform.position = Cube.transform.position - new Vector3(
            CellSize/6.0f,
            -1.0f * CellSize/3.0f,
            .2f);
    }

    internal bool GuessLetter(char letter)
    {
        SetValue(letter);
        return (Value == Solution);
    }
}