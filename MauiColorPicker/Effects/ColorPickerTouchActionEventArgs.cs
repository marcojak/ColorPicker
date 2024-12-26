using SkiaSharp.Views.Maui;

namespace MauiColorPicker.Effects;

public class ColorPickerTouchActionEventArgs : EventArgs
{
    public ColorPickerTouchActionEventArgs(long id, ColorPickerTouchActionType type, Point location, bool isInContact)
    {
        Id = id;
        Type = type;
        Location = location;
        IsInContact = isInContact;
    }

    public ColorPickerTouchActionEventArgs(SKTouchEventArgs id)
    {
        Id = id.Id;
        Location = new Point(id.Location.X, id.Location.Y);
        Type = (ColorPickerTouchActionType)id.ActionType;
        IsInContact = id.InContact;
    }

    public long Id { private set; get; }

    public ColorPickerTouchActionType Type { private set; get; }

    public Point Location { private set; get; }

    public bool IsInContact { private set; get; }
}