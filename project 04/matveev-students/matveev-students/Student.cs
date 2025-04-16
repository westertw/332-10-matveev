using System;
using System.Text.RegularExpressions;

namespace StudentManagement
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        private static readonly string[] ValidDomains = { "yandex.ru", "gmail.com", "icloud.com", "npu" };
        private static readonly DateTime MinDateOfBirth = new DateTime(1992, 1, 1);

        public bool IsValid(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(LastName))
            {
                errorMessage = "Фамилия обязательна для заполнения";
                return false;
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                errorMessage = "Имя обязательно для заполнения";
                return false;
            }

            if (string.IsNullOrWhiteSpace(MiddleName))
            {
                errorMessage = "Отчество обязательно для заполнения";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Group))
            {
                errorMessage = "Группа обязательна для заполнения";
                return false;
            }

            if (Course < 1 || Course > 6)
            {
                errorMessage = "Курс должен быть от 1 до 6";
                return false;
            }

            if (DateOfBirth < MinDateOfBirth || DateOfBirth > DateTime.Now)
            {
                errorMessage = "Некорректная дата рождения";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                errorMessage = "Некорректный формат email или неразрешенный домен";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var parts = email.Split('@');
            if (parts.Length != 2) return false;
            if (parts[0].Length < 3) return false;

            return ValidDomains.Contains(parts[1].ToLower());
        }



    }

} 