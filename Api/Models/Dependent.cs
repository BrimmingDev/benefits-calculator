namespace Api.Models;

public class Dependent
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Relationship Relationship { get; private set; }

    public Dependent(string firstName, string lastName, DateTime dateOfBirth, Relationship relationship)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Relationship = relationship;
    }
}
