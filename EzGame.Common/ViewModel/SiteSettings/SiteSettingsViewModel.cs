using System.ComponentModel.DataAnnotations;

namespace EzGame.Common.ViewModel.SiteSettings
{
    public class SiteSettingsViewModel
    {
        [Required]
        public decimal? DollarPrice { get; set; }
    }
}
