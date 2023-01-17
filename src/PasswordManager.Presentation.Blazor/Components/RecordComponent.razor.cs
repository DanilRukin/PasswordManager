using Microsoft.AspNetCore.Components;
using PasswordManager.Presentation.Blazor.ViewModels;

namespace PasswordManager.Presentation.Blazor.Components
{
    public partial class RecordComponent
    {
        [Parameter]
        public RecordViewModel Model { get; set; }
    }
}
