using Bogus;
using System;

namespace SeleniumAdvanced.Helpers
{
    public class PersonGenerator
    {
        public enum Gender
        {
            Male = 0,
            Female = 1
        }
        private readonly Faker personGenerator;

        public string Title { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Mail { get; }
        public string Password { get; }
        public string BirthDate { get; }

        public PersonGenerator()
        {
            personGenerator = new Faker();
            var r = new Random();
            var gender = (Gender)r.Next(Enum.GetNames(typeof(Gender)).Length);

            Title = gender == Gender.Male ? "Mr." : "Mrs.";
            FirstName = personGenerator.Name.FirstName(gender == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female);
            LastName = personGenerator.Name.LastName(gender == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female);

            Mail = GenerateMail();
            Password = personGenerator.Internet.Password(16, false, "", "!@#$%^&*()");
            BirthDate = personGenerator.Date.Past(80, DateTime.Today.AddYears(-18)).ToString("MM/dd/yyyy"); ;
        }
        private string GenerateMail()
        {
            return $"{FirstName.ToLower()}.{LastName.ToLower()}@somewhere.com";
        }

        private Faker GetPersonGenerator()
        {
            return personGenerator;
        }
    }
}