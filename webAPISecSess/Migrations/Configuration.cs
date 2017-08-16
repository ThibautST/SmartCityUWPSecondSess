namespace webAPISecSess.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<webAPISecSess.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(webAPISecSess.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "SuperPowerUser",
                Email = "taiseer.joudeh@mymail.com",
                EmailConfirmed = true,
                FirstName = "Taiseer",
                LastName = "Joudeh",
            };

            manager.Create(user, "MySuperP@ssword!");

            Transport transp1 = new Transport()
            {
                Id_Transport = 1,
                TransportName = "Marche",
                Speed = 5,
            };

            Transport transp2 = new Transport()
            {
                Id_Transport = 2,
                TransportName = "Vélo",
                Speed = 15,
            };
            Transport transp3 = new Transport()
            {
                Id_Transport = 3,
                TransportName = "Voiture",
                Speed = 50,
            };

            Category categ1 = new Category()
            {
                Id_Category = 1,
                CategoryName = "Sportive",
                Description = "Dénivellation plus importante"
            };

            Category categ2 = new Category()
            {
                Id_Category = 2,
                CategoryName = "Romantique",
                Description = "Promenade en amoureux. Paisible et idylique pour les couples. Possibilité de passer du temps à deux en toute tranquillité."
            };

            Category categ3 = new Category()
            {
                Id_Category = 3,
                CategoryName = "Découverte",
                Description = "Visite des sites touristiques principaux de la ville."
            };

            Category categ4 = new Category()
            {
                Id_Category = 4,
                CategoryName = "Familiale",
                Description = "Parfait pour passer du temps en famille. Parcours adaptés pour les plus grands comme pour les plus petits."
            };

            Category categ5 = new Category()
            {
                Id_Category = 5,
                CategoryName = "Culturel",
                Description = "Tour des musées Namurois"
            };

            PlaceToEat placetoeat1 = new PlaceToEat()
            {
                Id_PlaceToEat = 1,
                PlaceToEatName = "Les Sens du Goût",
                Latitude = 50.4661309,
                Longitude = 4.849598399999991,
                Address = "Rue des Bas Prés 52, 5000 Namur",
                Id_Photo = "1",
                Price_Min = 15,
                Price_Max = 100
            };

            PlaceToEat placetoeat2 = new PlaceToEat()
            {
                Id_PlaceToEat = 2,
                PlaceToEatName = "Piccolo Sibila",
                Latitude = 50.4644828,
                Longitude = 4.863955099999998,
                Address = "Rue Haute Marcelle 15, 5000 Namur",
                Id_Photo = "2",
                Price_Min = 15,
                Price_Max = 50
            };

            TouristPlace place1 = new TouristPlace()
            {
                Id_TouristPlace = 1,
                Latitude = 50.463385,
                Longitude = 4.8627025000000685,
                TouristPlaceName = "Musée Félicien Rops",
                Address = "Rue Fumal, 12 5000 Namur",
                Description = "L'exposition permanente offre une approche complète de l'oeuvre de Rops dans sa diversité : les débuts dans la satire sociale et la caricature, la lithographie, l'esprit baudelairien, la vie parisienne, l’omniprésence de la femme, la mort, l’érotisme, les liens avec le monde littéraire, les voyages… Outre cette collection permanente, le musée accueille également des expositions temporaires abordant les thèmes du XIXe siècle, d’artistes-graveurs contemporains belges et étrangers, etc. Possibilités de nombreuses animations pédagogiques.",
                Time = 60,
                Id_Photo = "1.jpg",
                Price = 3
            };

            TouristPlace place2 = new TouristPlace()
            {
                Id_TouristPlace = 2,
                Latitude = 50.466604,
                Longitude = 4.8708871000000045,
                TouristPlaceName = "Musée Africain",
                Address = "Rue du 1er Lanciers, 1 5000 Namur",
                Description = "la description du Musée devrait être : Hébergé dans le corps de garde de l’ancienne caserne Léopold (1885), le Musée, unique institution du genre en Wallonie, présente des documents et souvenirs liés à la présence belge en Afrique centrale, essentiellement au Congo. Une bibliothèque de 20.000 livres et périodiques couvre l’histoire, la géographie, la zoologie, la géologie, l’ethnographie, l’économie...",
                Time = 60,
                Id_Photo = "2.jpg",
                Price = 3
            };

            TouristPlace place3 = new TouristPlace()
            {
                Id_TouristPlace = 3,
                Latitude = 50.4444056,
                Longitude = 4.87806599999999,
                TouristPlaceName = "Musée du Génie",
                Address = "Chemin du Masuage 5100 Jambes",
                Description = "Toute l'histoire du génie militaire, de Vauban à nos jours. De nombreux objets, documents, mannequins et maquettes ainsi qu'une exposition d'anciens engins de construction routière, de pontage ou de combat, rappellent le rôle fondamental du génie : « Parfois détruire, souvent construire, toujours servir ».",
                Time = 60,
                Id_Photo = "3.jpg",
                Price = 5
            };

            TouristPlace place4 = new TouristPlace()
            {
                Id_TouristPlace = 4,
                Latitude = 50.4588001,
                Longitude = 4.86218550000001,
                TouristPlaceName = "Citadelle de Namur",
                Address = "Route Merveilleuse 64, 5000 Namur",
                Description = "La citadelle de Namur, en Belgique est divisée en trois parties : Donjon, représentant la partie inférieure, Médiane pour la partie intermédiaire et Terra Nova pour la partie supérieure. Son sommet culmine à 190 m d'altitude.",
                Time = 120,
                Id_Photo = "4.jpg",
                Price = 0
            };

            TouristPlace place5 = new TouristPlace()
            {
                Id_TouristPlace = 5,
                Latitude = 50.4645959,
                Longitude = 4.860694599999988,
                TouristPlaceName = "Cathédrale Saint-Aubain de Namur",
                Address = "Place du Chapitre 4, 5000 Namur",
                Description = "La cathédrale Saint-Aubain de Namur est un édifice religieux catholique qui est l'église épiscopale et siège du diocèse de Namur. Commencée en 1751, son édification se termine en 1767. La consécration a eu lieu le 20 septembre 1772",
                Time = 30,
                Id_Photo = "5.jpg",
                Price = 0
            };

            TouristPlace place6 = new TouristPlace()
            {
                Id_TouristPlace = 6,
                Latitude = 50.5081885,
                Longitude = 4.856186999999977,
                TouristPlaceName = "Fort d'Émines",
                Address = "Rue du Fort d'Emines, 5000 Namur",
                Description = "Le fort d'Emines construit entre les villages d'Émines et de Saint-Marc, est l'un des 9 forts construit entre 1888 et 1892 autour de Namur en Belgique conjointement à ceux de Liège afin de défendre la neutralité du pays contre les velléités françaises (ou allemandes pour Liège) qui étaient susceptibles d'emprunter la vallée de la Meuse pour s'envahir l'un l'autre en bafouant la neutralité Belge au passage. Tous ces forts ont été conçus par le général Henri Alexis Brialmont et mettent en œuvre un béton non armé, matériau assez novateur à l'époque. Il est positionné au nord de la ville et est considéré comme l'un des petits forts de la ceinture namuroise.",
                Time = 60,
                Id_Photo = "6.jpg",
                Price = 0
            };

            TouristPlace place7 = new TouristPlace()
            {
                Id_TouristPlace = 7,
                Latitude = 50.4645552,
                Longitude = 4.856444499999952,
                TouristPlaceName = "Beffroi de Namur",
                Address = "Rue Château des 4 Seigneurs 8, 5020 Namur",
                Description = "C'est une tour en pierre circulaire à l'origine appelée Tour de Saint Jacques et déclarée Patrimoine de l'Humanité par l'Unesco en 1999. Sa construction a commencé en 1388 et son aspect actuel date du XVIème siècle. Après la destruction en 1745 de l'église de Saint Pierre de la citadelle et sa tour en 1841, elle a été transformée en horloge municipale. Elle abrite aujourd'hui des expositions d'art temporaires.",
                Time = 45,
                Id_Photo = "7.jpg",
                Price = 0
            };

            TouristPlace place8 = new TouristPlace()
            {
                Id_TouristPlace = 8,
                Latitude = 50.4638187,
                Longitude = 4.862052500000004,
                TouristPlaceName = "Église Saint-Loup",
                Address = "Rue du Collège, 5000 Namur",
                Description = "Cette église est un des principaux monuments de Namur et un chef-d'oeuvre de style baroque en Belgique, qu'aimait beaucoup Baudelaire. Cette église était le couvent des jésuites, construit entre 1621 et 1645, un bel exemple de l'architecture religieuse du XVIIème. L'intérieur surprend par les magnifiques motifs floraux en marbre.",
                Time = 30,
                Id_Photo = "8.jpg",
                Price = 0
            };

            TouristPlace place9 = new TouristPlace()
            {
                Id_TouristPlace = 9,
                Latitude = 50.4684683,
                Longitude = 4.86253320000003,
                TouristPlaceName = "Namourette",
                Address = "Place de la Station, 5000 Namur",
                Description = "Une Namourette est un petit bateau de transport de passagers qui navigue sur la Meuse et la Sambre, entre Jambes et Salzinnes. Son nom est une référence à sa ville d'adoption, Namur, et à ses grands frères vénitiens.",
                Time = 15,
                Id_Photo = "9.jpg",
                Price = 1
            };

            TouristPlace place10 = new TouristPlace()
            {
                Id_TouristPlace = 10,
                Latitude = 50.46454360000001,
                Longitude = 4.868012399999998,
                TouristPlaceName = "Théâtres royal de Namur",
                Address = "Place du Théâtre 2, 5000 Namur",
                Description = "C'est un bâtiment magnifique qui a été détruit après un incendie et qui a été reconstruit en style italien en 1863. Il est fait en pierre sablonneuse, matérielle très rare à trouver dans la région. A l'intérieur, il dispose de plusieurs moyens techniques les plus modernes. Garouster, un peintre français, a décoré les plafonds de l'entrée de ce théâtre somptueux. Plusieurs bars ont leurs terrasses dans l'entrée, et c'est agréable de prendre un rafraîchissement à leurs portes.",
                Time = 45,
                Id_Photo = "10.jpg",
                Price = 30
            };

            GuidedTour route1 = new GuidedTour()
            {
                Id_GuidedTour = 1,
                GuidedTourName = "Tournée des musées",
                Distance = 15,
                Id_Transport = 3,
                Id_Category = 5,
                Id_PlaceToEat = 1
            };

            GuidedTour route2 = new GuidedTour()
            {
                Id_GuidedTour = 2,
                GuidedTourName = "Au bord de l'eau",
                Distance = 7,
                Id_Transport = 1,
                Id_Category = 2,
                Id_PlaceToEat = 1
            };

            GuidedTour route3 = new GuidedTour()
            {
                Id_GuidedTour = 3,
                GuidedTourName = "L'ascension",
                Distance = 10,
                Id_Transport = 1,
                Id_Category = 1,
                Id_PlaceToEat = 2
            };

            GuidedTour route4 = new GuidedTour()
            {
                Id_GuidedTour = 4,
                GuidedTourName = "La familiale ",
                Distance = 5,
                Id_Transport = 1,
                Id_Category = 4,
                Id_PlaceToEat = 2
            };

            GuidedTour route5 = new GuidedTour()
            {
                Id_GuidedTour = 5,
                GuidedTourName = "Le cycliste ",
                Distance = 20,
                Id_Transport = 2,
                Id_Category = 2,
                Id_PlaceToEat = 2
            };

            PlaceWithOrder compo1 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 1,
                Id_GuidedTour = 1,
                Id_TouristPlace = 1,
                OrderNumber = 1

            };

            PlaceWithOrder compo2 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 2,
                Id_GuidedTour = 1,
                Id_TouristPlace = 2,
                OrderNumber = 2

            };

            PlaceWithOrder compo3 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 3,
                Id_GuidedTour = 1,
                Id_TouristPlace = 3,
                OrderNumber = 3

            };

            PlaceWithOrder compo4 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 4,
                Id_GuidedTour = 2,
                Id_TouristPlace = 9,
                OrderNumber = 1

            };

            PlaceWithOrder compo5 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 5,
                Id_GuidedTour = 2,
                Id_TouristPlace = 10,
                OrderNumber = 2

            };

            PlaceWithOrder compo6 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 6,
                Id_GuidedTour = 2,
                Id_TouristPlace = 8,
                OrderNumber = 3
            };

            PlaceWithOrder compo7 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 7,
                Id_GuidedTour = 2,
                Id_TouristPlace = 7,
                OrderNumber = 4
            };

            PlaceWithOrder compo8 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 8,
                Id_GuidedTour = 2,
                Id_TouristPlace = 5,
                OrderNumber = 5
            };

            PlaceWithOrder compo9 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 9,
                Id_GuidedTour = 3,
                Id_TouristPlace = 10,
                OrderNumber = 1
            };

            PlaceWithOrder compo10 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 10,
                Id_GuidedTour = 3,
                Id_TouristPlace = 6,
                OrderNumber = 2
            };

            PlaceWithOrder compo11 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 11,
                Id_GuidedTour = 3,
                Id_TouristPlace = 5,
                OrderNumber = 3
            };

            PlaceWithOrder compo12 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 12,
                Id_GuidedTour = 3,
                Id_TouristPlace = 4,
                OrderNumber = 4
            };

            PlaceWithOrder compo13 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 13,
                Id_GuidedTour = 4,
                Id_TouristPlace = 10,
                OrderNumber = 1
            };

            PlaceWithOrder compo14 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 14,
                Id_GuidedTour = 4,
                Id_TouristPlace = 8,
                OrderNumber = 2
            };

            PlaceWithOrder compo15 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 15,
                Id_GuidedTour = 4,
                Id_TouristPlace = 7,
                OrderNumber = 3
            };

            PlaceWithOrder compo16 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 16,
                Id_GuidedTour = 5,
                Id_TouristPlace = 4,
                OrderNumber = 1
            };

            PlaceWithOrder compo17 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 17,
                Id_GuidedTour = 5,
                Id_TouristPlace = 5,
                OrderNumber = 2
            };

            PlaceWithOrder compo18 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 18,
                Id_GuidedTour = 5,
                Id_TouristPlace = 6,
                OrderNumber = 3
            };

            PlaceWithOrder compo19 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 19,
                Id_GuidedTour = 5,
                Id_TouristPlace = 7,
                OrderNumber = 4
            };

            PlaceWithOrder compo20 = new PlaceWithOrder()
            {
                Id_PlaceWithOrder = 20,
                Id_GuidedTour = 5,
                Id_TouristPlace = 8,
                OrderNumber = 5
            };

            context.TransportSet.Add(transp1);
            context.TransportSet.Add(transp2);
            context.TransportSet.Add(transp3);

            context.PlacesToEatSet.Add(placetoeat1);
            context.PlacesToEatSet.Add(placetoeat2);

            context.TouristPlaceSet.Add(place1);
            context.TouristPlaceSet.Add(place2);
            context.TouristPlaceSet.Add(place3);
            context.TouristPlaceSet.Add(place4);
            context.TouristPlaceSet.Add(place5);
            context.TouristPlaceSet.Add(place6);
            context.TouristPlaceSet.Add(place7);
            context.TouristPlaceSet.Add(place8);
            context.TouristPlaceSet.Add(place9);
            context.TouristPlaceSet.Add(place10);

            context.CategorySet.Add(categ1);
            context.CategorySet.Add(categ2);
            context.CategorySet.Add(categ3);
            context.CategorySet.Add(categ4);
            context.CategorySet.Add(categ5);

            context.GuidedTourSet.Add(route1);
            context.GuidedTourSet.Add(route2);
            context.GuidedTourSet.Add(route3);
            context.GuidedTourSet.Add(route4);
            context.GuidedTourSet.Add(route5);

            context.PlaceWithOrderSet.Add(compo1);
            context.PlaceWithOrderSet.Add(compo2);
            context.PlaceWithOrderSet.Add(compo3);
            context.PlaceWithOrderSet.Add(compo4);
            context.PlaceWithOrderSet.Add(compo5);
            context.PlaceWithOrderSet.Add(compo6);
            context.PlaceWithOrderSet.Add(compo7);
            context.PlaceWithOrderSet.Add(compo8);
            context.PlaceWithOrderSet.Add(compo9);
            context.PlaceWithOrderSet.Add(compo10);
            context.PlaceWithOrderSet.Add(compo11);
            context.PlaceWithOrderSet.Add(compo12);
            context.PlaceWithOrderSet.Add(compo13);
            context.PlaceWithOrderSet.Add(compo14);
            context.PlaceWithOrderSet.Add(compo15);
            context.PlaceWithOrderSet.Add(compo16);
            context.PlaceWithOrderSet.Add(compo17);
            context.PlaceWithOrderSet.Add(compo18);
            context.PlaceWithOrderSet.Add(compo19);
            context.PlaceWithOrderSet.Add(compo20);

            context.SaveChanges();
        }
    }
}
