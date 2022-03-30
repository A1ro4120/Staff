using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Staff
{
    public struct Repository
    {
        /// <summary>
        /// База данных сотрудников
        /// </summary>
        public Employee[] emps;

        private string path;
        
        int index;

        public Repository(string Path)
        {
            this.path = Path;
            this.index = 0;
            this.emps = new Employee[0];
        }

        /// <summary>
        /// Увеличивает размер репозитория
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.emps, this.emps.Length + 1);
            }
        }

        /// <summary>
        /// Печать заголовков
        /// </summary>
        private void TitlePrint()
        {
            Console.WriteLine($"{"ID",4}{"Дата записи",19}{"Ф.И.О",30}{"Возраст",9}{"Рост",9}{"Дата рождения",19} Место рождения");
        }

        /// <summary>
        /// Запись данных сотрудника в строковый массив
        /// </summary>
        /// <returns></returns>
        private string[] EmployeeRecord()
        {
            string[] str = new string[5];

            Console.Write("]Введите данные сотрудника" + "\n]Ф.И.О.:" + "\n>> ");
            string userFullName = Console.ReadLine();
            str[0] = userFullName;

            Console.Write("]Возраст:" + "\n>> ");
            string userAge = Console.ReadLine();
            str[1] = userAge;

            Console.Write("]Рост:" + "\n>> ");
            string userHeight = Console.ReadLine();
            str[2] = userHeight;

            Console.Write("]Дата рождения:" + "\n>> ");
            string userDateOfBirth = Console.ReadLine();
            str[3] = userDateOfBirth;

            Console.Write("]Место рождения:" + "\n>> ");
            string userPlaceOfBirth = Console.ReadLine();
            str[4] = userPlaceOfBirth;

            return str;
        }

        /// <summary>
        /// Добавляет сотрудника в репозиторий
        /// </summary>
        /// <param name="ConcreteEmployee"></param>
        private void AddToRepository(Employee ConcreteEmployee)
        {
            this.Resize(index >= this.emps.Length);
            this.emps[index] = ConcreteEmployee;
            this.index++;
        }

        /// <summary>
        /// Добавляет сотрудника в файл
        /// </summary>
        /// <param name="NewEmployee"></param>
        /// <param name="Path"></param>
        private void AddToFile(Employee NewEmployee)
        {
            using (StreamWriter sw = new StreamWriter(this.path, true))
            {
                string str = $"{NewEmployee.ID}#" +
                             $"{NewEmployee.DateAndTimeAdded.ToShortDateString()} {NewEmployee.DateAndTimeAdded.ToShortTimeString()}#" +
                             $"{NewEmployee.FullName}#" +
                             $"{NewEmployee.Age}#" +
                             $"{NewEmployee.Height}#" +
                             $"{NewEmployee.DateOfBirth.ToShortDateString()}#" +
                             $"{NewEmployee.PlaceOfBirth}";

                sw.WriteLine(str);
            }
        }

        /// <summary>
        /// Перезаписть файла
        /// </summary>
        private void OverwritingAFile()
        {
            using (StreamWriter sw = new StreamWriter(this.path, false))
            {
                for(int i = 0; i < this.emps.Length; i++)
                {
                    if(this.emps[i].ID != 0)
                    {
                        string str = $"{this.emps[i].ID}#" +
                                 $"{this.emps[i].DateAndTimeAdded.ToShortDateString()} {this.emps[i].DateAndTimeAdded.ToShortTimeString()}#" +
                                 $"{this.emps[i].FullName}#" +
                                 $"{this.emps[i].Age}#" +
                                 $"{this.emps[i].Height}#" +
                                 $"{this.emps[i].DateOfBirth.ToShortDateString()}#" +
                                 $"{this.emps[i].PlaceOfBirth}";

                        sw.WriteLine(str);
                    }
                }
            }
        }

        /// <summary>
        /// Загрузка данных с файла
        /// </summary>
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    AddToRepository(new Employee(
                        Convert.ToInt32(args[0]),
                        Convert.ToDateTime(args[1]), 
                        args[2], 
                        Convert.ToInt32(args[3]), 
                        Convert.ToInt32(args[4]),
                        Convert.ToDateTime(args[5]),
                        args[6]));
                }
            }
        }

        /// <summary>
        /// Просмотр записи
        /// </summary>
        /// <param name="Path"></param>
        private void ViewingAnEntry()
        {
            Console.Write("]Введите ID записи, которую хотите просмотреть" +
                        "\n>> ");

            bool flag = false;

            int userID = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < this.emps.Length; i++)
            {
                if (this.emps[i].ID == userID)
                {
                    TitlePrint();
                    this.emps[i].Print();
                    flag = true;
                    break;
                }
            }

            if (flag == false) Console.WriteLine("[_..] Нет пользователя с таким ID");
        }

        /// <summary>
        /// Создание записи
        /// </summary>
        private void CreateAnEntry()
        {
            int userID = 0;
            for (int i = 0; i < this.emps.Length; i++)
            {
                if (userID < this.emps[i].ID)
                {
                    userID = this.emps[i].ID;
                }
            }
            userID++;

            DateTime entryDate = new DateTime();
            entryDate = DateTime.Now;

            string[] str = new string[4];

            str = EmployeeRecord();

            this.AddToFile(new Employee(userID, entryDate, str[0], Convert.ToInt32(str[1]), Convert.ToInt32(str[2]), Convert.ToDateTime(str[3]), str[4]));

            Console.WriteLine($"[_..] Сотрудник {userID}:");

            TitlePrint();
            this.emps[userID].Print();

            Console.WriteLine("      Добавлен в базу данных");
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        private void DeletingAnEntry()
        {
            Console.Write("]Введите ID записи, которую хотите удалить" +
                        "\n>> ");

            int deleteID = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < this.emps.Length; i++)
            {
                if (deleteID == this.emps[i].ID)
                {
                    this.emps[i].ID = 0;
                }
            }

            OverwritingAFile();

            Console.WriteLine("[_..] Сотрудник удален из базы данных");
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        private void EditingAnEntry()
        {
            Console.Write("]Введите ID записи, которую вы хотите редактировать" +
                        "\n>> ");

            int redactID = Convert.ToInt32(Console.ReadLine());

            bool flag = false;
            for (int i = 0; i < this.emps.Length; i++)
            {
                if (redactID == this.emps[i].ID)
                {
                    int userID = this.emps[i].ID;
                    
                    DateTime entryDate = new DateTime();
                    entryDate = DateTime.Now;

                    string[] str = new string[4];

                    str = EmployeeRecord();

                    this.emps[i] = new Employee(userID, entryDate, str[0], Convert.ToInt32(str[1]), Convert.ToInt32(str[2]), Convert.ToDateTime(str[3]), str[4]);

                    flag = true;
                }
            }

            OverwritingAFile();

            if (flag == true) 
                Console.WriteLine("[_..] Запись отредактирована");
            else 
                Console.WriteLine("[_..] Сотрудник с данным ID не найден");
        }

        /// <summary>
        /// Загрузка записей в выбранном диапазоне дат
        /// </summary>
        private void DownloadEntrysInTheSelectedDateRange()
        {
            Console.Write("]Введите диапозон дат через \"-\" (без пробелов)" +
                "\n>> ");

            string[] dateRange = Console.ReadLine().Split('-');

            DateTime minDate = new DateTime();
            minDate = Convert.ToDateTime(dateRange[0]);

            DateTime maxDate = new DateTime();
            maxDate = Convert.ToDateTime(dateRange[1]);

            TitlePrint();

            for (int i = 0; i < this.emps.Length; i++)
            {
                if (minDate <= this.emps[i].DateAndTimeAdded && this.emps[i].DateAndTimeAdded <= maxDate)
                {
                    this.emps[i].Print();
                }
            }
        }

        /// <summary>
        /// Сортировка по возрастанию и убыванию даты
        /// </summary>
        private void SortByDateAscendingAndDescending()
        {
            Console.Write("]Отсортировка по убыванию даты введите: +" +
                        "\n]Отсортировка по возрастанию даты введите: -" +
                        "\n>> ");

            DateTime[] dateArray = new DateTime[this.emps.Length];
            
            for (int i = 0; i < this.emps.Length; i++)
            {
                dateArray[i] = this.emps[i].DateAndTimeAdded;
            }

            Array.Sort(dateArray, this.emps);

            string userSelection = Console.ReadLine();

            TitlePrint();

            switch (userSelection)
            {
                case "+":
                    for (int i = 0; i < this.emps.Length; i++)
                    {
                        this.emps[i].Print();
                    }

                    break;
                case "-":
                    Array.Reverse(this.emps);

                    for (int i = 0; i < this.emps.Length; i++)
                    {
                        this.emps[i].Print();
                    }

                    break;
                default: Console.WriteLine("]Неизвестный символ"); break;
            }
        }

        /// <summary>
        /// Функционал программы
        /// </summary>
        public void Functional()
        {
            Console.Write("               [STAFF]" +
                            "\n] Напишите номер функции, которой хотите воспользоваться:" +
                            "\n] 1 - Просмотр записи" +
                            "\n] 2 - Создание записи" +
                            "\n] 3 - Удаление записи" +
                            "\n] 4 - Редактирование записи" +
                            "\n] 5 - Загрузка записей в выбранном диапазоне дат" +
                            "\n] 6 - Сортировка по возрастанию и убыванию даты" +
                            "\n>> ");

            Load();

            switch (Console.ReadLine())
            {
                case "1": ViewingAnEntry(); break;
                case "2": CreateAnEntry(); break;
                case "3": DeletingAnEntry(); break;
                case "4": EditingAnEntry(); break;
                case "5": DownloadEntrysInTheSelectedDateRange(); break;
                case "6": SortByDateAscendingAndDescending(); break;
                default: Console.WriteLine("[_..] Напишите номер функции"); break;
            }

            Console.WriteLine("\n[_..] Нажмите ENTER чтобы выйти из программы");

            Console.ReadKey();
        }
    }
}