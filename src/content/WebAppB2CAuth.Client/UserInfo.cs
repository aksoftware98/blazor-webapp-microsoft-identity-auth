public class UserInfo
{

	public string Id { get; set; } = string.Empty;

	public string? DisplayName { get; set; }

	public string? GivenName { get; set; }

	public string? Surname { get; set; }

	public string? Email { get; set; }

	public List<Claim> Claims { get; set; } = new();

	// Add properties based on the claims or details you want the user to have in the WebAssembly pages and components.

}
