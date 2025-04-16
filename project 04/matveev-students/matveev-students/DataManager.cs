using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;

namespace StudentManagement
{
    public class DataManager
    {
        private string _jsonFilePath;
        private List<Student> _students;
        private bool _hasUnsavedChanges;

        public bool HasUnsavedChanges => _hasUnsavedChanges;
        public List<Student> Students => _students;

        public DataManager(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
            _students = new List<Student>();
            _hasUnsavedChanges = false;
            LoadData();
        }

        public void LoadData()
        {
            if (File.Exists(_jsonFilePath))
            {
                string jsonString = File.ReadAllText(_jsonFilePath);
                _students = JsonSerializer.Deserialize<List<Student>>(jsonString) ?? new List<Student>();
            }
        }

        public void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_students, options);
            File.WriteAllText(_jsonFilePath, jsonString);
            _hasUnsavedChanges = false;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
            _hasUnsavedChanges = true;
        }

        public void UpdateStudent(Student oldStudent, Student newStudent)
        {
            int index = _students.IndexOf(oldStudent);
            if (index != -1)
            {
                _students[index] = newStudent;
                _hasUnsavedChanges = true;
            }
        }

        public void DeleteStudent(Student student)
        {
            if (_students.Remove(student))
            {
                _hasUnsavedChanges = true;
            }
        }

        public void ImportFromCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // Skip header
            {
                var values = line.Split(',');
                if (values.Length >= 7)
                {
                    var student = new Student
                    {
                        LastName = values[0],
                        FirstName = values[1],
                        MiddleName = values[2],
                        Course = int.Parse(values[3]),
                        Group = values[4],
                        DateOfBirth = DateTime.Parse(values[5]),
                        Email = values[6]
                    };
                    
                    string errorMessage;
                    if (student.IsValid(out errorMessage))
                    {
                        _students.Add(student);
                    }
                }
            }
            _hasUnsavedChanges = true;
        }

        public void ExportToCsv(string filePath)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Фамилия,Имя,Отчество,Курс,Группа,Дата рождения,Email");
            
            foreach (var student in _students)
            {
                csv.AppendLine($"{student.LastName},{student.FirstName},{student.MiddleName}," +
                             $"{student.Course},{student.Group},{student.DateOfBirth:dd.MM.yyyy},{student.Email}");
            }
            
            File.WriteAllText(filePath, csv.ToString());
        }

        public List<Student> FilterStudents(string lastName = "", int? course = null, string group = "")
        {
            var query = _students.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(s => s.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
            }

            if (course.HasValue)
            {
                query = query.Where(s => s.Course == course.Value);
            }

            if (!string.IsNullOrWhiteSpace(group))
            {
                query = query.Where(s => s.Group.Contains(group, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }
    }
} 