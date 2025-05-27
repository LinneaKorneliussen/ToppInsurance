# ToppInsurance – Försäkringsapplikation

## Projektöversikt  
ToppInsurance är en WPF-applikation utvecklad i C# med .NET 6.0 som hanterar försäkringsärenden. Projektet är resultatet av en noggrann kravanalys och design med fokus på skalbarhet, återanvändbarhet och underhållbarhet. Applikationen bygger på en flerskiktsarkitektur med tydlig separation mellan frontend och backend, och använder beprövade designmönster för att säkerställa kvalitet och flexibilitet.

---

## Arkitektur

Lösningen är uppbyggd i flera lager med klart definierade ansvarsområden:

- **Frontend:**  
  - Utvecklad i WPF med XAML och designmönstret MVVM (Model-View-ViewModel).  
  - Användargränssnittet är responsivt och hanteras av ViewModels som kommunicerar med backend via controller-klasser.

- **Backend:**  
  - Utvecklat i .NET 6.0 och uppbyggt kring business layer och data layer.  
  - Business layer innehåller controller-klasser och services som implementerar affärslogiken.  
  - Data layer använder repository-mönstret tillsammans med Entity Framework Core 7.0 för effektiv databashantering och transaktionskontroll.  
  - Dependency Injection används för att underlätta modulär design och testbarhet.

---

## Teknologistack

- **Språk & Plattform:** C# (.NET 6.0), WPF  
- **Designmönster:** MVVM, Repository pattern, Dependency Injection  
- **Databas:** Microsoft SQL Server, hanteras via Entity Framework Core 7.0  
- **Bibliotek och paket:**  
  - `Microsoft.EntityFrameworkCore` och `Microsoft.EntityFrameworkCore.SqlServer`  
  - `MahApps.Metro` och `MahApps.Metro.IconPacks` för ett modernt och användarvänligt UI  
  - `LiveCharts.Wpf` för interaktiva diagram och visualiseringar  
  - `Newtonsoft.Json` för JSON-hantering

---

## Projektstruktur

- **TopInsurance (Frontend):** WPF-applikationen med Views och ViewModels  
- **TopInsuranceBL (Business Layer):** Affärslogik och controller-klasser  
- **TopInsuranceEntities:** Domänmodeller som representerar datan  
- **Data Layer:** Repository- och databaslogik med Entity Framework Core och Unit of Work

---

## Installation och Komma igång

1. Klona repot:  
   ```bash
   git clone <repo-url>
