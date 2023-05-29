using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz17
{
    public class Employee
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }

        public Employee(string fullName, string position, string contactPhone, string email, decimal salary)
        {
            FullName = fullName;
            Position = position;
            ContactPhone = contactPhone;
            Email = email;
            Salary = salary;
        }

        public string GetInfo()
        {
            return $"ПІБ: {FullName}\nПосада: {Position}\nКонтактний телефон: {ContactPhone}\nEmail: {Email}\nЗаробітна плата: {Salary}";
        }
    }

    public class Firm
    {
        public string Name { get; set; }
        public DateTime FoundedDate { get; set; }
        public string BusinessProfile { get; set; }
        public string DirectorFullName { get; set; }
        public int EmployeeCount { get; set; }
        public string Address { get; set; }
        public List<Employee> Employees { get; set; }

        public Firm(string name, DateTime foundedDate, string businessProfile, string directorFullName, int employeeCount, string address)
        {
            Name = name;
            FoundedDate = foundedDate;
            BusinessProfile = businessProfile;
            DirectorFullName = directorFullName;
            EmployeeCount = employeeCount;
            Address = address;
            Employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public string GetInfo()
        {
            string info = $"Назва фірми: {Name}\nДата заснування: {FoundedDate.ToShortDateString()}\nПрофіль бізнесу: {BusinessProfile}\n" +
                $"Директор: {DirectorFullName}\nКількість працівників: {EmployeeCount}\nАдреса: {Address}\n";

            if (Employees.Count > 0)
            {
                info += "Список працівників:\n";
                foreach (var employee in Employees)
                {
                    info += employee.GetInfo() + "\n";
                }
            }

            return info;
        }
    }

    public static class FirmExtensions
    {
        public static IEnumerable<Firm> GetFirmsWithNameContains(this IEnumerable<Firm> firms, string keyword)
        {
            return firms.Where(firm => firm.Name.Contains(keyword));
        }

        public static IEnumerable<Firm> GetFirmsWithBusinessProfile(this IEnumerable<Firm> firms, string businessProfile)
        {
            return firms.Where(firm => firm.BusinessProfile.Equals(businessProfile, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Firm> GetFirmsWithBusinessProfile(this IEnumerable<Firm> firms, params string[] businessProfiles)
        {
            return firms.Where(firm => businessProfiles.Contains(firm.BusinessProfile, StringComparer.OrdinalIgnoreCase));
        }

        public static IEnumerable<Firm> GetFirmsWithEmployeeCountGreaterThan(this IEnumerable<Firm> firms, int count)
        {
            return firms.Where(firm => firm.EmployeeCount > count);
        }

        public static IEnumerable<Firm> GetFirmsWithEmployeeCountInRange(this IEnumerable<Firm> firms, int minCount, int maxCount)
        {
            return firms.Where(firm => firm.EmployeeCount >= minCount && firm.EmployeeCount <= maxCount);
        }

        public static IEnumerable<Firm> GetFirmsWithAddress(this IEnumerable<Firm> firms, string address)
        {
            return firms.Where(firm => firm.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Firm> GetFirmsWithDirectorLastName(this IEnumerable<Firm> firms, string lastName)
        {
            return firms.Where(firm => firm.DirectorFullName.Split(' ')[1].Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Firm> GetFirmsFoundedBefore(this IEnumerable<Firm> firms, DateTime date)
        {
            return firms.Where(firm => firm.FoundedDate < date);
        }

        public static IEnumerable<Firm> GetFirmsFoundedOn(this IEnumerable<Firm> firms, DateTime date)
        {
            return firms.Where(firm => firm.FoundedDate.Date == date.Date);
        }

        public static IEnumerable<Firm> GetFirmsWithDirectorLastNameAndNameContains(this IEnumerable<Firm> firms, string lastName, string keyword)
        {
            return firms.Where(firm => firm.DirectorFullName.Split(' ')[1].Equals(lastName, StringComparison.OrdinalIgnoreCase)
                && firm.Name.Contains(keyword));
        }
    }
    internal class cs1
    {
        public static void task_1()
        {
            var firms = new List<Firm>
            {
                new Firm("ABC Food", new DateTime(2010, 5, 15), "Food", "John White", 150, "London"),
                new Firm("XYZ Marketing", new DateTime(2012, 9, 3), "Marketing", "Jane Black", 80, "New York"),
                new Firm("IT Solutions", new DateTime(2015, 2, 10), "IT", "Michael Green", 250, "San Francisco"),
                new Firm("Food Delight", new DateTime(2018, 7, 21), "Food", "Emily White", 120, "London"),
                new Firm("Tech Innovators", new DateTime(2019, 12, 5), "IT", "David Brown", 180, "Los Angeles"),
                new Firm("White-Mech Industries", new DateTime(2023, 1, 26), "IT", "David Black", 270, "New Orlean")
            };

            // Додавання співробітників до фірм
            firms[0].AddEmployee(new Employee("John Smith", "Manager", "123456789", "john.smith@example.com", 5000));
            firms[0].AddEmployee(new Employee("Sarah Johnson", "Sales Representative", "987654321", "sarah.johnson@example.com", 4000));
            firms[1].AddEmployee(new Employee("Mark Davis", "Marketing Manager", "456123789", "mark.davis@example.com", 6000));
            firms[2].AddEmployee(new Employee("Anna Wilson", "Software Developer", "789456123", "anna.wilson@example.com", 7000));
            firms[2].AddEmployee(new Employee("Kevin Lee", "Data Analyst", "321654987", "kevin.lee@example.com", 5500));
            firms[3].AddEmployee(new Employee("Jessica Taylor", "Chef", "159753468", "jessica.taylor@example.com", 4500));
            firms[4].AddEmployee(new Employee("Andrew Miller", "IT Consultant", "654987321", "andrew.miller@example.com", 6500));

            // Отримати інформацію про всі фірми
            Console.WriteLine("Інформація про всі фірми:");
            foreach (var firm in firms)
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, які мають у назві слово «Food»
            Console.WriteLine("Фірми, які мають у назві слово «Food»:");
            foreach (var firm in firms.GetFirmsWithNameContains("Food"))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, які працюють у галузі маркетингу
            Console.WriteLine("Фірми, які працюють у галузі маркетингу:");
            foreach (var firm in firms.GetFirmsWithBusinessProfile("Marketing"))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, які працюють у галузі маркетингу або IT
            Console.WriteLine("Фірми, які працюють у галузі маркетингу або IT:");
            foreach (var firm in firms.GetFirmsWithBusinessProfile("Marketing", "IT"))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми з кількістю працівників більшою, ніж 100
            Console.WriteLine("Фірми з кількістю працівників більшою, ніж 100:");
            foreach (var firm in firms.GetFirmsWithEmployeeCountGreaterThan(100))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми з кількістю працівників у діапазоні від 100 до 300
            Console.WriteLine("Фірми з кількістю працівників у діапазоні від 100 до 300:");
            foreach (var firm in firms.GetFirmsWithEmployeeCountInRange(100, 300))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, які знаходяться в Лондоні
            Console.WriteLine("Фірми, які знаходяться в Лондоні:");
            foreach (var firm in firms.GetFirmsWithAddress("London"))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, в яких прізвище директора "White"
            Console.WriteLine("Фірми, в яких прізвище директора 'White':");
            foreach (var firm in firms.GetFirmsWithDirectorLastName("White"))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, які засновані більше двох років тому
            Console.WriteLine("Фірми, які засновані більше двох років тому:");
            foreach (var firm in firms.GetFirmsFoundedBefore(DateTime.Now.AddYears(-2)))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми, в яких прізвище директора "Black" і мають у назві фірми слово "White"
            Console.WriteLine("Фірми, в яких прізвище директора 'Black' і мають у назві фірми слово 'White':");
            foreach (var firm in firms.GetFirmsWithDirectorLastNameAndNameContains("Black", "White"))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }

            // Отримати фірми з дня заснування яких минуло 123 дні
            Console.WriteLine("Фірми, з дня заснування яких минуло 123 дні:");
            foreach (var firm in firms.GetFirmsFoundedOn(DateTime.Now.AddDays(-123)))
            {
                Console.WriteLine(firm.GetInfo());
                Console.WriteLine("------------------------------");
            }
        }
    }
}
