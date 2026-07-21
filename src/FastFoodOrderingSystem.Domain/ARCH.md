# Domain layer

## Responsibility

Contains pure business rules.

## Contains

- Entities.
- Aggregate roots.
- Value objects.
- Domain results.
- Domain events.


## Must NOT depend on

- ASP.NET Core Framework.
- Entity Framework Core.
- Databases.
- Cache services.
- Logging.
- Email senders.

## Dependency

None.

## Folder structure

```
FastFoodOrderingSystem.Domain
│
├── Common/
│   ├── Abstractions/
│   ├── DomainResults/
│   ├── Enums/
│   ├── Validations/
│   └── ValueObjects/
│       └── Errors/
│                 └── ...
│
├── Users/       
│   ├── Errors/                     
│   ├── Events/                     
│   └── ValueObjects/               
│       ├── Errors/
│       │   └──...                  
│       └── ... 
│
...                   
```