INSERT INTO [dbo].[Location] ([street_address],[zip_code],[city],[country])
    VALUES
    ('Sysbjærrevej 54','2556','Testborg','Denmark'),('Bjernerdsgade 20','6000','Debugstad','Denmark')
GO


INSERT INTO [dbo].[Hotel] ([name],[description],[stars],[staffed_hours],[location_id],[active])
     VALUES
     ('Hotel Petrús','Classic old fashioned hotel with a river of red wine.',3,'24/7',1,1),
     ('Hotel Grøtten','An nice hotel inside a cave, there is no lights at all',1,'24/7',2,1)
GO

INSERT INTO [dbo].[RoomType] ([type],[beds],[description],[rating],[hotel_id], [active])
     VALUES 
     ('Junior Suite',4,'Junior suite smaller room but space for 4',2,1,1),
     ('Economy Suite',2,'Economy suite with 2 bunk beds',3,1,1),
     ('Standard Suite',2,'Standard suite with twin beds',4,1,1),
     ('Premium Suite',2,'Premium suite with champagne',5,1,1),
     ('Economy Suite',2,'Economy suite with 2 bunk beds',2,2,1),
     ('Standard Suite',2,'Standard suite with twin beds',4,2,1),
     ('Premium Suite',2,'Premium suite with champagne',5,2,1),
     ('Penthouse Suite',1,'Penthouse suite with queen-size bed',5,2,1)
GO

INSERT INTO [dbo].[Room] ([Room_number], [Room_Type_id], [notes], [active])
     VALUES 
    (1, 1, 'Lugter af nutella',1),
    (2, 1, 'Lugter ïkke af nutella',1),
    (3, 1, '',1),
    (4, 1, '',1),

    (1, 2, 'Lugter af slik',1),
    (2, 2, 'Lugter ïkke af slik',1),
    (3, 2, '',1),
    (4, 2, '',1),
    
    (1, 3, 'Lugter af monster',1),
    (2, 3, 'Lugter ïkke af monster',1),
    (3, 3, '',1),
    (4, 3, '',1),
    
    (1, 4, 'Lugter af crispy-chicken',1),
    (2, 4, 'Lugter ïkke af crispy-chicken',1),
    (3, 4, '',1),
    (4, 4, '',1),
    
    (1, 5, 'Lugter af coca cola',1),
    (2, 5, 'Lugter ïkke af coca cola',1),
    (3, 5, '',1),
    (4, 5, '',1),
    
    (1, 6, 'Lugter af pepsi',1),
    (2, 6, 'Lugter ïkke af pepsi',1),
    (3, 6, '',1),
    (4, 6, '',1),
    
    (1, 7, 'Lugter af kaffe',1),
    (2, 7, 'Lugter ïkke af kaffe',1),
    (3, 7, '',1),
    (4, 7, '',1),
    
    (1, 8, 'Lugter af ild',1),
    (2, 8, 'Lugter ïkke af ild',1),
    (3, 8, '',1),
    (4, 8, '',1)
GO

INSERT INTO [dbo].[Price] ([start_date],[end_date],[amount],[room_type_id])
    VALUES 
    ('2010-10-10','2011-10-10',1,1)
GO

INSERT INTO [dbo].[Customer] ([name],[email],[phone],[birthday])
    VALUES 
    ('Mia Afilahk','MiaAfilahk@watersports.com','64623510','1990-01-01'),
        ('Delete Test','Delete@test','Delete Test','1990-01-01'),
        ('Update Test','Update@test','Update Test','1991-01-01'),
        ('Read Test','Read@test','Read Test','1992-01-01')

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

INSERT INTO [dbo].[Role] ([name])
    VALUES
    ('admin'),
    ('user')
GO

INSERT INTO [dbo].[User] ([username], [password], [role_id], [salt])
    VALUES
    ('Michael', 'Ujm4jtwLY9/yLNBKIonnw85QmQ77UQDPEn0K0Cor2/k=', 1, '/9tABheeC6iXYRopgpe1bIe/xBM3jK41vYjfkPvbo0bVYAWyhhcYLZzh/xBcMalElw5n/eTiz55fB3XCdLWKaD79w5jyvS4rUeBRFzT/wqamWvvWAt0rv0MWRBAd18SYUGLdUluSzpo+znAGcov5jB6wKo0eVZ25vBBu14ZFkP7ltN6DZIc35YshNgVYIvheQlsEG7gZ8VAJhAn1dsaqJrTPcZBFLykJ9Das6kk8+enWNd/QpfqundOSoq0OHzeLPYBuiPJB1Q8CVCll05TKBiytShFpxeZJa72AFpHfQX5eHxhshJzy0eCxRCBu9UV67vbi2BC1WqObM1gN2BxP9g=='),
    ('Mike', 'kyp0iPlRSWkgex3AWXfAV4/8mEi4EynEyjMAY4JB9ek=', 2, '364zVZ1Z2n5DOp6G20kLE1cmenpaZ4FlcftJpe8UZO2ErCsPOrcCgMPRqF86ZFbq6x/NMmNOVVLpr7dxJ8Ik8ZDLoaWHI5ewc1XOXGZAsjLRSW7NZnfvMMN1a9BwiBEoPuS7RuLcEiWwUoi+8oK9tR2S3JKPfcwoW5tpTnCIxYg/jOvupv6QsNlzD+FMBMWDZWuf0uW5IpPA2Y2Ilvxc1U0Pxegd2qgx/fK9xYO+8ZtB9do7WmD0vHLWb9cChv8PNtsQxXwQYGs96hbGl6M2GX5K83sLk8hYIpMl0jS6sjG9RGN/w7XdLFdebyG9twUj0wkdCiaD5lPO/w9biZ/DvA=='),
    ('admin', '+vkKwhs55Ablp5+oVPQZ0dUdHKy3XF/xgQ9Hk4FF9r0=',1, 'ULT//gIJRbU3YoPW0eHxTR+6/rwmZ26RCJM+6kgw+sIW1EncFDvJ98qUZ4ovKf71Z/eaBVwLDW37X0qPqJO5p3Pk/dH7Ne4g8oNRZBEoApWVxbjXTcKpvPYBMfmd0cI65aTSv162ITgWJFtZw40CTlSYXXPRdWLZWVT2pjcL852TISCgifMBw2A0L9ckZzJK6F+XpjmKiZla6FdAhvxr27M1zdfV9fN6TCdeZP+BgYPUsoWmJqBckopa2URzqFoFylr6bwYk3Q+ptBpkKHgKDh3r5FGKfDcNpyoswpnBIua5Ovso23L4diFVRK0U8QHAd/3ob2A+A9Aj5i+8PtAPFw==')
GO