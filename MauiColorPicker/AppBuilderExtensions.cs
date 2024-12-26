using MauiColorPicker.Effects;

namespace MauiColorPicker;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseRGBSlider(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(handlers =>
        {
#if ANDROID
            //handlers.AddHandler(typeof(RGBSliders), typeof(RGBSlidersHandler));
#endif
        });
        return builder;
    }
}