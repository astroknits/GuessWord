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
    internal GameObject LetterCellParent;
    // internal float zOffset = 0.2f;

    internal LetterCell(char solution, GameObject letterCellParent)
    {
        Solution = solution;
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
        // target.transform.position -= new Vector3(0, 0, zOffset);
        RenderLetter(i, j);
    }

    internal void RenderLetter(int i, int j)
    {
        // TODO: Revisit, make text location tuning less hacky
        int fontSize = 50;
        GameObject text = new GameObject($"Guess {j} Letter {i}: {Value.ToString()}");
        text.transform.parent = LetterCellParent.transform;
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = Value.ToString();
        t.color = Color.gray;
        t.fontSize = fontSize;
        // fudge factor to get reasonable text size (need to scale by cell size)
        t.transform.localScale *= CellSize/8.125f;
        t.offsetZ = -0.1f;
        t.font.GetCharacterInfo(Value, out CharacterInfo info);
        /*
         https://docs.unity3d.com/ScriptReference/CharacterInfo.html
        t.transform.position = Cube.transform.position - new Vector3(
            info.glyphWidth / 2.0f + CellSize/4.0f,
            -1.0f * info.glyphHeight / 2.0f + CellSize/4.0f,
            .2f);
            */
        t.transform.position = Cube.transform.position - new Vector3(
            CellSize / 6.0f,
            -1.0f * CellSize / 3.0f,
            Cube.transform.localScale.z/2.0f - t.offsetZ);
    }

    internal bool GuessLetter(char letter, int i, int j)
    {
        SetValue(letter, i, j);
        return (Value == Solution);
    }
}