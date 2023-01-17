using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Presentation.Blazor.ViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Это обязательное поле.")]
        [MaxLength(512, ErrorMessage = "Максимальная длина не более 512 символов.")]
        public string ResourceName { get; set; }

        [Required(ErrorMessage = "Это обязательное поле.")]
        [MaxLength(512, ErrorMessage = "Максимальная длина не более 512 символов.")]
        public string ResourcePassword { get; set; }

        public string UserName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastAccessDate { get; set; }
        public string Description { get; set; }
        public string ResourceUrl { get; set; }

        public RecordViewModel(string resourceName, string resourcePassword)
        {
            ResourceName = resourceName;
            ResourcePassword = resourcePassword;
            Description = "Description";
            ResourceUrl = "ResourceUrl";
            UserName = "UserName";
            DateTime now = DateTime.UtcNow;
            CreationDate = now;
            LastAccessDate = now;
            LastModifiedDate = now;
        }
    }
}
