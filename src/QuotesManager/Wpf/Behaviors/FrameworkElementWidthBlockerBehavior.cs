using System.Windows;
using System.Windows.Interactivity;

namespace QuotesManager.Wpf.Behaviors
{
    internal class FrameworkElementWidthBlockerBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.MaxWidth = AssociatedObject.ActualWidth;
        }
    }
}