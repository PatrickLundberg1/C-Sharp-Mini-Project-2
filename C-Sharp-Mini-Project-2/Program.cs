
// Level 1

using C_Sharp_Mini_Project_2;
using System.Numerics;

// test list
/*
List<Asset> assets = new List<Asset>()
            {
                new Phone("iPhone", "8", "Spain", Convert.ToDateTime("2019-11-05"), 970),
                new Computer("HP", "Elitebook", "Spain", Convert.ToDateTime("2022-05-01"), 1423),
                new Phone("iPhone", "11", "Spain", Convert.ToDateTime("2022-04-25"), 990),
                new Phone("iPhone", "X", "Sweden", Convert.ToDateTime("2019-08-05"), 1245),
                new Phone("Motorola", "Razr", "Sweden", Convert.ToDateTime("2019-09-06"), 970),
                new Computer("HP", "Elitebook", "Sweden", Convert.ToDateTime("2019-10-07"), 588),
                new Computer("Asus", "W234", "USA", Convert.ToDateTime("2019-07-21"), 1200),
                new Computer("Lenovo", "Yoga 730", "USA", Convert.ToDateTime("2019-09-28"), 835),
                new Computer("Lenovo", "Yoga 530", "USA", Convert.ToDateTime("2019-11-21"), 1030)
            };
*/

Console.WriteLine("Welcome to Asset Tracking! Type in \"exit\" at any time to stop the program and display all Assets.");
string input = "", type = "", brand = "", model = "", office = "";
DateTime pd = DateTime.Now;
int price = 0;
bool on_exit = false;

AssetDbContext Context = new AssetDbContext();

while (true)
{
    while (!on_exit)
    {
        Console.Write("Please input P or C for (P)hone or (C)omputer: ");
        input = (Console.ReadLine() ?? "").Trim().ToLower();

        if (input == "exit")
        {
            on_exit = true;
        }
        else if (input == "p" || input == "c")
        {
            type = input;
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command not understood, please try again.");
            Console.ResetColor();
        }
    }

    while (!on_exit)
    {
        Console.Write("Please input the name of the brand: ");
        input = (Console.ReadLine() ?? "").Trim();

        if (input.ToLower() == "exit")
        {
            on_exit = true;
        }
        else
        {
            brand = input;
            break;
        }
    }

    while (!on_exit)
    {
        Console.Write("Please input the brand model: ");
        input = (Console.ReadLine() ?? "").Trim();

        if (input.ToLower() == "exit")
        {
            on_exit = true;
        }
        else
        {
            model = input;
            break;
        }
    }

    while (!on_exit)
    {
        Console.Write("Please input the purchase date, for example in the format year-month-day: ");
        input = (Console.ReadLine() ?? "").Trim();

        if (input.ToLower() == "exit")
        {
            on_exit = true;
        }
        else if (DateTime.TryParse(input, out pd))
        {
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Date format error, please try again");
            Console.ResetColor();
        }
    }

    while (!on_exit)
    {
        Console.Write("Please input the price (USD): ");
        input = (Console.ReadLine() ?? "").Trim();

        if (input.ToLower() == "exit")
        {
            on_exit = true;
        }
        else if (int.TryParse(input, out price))
        {
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Input format error, please type in an integer for the price.");
            Console.ResetColor();
        }
    }

    while (!on_exit)
    {
        Console.Write("Please select the office for the asset. There are currently offices in Spain, Sweden and USA.\nPlease input SP, SW or US for (Sp)ain, (Sw)eden or (US)A: ");
        input = (Console.ReadLine() ?? "").Trim().ToLower();

        if (input == "exit")
        {
            on_exit = true;
        }
        else if (input == "sp" || input == "sw" || input == "us")
        {
            switch (input)
            {
                case "sp":
                    office = "Spain";
                    break;
                case "sw":
                    office = "Sweden";
                    break;
                default:
                    office = "USA";
                    break;
            }
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command not understood, please try again.");
            Console.ResetColor();
        }
    }

    if (on_exit)
    {
        break;
    }
    else
    {
        if (type == "p")
        {
            //assets.Add(new Phone(brand, model, office, pd, price));
            Phone new_phone = new Phone(brand, model, office, pd, price);
            Context.phones.Add(new_phone);
            Context.SaveChanges();
        }
        else if (type == "c")
        {
            //assets.Add(new Computer(brand, model, office, pd, price));
            Computer new_comp = new Computer(brand, model, office, pd, price);
            Context.computers.Add(new_comp);
            Context.SaveChanges();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Asset successfully added!");
        Console.ResetColor();
    }
}

// display asset list
const int space = 15;
Console.WriteLine("Type".PadRight(space) + "Brand".PadRight(space) + "Model".PadRight(space) + "Office".PadRight(space) + "Purchase Date".PadRight(space) + "Price in USD".PadRight(space) + "Currency".PadRight(space) + "Local price today");
Console.WriteLine("----".PadRight(space) + "-----".PadRight(space) + "-----".PadRight(space) + "------".PadRight(space) + "-------------".PadRight(space) + "------------".PadRight(space) + "--------".PadRight(space) + "-----------------");

List<Phone> phones = Context.phones.ToList();
List<Computer> computers = Context.computers.ToList();

List<Asset> assets = phones.AsEnumerable<Asset>().Concat(computers.AsEnumerable<Asset>()).ToList();

List<Asset> ordered_list = assets.OrderBy(a => a.Office).ThenBy(a => a.Purchase_date).ToList();
const int average_year = 365, average_month = 30;

foreach (Asset asset in ordered_list)
{
    TimeSpan age = DateTime.Now - asset.Purchase_date;

    if (age.TotalDays > (average_year * 3 - average_month * 3))
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else if (age.TotalDays > (average_year * 3 - average_month * 6))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    string currency = asset.GetCurrency();
    Console.WriteLine(asset.GetType().Name.PadRight(space) + asset.Brand.PadRight(space) + asset.Model.PadRight(space) + asset.Office.PadRight(space) + asset.Purchase_date.ToString("MM\\/dd\\/yyyy").PadRight(space) + asset.Price.ToString().PadRight(space) + currency.PadRight(space) + asset.LocalPrice(currency).ToString("0.00"));
    Console.ResetColor();
}