using Android.Widget;
using Microsoft.Maui.Handlers;
using static Android.Views.ViewGroup;

namespace MauiColorPicker;

public partial class RGBSlidersHandler : ViewHandler<RGBSliders, FrameLayout>
{
    private FrameLayout _view;

    public static PropertyMapper<RGBSliders, RGBSlidersHandler> RGBSlidersMapper = new PropertyMapper<RGBSliders, RGBSlidersHandler>(ViewHandler.ViewMapper)
    {
    };

    public RGBSlidersHandler(): base(RGBSlidersMapper)
    {
        
    }
    
    protected override FrameLayout CreatePlatformView()
    {
        _view = new FrameLayout(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
        };
        return _view;
    }
}