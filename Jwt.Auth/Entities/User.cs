namespace Jwt.Auth.Entities;

// User should include a hashed password, but this is just a demo.

public sealed class User
{
    public User(Guid id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public Guid Id { get; private init; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }
}
