using Android.Content;
using Android.Runtime;
using System;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Button), typeof(TruthOrDareUI.Droid.FlatButtonRenderer))]
namespace TruthOrDareUI.Droid
{
    /// <summary>
    /// Created for enabling border radius and border color
    /// </summary>
    public class FlatButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        public FlatButtonRenderer(Context context) : base(context)
        {
        }
    }
}