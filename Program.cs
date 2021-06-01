using System;
using System.Collections;
using System.Collections.Generic;

namespace Restaurant
{
    class Program
    { 
        static void Main(string[] args)
        { 
            Console.OutputEncoding = System.Text.Encoding.Default;          //підключаємо українську мову
            new Interface().Start();                                        //створюємо об'єкт класса та запускаємо метод запуску інтерфейсу
        }
    }
    //Структура для запису замовлень
    public struct Order
    {
        public string Name { get; set; }
        public string Sizq { get; set; }
        public float Price { get; set; }
        //конструктор класу з параметрами
        public Order(string name, string sizq, float price)
        {
            Name = name;
            Sizq = sizq;
            Price = price;
        }
        //перевизначений стандартний метод порівняння
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Name == order.Name &&
                   Sizq == order.Sizq &&
                   Price == order.Price;
        }
        //перевизначений стандартний метод отримання хешу
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Sizq, Price);
        }

        //перевизначений стандартний метод перетворення об'єкта в рядок
        public override string ToString()
        {
            return $" Назва: {Name}, розмір: {Sizq}, Ціна: {Price}";
        }
    }
    //класс інгредієнти, потрібний для створення страви
    public class Ingredients
    {
        public string Name { get; set; }
        public float Price { get; set; }
        //конструктор класу без параметрів
        public Ingredients() { }
        //конструктор класу з параметрами
        public Ingredients(string name, float price)
        {
            Name = name;
            Price = price;
        }
        //перевизначений стандартний метод порівняння
        public override bool Equals(object obj)
        {
            return obj is Ingredients ingredients &&
                   Name == ingredients.Name &&
                   Price == ingredients.Price;
        }

        //перевизначений стандартний метод отримання хешу
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price);
        }

        //перевизначений стандартний метод перетворення об'єкта в рядок
        public override string ToString()
        {
            return $"Назва: {Name}\n Ціна: {Price}";
        }
    }
    //класс страви, страви які будуть занесені до прайс листа
    public class Dishes
    {
        public string Name { get; set; }
        public List<Ingredients> Ingredient { get; set; }
        public float BigPrice { get; set; }
        public float AveragePrice { get; set; }
        public float SmallPrice { get; set; }
        //конструктор класу без параметрів
        public Dishes() { }
        //конструктор класу з параметрами
        public Dishes(string name, List<Ingredients> ingredients)
        {
            Name = name;
            Ingredient = ingredients;
            Count_the_price();
        }
        //метод який розраховує ціну на всі види порцій
        public void Count_the_price()
        {
            float price = 0;
            foreach (var item in Ingredient)
            {
                price += item.Price;
            }
            BigPrice = (float)(price * 1.2);
            AveragePrice = price;
            SmallPrice = (float)(price * 0.8);
        }
        //перевизначений стандартний метод порівняння
        public override bool Equals(object obj)
        {
            return obj is Dishes dishes &&
                   Name == dishes.Name &&
                   EqualityComparer<List<Ingredients>>.Default.Equals(Ingredient, dishes.Ingredient) &&
                   BigPrice == dishes.BigPrice &&
                   AveragePrice == dishes.AveragePrice &&
                   SmallPrice == dishes.SmallPrice;
        }
        //перевизначений стандартнмй метод отримання хешу
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Ingredient, BigPrice, AveragePrice, SmallPrice);
        }
        //перевизначений стандартний метод перетворення об'єкта в рядок
        public override string ToString()
        {
            return $"Назва страви: {Name}";
        }
    }
    //класс для меню
    public class Price_List
    {
        public List<Dishes> List { get; set; }
        //конструктор класу з параметрами
        public Price_List(List<Dishes> list)
        {
            List = list;
        }
        //конструктор класу без параметрів
        public Price_List()
        {
        }
        //перевизначений стандартний метод порівняння
        public override bool Equals(object obj)
        {
            return obj is Price_List list &&
                   EqualityComparer<List<Dishes>>.Default.Equals(List, list.List);
        }
        //стандартний метод отримання хешкоду
        public override int GetHashCode()
        {
            return HashCode.Combine(List);
        }
        //перевизначений стандартний метод перетворення об'єкта в рядок 
        public override string ToString()
        {
            string str = "";
            foreach (var item in List)
            {
                str += $"{item.Name} \nВелика порція :{item.BigPrice} \n Звичайна порція:{item.AveragePrice}\n Маленька порція:{item.SmallPrice}\n";
            }
            return str;
        }
    }
    //класс в якому реалізована взаємодія з користувачем
    public class Interface
    {
        public Price_List list { get; set; }
        public List<Order> Order { get; set; }
        //конструктор класу без параметрів
        public Interface()
        {
            Default();
        }
        //метод який записує стартові значення для нашого меню
        public void Default()
        {
            Order = new List<Order>();
            list = new Price_List(new List<Dishes>()
             {
                new Dishes("Борщ",new List<Ingredients>()
                {
                    new Ingredients("Буряк", (float)19.5),
                    new Ingredients("Часник", (float)7.5),
                    new Ingredients("Капуста", (float)10.5),
                    new Ingredients("Сметана", (float)5.5)
                }),
                new Dishes("Окрошка",new List<Ingredients>()
                {
                    new Ingredients("Картопля", (float)15.5),
                    new Ingredients("Кефір", (float)45.5),
                    new Ingredients("Огірок", (float)30.5),
                    new Ingredients("Зелень", (float)15.5)
                }),
                new Dishes("Млинці",new List<Ingredients>()
                {
                    new Ingredients("Мука", (float)10.5),
                    new Ingredients("Молоко", (float)20.5),
                    new Ingredients("Яйця", (float)30.5),
                    new Ingredients("Олія", (float)16.5)
                }),
                new Dishes("Котлета по-київськи",new List<Ingredients>()
                {
                    new Ingredients("Філе куряче", (float)25.5),
                    new Ingredients("Масло", (float)20.5),
                    new Ingredients("Олія", (float)16.5),
                    new Ingredients("Петрушка", (float)5.5)
                }),
                new Dishes("Суші",new List<Ingredients>()
                {
                    new Ingredients("Лосось", (float)50.5),
                    new Ingredients("Рис", (float)15.5),
                    new Ingredients("Норі", (float)25.5),
                    new Ingredients("Соєвий соус", (float)30.5)
                }),
                new Dishes("Гамбургер",new List<Ingredients>()
                {
                    new Ingredients("Булочка с кунжутом", (float)25.5),
                    new Ingredients("М'ясна котлета", (float)15.5),
                    new Ingredients("Сир", (float)10.0),
                    new Ingredients("Огірок, помідор", (float)5.0)
                })
             });
        }
        //метод запускає стартове меню
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("***********************Вітаємо Вас у нашому ресторані***********************************");
            Console.WriteLine(" Оберіть дію:");
            Console.WriteLine(" 1. Зробити замовлення\n" +
                " 2. Переглянути своє замовлення або видалити з нього пункт\n" +
                " 3. Оплатити замовлення\n" +
                " 4. Залишити ресторан");
            switch (Console.ReadLine())
            {
                case "1":
                    Make_an_order();
                    break;

                case "2":
                    Write_and_delete_order();
                    break;

                case "3":
                    Payment();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Операції під таким номером не існує. Будь ласка, спробуйте ввести інший номер");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }
        //метод оплати рахунку
        public void Payment()
        {
            if (Order.Count == 0)
            {
                Console.Clear();
                Console.WriteLine(" Ви ще нічого не замовили");
                Console.ReadLine();
                Start();
            }
            Console.Clear();
            Console.WriteLine("Ваше замовлення: ");
            int count = 1;
            float price = 0;
            foreach (var item in Order)
            {
                Console.WriteLine($"{count++}. {item}");
                price += item.Price;
            }
            while (true)
            {
                Console.WriteLine($" До сплати: {price}$\n" +
                    $" Бажаєте оплатити замовлення:\n" +
                    $" 1. Так\n" +
                    $" 2. Ні");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(" Ваше замовлення сплачено\n" +
                            " Бажаємо гарного дня, навідуйтесь до нас частіше =)");
                        Order.Clear();
                        Console.ReadLine();
                        Start();
                        break;

                    case "2":
                        Start();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine(" Ви обрали не коректну команду");
                        Console.ReadLine();
                        break;
                }
            }

        }
        //метод виведення замовлення та можливості видалити щось з нього
        public void Write_and_delete_order()
        {
            Console.Clear();
            if (Order.Count == 0)
            {
                Console.Clear();
                Console.WriteLine(" Ви ще нічого не замовили");
                Console.ReadLine();
                Start();
            }

            while (true)
            {
                int count = 1;
                float price = 0;
                Console.Clear();
                foreach (var item in Order)
                {
                    Console.WriteLine($"{count++}. {item}");
                    price += item.Price;
                }
                Console.WriteLine($"\n Загальна сума замовлення: {price}$");
                Console.WriteLine($"\n Бажаєте щось прибрати з замовлення?\n" +
                    $" 1. Так\n" +
                    $" 2. Ні");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine(" Введіть номер пункту який бажаєте видалити: ");
                        if (int.TryParse(Console.ReadLine(), out int number))
                        {
                            if (number > 0 && number <= count)
                            {
                                Order.Remove(Order[number - 1]);
                                Console.Clear();
                                Console.WriteLine(" Видалення пройшло успішно");
                                Console.ReadLine();
                                Start();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(" Ви ввели не припустиме значення");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(" Ви ввели не коректну відповідь");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        Start();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine(" Ви обрали не коректну команду");
                        Console.ReadLine();
                        break;
                }
            }
        }
        //метод через який мі формуємо замовлення
        public void Make_an_order()
        {
            Console.Clear();
            Console.WriteLine(" До вашої уваги наше меню: ");
            int count = 1;
            foreach (var item in list.List)
            {
                Console.WriteLine($"{count++}. {item.Name}\n");
            }
            Console.WriteLine(" Аби обрати страву введіть її номер, або якщо хочете повернутиись до попереднього меню введіть \"0\"");
            string res = Console.ReadLine();
            if (int.TryParse(res, out int number))
            {
                if (number == 0)
                {
                    Start();
                }
                if (number <= count && number > 0)
                {
                    bool check = true;
                    while (check)
                    {
                        Console.Clear();
                        Console.WriteLine("Оберіть порцію: \n" +
                            $" 1. Велика порція вартістю: {list.List[number - 1].BigPrice}\n" +
                            $" 2. Середня порція вартістю: {list.List[number - 1].AveragePrice}\n" +
                            $" 3. Маленька порція вартістю: {list.List[number - 1].SmallPrice}\n" +
                            " 4. Повернутись до попереднього меню");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                Order.Add(new Order(list.List[number - 1].Name, "Велика", list.List[number - 1].BigPrice));
                                check = false;
                                break;

                            case "2":
                                Order.Add(new Order(list.List[number - 1].Name, "Середня", list.List[number - 1].AveragePrice));
                                check = false;
                                break;

                            case "3":
                                Order.Add(new Order(list.List[number - 1].Name, "Маленька", list.List[number - 1].SmallPrice));
                                check = false;
                                break;

                            case "4":
                                Start();
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine(" Ви не коректно обрали порцію");
                                Console.ReadLine();
                                break;
                        }
                    }
                    Make_an_order();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" Ви не коректно обрали страву");
                    Console.ReadLine();
                    Make_an_order();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(" Ви не коректно обрали страву");
                Console.ReadLine();
                Make_an_order();
            }
        }
        //перевизначений стандартний метод порівняння
        public override bool Equals(object obj)
        {
            return obj is Interface @interface &&
                   EqualityComparer<Price_List>.Default.Equals(list, @interface.list);
        }
        //перевизначений стандартний метод отримання хешкоду
        public override int GetHashCode()
        {
            return HashCode.Combine(list);
        }
    }
}
