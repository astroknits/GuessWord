using UnityEngine;

internal class KeyboardArrangement : Object
{
    internal KeyboardType m_Type;
    internal int m_MaxCols;
    internal int m_MaxRows;
    internal char[][] m_KeyboardLetterGrid;

    internal static KeyboardArrangement GetKeyboardArrangement(KeyboardType type)
    {
        switch (type)
        {
            case KeyboardType.ALPHA:
                return new AlphaKeyboard();

            case KeyboardType.QWERTY:
                return new QwertyKeyboard();

            default:
                return new QwertyKeyboard();
        }
    }
}

internal class QwertyKeyboard : KeyboardArrangement
{
    internal QwertyKeyboard()
    {
        m_Type = KeyboardType.QWERTY;
        m_MaxCols = 10;
        m_MaxRows = 3;
        m_KeyboardLetterGrid = new char[3][];
        m_KeyboardLetterGrid[0] = "QWERTYUIOP".ToCharArray();
        m_KeyboardLetterGrid[1] = "ASDFGHJKL".ToCharArray();
        m_KeyboardLetterGrid[2] = "ZXCVBNM".ToCharArray();
    }
}

internal class AlphaKeyboard : KeyboardArrangement
{
    internal AlphaKeyboard()
    {
        m_Type = KeyboardType.QWERTY;
        m_MaxCols = 9;
        m_MaxRows = 3;
        m_KeyboardLetterGrid = new char[3][];
        m_KeyboardLetterGrid[0] = "ABCDEFGHI".ToCharArray();
        m_KeyboardLetterGrid[1] = "JKLMNOPQR".ToCharArray();
        m_KeyboardLetterGrid[2] = "STUVWXYZ".ToCharArray();
    }
}