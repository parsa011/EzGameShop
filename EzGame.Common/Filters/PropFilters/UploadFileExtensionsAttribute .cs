using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace EzGame.Common.Filters.PropFilters
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class UploadFileExtensionsAttribute : ValidationAttribute
    {
        private readonly IList<string> _allowedExtensions;
        public UploadFileExtensionsAttribute(string fileExtensions)
        {
            _allowedExtensions = fileExtensions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                return IsValidFile(file);
            }

            var files = value as IList<IFormFile>;
            if (files == null)
            {
                return false;
            }

            return files.All(IsValidFile);
        }

        private bool IsValidFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            var fileExtension = Path.GetExtension(file.FileName);
            return !string.IsNullOrWhiteSpace(fileExtension) &&
                   _allowedExtensions.Any(ext => fileExtension.Equals(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
}