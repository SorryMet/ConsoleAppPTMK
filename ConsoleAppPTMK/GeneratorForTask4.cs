using System;

namespace ConsoleAppPTMK
{

    public class GeneratorForTask4
    {
        public static string GenerateFullName(Random random)
        {
            string[] firstNames = { "Danil", "Yuri", "Arina", "Igor", "Kirill", "Vlad", "Anna", "Milana" };
            string[] lastNames = { "Ivanov", "Petrov", "Sidorov", "Antonov", "Denisov", "Blok","Fedorov" };
            string[] surNames = { "Ivanovich", "Petrovich", "Sidorovich", "Semenovich", "Denisovich", "Blokovich" };
         
            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];
            string surName = surNames[random.Next(surNames.Length)];

            return $"{firstName} {lastName} {surName}";
        }

        public static string GenerateGender(Random random)
        {
            string[] gender = { "Male", "Female" };
            
            return gender[random.Next(gender.Length)];
        }
        public static DateTime GenerateDateWithoutTime(Random random)
        {
            int year = random.Next(1950, 2023);
            int month = random.Next(1, 13);

            int maxDay = DateTime.DaysInMonth(year, month);
            int day = random.Next(1, maxDay + 1);

            DateTime date = new DateTime(year, month, day);

            return date;
        }
    }
}
