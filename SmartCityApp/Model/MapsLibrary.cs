using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SmartCityApp.Model
{
    public class MapsLibrary
    {
        public async Task<Geopoint> Position()
        {
            return (await new Geolocator().GetGeopositionAsync()).Coordinate.Point;
        }

        public UIElement MarkerMyPosition()
        {
            Canvas marker = new Canvas();
            Ellipse outer = new Ellipse();
            outer.Fill = new SolidColorBrush(Colors.Blue);
            outer.Margin = new Thickness(-12.5, -12.5, 0, 0);
            Ellipse inner = new Ellipse() { Width = 50, Height = 50 };
            inner.Margin = new Thickness(-10, -10, 0, 0);
            Ellipse core = new Ellipse() { Width = 10, Height = 10 };
            core.Fill = new SolidColorBrush(Colors.Green);
            core.Margin = new Thickness(-5, -5, 0, 0);
            TextBlock textMarker = new TextBlock();
            textMarker.Foreground = new SolidColorBrush(Colors.Green);
            textMarker.FontSize = 14;
            textMarker.FontWeight = FontWeights.ExtraBold;
            marker.Background = new SolidColorBrush(Colors.Black);
            textMarker.Text = "My Position";
            marker.Children.Add(outer);
            marker.Children.Add(inner);
            marker.Children.Add(core);
            marker.Children.Add(textMarker);
            return marker;
        }

        public UIElement Marker(String txt)
        {
            Canvas marker = new Canvas();
            Ellipse outer = new Ellipse();
            outer.Fill = new SolidColorBrush(Colors.Blue);
            outer.Margin = new Thickness(-12.5, -12.5, 0, 0);
            Ellipse inner = new Ellipse() { Width = 50, Height = 50 };
            inner.Margin = new Thickness(-10, -10, 0, 0);
            Ellipse core = new Ellipse() { Width = 10, Height = 10 };
            core.Fill = new SolidColorBrush(Colors.Blue);
            core.Margin = new Thickness(-5, -5, 0, 0);
            TextBlock textMarker = new TextBlock();
            textMarker.Foreground = new SolidColorBrush(Colors.Green);
            textMarker.FontSize = 14;
            textMarker.FontWeight = FontWeights.ExtraBold;
            marker.Background = new SolidColorBrush(Colors.Black);
            textMarker.Text = txt;
            marker.Children.Add(outer);
            marker.Children.Add(inner);
            marker.Children.Add(core);
            marker.Children.Add(textMarker);
            return marker;
        }

        public UIElement MarkerText(string Text)
        {
            Canvas marker = new Canvas();
            TextBlock textMarker = new TextBlock();
            textMarker.Foreground = new SolidColorBrush(Colors.Red);
            marker.Background = new SolidColorBrush(Colors.Black);
            textMarker.FontSize = 16;
            textMarker.Text = Text;
            marker.Children.Add(textMarker);
            return marker;
        }
    }
}