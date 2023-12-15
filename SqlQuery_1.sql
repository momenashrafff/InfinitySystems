use Homesync
GO
CREATE PROCEDURE ViewMyRoom
    @user_id INT
AS
BEGIN
    DECLARE @room_id INT
    SELECT @room_id = room
    FROM Users
    WHERE ID = @user_id

    SELECT *
    FROM Room
    WHERE ID = @room_id
END;



GO
CREATE PROCEDURE ViewMyDevices
    @user_id INT
AS
BEGIN
    DECLARE @room_id INT

    SELECT @room_id = room
    FROM Users
    WHERE ID = @user_id

    SELECT *
    FROM Device
    WHERE room = @room_id
END;


-- Insert 5 bedrooms into the database
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Bedroom', 1, 'Not Useds');
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Bedroom', 2, 'Not Used');
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Bedroom', 3, 'Not Used');
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Bedroom', 4, 'Not Used');
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Bedroom', 5, 'Not Used');

-- Insert a living room into the database
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Living Room', 1, 'Not Used');

-- Insert a kitchen into the database
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Kitchen', 1, 'Not Used');

-- Insert a garden into the database
INSERT INTO Room
    ([type], [floor], [status])
VALUES
    ('Garden', 1, 'Not Used');


-- Insert 5 admins into the database
INSERT INTO Users
VALUES('Mohammad', 'Moharram', '1234', 'mohammad@gmail.com', 'Preferences', 1, 'Admin', '2004/07/22');
INSERT INTO [Admin]
VALUES(1, 30, 100000);
INSERT INTO Users
VALUES('Momen', 'Ashraf', '1234', 'momen@gmail.com', 'Preferences', 2, 'Admin', '2004/10/12');
INSERT INTO [Admin]
VALUES(2, 30, 10000);
INSERT INTO Users
VALUES('AbdulRahman', 'Nagar', '1234', 'abdulrahman@gmail.com', 'Preferences', 3, 'Admin', '2004/02/01');
INSERT INTO [Admin]
VALUES(3, 30, 10000);
INSERT INTO Users
VALUES('Ahmed', 'Negm', '1234', 'negm@gmail.com', 'Preferences', 4, 'Admin', '2004/09/02');
INSERT INTO [Admin]
VALUES(4, 30, 10000);
INSERT INTO Users
VALUES('Omar', 'Hossam', '1234', 'omar@gmail.com', 'Preferences', 5, 'Admin', '2004/12/21');
INSERT INTO [Admin]
VALUES(5, 30, 10000);

-- Insert 5 guests into the database
INSERT INTO Users
VALUES('Alaa', 'Ashraf', '1234', 'alaa@gmail.com', 'Preferences', 6, 'Guest', '2004/05/13');
INSERT INTO Guest
VALUES(6, 1, 'Cairo', '2023/12/12', '2024/01/01', 'Cairo');
INSERT INTO Users
VALUES('Omar', 'Ashraf', '1234', 'omarashraf@gmail.com', 'Preferences', 6, 'Guest', '2004/01/13');
INSERT INTO Guest
VALUES(7, 1, 'Cairo', '2023/12/12', '2024/01/01', 'Cairo');
INSERT INTO Users
VALUES('AbdulRahma', 'Samir', '1234', 'abdulrahmans@gmail.com', 'Preferences', 6, 'Guest', '2004/09/16');
INSERT INTO Guest
VALUES(8, 1, 'Cairo', '2023/12/12', '2024/01/01', 'Cairo');
INSERT INTO Users
VALUES('Mohsen', 'Reda', '1234', 'mohsen@gmail.com', 'Preferences', 6, 'Guest', '1995/07/10');
INSERT INTO Guest
VALUES(9, 1, 'Austria', '2023/12/12', '2024/01/01', 'Austria');
INSERT INTO Users
VALUES('Sherif', 'Ahmed', '1234', 'sherif@gmail.com', 'Preferences', 6, 'Guest', '2004/07/31');
INSERT INTO Guest
VALUES(10, 1, 'London', '2023/12/12', '2024/01/01', 'London');

-- Insert 5 devices into the database
INSERT INTO Device
VALUES(1, 1, 'TV', 'ON', 100);
INSERT INTO Device
VALUES(2, 2, 'Phone', 'OFF', 0);
INSERT INTO Device
VALUES(3, 2, 'Speaker', 'OFF', 0);
INSERT INTO Device
VALUES(4, 2, 'Laptop', 'OFF', 0);
INSERT INTO Device
VALUES(5, 5, 'Tablet', 'ON', 1);


-- Insert 2 events into the database
INSERT INTO Calendar
VALUES(1, 8, 'Match', 'Football Match', 'Stadium', '2023/12/12');
INSERT INTO Calendar
VALUES(2, 8, 'Match', 'TENNIS Match', 'Stadium', '2023/12/12');
INSERT INTO Calendar
VALUES(2, 1, 'Match', 'Football Match', 'Stadium', '2023/12/12');
INSERT INTO Calendar
VALUES(2, 9, 'Movie', 'Action Movie', 'Cinema', '2024/01/12');

-- Insert 3 tasks into the database
INSERT INTO Task
VALUES('Print', '2023/11/30', '2023/12/29', 'Print', 1, 'Not Done', '2023/12/11', 1);
INSERT INTO Assigned_to
VALUES(1, 1, 2);

INSERT INTO Task
VALUES('Clean', '2023/12/03', '2023/12/22', 'Clean', 2, 'Not Done', '2023/12/20', 2);
INSERT INTO Assigned_to
VALUES(2, 2, 2);

INSERT INTO Task
VALUES('Cook', '2023/11/03', '2023/12/22', 'Cook', 3, 'Not Done', '2023/12/20', 2);
INSERT INTO Assigned_to
VALUES(1, 3, 2);

-- Insert into Consumption
INSERT INTO Consumption
    (device_id, consumption, [date])
VALUES
    (5, 10.00, '2023-11-01');

-- Insert a flight into the database
INSERT INTO Travel
VALUES('Hilton', 'London', 1, 1, '2023/12/29', '2024/01/05', 'Cairo', 'London', 'Airplane');

-- Insert a payment into the database
INSERT INTO Finance
VALUES(6, 'RECIEVED', 1000, 'EGP', 'Visa', 'Unpaid', '2024/10/05', 2, '2023/11/01', NULL);

-- Insert 2 inventories with 0 quantity into the database
INSERT INTO Inventory
VALUES(1, 'Milk', 0, '2023/12/12', 50, 'Juhayna', 'Food');
INSERT INTO Inventory
VALUES(2, 'Bread', 0, '2023/12/12', 1, 'Rich Bake', 'Food');

-- Insert 2 inventories with more than 0 quantity into the database
INSERT INTO Inventory
VALUES(3, 'Juice', 10, '2023/12/12', 10, 'Dina Farms', 'Food');
INSERT INTO Inventory
VALUES(4, 'Cheese', 5, '2023/12/12', 150, 'Domty', 'Food');

-- Insert 2 Preferences
INSERT INTO Preferences
VALUES(8, 50, 'open the AC p', 'Summer')
INSERT INTO Preferences
VALUES(9, 51, 'open the fridge p', 'winter')

-- Insert 2 Recommendations
INSERT INTO Recommendation
VALUES(8, 50, 'open the AC r', 'Summer')
INSERT INTO Recommendation
VALUES(9, 51, 'open the fridge r', 'winter')

-- Insert 2 Notes
INSERT INTO Notes
VALUES(1, 8, 'THIS IS MY NOTE 8', '2023/12/12', 'TITLE OF 1')
INSERT INTO Notes
VALUES(2, 9, 'THIS IS MY NOTE 9', '2021/12/25', 'TITLE OF 2')

-- Insert 2 Communication
INSERT INTO Communication
VALUES(3, 4, 'this is content 1', '10:00:00', '09:00:00', '09:00:01', 'title1')
INSERT INTO Communication
VALUES(4, 3, 'this content 2', '11:20:00', '08:24:00', '08:25:00', 'title2')

-- Insert into Camera
INSERT INTO Camera
VALUES
    (1, 1, 1);

-- Insert into Log
INSERT INTO [Log]
    (room_id, device_id, [user_id], [date], duration)
VALUES
    (1, 1, 4, '2023-11-15', '3 hour');