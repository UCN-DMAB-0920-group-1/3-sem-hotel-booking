CREATE TABLE [Hotel] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [name] VARCHAR(500)  NOT NULL ,
    [description] VARCHAR(6000)  NOT NULL ,
    [stars] int  NOT NULL ,
    [staffed_hours] VARCHAR(1000)  NOT NULL ,
    [location_id] int  NOT NULL ,
    CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    CONSTRAINT [UK_Hotel_name] UNIQUE (
        [name]
    )
)

CREATE TABLE [Room] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [type] VARCHAR(250)  NOT NULL ,
    [num_of_avaliable] int  NOT NULL ,
    [num_of_beds] int  NOT NULL ,
    [description] VARCHAR(250)  NULL ,
    [rating] int  NOT NULL ,
    [hotel_id] int  NOT NULL ,  
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    FOREIGN KEY (hotel_id) REFERENCES Hotel(id)
)

CREATE TABLE [Booking] (
    [booking_id] int IDENTITY(1,1) NOT NULL ,
    [start_date] DATE  NOT NULL ,
    [end_date] DATE  NOT NULL ,
    [num_of_guests] int  NOT NULL ,
    [room_id] int  NOT NULL ,
    [customer_id] int  NOT NULL ,
    CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED (
        [booking_id] ASC
    )
)

CREATE TABLE [Picture] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [title] VARCHAR(250)  NULL ,
    [description] VARCHAR(250)  NULL ,
    [path] VARCHAR(250)  NOT NULL ,
    CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    CONSTRAINT [UK_Picture_path] UNIQUE (
        [path]
    )
)

CREATE TABLE [Price] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [start_date] date  NOT NULL ,
    [end_date] date  NOT NULL ,
    [amount] int  NOT NULL ,
    [room_id] int  NOT NULL ,
    CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED (
        [id] ASC
    )
)

CREATE TABLE [Location] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [street_address] VARCHAR(250)  NOT NULL ,
    [zip_code] VARCHAR(250)  NOT NULL ,
    [city] VARCHAR(250)  NOT NULL ,
    [country] VARCHAR(250)  NOT NULL ,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    CONSTRAINT [UK_Location_street_address] UNIQUE (
        [street_address]
    )
)

CREATE TABLE [Customer] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [name] VARCHAR(250)  NOT NULL ,
    [email] VARCHAR(250)  NOT NULL ,
    [phone] VARCHAR(250)  NOT NULL ,
    [age] int  NOT NULL ,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    CONSTRAINT [UK_Customer_email] UNIQUE (
        [email]
    )
)

CREATE TABLE [Credit_Information] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [customer_id] int  NOT NULL ,
    [card_holder_name] VARCHAR(250)  NOT NULL ,
    [card_number] VARCHAR(250)  NOT NULL ,
    [expiration_date] VARCHAR(250)  NOT NULL ,
    [CVC_number] VARCHAR(250)  NOT NULL ,
    CONSTRAINT [PK_Credit_Information] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    CONSTRAINT [UK_card_number] UNIQUE (
        [card_number]
    )
)

CREATE TABLE [Staff] (
    [id] int IDENTITY(1,1) NOT NULL ,
    [name] VARCHAR(250)  NOT NULL ,
    [email] VARCHAR(250)  NOT NULL ,
    [phone] VARCHAR(250)  NOT NULL ,
    [age] int  NOT NULL ,
    [employee_number] int  NOT NULL ,
    [password] VARCHAR(250)  NOT NULL ,
    [hotel_id] int  NOT NULL ,
    CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED (
        [id] ASC
    ),
    CONSTRAINT [UK_Staff_email] UNIQUE (
        [email]
    ),
    CONSTRAINT [UK_Staff_employee_number] UNIQUE (
        [employee_number]
    ),
    CONSTRAINT [UK_Staff_password] UNIQUE (
        [password]
    )
)

CREATE TABLE [TablePictures] (
    [hotel_id] int  NULL ,
    [room_id] int  NULL ,
    [type] VARCHAR(250)  NOT NULL ,
    [picture_id] int  NOT NULL 
)

ALTER TABLE [Price] ADD CONSTRAINT [Price_start_end] 
UNIQUE ([start_date],[end_date]);

ALTER TABLE [Hotel] WITH CHECK ADD CONSTRAINT [FK_Hotel_location_id] FOREIGN KEY([location_id])
REFERENCES [Location] ([id])

ALTER TABLE [Hotel] CHECK CONSTRAINT [FK_Hotel_location_id]

ALTER TABLE [Room] WITH CHECK ADD CONSTRAINT [FK_Room_hotel_id] FOREIGN KEY([hotel_id])
REFERENCES [Hotel] ([id])

ALTER TABLE [Room] CHECK CONSTRAINT [FK_Room_hotel_id]

ALTER TABLE [Booking] WITH CHECK ADD CONSTRAINT [FK_Booking_room_id] FOREIGN KEY([room_id])
REFERENCES [Room] ([id])

ALTER TABLE [Booking] CHECK CONSTRAINT [FK_Booking_room_id]

ALTER TABLE [Booking] WITH CHECK ADD CONSTRAINT [FK_Booking_customer_id] FOREIGN KEY([customer_id])
REFERENCES [Customer] ([id])

ALTER TABLE [Booking] CHECK CONSTRAINT [FK_Booking_customer_id]

ALTER TABLE [Price] WITH CHECK ADD CONSTRAINT [FK_Price_room_id] FOREIGN KEY([room_id])
REFERENCES [Room] ([id])

ALTER TABLE [Price] CHECK CONSTRAINT [FK_Price_room_id]

ALTER TABLE [Credit_Information] WITH CHECK ADD CONSTRAINT [FK_Credit_Information_customer_id] FOREIGN KEY([customer_id])
REFERENCES [Customer] ([id])


ALTER TABLE [Credit_Information] CHECK CONSTRAINT [FK_Credit_Information_customer_id]

ALTER TABLE [Staff] WITH CHECK ADD CONSTRAINT [FK_Staff_hotel_id] FOREIGN KEY([hotel_id])
REFERENCES [Hotel] ([id])

ALTER TABLE [Staff] CHECK CONSTRAINT [FK_Staff_hotel_id]

ALTER TABLE [TablePictures] WITH CHECK ADD CONSTRAINT [FK_TablePictures_hotel_id] FOREIGN KEY([hotel_id])
REFERENCES [Hotel] ([id])

ALTER TABLE [TablePictures] CHECK CONSTRAINT [FK_TablePictures_hotel_id]

ALTER TABLE [TablePictures] WITH CHECK ADD CONSTRAINT [FK_TablePictures_room_id] FOREIGN KEY([room_id])
REFERENCES [Room] ([id])

ALTER TABLE [TablePictures] CHECK CONSTRAINT [FK_TablePictures_room_id]

ALTER TABLE [TablePictures] WITH CHECK ADD CONSTRAINT [FK_TablePictures_picture_id] FOREIGN KEY([picture_id])
REFERENCES [Picture] ([id])

ALTER TABLE [TablePictures] CHECK CONSTRAINT [FK_TablePictures_picture_id]