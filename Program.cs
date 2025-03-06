
using NUnit.Framework.Constraints;
using DirectoryManager;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

DirectoryData directories = null;
bool exiting = false;

while (!exiting)
{


    ReadLoadUserInput();
    if (exiting == true)
    {
        break;
    }
    if (directories != null)
    {

        PrintOptions();
    }



}

void ReadLoadUserInput()
{
    Console.WriteLine("Please provide a folder or JSON with folder info. (Type 'exit' to close the application) \n");
    string userInput = Console.ReadLine().Trim();
    JsonSerializer jsonSerializer = new JsonSerializer();


    try
    {
        if (userInput.ToLower() == "exit")
        {
            Console.WriteLine("Closing the application...");
            exiting = true;
            return;
        }


        else if (Directory.Exists(userInput))
        {
            directories = new DirectoryData(userInput);
        }


        else if (userInput.StartsWith("{") && userInput.EndsWith("}"))
        {

            directories = jsonSerializer.Deserialize(userInput);
        }



        else if (userInput.EndsWith(".json", StringComparison.OrdinalIgnoreCase) && File.Exists(userInput))
        {

            string jsonData = File.ReadAllText(userInput);
            directories = jsonSerializer.Deserialize(jsonData);

        }
        else
        {

            Console.WriteLine("Invalid input! Provide valid path, JSON string or JSON file");

        }
    }

    catch (Exception ex)
    {
        Console.WriteLine($"Data loading failed! Details: {ex.Message}");
        directories = null;
    }
}

void PrintOptions()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Choose an option (or type 'exit' to change path):");
        Console.WriteLine("1. Print Directory Structure");
        Console.WriteLine("2. Print all unique file extensions");
        Console.WriteLine("3. Serialize directory structure to JSON");
        Console.WriteLine("4. Deserialize and print JSON file");
        string userChoice = Console.ReadLine().Trim();
        Console.WriteLine();

        try
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            FileHandler fileHandler = new FileHandler(jsonSerializer);
            PrintDirectories printer = new PrintDirectories();

            switch (userChoice)
            {

                case "1":
                    printer.PrintDirectory(directories);

                    break;

                case "2":
                    printer.PrintUniqueExtensions(directories);

                    break;

                case "3":
                    string serializeData = jsonSerializer.Serialize(directories);
                    Console.WriteLine("Serialization successfull!");
                    Console.WriteLine("Save JSON to a file? YES/NO");
                    if (Console.ReadLine().Trim().ToLower() == "yes")
                    {
                        Console.WriteLine("Please provide the JSON file path");
                        string jsonFilePath = Console.ReadLine().Trim();
                        fileHandler.SaveToFile(jsonFilePath, directories);

                    }

                    break;

                case "4":
                    Console.WriteLine("Please provide the JSON file path");
                    string jsonLocation = Console.ReadLine().Trim();
                    var loadedJson = fileHandler.LoadFromFile(jsonLocation);
                    printer.PrintDirectory(loadedJson);
                    Console.WriteLine();
                    printer.PrintUniqueExtensions(loadedJson);

                    break;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Invalid option! Try again.");

                    break;
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O exception occurred. Details: {ex.Message}");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error occured. Details: {ex.Message}");

        }


    }
}

