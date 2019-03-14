namespace Ads.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ads.Models;
    

    internal sealed class Configuration : DbMigrationsConfiguration<Ads.Models.AdContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ads.Models.AdContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Ads', RESEED, 0)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Categories', RESEED, 0)");
            // context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Attributes', RESEED, 0)");
            // context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Users', RESEED, 0)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('ForbiddenWords', RESEED, 0)");

            context.Users.AddOrUpdate(x => x.UserID,
                new User() { UserID = 1, Name = "Gosia", Login = "GosiaKuc@gmail.com", Surname = " Kuc", Phone = 12345, Password = "pizza", ConfirmPassword = "pizza", IsActive = true, IsAdmin = true, MyPagination =1},
                new User() { UserID = 2, Name = "Mati", Login = "Matychuck@gmail.com", Surname = "Matyczak", Phone = 12234, Password = "keebs", ConfirmPassword = "keebs", IsActive = true, IsAdmin = true, MyPagination = 1 },
                new User() { UserID = 3, Name = "Przemek", Login = "Przemek@gmail.com", Surname = " Przemek", Phone = 123398, Password = "12qw", ConfirmPassword = "12qw", IsActive = true, IsAdmin = true, MyPagination = 1 },
                new User() { UserID = 4, Name = "Ala", Login = "Ala@gmail.com", Surname = "Kot", Phone = 252334, Password = "12qw", ConfirmPassword = "12qw", IsActive = true, IsAdmin = false, MyPagination = 1 },
                new User() { UserID = 5, Name = "Jan", Login = "jan@gmail.com", Surname = " Kowalski", Phone = 534414, Password = "12qw", ConfirmPassword = "12qw", IsActive = true, IsAdmin = false , MyPagination = 1 },
                new User() { UserID = 6, Name = "Eugeniusz", Login = "nowaczek@gmail.com", Surname = "Nowak", Phone = 225334, Password = "12qw", ConfirmPassword = "12qw", IsActive = true, IsAdmin = false, MyPagination = 1 },
                new User() { UserID = 7, Name = "Maurycy", Login = "kowal@gmail.com", Surname = "Kowal", Phone = 12525233, Password = "12qw", ConfirmPassword = "12qw", IsActive = true, IsAdmin = false, MyPagination = 1 },
                new User() { UserID = 8, Name = "Jessica", Login = "Jes@gmail.com", Surname = "Brown", Phone = 832525233, Password = "12qw", ConfirmPassword = "12qw", IsActive = true, IsAdmin = false, MyPagination = 1 }
              );

            context.Attributes.AddOrUpdate(x => x.AttributeID,
               new Models.Attribute() { AttributeID = 2, Type = "int", Name = "Powierzchnia", CategoryID = 1},
               new Models.Attribute() { AttributeID = 3, Type = "int", Name = "Cena", CategoryID = 2},
               new Models.Attribute() { AttributeID = 4, Type = "int", Name = "Wiek", CategoryID = 2 },
               new Models.Attribute() { AttributeID = 5, Type = "shorttext", Name = "Ulica", CategoryID = 4 },
               new Models.Attribute() { AttributeID = 6, Type = "shorttext", Name = "Kolor", CategoryID = 8 },
               new Models.Attribute() { AttributeID = 7, Type = "shorttext", Name = "Przebieg", CategoryID = 2 },
               new Models.Attribute() { AttributeID = 8, Type = "shorttext", Name = "Marka", CategoryID =2 },
               new Models.Attribute() { AttributeID = 9, Type = "shorttext", Name = "Silnik", CategoryID = 2 },
               new Models.Attribute() { AttributeID = 10, Type = "shorttext", Name = "Promocja", CategoryID = 7 },
               new Models.Attribute() { AttributeID = 23, Type = "shorttext", Name = "Projektant", CategoryID = 8 },
                new Models.Attribute() { AttributeID = 26, Type = "shorttext", Name = "Lokalizacja", CategoryID = 1 },
               new Models.Attribute() { AttributeID = 27, Type = "int", Name = "Koszt", CategoryID = 10},
               new Models.Attribute() { AttributeID = 28, Type = "shorttext", Name = "Materia³", CategoryID = 8 },
               new Models.Attribute() { AttributeID = 29, Type = "shorttext", Name = "Rozmiar", CategoryID = 8 },
               new Models.Attribute() { AttributeID = 30, Type = "shorttext", Name = "Producent", CategoryID = 35 }

               );

            context.Categories.AddOrUpdate(x => x.CategoryID,
                new Category() { CategoryID = 1, Name = "Nieruchomosci", ParentcategoryID = null },
                new Category() { CategoryID = 2, Name = "Auto-moto", ParentcategoryID = null },
                new Category() { CategoryID = 3, Name = "Dom i ogród", ParentcategoryID = null },
                new Category() { CategoryID = 4, Name = "Praca", ParentcategoryID = null },
                new Category() { CategoryID = 5, Name = "Nauka", ParentcategoryID = null },
                new Category() { CategoryID = 6, Name = "Zwierzêta", ParentcategoryID = null },
                new Category() { CategoryID = 7, Name = "Sport i hobby", ParentcategoryID = null },
                new Category() { CategoryID = 8, Name = "Moda", ParentcategoryID = null },
                new Category() { CategoryID = 9, Name = "Muzyka i edukacja", ParentcategoryID = null },
                new Category() { CategoryID = 10, Name = "Us³ugi i firmy", ParentcategoryID = null },
                new Category() { CategoryID = 11, Name = "Rolnictwo", ParentcategoryID = null },
                ///////////////////////////// Nieruchomosci /////////////////////////////////////

                new Category() { CategoryID = 12, Name = "Wynajem", ParentcategoryID = 1 },
                new Category() { CategoryID = 13, Name = "Mieszkania", ParentcategoryID = 12 },
                new Category() { CategoryID = 14, Name = "Pokoje", ParentcategoryID = 12 },
                new Category() { CategoryID = 15, Name = "Biura i lokale", ParentcategoryID = 12 },
                new Category() { CategoryID = 16, Name = "Gara¿ i parking", ParentcategoryID = 12 },

                new Category() { CategoryID = 17, Name = "Sprzeda¿", ParentcategoryID = 1 },
                new Category() { CategoryID = 18, Name = "Mieszkania", ParentcategoryID = 17 },
                new Category() { CategoryID = 19, Name = "Domy", ParentcategoryID = 17 },
                new Category() { CategoryID = 20, Name = "Biura i lokale", ParentcategoryID = 17 },
                new Category() { CategoryID = 21, Name = "Gara¿", ParentcategoryID = 17 },

                ///////////////////////////// Automoto /////////////////////////////////////
                new Category() { CategoryID = 22, Name = "Samochody", ParentcategoryID = 2 },
                new Category() { CategoryID = 23, Name = "Jednoœlady", ParentcategoryID = 2 },
                new Category() { CategoryID = 24, Name = "Czêœci", ParentcategoryID = 2 },
                new Category() { CategoryID = 25, Name = "Naprawa i serwis", ParentcategoryID = 2 },

                new Category() { CategoryID = 26, Name = "Osobowe", ParentcategoryID = 22 },
                new Category() { CategoryID = 27, Name = "Dostawcze", ParentcategoryID = 22 },
                new Category() { CategoryID = 28, Name = "Ciê¿arowe", ParentcategoryID = 22 },

                new Category() { CategoryID = 29, Name = "Motocykle", ParentcategoryID = 23 },
                new Category() { CategoryID = 30, Name = "Skutery", ParentcategoryID = 23 },
                new Category() { CategoryID = 31, Name = "Rowery", ParentcategoryID = 23 },

                ///////////////////////////// Dom i ogród /////////////////////////////////////
                new Category() { CategoryID = 32, Name = "Sprzêt", ParentcategoryID = 3 },
                new Category() { CategoryID = 33, Name = "Meble", ParentcategoryID = 3 },

                new Category() { CategoryID = 34, Name = "AGD", ParentcategoryID = 32 },
                new Category() { CategoryID = 35, Name = "Elektronika", ParentcategoryID = 32 },

                new Category() { CategoryID = 80, Name = "Telefony", ParentcategoryID = 35 },
                new Category() { CategoryID = 81, Name = "Komputery i konsole", ParentcategoryID = 35 },
                new Category() { CategoryID = 82, Name = "Fotografia", ParentcategoryID = 35 },
                new Category() { CategoryID = 83, Name = "Telewizory", ParentcategoryID = 35 },

                new Category() { CategoryID = 36, Name = "Ogrodowe", ParentcategoryID = 33 },
                new Category() { CategoryID = 37, Name = "Mieszkaniowe", ParentcategoryID = 33 },

                new Category() { CategoryID = 38, Name = "Plastikowe", ParentcategoryID = 37 },
                new Category() { CategoryID = 39, Name = "Drewniane", ParentcategoryID = 37 },
                new Category() { CategoryID = 40, Name = "Metalowe", ParentcategoryID = 37 },


                ///////////////////////////// Praca /////////////////////////////////////        
                new Category() { CategoryID = 41, Name = "Oferujê pracê", ParentcategoryID = 4 },
                new Category() { CategoryID = 42, Name = "Szukam pracy", ParentcategoryID = 4 },

                ///////////////////////////// Nauka /////////////////////////////////////
                new Category() { CategoryID = 43, Name = "Korepetycje", ParentcategoryID = 5 },
                new Category() { CategoryID = 44, Name = "Szkolenia i kursy", ParentcategoryID = 5 },
                new Category() { CategoryID = 45, Name = "Szko³y", ParentcategoryID = 5 },
                new Category() { CategoryID = 46, Name = "Pomoce naukowe", ParentcategoryID = 5 },

                ///////////////////////////// Zwierzêta /////////////////////////////////////
                new Category() { CategoryID = 47, Name = "Sprzedam", ParentcategoryID = 6 },
                new Category() { CategoryID = 48, Name = "Kupiê", ParentcategoryID = 6 },
                new Category() { CategoryID = 49, Name = "Oddam", ParentcategoryID = 6 },

                new Category() { CategoryID = 50, Name = "Domowe", ParentcategoryID = 47 },
                new Category() { CategoryID = 51, Name = "Gospodarcze", ParentcategoryID = 47 },
                new Category() { CategoryID = 52, Name = "Pozosta³e", ParentcategoryID = 47 },

                new Category() { CategoryID = 53, Name = "Domowe", ParentcategoryID = 48 },
                new Category() { CategoryID = 54, Name = "Gospodarcze", ParentcategoryID = 48 },
                new Category() { CategoryID = 55, Name = "Pozosta³e", ParentcategoryID = 48 },

                new Category() { CategoryID = 56, Name = "Domowe", ParentcategoryID = 49 },
                new Category() { CategoryID = 57, Name = "Gospodarcze", ParentcategoryID = 49 },
                new Category() { CategoryID = 58, Name = "Pozosta³e", ParentcategoryID = 49 },

                ///////////////////////////// Sport i hobby /////////////////////////////////////

                new Category() { CategoryID = 59, Name = "Sporty zimowe", ParentcategoryID = 7 },
                new Category() { CategoryID = 60, Name = "Sporty letnie", ParentcategoryID = 7 },
                new Category() { CategoryID = 61, Name = "Sporty wodne", ParentcategoryID = 7 },
                new Category() { CategoryID = 62, Name = "Pozosta³e", ParentcategoryID = 7 },

                ///////////////////////////// Moda /////////////////////////////////////

                new Category() { CategoryID = 63, Name = "Ubrania", ParentcategoryID = 8 },
                new Category() { CategoryID = 64, Name = "Dla Niej", ParentcategoryID = 8 },
                new Category() { CategoryID = 65, Name = "Dla Niego", ParentcategoryID = 8 },
                new Category() { CategoryID = 66, Name = "Dla dzieci", ParentcategoryID = 8 },
                new Category() { CategoryID = 67, Name = "Buty", ParentcategoryID = 8 },

                ///////////////////////////// Muzyka i edukacja /////////////////////////////////////
                new Category() { CategoryID = 68, Name = "Ksi¹¿ki", ParentcategoryID = 9 },
                new Category() { CategoryID = 69, Name = "Filmy", ParentcategoryID = 9 },
                new Category() { CategoryID = 70, Name = "Sprzêt muzyczny", ParentcategoryID = 9 },
                new Category() { CategoryID = 71, Name = "Muzyka", ParentcategoryID = 9 },
                new Category() { CategoryID = 72, Name = "Inne", ParentcategoryID = 9 },

                ///////////////////////////// Us³ugi i firmy /////////////////////////////////////

                new Category() { CategoryID = 73, Name = "Us³ugi", ParentcategoryID = 10 },
                new Category() { CategoryID = 74, Name = "Wspó³praca biznesowa", ParentcategoryID = 10 },
                new Category() { CategoryID = 75, Name = "Wyposa¿enie firm", ParentcategoryID = 10 },

                ///////////////////////////// Rolnictwo    /////////////////////////////////////
                new Category() { CategoryID = 76, Name = "Ci¹gniki", ParentcategoryID = 11 },
                new Category() { CategoryID = 77, Name = "Czêœci do maszyn", ParentcategoryID = 11 },
                new Category() { CategoryID = 78, Name = "Produkty rolne", ParentcategoryID = 11 },
                new Category() { CategoryID = 79, Name = "Pozosta³e", ParentcategoryID = 11 }
                );

            context.Ads.AddOrUpdate(x => x.AdID,
               new Ad() { AdID = 1, Title = "Wynajme mieszkanie", Content = "Mieszkanie w centrum do 2tysiecy z³otych", CategoryID = 13, DateOfInsert = DateTime.ParseExact("11/11/2018 13:45:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 4 },
               new Ad() { AdID = 2, Title = "Sprzedam mieszkanie", Content = "Kawalerka na sprzeda¿ w centrum za 200tys z³", CategoryID = 18, DateOfInsert = DateTime.ParseExact("21/11/2018 13:45:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 4 },
               new Ad() { AdID = 3, Title = "Sprzedam samochód", Content = "Sprzedam u¿ywanego fiata Punto za 5tys z³", CategoryID = 26, DateOfInsert = DateTime.ParseExact("22/11/2018 13:05:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 6 },
               new Ad() { AdID = 4, Title = "Sprzedam ciê¿arówke", Content = "Sprzedam now¹ ciê¿arówke za 500tys z³", CategoryID = 28, DateOfInsert = DateTime.ParseExact("23/11/2018 13:35:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 6 },
               new Ad() { AdID = 5, Title = "Kupie rower górski", Content = "Kupie rower górski do 3tyz³ dla kobiety", CategoryID = 31, DateOfInsert = DateTime.ParseExact("24/10/2018 19:05:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 6 },
               new Ad() { AdID = 6, Title = "Sprzedam motocykl", Content = "Sprzedam motocykl Harley-Davidson za 100tys z³", CategoryID = 29, DateOfInsert = DateTime.ParseExact("25/11/2018 22:41:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 4 },
               new Ad() { AdID = 7, Title = "Sprzedam mikser", Content = "Sprzedam 2letni lekko u¿ywany mikser firmy Zelmer", CategoryID = 34, DateOfInsert = DateTime.ParseExact("26/11/2018 13:25:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 1 },
               new Ad() { AdID = 8, Title = "Kupie blender", Content = "Kupie blender do koktajli najlepiej do 100z³", CategoryID = 34, DateOfInsert = DateTime.ParseExact("27/11/2018 14:45:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 1 },
               new Ad() { AdID = 9, Title = "Sprzedam telewizor", Content = "Sprzedam telewizor Samsung 49cali ", CategoryID = 83, DateOfInsert = DateTime.ParseExact("28/11/2018 12:45:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 2 },
               new Ad() { AdID = 10, Title = "Sprzedam miktofalówkê", Content = "Sprzedam mikrofalówkê o mocy 900W", CategoryID = 34, DateOfInsert = DateTime.ParseExact("29/11/2018 11:45:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 1 },
               new Ad() { AdID = 11, Title = "Sprzedam lodówkê", Content = "Lekko u¿ywana lodówka Amica za 1200z³", CategoryID = 34, DateOfInsert = DateTime.ParseExact("30/11/2018 07:28:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 1 },
               new Ad() { AdID = 12, Title = "Kupiê monitor", Content = "Kupiê monitor 17 cali najlepiej firmy Samsung", CategoryID = 80, DateOfInsert = DateTime.ParseExact("20/11/2018 08:45:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 1 },
               new Ad() { AdID = 13, Title = "Sprzedam aparat fotograficzny", Content = "Oferujê aparat NEX 6 z zestawem obiektywów za 3tys z³", CategoryID = 82, DateOfInsert = DateTime.ParseExact("19/11/2018 09:44:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 5 },
               new Ad() { AdID = 14, Title = "Sprzedam iPhone 7", Content = "Sprzedam u¿ywanego iPhone7 za 3tysiece z³", CategoryID = 80, DateOfInsert = DateTime.ParseExact("18/11/2018 10:55:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 5 },
               new Ad() { AdID = 15, Title = "Sprzedam iPhone 6", Content = "Sprzedam u¿ywanego iPhone 6 za 2tysiece z³", CategoryID = 80, DateOfInsert = DateTime.ParseExact("18/11/2018 10:55:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 5 },
               new Ad() { AdID = 16, Title = "Ubranka dla dziecka ", Content = "Sprzedam ubranka dla dziecka rozmiar 86cm", CategoryID = 65, DateOfInsert = DateTime.ParseExact("17/11/2018 14:33:00", "dd/MM/yyyy HH:mm:ss", null), UserID = 1 }
                );

            //context.ForbiddenWords.AddOrUpdate(x => x.ForbiddenWordID,
            //    new ForbiddenWord() { ForbiddenWordID = 1, Name = "sony" },
            //    new ForbiddenWord() { ForbiddenWordID = 2, Name = "linux" }
            //    );


        }
    }
}
