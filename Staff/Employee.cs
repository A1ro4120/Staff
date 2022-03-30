using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff
{
    public struct Employee
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Свойство для доступа к идентификатору
        /// </summary>
        public int ID 
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Дата и время добавления записи
        /// </summary>
        private DateTime dateAndTimeAdded;
        /// <summary>
        /// Свойство для доступа к дате и времени добавления записи
        /// </summary>
        public DateTime DateAndTimeAdded 
        {
            get { return this.dateAndTimeAdded; }
            private set { this.dateAndTimeAdded = value; }
        }

        /// <summary>
        /// Свойство для доступа к Ф.И.О.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Свойство для доступа к возрасту
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Свойство для доступа к росту
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Свойство для доступа к дате рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Свойство для доступа к месту рождения
        /// </summary>
        public string PlaceOfBirth { get; set; }

        public Employee(int ID, DateTime DateAndTimeAdded, string FullName, int Age, int Height, DateTime DateOfBirth, string PlaceOfBirth)
        {
            this.id = ID;
            this.dateAndTimeAdded = DateAndTimeAdded;
            this.FullName = FullName;
            this.Age = Age;
            this.Height = Height;
            this.DateOfBirth = DateOfBirth;
            this.PlaceOfBirth = PlaceOfBirth;
        }

        /// <summary>
        /// Выводит данные сотрудника на экран
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"" +
                $"{id, 4}" +
                $"{dateAndTimeAdded.ToShortDateString(), 13}{dateAndTimeAdded.ToShortTimeString(), 6}" +
                $"{FullName, 30}" +
                $"{Age, 9}" +
                $"{Height, 9}" +
                $"{DateOfBirth.ToShortDateString(), 19}" +
                $" {PlaceOfBirth}");
        }
    }
}