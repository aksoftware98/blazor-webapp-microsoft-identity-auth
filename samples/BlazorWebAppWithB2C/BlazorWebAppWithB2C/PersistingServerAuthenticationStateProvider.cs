using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


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
			if (!ValidateIdClaim(user, out var userId))
				return;
			
			var userInfo = BuildUserInfo(user, userId!);
			_state.PersistAsJson(nameof(UserInfo), userInfo);
		}
	}

	private static UserInfo BuildUserInfo(ClaimsPrincipal user, string userId)
	{
		var name = user.FindFirstValue("name"); // Based on AD B2C, the email claim is not always ClaimTypes.Name
		var email = user.FindFirstValue("emails"); // Based on AD B2C, the email claim is not always ClaimTypes.Email
		var givenName = user.FindFirstValue(ClaimTypes.GivenName);
		var surname = user.FindFirstValue(ClaimTypes.Surname);
		// Extract more claims as needed to populate the user object
		var userInfo = new UserInfo
		{
			Id = userId!,
			DisplayName = name,
			Email = email,
			GivenName = givenName,
			Surname = surname
		};
		return userInfo;
	}

	private static bool ValidateIdClaim(ClaimsPrincipal user, out string? userId)
	{
		userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(userId)) // Abort the operation if the user ID is not found
			return false;

		return true;
	}

}
