using System;
using System.Collections.Generic;

class Reservation
{
    private string _firstName { get; set; }
    private string _lastName { get; set; }
    private int _seatRow { get; set; }
    private int _seatNumber { get; set; }
    private string _eticket { get; set; }

    public Reservation(string firstName, int seatRow, int seatNumber, string eticket, string lastName = "")
    {
        _firstName = firstName;
        _lastName = lastName;
        _seatRow = seatRow;
        _seatNumber = seatNumber;
        if (eticket == "")
        {
            _eticket = GenerateEticket();    
        }
        else
        {
            _eticket = eticket;
        }
    }

    private string GenerateEticket()
    {
        string course = "INFOTC1040";
        string eticket = "";

        char[] nameArray = _firstName.ToCharArray();
        char[] courseArray = course.ToCharArray();

        if (courseArray.Length > nameArray.Length)
        {
            for (int i = 0; i < courseArray.Length; i++)
            {
                if (i < nameArray.Length)
                {
                    eticket += nameArray[i].ToString() + courseArray[i].ToString();
                }
                else
                {
                    eticket += courseArray[i].ToString();
                }
            }
        }
        else
        {
            for (int i = 0; i < nameArray.Length; i++)
            {
                if (i < courseArray.Length)
                {
                    eticket += nameArray[i].ToString() + courseArray[i].ToString();
                }
                else
                {
                    eticket += nameArray[i].ToString();
                }
            }
        }
        return eticket;
    }

    public string GetEticket()
    {
        return _eticket;
    }

    public string GetFirstName()
    {
        return _firstName;
    }

    public int GetSeatRow()
    {
        return _seatRow;
    }

    public int GetSeatNum()
    {
        return _seatNumber;
    }

    public string GetReservationInfo()
    {
        return $"{_firstName}, {_seatRow}, {_seatNumber}, {_eticket}";
    }

    private void DisplayReservationSuccess(Flight flight)
    {
        Console.WriteLine($"\nYour requested seat, Row: {_seatRow + 1} Seat: {_seatNumber + 1} has been assigned.");
        flight.DisplayFlightMap();
        Console.WriteLine($"Congratulations {_firstName} {_lastName}! Your trip is now booked! Enjoy!");
        Console.WriteLine($"Your e-ticket number is: {_eticket}\n");
    }

    public bool MakeReservation(Flight flight)
    {
        bool flightUpdateSuccess = flight.UpdateFlightReservations(this);
        if (flightUpdateSuccess)
        {
            DisplayReservationSuccess(flight);
            return true;
        }
        else
        {
            Console.WriteLine("\nERROR: Flight reservation update unsuccessful." +
            " Please try again later or contact an administrator if the problem persists.\n");
            return false;
        }
    }
}