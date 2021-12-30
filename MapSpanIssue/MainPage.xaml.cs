using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapSpanIssue
{
    public partial class MainPage : ContentPage
    {
        private bool done;
        public MainPage()
        {
            InitializeComponent();
            UpdateRegion();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!done)
                    UpdateRegion();
                return true;
            });
        }

        ~MainPage()
        {
            done = true;
        }

        private void UpdateRegion()
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            StatusLabel.Text = FormatMapSpan(CurrentMap?.VisibleRegion));
        }

        private string FormatMapSpan(MapSpan mapSpan)
        {
            if (mapSpan != null)
                return $"{nameof(mapSpan.Center)}: [{mapSpan.Center.Latitude},{mapSpan.Center.Longitude}]\n{nameof(mapSpan.Radius)}: {mapSpan.Radius.Meters}m\n{nameof(mapSpan.LatitudeDegrees)}: {mapSpan.LatitudeDegrees}\n{nameof(mapSpan.LongitudeDegrees)}: {mapSpan.LongitudeDegrees}";
            return "No region visible";
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-23.0, 180), new Distance(1000)));
        }
    }
}
