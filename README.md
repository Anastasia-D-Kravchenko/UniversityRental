# University Equipment Rental System

## Project Overview
A console application built in C# to manage university equipment rentals. It supports registering users and equipment, handling rental limits, tracking overdue items, and calculating penalties.

## How to Run
1. Open a terminal in the project directory.
2. Run `dotnet build` to compile the project.
3. Run `dotnet run` to execute the demonstration scenario outlined in `Program.cs`.

## Design Decisions & OOP Principles

* **Separation of Concerns (Folders/Namespaces):** The code is divided into `Models`, `Services`, and `Exceptions`, ensuring `Program.cs` is only responsible for the UI/Execution flow, completely detached from business rules.
* **Cohesion:** `RentalService.cs` is highly cohesive. It acts as the singular domain service managing the interactions between Users, Equipment, and Rentals. It doesn't print menus or prompt the user, keeping its responsibility strictly tied to business operations.
* **Coupling:** Dependencies are injected or passed as parameters (e.g., passing `User` and `Equipment` to `Rental`). I used standard generic collections (`IEnumerable`) to return data from the service to avoid exposing internal `List` representations, reducing tight coupling.
* **Polymorphism & Inheritance:** `Equipment` and `User` are abstract base classes. Instead of using `if (user.Type == "Student")` to check rental limits, the limit is a polymorphic property `MaxActiveRentals` overridden by `Student` and `Employee`.
* **Exception Handling:** Custom domain exceptions (`RentalLimitExceededException`, `EquipmentUnavailableException`) are used to handle business rule violations cleanly, avoiding generic "Exception" throws.