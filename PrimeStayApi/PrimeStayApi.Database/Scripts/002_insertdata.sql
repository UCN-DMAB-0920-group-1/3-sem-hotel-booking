INSERT INTO [dbo].[Location] ([street_address],[zip_code],[city],[country])
    VALUES
    ('Sysbjærrevej 54','2556','Testborg','Denmark'),('Bjernerdsgade 20','6000','Debugstad','Denmark')
GO


INSERT INTO [dbo].[Hotel] ([name],[description],[stars],[staffed_hours],[location_id])
     VALUES
     ('Hotel Petrús','Classic old fashioned hotel with a river of red wine.',3,'24/7',1),
     ('Hotel Grøtten','An nice hotel inside a cave, there is no lights at all',1,'24/7',2)
GO

INSERT INTO [dbo].[RoomType] ([type],[beds],[description],[rating],[hotel_id])
     VALUES 
     ('Junior Suite',4,'Junior suite smaller room but space for 4',2,1),
     ('Economy Suite',2,'Economy suite with 2 bunk beds',3,1),
     ('Standard Suite',2,'Standard suite with twin beds',4,1),
     ('Premium Suite',2,'Premium suite with champagne',5,1),
     ('Economy Suite',2,'Economy suite with 2 bunk beds',2,2),
     ('Standard Suite',2,'Standard suite with twin beds',4,2),
     ('Premium Suite',2,'Premium suite with champagne',5,2),
     ('Penthouse Suite',1,'Penthouse suite with queen-size bed',5,2)
GO

INSERT INTO [dbo].[Room] ([Room_number], [Room_Type_id], [notes])
     VALUES 
    (1, 1, 'Lugter af nutella'),
    (2, 1, 'Lugter ïkke af nutella'),
    (3, 1, ''),
    (4, 1, ''),

    (1, 2, 'Lugter af slik'),
    (2, 2, 'Lugter ïkke af slik'),
    (3, 2, ''),
    (4, 2, ''),
    
    (1, 3, 'Lugter af monster'),
    (2, 3, 'Lugter ïkke af monster'),
    (3, 3, ''),
    (4, 3, ''),
    
    (1, 4, 'Lugter af crispy-chicken'),
    (2, 4, 'Lugter ïkke af crispy-chicken'),
    (3, 4, ''),
    (4, 4, ''),
    
    (1, 5, 'Lugter af coca cola'),
    (2, 5, 'Lugter ïkke af coca cola'),
    (3, 5, ''),
    (4, 5, ''),
    
    (1, 6, 'Lugter af pepsi'),
    (2, 6, 'Lugter ïkke af pepsi'),
    (3, 6, ''),
    (4, 6, ''),
    
    (1, 7, 'Lugter af kaffe'),
    (2, 7, 'Lugter ïkke af kaffe'),
    (3, 7, ''),
    (4, 7, ''),
    
    (1, 8, 'Lugter af ild'),
    (2, 8, 'Lugter ïkke af ild'),
    (3, 8, ''),
    (4, 8, '')
GO

INSERT INTO [dbo].[Price] ([start_date],[end_date],[amount],[room_type_id])
    VALUES 
    ('2010-10-10','2011-10-10',1,1)
GO

INSERT INTO [dbo].[Customer] ([name],[email],[phone],[birthday])
    VALUES 
    ('Mia Afilahk','MiaAfilahk@watersports.com','64623510','1990-01-01')
GO

INSERT INTO [dbo].[Credit_Information] ([customer_id],[card_holder_name],[card_number],[expiration_date],[CVC_number])
    VALUES 
    (1,'Mia Afilahk','6400555543002300','12/23','832')
GO

INSERT INTO [dbo].[Booking] ([start_date],[end_date],[guests],[room_id],[customer_id])
    VALUES 
    ('2010-11-04','2010-11-16',4,1,1),
    ('2010-11-19','2010-11-21',7,1,1),
    ('2010-11-25','2010-11-26',1,1,1),
    ('2010-11-27','2010-11-30',2,1,1),
    ('2010-12-01','2010-12-24',4,1,1),

    ('2010-12-04','2010-12-16',7,2,1),
    ('2010-11-08','2010-11-15',1,2,1),
    ('2010-11-06','2010-11-20',2,2,1),
    ('2010-11-12','2010-11-24',4,2,1),

    ('2010-11-04','2010-11-16',4,3,1),
    ('2010-12-04','2010-12-16',7,3,1),
    ('2010-11-12','2010-11-24',4,3,1),

    ('2010-09-04','2010-09-16',4,4,1),
    ('2010-12-04','2010-12-16',7,4,1),
    ('2010-12-08','2010-12-15',1,4,1),
    ('2010-10-06','2010-10-20',2,4,1),
    ('2010-10-12','2010-10-24',4,4,1)


GO


INSERT INTO [dbo].[Staff] ([name],[email],[phone],birthday,[employee_number],[password],[hotel_id])
    VALUES 
    ('Kim Jong-un','KimTheLeader@NKorea.gov','0000000001','1980-01-01',12,'TooSexyForMyCountry9000',1),
    ('Donald Trump','DonaldTheGreatLeader@Unemployed.gov','00000000020','1985-01-01',14,'BiggerThanKim11',1)
GO

INSERT INTO [dbo].[Picture] ([title],[description],[path])
    VALUES 
    ('Hotel Petrús', 'Picture of hotel Petrús', 'https://juto.dk/semester/hotel/1.png'),
    ('Hotel Petrús', 'Picture of hotel Petrús', 'https://juto.dk/semester/hotel/2.png'),
    ('Hotel Petrús', 'Picture of hotel Petrús', 'https://juto.dk/semester/hotel/3.png'),
    ('Hotel Grøtten', 'Picture of hotel Grøtten', 'https://juto.dk/semester/hotel/4.png'),
    ('Hotel Grøtten', 'Picture of hotel Grøtten', 'https://juto.dk/semester/hotel/5.png'),
    ('Petrus Room Suite', 'Suite description', 'https://juto.dk/semester/room/1.png'),
    ('Grøtten Room Suite', 'Suite description', 'https://juto.dk/semester/room/2.png'),
    ('Grøtten Room Junior Suite', 'Suite description', 'https://juto.dk/semester/room/3.png'),
    ('Test room', 'Suite description', 'https://juto.dk/semester/room/4.png'),
    ('Some suite', 'Suite description', 'https://juto.dk/semester/room/5.png')
GO

INSERT INTO [dbo].[TablePictures] ([hotel_id],[room_type_id],[type],[picture_id])
    VALUES 
    (1,null,'hotel',1),
    (1,null,'hotel',2),
    (1,null,'hotel',3),
    (2,null,'hotel',4),
    (2,null,'hotel',5),
    (null,1,'room',6),
    (null,1,'room',7),
    (null,1,'room',8),
    (null,2,'room',7),
    (null,3,'room',9),
    (null,4,'room',10),
    (null,5,'room',2),
    (null,6,'room',7),
    (null,7,'room',9),
    (null,8,'room',10)
GO

