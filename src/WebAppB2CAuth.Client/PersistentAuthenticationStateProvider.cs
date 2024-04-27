using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class PersistentAuthenticationStateProvider : AuthenticationStateProvider
{
	private static readonly Task<AuthenticationState> _defaultUnauthenticatedTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

	private readonly Task<AuthenticationState> _authenticationStateTask = _defaultUnauthenticatedTask;

	public PersistentAuthenticationStateProvider(PersistentComponentState state)
	{
		if (!state.TryTakeFromJson<IEnumerable<Claim>>(nameof(ClaimsPrincipal), out var claims) || claims is null)
		{
			return;
		}

		// You can control the claims that are added to the identity here:
		var identity = new ClaimsIdentity(claims, "PersistentAuthenticationStateProvider");

		_authenticationStateTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
	}

	public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authenticationStateTask;
}