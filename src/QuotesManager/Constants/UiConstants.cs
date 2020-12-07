using System.Windows;

namespace QuotesManager.Constants
{
    internal static class UiSizes
    {
        public const double BorderRadiusSize = 2d;
        public const double BorderThicknessSize = 1d;

        public const double TextSpacing = 2d;
        public const double InnerSpacing = 5d;
        public const double OuterSpacing = 20d;

        public static readonly CornerRadius BorderRadius = new CornerRadius(BorderRadiusSize);
        public static readonly Thickness BorderThickness = new Thickness(BorderThicknessSize);

        public static readonly GridLength TextSpacingGridLength = new GridLength(TextSpacing);
        public static readonly GridLength InnerSpacingGridLength = new GridLength(InnerSpacing);
        public static readonly GridLength OuterSpacingGridLength = new GridLength(OuterSpacing);

        public static readonly Thickness TextSpacingThickness = new Thickness(TextSpacing);
        public static readonly Thickness InnerSpacingThickness = new Thickness(InnerSpacing);
        public static readonly Thickness OuterSpacingThickness = new Thickness(OuterSpacing);
    }
}