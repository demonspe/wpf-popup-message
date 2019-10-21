using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PopupWpfTest
{
  /// <summary>
  /// Interaction logic for PopupWindow.xaml
  /// </summary>
  public partial class PopupWindow : Window
  {
    public PopupWindow()
    {
      InitializeComponent();
      
      this.Opacity = 0.0;

      
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      Dispatcher.BeginInvoke(new Action(() => this.CloseSlowly()));
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      this.CloseSlowly();
    }

    private void CloseSlowly()
    {
      this.Close();
    }

    private bool shown = false;
    private void PopupWindow1_ContentRendered(object sender, EventArgs e)
    {
      if (shown)
        return;

      shown = true;

      var topPos = this.Top;
      Storyboard storyboard = new Storyboard();
      var duration = new TimeSpan(0, 0, 0, 0, 200);
      DoubleAnimation animation = new DoubleAnimation { From = 0.0, To = 1.0, Duration = new Duration(duration) };
      DoubleAnimation animationPos = new DoubleAnimation { From = topPos + 50, To = topPos, Duration = new Duration(duration) };

      Storyboard.SetTargetName(animation, "PopupWindow1");
      Storyboard.SetTargetName(animationPos, "PopupWindow1");
      Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity", 0));
      Storyboard.SetTargetProperty(animationPos, new PropertyPath("Top"));
      storyboard.Children.Add(animation);
      storyboard.Children.Add(animationPos);
      storyboard.Begin(this);

      Timer timer = new Timer(2000);
      timer.Start();
      timer.Elapsed += Timer_Elapsed;
    }
  }
}
