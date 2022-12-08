
// Level 3

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
string input = "", type = "", brand = "", model = "", office = "", edit = "";
DateTime pd = DateTime.Now;
int price = 0, command, id;

AssetDbContext Context = new AssetDbContext();

while (true)
{
    // read command from user
    while(true)
    {
        Console.WriteLine("Choose a command:");
        Console.WriteLine("(1) Add a new Asset");
        Console.WriteLine("(2) Edit an Asset");
        Console.WriteLine("(3) Delete an Asset");
        Console.WriteLine("(4) Exit and display Assets");
        input = (Console.ReadLine() ?? "").Trim();

        if(int.TryParse(input, out command) && command > 0 && command < 5)
        {
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command not understood, please input one of the numbers 1-4.");
            Console.ResetColor();
        }
    }

    // execute command
    if (command == 1)
    {
        while (true)
        {
            Console.Write("Please input P or C for (P)hone or (C)omputer: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "p" || input == "c")
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

        Console.Write("Please input the name of the brand: ");
        brand = (Console.ReadLine() ?? "").Trim();

        Console.Write("Please input the brand model: ");
        model = (Console.ReadLine() ?? "").Trim();

        while (true)
        {
            Console.Write("Please input the purchase date, for example in the format year-month-day: ");
            input = (Console.ReadLine() ?? "").Trim();

            if (DateTime.TryParse(input, out pd))
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

        while (true)
        {
            Console.Write("Please input the price (USD): ");
            input = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(input, out price))
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

        while (true)
        {
            Console.Write("Please select the office for the asset. There are currently offices in Spain, Sweden and USA.\nPlease input SP, SW or US for (Sp)ain, (Sw)eden or (US)A: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "sp" || input == "sw" || input == "us")
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

        if (type == "p")
        {
            Phone new_phone = new Phone(brand, model, office, pd, price);
            Context.phones.Add(new_phone);
            Context.SaveChanges();
        }
        else if (type == "c")
        {
            Computer new_comp = new Computer(brand, model, office, pd, price);
            Context.computers.Add(new_comp);
            Context.SaveChanges();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Asset successfully added!");
        Console.ResetColor();
    }
    else if(command == 2)
    {
        while (true)
        {
            Console.Write("Please input P or C for (P)hone or (C)omputer: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "p" || input == "c")
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

        if(input == "p")
        {
            List<Phone> phone_list = Context.phones.ToList();

            if(phone_list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No phones exist.");
                Console.ResetColor();
                continue;
            }
            
            while(true)
            {
                Console.WriteLine("Please input the Id of the phone to edit: ");
                input = (Console.ReadLine() ?? "").Trim();

                if(int.TryParse(input, out id)){
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input format error, please type in an integer for the id.");
                    Console.ResetColor();
                }
            }
                
            Phone? phone_edit = Context.phones.FirstOrDefault(p => p.Id == id);

            if (phone_edit == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No phones with that Id exists.");
                Console.ResetColor();
                continue;
            }

            // Check what field to edit
            // Read new value for field
            // Update table
            while (true)
            {
                Console.WriteLine("Please input B, M, O, D or P to edit the (B)rand, (M)odel, (O)ffice," +
                                    " (D)ate or (P)rice: ");
                input = (Console.ReadLine() ?? "").Trim().ToLower();

                string[] valid = { "b", "m", "o", "d", "p" };
                if (valid.Contains(input))
                {
                    edit = input;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not understood, please try again.");
                    Console.ResetColor();
                }
            }

            switch (edit)
            {
                case "b":

                    Console.Write("Please input the name of the brand: ");
                    brand = (Console.ReadLine() ?? "").Trim();

                    phone_edit.Brand = brand;
                    break;          
                case "m":

                    Console.Write("Please input the brand model: ");
                    model = (Console.ReadLine() ?? "").Trim();

                    phone_edit.Model = model;
                    break; 
                case "o":

                    while (true)
                    {
                        Console.Write("Please select the office for the asset. There are currently offices in Spain, Sweden and USA.\nPlease input SP, SW or US for (Sp)ain, (Sw)eden or (US)A: ");
                        input = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (input == "sp" || input == "sw" || input == "us")
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

                    phone_edit.Office = office;
                    break; 
                case "d":

                    while (true)
                    {
                        Console.Write("Please input the purchase date, for example in the format year-month-day: ");
                        input = (Console.ReadLine() ?? "").Trim();

                        if (DateTime.TryParse(input, out pd))
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

                    phone_edit.Purchase_date = pd;
                    break;
                case "p":

                    while (true)
                    {
                        Console.Write("Please input the price (USD): ");
                        input = (Console.ReadLine() ?? "").Trim();

                        if (int.TryParse(input, out price))
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

                    phone_edit.Price = price;
                    break;
                default:
                    break;
            }

            Context.phones.Update(phone_edit);
            Context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Phone data successfully edited.");
            Console.ResetColor();
        }
        else
        {
            // Same as above but for computer table
            List<Computer> comp_list = Context.computers.ToList();

            if (comp_list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No computers exist.");
                Console.ResetColor();
                continue;
            }

            while (true)
            {
                Console.WriteLine("Please input the Id of the computer to edit: ");
                input = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(input, out id))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input format error, please type in an integer for the id.");
                    Console.ResetColor();
                }
            }

            Computer? comp_edit = Context.computers.FirstOrDefault(c => c.Id == id);

            if (comp_edit == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No computers with that Id exists.");
                Console.ResetColor();
                continue;
            }

            // Check what field to edit
            // Read new value for field
            // Update table
            while (true)
            {
                Console.WriteLine("Please input B, M, O, D or P to edit the (B)rand, (M)odel, (O)ffice," +
                                    " (D)ate or (P)rice: ");
                input = (Console.ReadLine() ?? "").Trim().ToLower();

                string[] valid = { "b", "m", "o", "d", "p" };
                if (valid.Contains(input))
                {
                    edit = input;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not understood, please try again.");
                    Console.ResetColor();
                }
            }

            switch (edit)
            {
                case "b":

                    Console.Write("Please input the name of the brand: ");
                    brand = (Console.ReadLine() ?? "").Trim();

                    comp_edit.Brand = brand;
                    break;
                case "m":

                    Console.Write("Please input the brand model: ");
                    model = (Console.ReadLine() ?? "").Trim();

                    comp_edit.Model = model;
                    break;
                case "o":

                    while (true)
                    {
                        Console.Write("Please select the office for the asset. There are currently offices in Spain, Sweden and USA.\nPlease input SP, SW or US for (Sp)ain, (Sw)eden or (US)A: ");
                        input = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (input == "sp" || input == "sw" || input == "us")
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

                    comp_edit.Office = office;
                    break;
                case "d":

                    while (true)
                    {
                        Console.Write("Please input the purchase date, for example in the format year-month-day: ");
                        input = (Console.ReadLine() ?? "").Trim();

                        if (DateTime.TryParse(input, out pd))
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

                    comp_edit.Purchase_date = pd;
                    break;
                case "p":

                    while (true)
                    {
                        Console.Write("Please input the price (USD): ");
                        input = (Console.ReadLine() ?? "").Trim();

                        if (int.TryParse(input, out price))
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

                    comp_edit.Price = price;
                    break;
                default:
                    break;
            }

            Context.computers.Update(comp_edit);
            Context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Computer data successfully edited.");
            Console.ResetColor();
        }
    }
    else if(command == 3)
    {
        while (true)
        {
            Console.Write("Please input P or C for (P)hone or (C)omputer: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "p" || input == "c")
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

        if (input == "p")
        {
            // Read id from user
            // Delete field from table if id exists
            List<Phone> phone_list = Context.phones.ToList();

            if (phone_list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No phones exist.");
                Console.ResetColor();
                continue;
            }

            while (true)
            {
                Console.WriteLine("Please input the Id of the phone to remove: ");
                input = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(input, out id))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input format error, please type in an integer for the id.");
                    Console.ResetColor();
                }
            }

            Phone? phone_edit = Context.phones.FirstOrDefault(p => p.Id == id);

            if (phone_edit == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No phones with that Id exists.");
                Console.ResetColor();
                continue;
            }

            Context.phones.Remove(phone_edit);
            Context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Phone data successfully removed.");
            Console.ResetColor();
        }
        else
        {
            // Same as above but for computers
            List<Computer> comp_list = Context.computers.ToList();

            if (comp_list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No computers exist.");
                Console.ResetColor();
                continue;
            }

            while (true)
            {
                Console.WriteLine("Please input the Id of the computer to remove: ");
                input = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(input, out id))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input format error, please type in an integer for the id.");
                    Console.ResetColor();
                }
            }

            Computer? comp_edit = Context.computers.FirstOrDefault(c => c.Id == id);

            if (comp_edit == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No computers with that Id exists.");
                Console.ResetColor();
                continue;
            }

            Context.computers.Remove(comp_edit);
            Context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Computer data successfully removed.");
            Console.ResetColor();
        }
    }
    else if(command == 4)
    {
        break;
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