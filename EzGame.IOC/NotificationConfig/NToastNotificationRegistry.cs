using Microsoft.Extensions.DependencyInjection;
using NToastNotify;

namespace EzGame.IOC.NotificationConfig
{
    public static class NToastNotificationRegistry
    {
        public static IMvcBuilder AddToastWithOptions(this IMvcBuilder builder)
        {
            builder.AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PreventDuplicates = true,
                NewestOnTop = true
            }, new NToastNotifyOption
            {
                DefaultSuccessTitle = "موفقیت",
                DefaultAlertTitle = "هشدار",
                DefaultWarningTitle = "اخطار",
                DefaultErrorTitle = "خطا"

            });
            return builder;
        }

        public static IMvcBuilder AddNotifyWithOptions(this IMvcBuilder builder)
        {
            builder.AddNToastNotifyNoty(new NotyOptions()
            {
                ProgressBar = true
            });
            return builder;
        }
    }
}