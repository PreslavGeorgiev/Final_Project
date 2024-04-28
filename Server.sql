  
Create table product_category (
    id int primary key,
    product_name varchar(255),
    description_of_product varchar(max)
);

Create table accountdetails (
    username varchar(255) primary key not null,
    [password] varchar(max),
    creationDate date,
    first_name varchar(max),
    last_name varchar(max),
    contact_id int,
    adress_id int
);

Create Table product_inventory (
    product_id int,
    quantity int,
	mainPhoto varbinary(max),
	price decimal(2),
	productName varchar(max),
	productDescription varchar(max),
    foreign key (product_id) references product_category(id)
);

Create table user_payment (
    id int primary key,
    _user_id varchar(255),
    payment_type varchar(255),
    total decimal (10, 2),
    payment_date date,
    foreign key (_user_id) references accountdetails(username)
);

Create table advertisements (
    advertisements_id int primary key,
    advertisement_company varchar(max),
    clicks_on_ad int,
    time_spent varchar(max)
);

Create Table shopping_session (
    id int primary key not null,
    username varchar(255),
    _session_id int,
    active varchar(max),
    advertisements_id int,
    foreign key (username) references accountdetails(username),
    foreign key (advertisements_id) references advertisements(advertisements_id)
);

Create table order_details (
    id int primary key,
    [user_id] varchar(255),  
    product_id int,
    quantity int,
    payment_id int,
    foreign key ([user_id]) references accountdetails(username),
    foreign key (product_id) references product_category(id),
    foreign key (payment_id) references user_payment(id)
);

create table user_contact (
    contact_id int primary key not null,
    contact_type varchar(50), 
    contact_number varchar(255)
);

create table location_of_adress (
    postal_code int primary key,
    country varchar(max),
    city varchar(max)
);

create table location_details (
    adress_id int PRIMARY KEY,
    postal_code int,
    foreign key (postal_code) references location_of_adress(postal_code)
);

INSERT INTO user_contact (contact_id, contact_type, contact_number)
VALUES
    (1, 'telephone', '123-456-7890'),
    (2, 'mobile', '987-654-3210'),
    (3, 'email', 'ivani@email.com');

INSERT INTO location_of_adress (postal_code, country, city)
VALUES
    (1000, 'Bulgaria', 'Sofia'),
    (2003, 'Bulgaria', 'Pleven'),
    (2005, 'Bulgaria', 'Plovdiv');

INSERT INTO location_details (adress_id, postal_code)
VALUES
    (101, 1000),
    (202, 1000),
    (303, 2003);

INSERT INTO accountdetails (username, [password], creationDate, first_name, last_name, contact_id, adress_id)
VALUES
    ('100', 'password123', '2023-01-01', 'John', 'Doe', 1, 101),
    ('200', 'pass456', '2023-02-01', 'Jane', 'Smith', 2, 202),
    ('300', 'pass789', '2023-03-01', 'Bob', 'Johnson', 3, 303);

INSERT INTO product_category (id, product_name, description_of_product)
VALUES
    (1, 'Electronics', 'Various electronic gadgets'),
    (2, 'Clothing', 'Fashionable clothing items'),
    (3, 'Books', 'A wide range of books');

INSERT INTO product_inventory (product_id, quantity, productName, productDescription, price, mainPhoto)
VALUES

INSERT INTO user_payment (id, _user_id, payment_type, total, payment_date)
VALUES
    (1000, '100', 'Credit Card', 50.00, '2023-01-15'),
    (2000, '200', 'PayPal', 75.50, '2023-02-20'),
    (3000, '300', 'Cash', 30.25, '2023-03-25');

INSERT INTO advertisements (advertisements_id, advertisement_company, clicks_on_ad, time_spent)
VALUES
    (10, 'Paypal', 1000, '30 minutes'),
    (20, 'FIFA', 800, '25 minutes'),
    (30, 'GameOFThrones', 1200, '35 minutes');

INSERT INTO shopping_session (id, username, _session_id, active, advertisements_id)
VALUES
    (500, '100', 1, 'Yes', 10),
    (600, '200', 2, 'No', 20),
    (700, '300', 3, 'Yes', 30);

INSERT INTO order_details (id, [user_id], product_id, quantity, payment_id)
VALUES
    (12345, '100', 1, 2, 1000),
    (12346, '200', 2, 1, 2000),
    (12347, '300', 3, 3, 3000);