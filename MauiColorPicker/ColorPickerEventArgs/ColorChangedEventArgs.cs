namespace MauiColorPicker.ColorPickerEventArgs;

public class ColorChangedEventArgs : EventArgs
{
    public ColorChangedEventArgs(Color oldColor, Color newColor)
    {
        OldColor = oldColor;
        NewColor = newColor;
    }

    public Color OldColor { get; private set; }
    public Color NewColor { get; private set; }
}