using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Binix.Api.Model
{
    public class ProtocolAttribute : ValidationAttribute
    {
        private static readonly string[] _allowedProtocols = {"https", "http", "ws", "wss", "ftp"};
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string val
                || _allowedProtocols.Any(s => s.Equals(val, StringComparison.InvariantCultureIgnoreCase))) 
                return new(ErrorMessage ?? "Invalid protocol specified.");
            
            return ValidationResult.Success;
        }

        public override bool IsValid(object? value) => IsValid(value, null) == ValidationResult.Success;
    }
}