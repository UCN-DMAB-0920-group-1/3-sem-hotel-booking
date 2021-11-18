
CREATE TABLE [Hotel] (
  [id] int NOT NULL IDENTITY(1, 1),
  [name] VARCHAR(500) NOT NULL,
  [description] VARCHAR(6000) NOT NULL,
  [stars] int NOT NULL,
  [staffed_hours] VARCHAR(1000) NOT NULL,
  [location_id] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [RoomType] (
  [id] int NOT NULL IDENTITY(1, 1),
  [type] VARCHAR(250) NOT NULL,
  [beds] int NOT NULL,
  [description] VARCHAR(250),
  [rating] int NOT NULL,
  [hotel_id] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Room] (
  [id] int NOT NULL IDENTITY(1, 1),
  [room_type_id] int NOT NULL,
  [room_number] VARCHAR(250),
  [notes] VARCHAR(1000),
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Booking] (
  [id] int NOT NULL IDENTITY(1, 1),
  [start_date] DATE NOT NULL,
  [end_date] DATE NOT NULL,
  [guests] int NOT NULL,
  [room_id] int NOT NULL,
  [customer_id] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Picture] (
  [id] int NOT NULL IDENTITY(1, 1),
  [title] VARCHAR(250),
  [description] VARCHAR(250),
  [path] VARCHAR(250) NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Price] (
  [id] int NOT NULL IDENTITY(1, 1),
  [start_date] date NOT NULL,
  [end_date] date NOT NULL,
  [amount] int NOT NULL,
  [room_type_id] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Location] (
  [id] int NOT NULL IDENTITY(1, 1),
  [street_address] VARCHAR(250) NOT NULL,
  [zip_code] VARCHAR(250) NOT NULL,
  [city] VARCHAR(250) NOT NULL,
  [country] VARCHAR(250) NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Customer] (
  [id] int NOT NULL IDENTITY(1, 1),
  [name] VARCHAR(250) NOT NULL,
  [email] VARCHAR(250) NOT NULL,
  [phone] VARCHAR(250) NOT NULL,
  [birthday] datetime NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Credit_Information] (
  [id] int NOT NULL IDENTITY(1, 1),
  [customer_id] int NOT NULL,
  [card_holder_name] VARCHAR(250) NOT NULL,
  [card_number] VARCHAR(250) NOT NULL,
  [expiration_date] VARCHAR(250) NOT NULL,
  [CVC_number] VARCHAR(250) NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [Staff] (
  [id] int NOT NULL IDENTITY(1, 1),
  [name] VARCHAR(250) NOT NULL,
  [email] VARCHAR(250) NOT NULL,
  [phone] VARCHAR(250) NOT NULL,
  [birthday] datetime NOT NULL,
  [employee_number] int NOT NULL,
  [password] VARCHAR(250) NOT NULL,
  [hotel_id] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [TablePictures] (
  [hotel_id] int,
  [room_type_id] int,
  [type] VARCHAR(250) NOT NULL,
  [picture_id] int NOT NULL
)
GO

CREATE TABLE [User] (
  [id] int NOT NULL IDENTITY(1, 1),
  [username]  VARCHAR(250) NOT NULL,
  [password] VARCHAR(250) NOT NULL,
  [role] VARCHAR(250) NOT NULL,
  [salt] VARCHAR(512) NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE UNIQUE INDEX [UK_Hotel_name] ON [Hotel] ("name")
GO

CREATE UNIQUE INDEX [UK_Picture_path] ON [Picture] ("path")
GO

CREATE UNIQUE INDEX [Price_start_end] ON [Price] ("start_date", "end_date")
GO

CREATE UNIQUE INDEX [UK_Location_street_address] ON [Location] ("street_address")
GO

CREATE UNIQUE INDEX [UK_Customer_email] ON [Customer] ("email")
GO

CREATE UNIQUE INDEX [UK_card_number] ON [Credit_Information] ("card_number")
GO

CREATE UNIQUE INDEX [UK_Staff_email] ON [Staff] ("email")
GO

CREATE UNIQUE INDEX [UK_Staff_employee_number] ON [Staff] ("employee_number")
GO

CREATE UNIQUE INDEX [UK_Staff_password] ON [Staff] ("password")
GO

CREATE UNIQUE INDEX [UK_User_name] ON [User] ("username")
GO

ALTER TABLE [Hotel] WITH CHECK ADD CONSTRAINT [FK_Hotel_location_id] FOREIGN KEY([location_id])
REFERENCES [Location] ([id])

ALTER TABLE [Hotel] CHECK CONSTRAINT [FK_Hotel_location_id]

ALTER TABLE [RoomType] WITH CHECK ADD CONSTRAINT [FK_RoomType_hotel_id] FOREIGN KEY([hotel_id])
REFERENCES [Hotel] ([id])

ALTER TABLE [RoomType] CHECK CONSTRAINT [FK_RoomType_hotel_id]

ALTER TABLE [Booking] WITH CHECK ADD CONSTRAINT [FK_Booking_room_id] FOREIGN KEY([room_id])
REFERENCES [Room] ([id])

ALTER TABLE [Booking] CHECK CONSTRAINT [FK_Booking_room_id]

ALTER TABLE [Booking] WITH CHECK ADD CONSTRAINT [FK_Booking_customer_id] FOREIGN KEY([customer_id])
REFERENCES [Customer] ([id])

ALTER TABLE [Booking] CHECK CONSTRAINT [FK_Booking_customer_id]

ALTER TABLE [Price] WITH CHECK ADD CONSTRAINT [FK_Price_room_type_id] FOREIGN KEY([room_type_id])
REFERENCES [Room] ([id])

ALTER TABLE [Price] CHECK CONSTRAINT [FK_Price_room_type_id]

ALTER TABLE [Credit_Information] WITH CHECK ADD CONSTRAINT [FK_Credit_Information_customer_id] FOREIGN KEY([customer_id])
REFERENCES [Customer] ([id])


ALTER TABLE [Credit_Information] CHECK CONSTRAINT [FK_Credit_Information_customer_id]

ALTER TABLE [Staff] WITH CHECK ADD CONSTRAINT [FK_Staff_hotel_id] FOREIGN KEY([hotel_id])
REFERENCES [Hotel] ([id])

ALTER TABLE [Staff] CHECK CONSTRAINT [FK_Staff_hotel_id]

ALTER TABLE [TablePictures] WITH CHECK ADD CONSTRAINT [FK_TablePictures_hotel_id] FOREIGN KEY([hotel_id])
REFERENCES [Hotel] ([id])

ALTER TABLE [TablePictures] CHECK CONSTRAINT [FK_TablePictures_hotel_id]

ALTER TABLE [TablePictures] WITH CHECK ADD CONSTRAINT [FK_TablePictures_room_type_id] FOREIGN KEY([room_type_id])
REFERENCES [Room] ([id])

ALTER TABLE [TablePictures] CHECK CONSTRAINT [FK_TablePictures_room_type_id]

ALTER TABLE [TablePictures] WITH CHECK ADD CONSTRAINT [FK_TablePictures_picture_id] FOREIGN KEY([picture_id])
REFERENCES [Picture] ([id])

ALTER TABLE [TablePictures] CHECK CONSTRAINT [FK_TablePictures_picture_id]