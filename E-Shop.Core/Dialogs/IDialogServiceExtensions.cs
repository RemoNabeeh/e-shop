using Prism.Services.Dialogs;
using System;

namespace E_Shop.Core.Dialogs
{
    public static class IDialogServiceExtensions
    {
        public static void ShowMessageDialog(this IDialogService dialogService, string dialogName, string message, Action<IDialogResult> callBack)
        {
            var param = new DialogParameters();
            param.Add("message", message);

            dialogService.ShowDialog(dialogName, param, callBack);
        }
    }
}
