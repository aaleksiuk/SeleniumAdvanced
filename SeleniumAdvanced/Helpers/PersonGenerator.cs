using RandomNameGeneratorLibrary;
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
        private readonly PersonNameGenerator personGenerator;

        public string Title { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Mail { get; }

        public PersonGenerator()
        {
            personGenerator = new PersonNameGenerator();
            var r = new Random();
            var gender = (Gender)r.Next(Enum.GetNames(typeof(Gender)).Length);

            if (gender == Gender.Male)
            {
                Title = "Mr.";
                FirstName = personGenerator.GenerateRandomMaleFirstName();
            }
            else
            {
                Title = "Mrs.";
                FirstName = personGenerator.GenerateRandomFemaleFirstName();
            }
            LastName = personGenerator.GenerateRandomLastName();
            Mail = GenerateMail();
        }
        private string GenerateMail()
        {
            return $"{FirstName.ToLower()}.{LastName.ToLower()}@somewhere.com";
        }
    }
}