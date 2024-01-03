using System;
using System.Collections.Generic;

class Login
{
    private List<User> _credentials { get; set; }

    public Login()
    {
        Database db = new Database();
        _credentials = db.GetAdmins();
    }

    public bool LoginAdmin()
    {
        int attempts = 0;
        bool result = false;
        while (attempts < 3)
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();
            foreach (var admin in _credentials)
            {
                if (admin.GetRole() == "Administrator")
                {
                    result = admin.CheckCredentials(username, password);
                    if (result)
                    {
                        return result;
                    }
                }
            }
            Console.WriteLine("\nInvalid username/password combination\n");
            attempts += 1;
        }
        Console.WriteLine("Sorry you have exceeded the allowed logon attempts. Returning to option menu.\n");
        return result;
    }
}