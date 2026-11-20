# BankApp

Administrationssystem för bankens anställda (individuell inlämning, Systementor). Systemet används av **Cashiers** och **Admins**. Bankens kunder är inte användare i systemet.

## Live på Azure

https://bankapp-mahdi-a9aabsetcqgpabeh.norwayeast-01.azurewebsites.net/

Applikationen körs i Azure App Service och databasen i Azure SQL Database. Första laddningen kan vara långsam.

## Inloggning

| Roll    | Användarnamn                  | Lösenord |
|---------|-------------------------------|----------|
| Admin   | robert.johansson@admin.se     | Abc123#  |
| Cashier | robert.johansson@cashier.se   | Abc123#  |

Rollerna och de två användarna skapas automatiskt vid första start.

## Funktioner

- **Startsida (publik):** statistik per land med antal kunder, antal konton och totalt saldo.
- **Kundsökning:** sökning på namn och stad, 50 kunder per sida med föregående/nästa.
- **Kundbild:** nås via kundnummer eller genom att klicka på en kund. Visar kundinformation, alla konton och totalt saldo.
- **Kontobild:** kontonummer, saldo och transaktioner i fallande datumordning. Fler än 20 transaktioner laddas 20 åt gången med JavaScript/AJAX.
- **Insättning, uttag och överföring:** belopp hanteras som `decimal`. Det går inte att ta ut eller överföra mer än saldot, och felet visas tydligt. Saldot ändras bara via transaktioner.
- **Roller:** Admin och Cashier har tillgång till kund- och kontofunktionerna. Sidan för användaradministration är bara tillgänglig för Admin.

## Teknik

- ASP.NET Core Razor Pages (.NET 10)
- Entity Framework Core, Database First
- ASP.NET Core Identity med roller
- AutoMapper och ViewModels
- Tabler (Bootstrap 5) för layout

## Projektstruktur

- `BankApp.Web`: Razor Pages, Identity och layout
- `BankApp.Services`: services, ViewModels och AutoMapper-profiler
- `BankApp.Data`: entiteter, `DbContext` och repositories (Data Access Layer)

## Köra lokalt

1. Återställ databasen **BankAppData** från `.bak`-filen i SQL Server (SSMS: *Restore Database*).
2. Kör vid behov (byt ut databasnamnet om din databas heter något annat):
   ```sql
   ALTER AUTHORIZATION ON DATABASE::"BankAppData" TO [sa]
   ```
3. Kontrollera connection stringen i `BankApp.Web/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=BankAppData;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```
   Ändra `Server` och `Database` om din SQL Server eller databas heter något annat.
4. Starta projektet **BankApp.Web** (F5). Identity-tabellerna skapas automatiskt av migreringarna, och rollerna och användarna seedas vid uppstart.
