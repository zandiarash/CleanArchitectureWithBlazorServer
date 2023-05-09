using Blazor.Server.UI.Services.Notifications;

namespace Blazor.Server.UI.Components.Shared;

public partial class ChatMenu : MudComponentBase
{

    private bool _newMessageAvailable = false;
    private IDictionary<NotificationMessage, bool>? _messages = null;
 
    private async Task MarkMessageAsRead()
    {
        _newMessageAvailable = false;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == true)
        {
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}