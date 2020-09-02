
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

        #region [Parameters]

        public const string Username = "username";
        public const string Message = "message";
        public const string Product = "product";

        #endregion
    }
}
