using E_Shop.Core.Interfaces.Services;
using System.Windows;

namespace E_Shop.Business
{
    public class StringsResourceService : IStringsResourceService
    {
        public string GetMessage(string key)
        {
            return (string)Application.Current.FindResource(key);
        }
    }
}
