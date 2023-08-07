SELECT * FROM SimpleCrm.Customer;

INSERT INTO Customer (FirstName, LastName, PhoneNumber, OptInNewsletter, Type)
VALUES ('Jeo', 'Maan', '343-555-1212', true, 2);

INSERT INTO Customer (FirstName, LastName, PhoneNumber, OptInNewsletter, Type)
VALUES ('Mary', 'Jane', '235-555-4343', true, 0);

DELETE FROM Customer WHERE Id = 3;

