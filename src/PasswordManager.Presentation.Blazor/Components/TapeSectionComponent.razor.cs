using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PasswordManager.Presentation.Blazor.Components
{
    public partial class TapeSectionComponent
    {
        [Parameter]
        public string SectionName { get; set; } = nameof(SectionName);

        [Parameter]
        public string FirstButtonIcon { get; set; } = Icons.Material.Filled.Add;

        [Parameter]
        public string SecondButtonIcon { get; set; } = Icons.Material.Filled.Edit;

        [Parameter]
        public string ThirdButtonIcon { get; set; } = Icons.Material.Filled.Delete;
    }
}
