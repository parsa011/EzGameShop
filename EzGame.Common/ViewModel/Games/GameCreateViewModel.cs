using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EzGame.Common.ViewModel.Games
{
   public class GameCreateViewModel
    {
        public string Id { get; set; }
        [Display(Name ="تصویر")]
        public string ImageName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [Display(Name ="نام")]
        [MaxLength(100 ,ErrorMessage = "نام نمی تواند بیشتر از 100 حروف باشد")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [Display(Name = "توضیح کوتاه")]
        [MaxLength(500, ErrorMessage = "نام نمی تواند بیشتر از 500 حروف باشد")]
        public string Explanation { get; set; }
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [Display(Name = "توضیح کوتاه")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
        public int Count { get; set; }
        public bool ComingSoon { get; set; }
        public string Image { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }

    }
}
