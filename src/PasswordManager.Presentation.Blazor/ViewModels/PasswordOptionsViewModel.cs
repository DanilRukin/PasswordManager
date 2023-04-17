using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Presentation.Blazor.ViewModels
{
    public class PasswordOptionsViewModel
    {
        [Required]
        public int Length { get; set; }
        public int CountOfNumbers { get; set; }
        public int CountOfCharacters { get; set; }
        private bool _upperCaseOnly = false;
        public bool UpperCaseOnly
        {
            get => _upperCaseOnly;
            set
            {
                if (value && _lowerCaseOnly)
                {
                    _lowerCaseOnly = false;
                }
                _upperCaseOnly = value;
            }
        }
        private bool _lowerCaseOnly = false;
        public bool LowerCaseOnly
        {
            get => _lowerCaseOnly;
            set
            {
                if (value && _upperCaseOnly)
                {
                    _upperCaseOnly = false;
                }
                _lowerCaseOnly = value;
            }
        }
        public string SpecialSymbols { get; set; } = "";
    }
}
