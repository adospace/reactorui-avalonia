using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaReactorUI.DemoApp
{
    public class TimerState : IState
    { 
        public int Timer { get; set; }
        public bool IsRunning { get; set; }
        public IDisposable SystemTimer { get; set;}
    }

    public class TimerComponent : RxComponent<TimerState>
    {
        private IDisposable SetupTimer() =>
         DispatcherTimer.Run(() =>
            {
                if (State.IsRunning)
                {
                    SetState(_ => _.Timer++);
                }

                return true;
            }, TimeSpan.FromSeconds(1));

        protected override void OnMounted()
        {
            State.SystemTimer = SetupTimer();
            base.OnMounted();
        }

        protected override void OnUpdated()
        {
            State.SystemTimer?.Dispose();
            State.SystemTimer = SetupTimer();
            base.OnUpdated();
        }

        public override VisualNode Render()
        {
            return new RxWindow()
            {
                new RxStackPanel()
                {
                    new RxTextBlock()
                        .Text(State.Timer.ToString())
                        .FontSize(24)
                        .Foreground(Brushes.Ivory)
                        .HCenter(),

                    new RxStackPanel()
                    {
                        new RxButton()
                            .Content(State.IsRunning ? "Stop" : "Start")
                            .OnClick(() => SetState(s=> s.IsRunning = !s.IsRunning)),
                        !State.IsRunning && State.Timer > 0 ? 
                        new RxButton()
                            .Content("Reset")
                            .OnClick(() => SetState(s=> s.Timer = 0))
                        :
                        null
                    }
                    .Orientation(Avalonia.Layout.Orientation.Horizontal)
                    .Spacing(10)
                }
                .Spacing(20)
                .Orientation(Avalonia.Layout.Orientation.Vertical)
                .HCenter()
                .VCenter()
                
            }
            .Background(Brushes.Green)
            .Title("Timer Demo App");
        }
    }
}
