# C# Console app to simulate reserving a seat on an airline.

**Assumptions:**

1. 50 seats available, 10 rows of 5 across
2. Cost matrix is: $500, $200, $500, $200, $500
3. E-Ticket number is an interlacing of the course number and the passenger's first name (Ex: TIrNaFvOiTsC1040)
4. Reservations are read from and written to a text file named reservations in the same directory as the app.
5. Admin logins are stored and verified against a text file named passcodes in the same directory as the app.

While the above assumptions are hard-coded into the app, I wrote it to allow them to be changed easily.
