using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ButtonClickSepratePannel
{
   public class MouseClickElementBehaviour
    {
		public static readonly DependencyProperty HiddenUiElementProperty =
			DependencyProperty.RegisterAttached("HiddenUiElement",
				typeof(UIElement), typeof(MouseClickElementBehaviour),
				new PropertyMetadata(null, OnHiddenUiElementChanged));

		public static UIElement GetHiddenUiElement(UIElement uiElement)
		{
			return (UIElement)uiElement.GetValue(HiddenUiElementProperty);
		}

		public static void SetHiddenUiElement(UIElement uiElement, UIElement value)
		{
			uiElement.Visibility = Visibility.Collapsed;
			uiElement.SetValue(HiddenUiElementProperty, value);
		}

		private static void OnHiddenUiElementChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var uiElement = (UIElement)sender;
			if (e.OldValue != null) GetVisibilityTimer((UIElement)e.OldValue).Dispose();

			if (e.NewValue == null)
			{
				uiElement.MouseLeftButtonDown -= MouseDownEvent;
				SetVisibilityTimer(uiElement, null);
			}
			else if (e.OldValue == null)
			{
				var hiddenUiElement = (UIElement)e.NewValue;
				hiddenUiElement.Visibility = Visibility.Collapsed;
				uiElement.MouseLeftButtonDown += MouseDownEvent;
				var visibilityTimer = new PanelControlTimer(hiddenUiElement);
				SetVisibilityTimer(uiElement, visibilityTimer);
			}
		}

		private static void MouseDownEvent(object sender, RoutedEventArgs e)
		{
			var hiddenUiElement = GetHiddenUiElement((UIElement)sender);
			hiddenUiElement.Visibility = (hiddenUiElement.Visibility == Visibility.Collapsed)
				? Visibility.Visible : Visibility.Collapsed;
			if (hiddenUiElement.Visibility == Visibility.Visible)
				GetVisibilityTimer((UIElement)sender).Start();
			else GetVisibilityTimer((UIElement)sender).Stop();
		}

		private static readonly DependencyProperty VisibilityTimerProperty =
			DependencyProperty.RegisterAttached("VisibilityTimer",
				typeof(PanelControlTimer), typeof(MouseClickElementBehaviour), new PropertyMetadata(null));

		private static PanelControlTimer GetVisibilityTimer(UIElement uiElement)
		{
			return (PanelControlTimer)uiElement.GetValue(VisibilityTimerProperty);
		}

		private static void SetVisibilityTimer(UIElement uiElement, PanelControlTimer value)
		{
			uiElement.SetValue(VisibilityTimerProperty, value);
		}
	}
}
