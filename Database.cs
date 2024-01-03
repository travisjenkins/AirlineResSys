using System;
using System.Collections.Generic;
using System.IO;

class Database
{
    private string _errorMsg = "\nERROR: Database unavailable." +
    " Please try again later or contact an administrator if the problem persists.\n";
    private List<User> _users { get; set; }
    private List<Reservation> _reservations { get; set; }
    private string _adminFilePath { get; set; }
    private string _reservationsFilePath { get; set; }

    public Database()
    {
        _users = LoadAdmins();
        _reservations = LoadReservations();
    }

    private void DisplayErrorMsg()
    {
        Console.WriteLine(_errorMsg);
    }

    private List<User> LoadAdmins()
    {
        try
        {
            string fileName = "passcodes.txt";
            string filePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}";
            _adminFilePath = filePath;
            string[] adminsText = File.ReadAllLines(filePath);
            List<User> admins = new List<User>();

            foreach (string admin in adminsText)
            {
                string[] adminData = admin.Split(",");
                User user = new User("Administrator", adminData[0].Trim(), adminData[1].Trim());
                admins.Add(user);
            }
            return admins;    
        }
        catch (System.Exception)
        {
            DisplayErrorMsg();
            Environment.Exit(0);
            return new List<User>();
        }
        
    }

    private List<Reservation> LoadReservations()
    {
        try
        {
            string fileName = "reservations.txt";
            string filePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}";
            _reservationsFilePath = filePath;
            string[] reservationsText = File.ReadAllLines(filePath);
            List<Reservation> reservations = new List<Reservation>();
            
            foreach (string reservation in reservationsText)
            {
                string[] reservationData = reservation.Split(",");
                Reservation res = new Reservation(reservationData[0],
                                                  int.Parse(reservationData[1]),
                                                  int.Parse(reservationData[2]),
                                                  reservationData[3]);
                reservations.Add(res);
            }
            return reservations;
        }
        catch (System.Exception)
        {
            DisplayErrorMsg();
            Environment.Exit(0);
            return new List<Reservation>();
        }
    }

    public List<User> GetAdmins()
    {
        return _users;
    }

    public List<Reservation> GetReservations()
    {
        return _reservations;
    }

    public bool PersistReservation(Reservation reservation)
    {
        try
        {
            using (StreamWriter file = new StreamWriter(_reservationsFilePath, true))
            {
                string reservationInfo = reservation.GetReservationInfo();
                file.WriteLine(reservationInfo);
            }
            _reservations.Add(reservation);
            return true;
        }
        catch (System.Exception)
        {
            DisplayErrorMsg();
            return false;
        }
    }
}