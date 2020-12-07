using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Wpf.Tools.Helpers
{
    public static class ExceptionLogHelper
    {
        public static void LogCriticalException(this object raiser, Exception e, [CallerMemberName] string method = "")
        {
            var exceptionSource = $"{raiser.GetType().Name}.{method}";

            #if DEBUG
            Debug.Fail(exceptionSource, e.Message);
            #else
            MessageBox.Show(e.Message, exceptionSource, MessageBoxButton.OK, MessageBoxImage.Error);
            #endif
        }
    }
}