using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MonitorSystem.Extention
{
    public static class ConfirmExtensions
    {
        public static ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message)
        {
            return jsRuntime.InvokeAsync<bool>("confirm", message);
        }
        public static ValueTask Alert(this IJSRuntime jsRuntime, string message)
        {
             return jsRuntime.InvokeVoidAsync("alert", message);
        }
    }
}
