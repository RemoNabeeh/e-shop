
namespace E_Shop.Helpers
{
    public static class Constants
    {
        public const string UsernameValidationRegex = @"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";

        public const string ApplicationTitle = "ApplicationTitle";
        public const string UserNotFound = "UserNotFound";
        public const string InvalidUsername = "InvalidUsername";
        public const string LoginDialogTitle = "LoginDialogTitle";
        public const string CartDialogTitle = "CartDialogTitle";
        public const string FilterDialogTitle = "FilterDialogTitle";
        public const string CartUpdated = "CartUpdated";
        public const string OrderSubmitted = "OrderSubmitted";

        public const string Previous = "Previous";
        public const string Next = "Next";


        #region [Parameters]

        public const string HomeMenuItem = "Home";
        public const string FilterMenuItem = "Filter";
        public const string LogoutMenuItem = "Logout";
        public const string CartNavigation = "Cart";

        public const string Username = "username";
        public const string Message = "message";
        public const string Product = "product";
        public const string FilterMinValue = "minValue";
        public const string FilterMaxnValue = "maxValue";

        public const string FilterDialogButton = "Filter";
        public const string CancelDialogButton = "Cancel";

        #endregion
    }
}
