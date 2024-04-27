using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class PersistentAuthenticationStateProvider : AuthenticationStateProvider
{
	private static readonly Task<AuthenticationState> _defaultUnauthenticatedTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

	private readonly Task<AuthenticationState> _authenticationStateTask = _defaultUnauthenticatedTask;

	public PersistentAuthenticationStateProvider(PersistentComponentState state)
	{
		if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo == null)
		{
			return;
		}

		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
			new Claim(ClaimTypes.Name, userInfo.DisplayName ?? string.Empty),
			new Claim(ClaimTypes.Email, userInfo.Email ?? string.Empty),
			new Claim(ClaimTypes.GivenName, userInfo.GivenName ?? string.Empty),
			new Claim(ClaimTypes.Surname, userInfo.Surname ?? string.Empty),
			// Add more claims as needed
		};

		// You can control the claims that are added to the identity here:
		var identity = new ClaimsIdentity(claims, "PersistentAuthenticationStateProvider");
		_authenticationStateTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
	}

	public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authenticationStateTask;
}