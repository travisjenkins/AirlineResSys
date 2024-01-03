using System;
using System.Linq;
class Option
{
    private int[] _options { get; set; }
    private string[] _optionsText { get; set; }

    public Option()
    {
        _options = new int[] { 1, 2, 3 };
        _optionsText = new string[] { "Admin Log-In", "Reserve a seat", "Exit" };
    }

    public void DisplayOptions()
    {
        foreach (var option in _options)
        {
            Console.WriteLine($"{_options[option - 1]}. {_optionsText[option - 1]}");
        }
    }

    private string GetAvailableOptions()
    {
        string result = "";
        foreach (var option in _options)
        {
            if (option != _options.Max())
            {
                result += $" {option} or";
            }
            else
            {
                result += $" {option}";
            }
        }
        return result;
    }

    public int GetUserOption()
    {
        int loginOption = 0;
        bool userContinue = true;
        while (userContinue)
        {
            try
            {
                Console.WriteLine("\nChoose an option: ");
                string userChoice = Console.ReadLine().Trim();
                if (int.TryParse(userChoice, out loginOption))
                {
                    if(loginOption < _options.Min() || loginOption > _options.Max())
                    {
                        Console.WriteLine($"\nERROR: Invalid Option! Select{GetAvailableOptions()}");
                    }
                    else
                    {
                        userContinue = false;
                    }
                }
                else 
                {
                    Console.WriteLine($"\nERROR: Invalid Option! Select{GetAvailableOptions()}");
                }         
            }
            catch (System.Exception)
            {
                Console.WriteLine($"\nERROR: Invalid Option! Select{GetAvailableOptions()}");
            }
        }
        return loginOption;
    }
}