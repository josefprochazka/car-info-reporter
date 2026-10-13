# Assignment

The original assignment (in Czech) is in [backend-developer-practical-assignment.pdf](backend-developer-practical-assignment.pdf).
Below is its English translation.

## Practical assignment for applicants - programmers

1. Design a data structure in the XML format and fill it manually with the data from the table below.

   Table of sold cars:

   | Model name (string) | Sale date (datetime) | Price (double) | VAT (double) |
   |---------------------|----------------------|---------------:|-------------:|
   | Škoda Oktávia       | 2.12.2010            |      500.000,- |           20 |
   | Škoda Felicia       | 3.12.2000            |      210.000,- |           20 |
   | Škoda Fabia         | 4.12.2010            |      350.000,- |           20 |
   | Škoda Oktávia       | 4.12.2010            |      500.000,- |           20 |
   | Škoda Oktávia       | 5.12.2010            |      500.000,- |           20 |
   | Škoda Fabia         | 5.12.2010            |      350.000,- |           20 |
   | Škoda Fabia         | 6.12.2010            |      350.000,- |           20 |
   | Škoda Forman        | 4.12.2000            |      100.000,- |           19 |
   | Škoda Favorit       | 5.12.2000            |       80.000,- |           19 |
   | Škoda Forman        | 6.12.2000            |      100.000,- |           19 |
   | Škoda Felicia       | 3.12.2000            |      210.000,- |           19 |
   | Škoda Felicia       | 2.12.2000            |      210.000,- |           19 |
   | Škoda Oktávia       | 7.12.2010            |      500.000,- |           20 |

2. In the Visual Studio development environment, on the WPF (WinForms) platform, in the C# language,
   create a program with the following functionality:

   - It lets the user select an XML file from the disk.
   - It loads the selected XML file into a table.
   - For each model, it sums the total price of the cars sold on a weekend and calculates the total price with VAT.
   - It shows the results in this form:

     ```
     Model name
     Price without VAT   Price with VAT
     ----------------------------------
     Škoda XXXXX
     000,00              000,00
     Škoda YYYYY
     000,00              000,00
     Škoda ZZZZZ
     .....
     ```

3. Please hand in all source files of the application + the designed XML filled with the sample data,
   so that we can compile and try the application. Any additional functionality that shows your
   programming skills and thinking is welcome.

4. Hand in the finished assignment as a link to GitHub.
