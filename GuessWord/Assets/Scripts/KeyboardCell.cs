using UnityEngine;

internal class KeyboardCell : LetterCell
{
    internal KeyboardCell(char value, GameObject letterCellParent) : base(letterCellParent)
    {
        SetValue(value);
    }
}
