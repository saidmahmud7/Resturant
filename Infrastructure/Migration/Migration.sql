CREATE TABLE Customers
(
    CustomerId  serial primary key,
    Name        varchar(100),
    PhoneNumber varchar(20)
);

CREATE TABLE Tables
(
    TableId     serial primary key,
    TableNumber int,
    IsOccupied  bool DEFAULT FALSE
);

CREATE TABLE MenuItems
(
    MenuItemId serial primary key,
    Name       varchar(100),
    Price      decimal CHECK (price > 0),
    Category   text
);

CREATE TABLE Orders
(
    OrderId    serial PRIMARY key,
    CustomerId int REFERENCES Customers (CustomerId),
    TableId    int REFERENCES Tables (TableId),
    Status     text
)

CREATE TABLE OrderItems
(
    Id         serial primary key,
    OrderId    int REFERENCES Orders (OrderId),
    MenuItemId int REFERENCES MenuItems (MenuItemId)
)