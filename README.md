﻿# Тестовое задание.
Разработать WebAPI приложение для работы с timescale данными некоторых результатов
обработки

## Функциональные требования
- Загрузка файла (в формате _CSV_) с валидацией и рассчетом интегральных значений.
- Возможность получить список записей из таблицы Results, подходящих под фильтры.
- Возможность получит из списка последние 10 значений, отсортированных по начальному
  времени запуска _Date_ по имени заданного файла.

## Нефункциональные требования
-  .NET 8+
-  EF Core
-  PostgreSQL
-  Swagger

---
## Запуск приложения:
1. Создать миграции
```
dotnet ef migrations add InitialCreate   --project Infrastructure/MigrationService  --startup-project Infrastructure/MigrationService
```
2. Запустить _docker-compose.yml_.
```
docker-compose up -d --build
```
Приложение по следующему пути: [Ссылка](http://localhost:5049)
Swagger доступен по следующему пути: [Swagger](http://localhost:5049/swagger)

### Пример данных в _CSV_ файле:
```
Date;ExecutionTime;Value
2024-03-15T10:00:38.4883Z;1.87;19.99
2024-03-15T10:02:56.2426Z;1.05;16.18
...
```
---
## Что хочется отметить:
### DataReader - отвечат за чтение данных и преобразование их в доменные модели.
Для обеспечения расширяемости реализовал паттерн стратегия, который позволяет добавить новую реализацию, когда необходимо уметь обрабатывать новый тип файла.

### ISpecification - спецификации позволяют комбинировать различные выражения _(Expression)_ для того, чтобы фильтровать данные.
- Каждая спецификация — это выражение Expression<Func<TEntity,bool>>, которое можно легко соединять через And, Or
- Не добавляю жесткие условия в репозиторий.

### ValidatorChain - цепочка обязанностей, которая валидирует доменные сущности.
- Позволяет легко изменять последовательность и содержимое условий валидации.
- При неудаче сохраняет внутри себя исключение, которое позже можно обработать.

### Прочее:
- Единая обработка ошибок через ExceptionHandler
- Отдельный сервис для миграций.
- Разделение приложения на следующие слои: Домен/Приложение/Инфраструктура/Веб
- Для юнит тестов использовал XUnit и Moq
