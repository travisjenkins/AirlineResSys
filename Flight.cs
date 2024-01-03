using System;
using System.Collections.Generic;
using System.Linq;

class Flight
{
    private string _flightClass { get; set; }
    private int _totalSeats { get; set; }
    private double _totalSales { get; set; }
    private List<Reservation> _reservations { get; set; }
    private List<List<string>> _flightMap { get; set; }

    public Flight(string flightClass)
    {
        Database db = new Database();
        _reservations = db.GetReservations();
        _flightClass = flightClass;
        _flightMap = GetFlightMap(flightClass);
    }

    private List<List<string>> GetFlightMap(string flightClass)
    {
        List<List<string>> seatList = new List<List<string>>();
        if (flightClass.ToLower() == "economy")
        {
            _totalSeats = 50;
            foreach (var row in Enumerable.Range(0, 10))
            {
                List<string> seatRow = new List<string>();
                foreach (var col in Enumerable.Range(0, 5))
                {
                    string colStr = col.ToString();
                    colStr = "O";
                    seatRow.Add(colStr);
                }
                seatList.Add(seatRow);
            }
            foreach (var reservation in _reservations)
            {
                int row = reservation.GetSeatRow();
                int seat = reservation.GetSeatNum();
                for (int i = 0; i < seatList[0].Count; i++)
                {
                    seatList[row][seat] = "X";
                }
            }   
        }
        return seatList;
    }

    public int GetSeatsRemaining()
    {
        return _totalSeats - _reservations.Count;
    }

    public void DisplayFlightMap()
    {
        Console.WriteLine("");
        Console.WriteLine("Printing the Flight Map...");
        foreach (var row in _flightMap)
        {
            Console.WriteLine("[" + string.Join(",", row) + "]");
        }
        Console.WriteLine("");
    }

    private List<List<int>> GetEconomyCostMatrix()
    {
        List<List<int>> costMatrix = new List<List<int>>();
        foreach (var row in Enumerable.Range(0, 10))
        {
            List<int> seatRow = new List<int> { 500, 200, 500, 200, 500 };
            costMatrix.Add(seatRow);
        }
        return costMatrix;
    }

    private double CalculateTotalSales()
    {
        double total = 0.0;
        List<List<int>> costMatrix = GetEconomyCostMatrix();

        foreach (var row in Enumerable.Range(0, _flightMap.Count))
        {
            foreach (var seat in Enumerable.Range(0, _flightMap[0].Count))
            {
                if (_flightMap[row][seat] == "X")
                {
                    total += costMatrix[row][seat];
                }
            }
        }
        _totalSales = total;
        return total;
    }

    public void DisplayAdminFlightInfo()
    {
        DisplayFlightMap();
        CalculateTotalSales();
        Console.WriteLine($"{_totalSales:C}");
        Console.WriteLine("You are logged out now!\n");
    }

    public bool CheckSeatAvailability(int seatRow, int seatNum)
    {
        foreach (var reservation in _reservations)
        {
            if (seatRow == reservation.GetSeatRow())
            {
                if (seatNum == reservation.GetSeatNum())
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool UpdateFlightReservations(Reservation reservation)
    {
        try
        {
            Database db = new Database();
            db.PersistReservation(reservation);
            _reservations.Add(reservation);
            _flightMap = GetFlightMap("Economy");
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}