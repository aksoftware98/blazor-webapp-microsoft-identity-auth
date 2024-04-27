using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppB2CAuth.Client;

namespace WebAppB2CAuth;

public class PersistingServerAuthenticationStateProvider : ServerAuthenticationStateProvider
{
	private readonly PersistentComponentState _state;
	private readonly PersistingComponentStateSubscription _subscription;
	private Task<AuthenticationState>? _authenticationState;

    public PersistingServerAuthenticationStateProvider(PersistentComponentState state)
    {
		_state = state;
		_subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
		AuthenticationStateChanged += OnAuthenticationStateChanged;
    }

	private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
	{
		_authenticationState = task;
	}

	private async Task OnPersistingAsync()
	{
		if (_authenticationState is null)
		{
			return;
		}

		var authState = await _authenticationState;
		var user = authState.User;
		if (user.Identity!.IsAuthenticated)
		{
			_state.PersistAsJson(nameof(ClaimsPrincipal), user.Claims);
		}
	}
}
