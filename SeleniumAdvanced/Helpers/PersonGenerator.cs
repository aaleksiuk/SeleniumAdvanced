using Bogus;
using System;

namespace SeleniumAdvanced.Helpers;

public class PersonGenerator
{
    public enum Gender
    {
        Male,
        Female
    }
    private readonly Faker personGenerator;

    public Gender Title { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName { get; }
    public string Mail { get; }
    public string Password { get; }
    public string BirthDate { get; }

    public PersonGenerator()
    {
        personGenerator = new Faker();

        Title = RandomHelper.GetRandomEnum<Gender>();
        FirstName = personGenerator.Name.FirstName(Title == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female);
        LastName = personGenerator.Name.LastName(Title == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female);
        FullName = FirstName + " " + LastName;

        Mail = GenerateMail();
        Password = personGenerator.Internet.Password(16, false, "", "!@#$%^&*()");
        BirthDate = personGenerator.Date.Past(80, DateTime.Today.AddYears(-18)).ToString("MM/dd/yyyy"); ;
    }
    private string GenerateMail()
    {
        return $"{FirstName.ToLower()}.{LastName.ToLower()}@somewhere.com";
    }
}