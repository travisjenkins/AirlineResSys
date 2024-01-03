using System;
using System.Collections.Generic;
using System.Linq;

namespace AirlineResSys
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n  Airline Reservation System");
            Console.WriteLine("----------------------------\n");
            // Create loop to force data reload and allow
            // user to access multiple menus without exiting
            while (true)
            {
                Flight flight = new Flight("Economy");
                Option option = new Option();
                option.DisplayOptions();
                int selectedOption = option.GetUserOption();
                
                if (selectedOption == 1)
                {
                    Console.WriteLine("\n      Admin Login      ");
                    Console.WriteLine("-------------------------\n");
                    bool loggedIn = new Login().LoginAdmin();
                    if (loggedIn)
                    {
                        flight.DisplayAdminFlightInfo();
                    }
                }
                else if (selectedOption == 2)
                {
                    if (flight.GetSeatsRemaining() != 0)
                    {
                        // Get passenger first & last name
                        Console.WriteLine("\nEnter Passenger First Name: ");
                        string firstName = Console.ReadLine().Trim();
                        Console.WriteLine("Enter Passenger Last Name: ");
                        string lastName = Console.ReadLine().Trim();

                        flight.DisplayFlightMap();
                        while (true)
                        {
                            // Get passenger seat request
                            Console.WriteLine("Which seat row do you want? ");
                            string rowRequest = Console.ReadLine().Trim();
                            int row = 0;
                            if (int.TryParse(rowRequest, out row))
                            {
                                row -= 1;
                                Console.WriteLine("Which seat column do you want? ");
                                string seatNumRequest = Console.ReadLine().Trim();
                                int seatNum = 0;
                                if (int.TryParse(seatNumRequest, out seatNum))
                                {
                                    seatNum -= 1;
                                    bool available = flight.CheckSeatAvailability(row, seatNum);
                                    if (available)
                                    {
                                        Reservation reservation = new Reservation(firstName, row, seatNum, "", lastName);
                                        bool success = reservation.MakeReservation(flight);
                                        if (success)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }   
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\nRow: {row + 1} Seat: {seatNum + 1}," +
                                        " is already assigned. Choose again.\n");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry, flight is sold out.\n");
                    }
                }
                else if (selectedOption == 3)
                {
                    Console.WriteLine("\nThank you for choosing our Airway! Goodbye :)\n");
                    break;
                }
            }
        }
    }
}
