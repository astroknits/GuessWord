using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class LetterCell: Object
{
    internal Vector3 Centre;
    internal char Value;
    internal char Solution;
    internal GameObject Cube;

    internal LetterCell(char solution)
    {
        Solution = solution;
    }

    internal void ConfigureCell(Vector3 centre, float cellSize, GameObject prefab)
    {
        Centre = centre;
        Cube = Instantiate(prefab, centre, Quaternion.identity);
        Cube.transform.localScale *= cellSize;

        int fontSize = 50;
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "T";
        t.color = Color.gray;
        t.fontSize = fontSize;
        // CharacterInfo info;
        t.transform.localScale *= 0.1f;  // + new Vector3(0, 0, -.2f);
        t.font.GetCharacterInfo('T', out CharacterInfo info);
        t.transform.position = Cube.transform.position - new Vector3(
            info.glyphWidth / (float)fontSize,
            -1.0f * info.glyphHeight / (float)fontSize,
            .2f);

        // t.transform.localPosition = t.transform.localPosition - new Vector3(info.maxX / 2.0f, -1.0f * info.maxY / 2.0f, .2f);
    }

    internal void SetValue(char letter)
    {
        Value = letter;
        var target = Cube.transform;
        target.transform.position += new Vector3(0, 0, 0.5f);
    }

    internal bool GuessLetter(char letter)
    {
        SetValue(letter);
        return (Value == Solution);
    }
}