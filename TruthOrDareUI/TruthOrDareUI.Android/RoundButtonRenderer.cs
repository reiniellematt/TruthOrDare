using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(TruthOrDareUI.Droid.RoundButtonRenderer))]
namespace TruthOrDareUI.Droid
{
    public class RoundButtonRenderer : ButtonRenderer
    {
        public RoundButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}