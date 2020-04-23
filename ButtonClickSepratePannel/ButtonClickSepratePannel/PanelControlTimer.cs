using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace ButtonClickSepratePannel
{
    internal class PanelControlTimer :  IDisposable
    {
		private readonly UIElement _hiddenUiElement;
		private readonly DispatcherTimer _timer = new DispatcherTimer();

		public PanelControlTimer(UIElement hiddenUiElement)
		{
			_hiddenUiElement = hiddenUiElement;
			_timer.Interval = new TimeSpan(0, 0, 10);
			_timer.Tick += (s, arg) =>
			{
				//FadeOutStoryBoard(_hiddenUiElement);
				_timer.Stop();
			};
			hiddenUiElement.MouseEnter += HiddenUiElementMouseEnter;
			hiddenUiElement.MouseLeave += HiddenUiElementMouseLeave;
		}

		public void Start()
		{
			_timer.Start();
			_hiddenUiElement.Visibility = Visibility.Visible;
		}

		public void Stop()
		{
			_timer.Stop();
		}

		private void HiddenUiElementMouseLeave(object sender, MouseEventArgs e) { Start(); }

		private void HiddenUiElementMouseEnter(object sender, MouseEventArgs e) { Stop(); }

		public void Dispose()
		{
			_hiddenUiElement.MouseEnter += HiddenUiElementMouseEnter;
			_hiddenUiElement.MouseLeave += HiddenUiElementMouseLeave;
		}

		
	}
}
