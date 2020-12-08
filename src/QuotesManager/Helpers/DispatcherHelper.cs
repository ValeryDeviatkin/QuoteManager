using System;
using System.Windows;
using System.Windows.Threading;

namespace QuotesManager.Helpers
{
    internal static class DispatcherHelper
    {
        public static DispatcherOperation BeginInvokeInMainThread(Action action) =>
            Application.Current.Dispatcher.BeginInvoke(action);
    }
}