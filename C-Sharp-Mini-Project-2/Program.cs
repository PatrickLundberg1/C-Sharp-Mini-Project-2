
// Level 3

using C_Sharp_Mini_Project_2;
using System.Numerics;

// Reads input from user to load phone for other actions
// curr_context: current asset database context used
// Returns null if the phone can not be found
static Phone? LoadPhone(AssetDbContext curr_context)
{
    int id = 0;
    string input = "";

    List<Phone> phone_list = curr_context.phones.ToList();

    if (phone_list.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No phones exist.");
        Console.ResetColor();
        return null;
    }

    Console.WriteLine("Current list of phones: ");
    Console.ForegroundColor = ConsoleColor.Blue;
    foreach (Phone phone in phone_list)
    {
        Console.WriteLine($"Id:{phone.Id} | Brand:{phone.Brand} | Model:{phone.Model} | Office:{phone.Office} | Date:{phone.Purchase_date} | Price(USD):{phone.Price}");
    }
    Console.ResetColor();


    while (true)
    {
        Console.WriteLine("Please input the Id of the phone: ");
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

    Phone? phone_edit = curr_context.phones.FirstOrDefault(p => p.Id == id);

    if (phone_edit == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No phones with that Id exists.");
        Console.ResetColor();
    }
    return phone_edit;
}

// Reads input from user to load computer for other actions
// curr_context: current asset database context used
// Returns null if the computer can not be found
static Computer? LoadComputer(AssetDbContext curr_context)
{
    int id = 0;
    string input = "";

    List<Computer> comp_list = curr_context.computers.ToList();

    if (comp_list.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No computers exist.");
        Console.ResetColor();
        return null;
    }

    Console.WriteLine("Current list of computers: ");
    Console.ForegroundColor = ConsoleColor.Blue;
    foreach (Computer comp in comp_list)
    {
        Console.WriteLine($"Id:{comp.Id} | Brand:{comp.Brand} | Model:{comp.Model} | Office:{comp.Office} | Date:{comp.Purchase_date} | Price(USD):{comp.Price}");
    }
    Console.ResetColor();

    while (true)
    {
        Console.WriteLine("Please input the Id of the computer: ");
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

    Computer? comp_edit = curr_context.computers.FirstOrDefault(c => c.Id == id);

    if (comp_edit == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No computers with that Id exists.");
        Console.ResetColor();
    }

    return comp_edit;
}

// Handles the logic when asking the user for input
// data_type: used to determine what data to ask user for, use the Input_Type enum for valid input
// Returns the result after proper user data validation as a string
static string GetUserData(Input_Type data_type)
{
    string input = "";
    if (data_type == Input_Type.Asset_Type)
    {
        while (true)
        {
            Console.Write("Please input P or C for (P)hone or (C)omputer: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "p" || input == "c")
            {
                return input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Command not understood, please try again.");
                Console.ResetColor();
            }
        }
    }
    else if(data_type == Input_Type.Purchase_Date)
    {
        while (true)
        {
            Console.Write("Please input the purchase date, for example in the format year-month-day: ");
            input = (Console.ReadLine() ?? "").Trim();

            if (DateTime.TryParse(input, out _))
            {
                return input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Date format error, please try again");
                Console.ResetColor();
            }
        }
    }
    else if(data_type == Input_Type.Price)
    {
        while (true)
        {
            Console.Write("Please input the price (USD): ");
            input = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(input, out _))
            {
                return input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input format error, please type in an integer for the price.");
                Console.ResetColor();
            }
        }
    }
    else if(data_type == Input_Type.Office)
    {
        while (true)
        {
            Console.Write("Please select the office for the asset. There are currently offices in Spain, Sweden and USA.\nPlease input SP, SW or US for (Sp)ain, (Sw)eden or (US)A: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "sp" || input == "sw" || input == "us")
            {
                switch (input)
                {
                    case "sp":
                        return "Spain";
                    case "sw":
                        return "Sweden";
                    default:
                        return "USA";
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Command not understood, please try again.");
                Console.ResetColor();
            }
        }
    }
    else if(data_type == Input_Type.Edit_Type)
    {
        while (true)
        {
            Console.WriteLine("Please input B, M, O, D or P to edit the (B)rand, (M)odel, (O)ffice," +
                                " (D)ate or (P)rice: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            string[] valid = { "b", "m", "o", "d", "p" };
            if (valid.Contains(input))
            {
                return input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Command not understood, please try again.");
                Console.ResetColor();
            }
        }
    }
    else if(data_type == Input_Type.Brand)
    {
        Console.Write("Please input the name of the brand: ");
        return (Console.ReadLine() ?? "").Trim();
    }
    else if(data_type == Input_Type.Model)
    {
        Console.Write("Please input the brand model: ");
        return (Console.ReadLine() ?? "").Trim();
    }

    throw new ArgumentException("Invalid data type used as argument");
}

// Start of main program
Console.WriteLine("Welcome to Asset Tracking!");
string input = "", type = "", brand = "", model = "", office = "", edit = "";
DateTime pd = DateTime.Now;
int price = 0, command;

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
        type = GetUserData(Input_Type.Asset_Type);

        brand = GetUserData(Input_Type.Brand);

        model = GetUserData(Input_Type.Model);

        pd = DateTime.Parse(GetUserData(Input_Type.Purchase_Date));

        price = int.Parse(GetUserData(Input_Type.Price));

        office = GetUserData(Input_Type.Office);

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
        type = GetUserData(Input_Type.Asset_Type);

        if (type == "p")
        {
            Phone? phone_edit = LoadPhone(Context);
            if(phone_edit == null) 
            {
                // no phone found
                continue;
            }

            // Check what field to edit
            // Read new value for field
            // Update table
            edit = GetUserData(Input_Type.Edit_Type);

            switch (edit)
            {
                case "b":
                    brand = GetUserData(Input_Type.Brand);

                    phone_edit.Brand = brand;
                    break;          
                case "m":
                    model = GetUserData(Input_Type.Model);

                    phone_edit.Model = model;
                    break; 
                case "o":
                    office = GetUserData(Input_Type.Office);

                    phone_edit.Office = office;
                    break; 
                case "d":
                    pd = DateTime.Parse(GetUserData(Input_Type.Purchase_Date));

                    phone_edit.Purchase_date = pd;
                    break;
                case "p":
                    price = int.Parse(GetUserData(Input_Type.Price));

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
            Computer? comp_edit = LoadComputer(Context);
            if(comp_edit == null)
            {
                // no computer found
                continue;
            }

            // Check what field to edit
            // Read new value for field
            // Update table
            edit = GetUserData(Input_Type.Edit_Type);

            switch (edit)
            {
                case "b":
                    brand = GetUserData(Input_Type.Brand);

                    comp_edit.Brand = brand;
                    break;
                case "m":
                    model = GetUserData(Input_Type.Model);

                    comp_edit.Model = model;
                    break;
                case "o":
                    office = GetUserData(Input_Type.Office);

                    comp_edit.Office = office;
                    break;
                case "d":
                    pd = DateTime.Parse(GetUserData(Input_Type.Purchase_Date));

                    comp_edit.Purchase_date = pd;
                    break;
                case "p":
                    price = int.Parse(GetUserData(Input_Type.Price));

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
        type = GetUserData(Input_Type.Asset_Type);

        if (type == "p")
        {
            // Read id from user
            // Delete field from table if id exists
            Phone? phone_edit = LoadPhone(Context);
            if (phone_edit == null)
            {
                // no phone found
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
            Computer? comp_edit = LoadComputer(Context);
            if(comp_edit == null)
            {
                // no computer found
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

// Enum used with the GetUserData function
enum Input_Type
{
    Asset_Type,
    Purchase_Date,
    Price,
    Office,
    Edit_Type,
    Brand,
    Model
}