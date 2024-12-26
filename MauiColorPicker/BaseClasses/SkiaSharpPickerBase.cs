using System.Diagnostics;
using MauiColorPicker.Effects;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace MauiColorPicker.BaseClasses;

public abstract class SkiaSharpPickerBase : ColorPickerViewBase
    {
        protected readonly View canvasView;

        public SkiaSharpPickerBase()
        {
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;

            //if (Device.RuntimePlatform == "Windows" && Device.Idiom == TargetIdiom.Phone)
            //{
            var view = new SKCanvasView() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            view.PaintSurface += CanvasView_PaintSurface; 
            view.EnableTouchEvents = true;
            view.Touch += ViewOnTouch;
            canvasView = view;
            // }
            // else
            // {
            //     var view = new SKGLView();
            //     view.PaintSurface += GLView_PaintSurface;
            //     canvasView = view;
            // }

            // ColorPickerTouchEffect touchEffect = new ColorPickerTouchEffect()
            // {
            //     Capture = true
            // };
            // touchEffect.TouchAction += TouchEffect_TouchAction;
            //Effects.Add(touchEffect);
            //Children.Add(new Label() {Text = "A"});
            Children.Add(canvasView);
            //Children.Add(new Label() {Text = "B"});
        }

        private void ViewOnTouch(object? sender, SKTouchEventArgs e)
        {
            Debug.WriteLine($"Touch: {e.ActionType}. Location: {e.Location}");
            if (e.ActionType == SKTouchAction.Pressed)
            {
                OnTouchActionPressed(new ColorPickerTouchActionEventArgs(e));
            }
            else if (e.ActionType == SKTouchAction.Moved)
            {
                OnTouchActionMoved(new ColorPickerTouchActionEventArgs(e));
            }
            else if (e.ActionType == SKTouchAction.Released)
            {
                OnTouchActionReleased(new ColorPickerTouchActionEventArgs(e));
            }
            else if (e.ActionType == SKTouchAction.Cancelled)
            {
                OnTouchActionCancelled(new ColorPickerTouchActionEventArgs(e));
            }
            e.Handled = true;
        }

        public static readonly BindableProperty PickerRadiusScaleProperty = BindableProperty.Create(
           nameof(PickerRadiusScale),
           typeof(float),
           typeof(SkiaSharpPickerBase),
           0.05F,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandlePickerRadiusScaleSet));

        static void HandlePickerRadiusScaleSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((SkiaSharpPickerBase)bindable).InvalidateSurface();
        }

        public float PickerRadiusScale
        {
            get
            {
                return (float)GetValue(PickerRadiusScaleProperty);
            }
            set
            {
                SetValue(PickerRadiusScaleProperty, value);
            }
        }

        public abstract float GetPickerRadiusPixels();
        public abstract float GetPickerRadiusPixels(SKSize canvasSize);

        protected abstract SizeRequest GetMeasure(double widthConstraint, double heightConstraint);
        protected abstract float GetSize();
        protected abstract float GetSize(SKSize canvasSize);
        protected abstract void OnPaintSurface(SKCanvas canvas, int width, int height);
        protected abstract void OnTouchActionPressed(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionMoved(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionReleased(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args);

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return GetMeasure(widthConstraint, heightConstraint);
        }

        // protected override void LayoutChildren(double x, double y, double width, double height)
        // {
        //     canvasView.Layout(new Rect(x, y, width, height));
        // }

        protected SKPoint ConvertToPixel(Point pt)
        {
            //var canvasSize = GetCanvasSize();
            // return new SKPoint((float)(canvasSize.Width * pt.X / canvasView.Width),
            //                    (float)(canvasSize.Height * pt.Y / canvasView.Height));
            
            return new SKPoint((float)pt.X, (float)pt.Y);
        }

        protected void InvalidateSurface()
        {
            if (canvasView is SKCanvasView)
            {
                (canvasView as SKCanvasView).InvalidateSurface();
            }
            // else
            // {
            //     (canvasView as SKGLView).InvalidateSurface();
            // }
        }

        protected SKSize GetCanvasSize()
        {
            //if (canvasView is SKCanvasView)
            {
                return (canvasView as SKCanvasView).CanvasSize;
            }
            // else
            // {
            //     return (canvasView as SKGLView).CanvasSize;
            // }
        }

        protected void PaintPicker(SKCanvas canvas, SKPoint point)
        {
            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            paint.Color = Colors.White.ToSKColor();
            paint.StrokeWidth = 2;
            canvas.DrawCircle(point, GetPickerRadiusPixels() - 2, paint);

            paint.Color = Colors.Black.ToSKColor();
            paint.StrokeWidth = 1;
            canvas.DrawCircle(point, GetPickerRadiusPixels() - 4, paint);
            canvas.DrawCircle(point, GetPickerRadiusPixels(), paint);
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

        private void GLView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas, e.BackendRenderTarget.Width, e.BackendRenderTarget.Height);
        }
    }