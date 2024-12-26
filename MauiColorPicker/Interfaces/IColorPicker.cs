using System.ComponentModel;

namespace MauiColorPicker.Interfaces;

public interface IColorPicker : INotifyPropertyChanged
{
    Color SelectedColor { get; set; }
    IColorPicker ConnectedColorPicker { get; set; }
}