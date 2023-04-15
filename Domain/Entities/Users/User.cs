namespace Domain.Entities.Users;

public class User
{
	public Guid Id { get; set; }
	public Email Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
}
