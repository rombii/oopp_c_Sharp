# Projekt zaliczeniowy z przedmiotu: _**Progrmowanie obiektowe 2**_

# Temat projektu: Gra z wykorzystaniem bazy danych
## Skład grupy: Jakub Foltarz, Kamil Kondziołka
## Specyfikacja projektu
### Cel projektu : Stworzenie aplikacji z wykorzystaniem bazy danych
#### Cele szczegółowe:
   1. Wykorzystać możliwości WPF
   2. Wykorzystać możliwości EF Core
### Funkcjonalności:
   1. Możliwość dodawania własnych przeciwników, przedmiotów i elementów do gry
   2. Możliwość edycji domyślnych rekordów lub ich usunięcia
   3. Prezentacja możliwości stworzenia gry przy użyciu WPF
   4. Możliwość obserwacji działania połączenia c# z bazą danych
### Interfejs:

   <details>
       <summary>Ekran główny</summary>
           Ekran główny zawiera 3 przyciski, służące do przejścia na ekran gry, ekran służący do edytowania oraz wyjście z aplikacji
	
![mainScreen](https://i.gyazo.com/a4ab06dfff5993555136a9e929748508.png)
   </details>
   <details>
       <summary>Ekran gry</summary>
           Ekran gry zawiera informacje o wykonanych turach, przedmiocie aktualnie trzymanym przez gracza, główną planszę, oraz pasek z przedmiotami
	
![gameScreen](https://i.gyazo.com/2fde9dbc27a9f88a8efdda4b1e1971a3.png)
   </details>
   
   <details>
       <summary>Ekran edycji</summary>
           Ekran edycji są to wyświetlane w postaci tabeli dane pobierane z bazy danych z możliwościa dodania, edycji, oraz usunięcia rekordów
	
![editMain](https://i.gyazo.com/c0dd49d79360f472143913af8d89491e.png)
![editEditor](https://i.gyazo.com/33edf9d66ce40537873ef83de2193e85.png)
   </details>
         
### Baza danych

####	Diagram ERD
![image](https://i.gyazo.com/54da70ba618fac17c8643c30241f2932.png)

####	Skrypt do utworzenia struktury bazy danych

    create table Elements
    (
        ElementId  INTEGER not null
            constraint PK_Elements
                primary key autoincrement,
        Name       TEXT,
        SpriteUrl  TEXT,
        WeakToId   INTEGER not null,
        StrongToId INTEGER not null
    );

    create table Items
    (
        ItemId    INTEGER not null
            constraint PK_Items
                primary key autoincrement,
        Name      TEXT,
        SpriteUrl TEXT,
        ElementId INTEGER
            references Elements
                on delete cascade,
        DmgMin    INTEGER not null,
        DmgMax    INTEGER not null,
        Type      INTEGER not null
    );

    create table Enemies
    (
        EnemyId   INTEGER not null
            constraint PK_Enemies
                primary key autoincrement,
        Name      TEXT,
        Sprite    TEXT,
        ElementId INTEGER
            constraint FK_Enemies_Elements_ElementId
                references Elements,
        Health    INTEGER not null,
        DmgMin    INTEGER not null,
        DmgMax    INTEGER not null,
        ItemId    INTEGER
            constraint FK_Enemies_Items_ItemId
                references Items
    );

    create index IX_Enemies_ElementId
        on Enemies (ElementId);

    create index IX_Enemies_ItemId
        on Enemies (ItemId);

    create index IX_Items_ElementId
        on Items (ElementId);

    create table __EFMigrationsHistory
    (
        MigrationId    TEXT not null
            constraint PK___EFMigrationsHistory
                primary key,
        ProductVersion TEXT not null
    );



### Wykorzystane technologie:
    1. WPF
    2. EF Core
    3. SQLite
### Diagram przypadków użycia
![image](https://i.gyazo.com/99c91cc314cde420b35fd9a0b6ece1fa.png)
