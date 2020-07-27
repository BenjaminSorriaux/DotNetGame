using DotNetGame.Entities;
using DotNetGame.Server.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DotNetGame.Server.UniTest
{
    [TestClass]
    public class UnitTest1
    {
        public void Test()
        {
            Skill skill = new Skill { Id = 1, Description = "C#" };

            List<Developer> developers = new List<Developer> { 

                new Developer { Id = 1, DevSkills = new List<Skill> { skill } },
                new Developer { Id = 2 , DevSkills = new List<Skill> { skill } },
                    new Developer { Id = 3 } };
            Player player = new Player();

            player.Developers.AddRange(developers)

        }
        [TestMethod]
        public void TestMethodBuyDev()
        {
            Service service = new Service();
            Player player = new Player();
            Developer developer = new Developer();
            Developer developer1 = new Developer();
            service.BuyDev(developer, player);
            Assert.AreEqual(1, player.Developers.Count); //return 1 because only one developer was added
            service.BuyDev(developer1, player);
            Assert.AreEqual(2, player.Developers.Count); //return 2 because another developer was added
            Assert.AreEqual(2, service.BuyDev(developer, player));
        }

        [TestMethod]
        public void TestMethodFireDev()
        {
            Service service = new Service();
            Player player = new Player();
            Developer developer = new Developer();
            Developer developer1 = new Developer();
            service.BuyDev(developer, player);
            service.BuyDev(developer1, player);
            Assert.AreEqual(1, service.FireDev(developer1, player));
            Assert.AreEqual(false, player.Developers.Contains(developer1));
        }

        [TestMethod]
        public void TestMethodChooseProject()
        {
            Service service = new Service();
            Player player = new Player();
            Project project = new Project();
            Assert.AreEqual(0, player.Projects.Count);
            Assert.AreEqual(1, service.ChooseProject(project, player));
        }

        [TestMethod]
        public void TestMethodCancelProject()
        {
            Player player = new Player();
            Service service = new Service();
            Project project = new Project();
            Project project1 = new Project();
            Assert.AreEqual(1, service.ChooseProject(project, player));
            Assert.AreEqual(2, service.ChooseProject(project1, player));
            Assert.AreEqual(1, service.CancelProject(project, player));
        }

        [TestMethod]

        public void TestMethodAddMoney()
        {
            Service service = new Service();
            Player player = new Player();
            player.Money = 500;
            Assert.AreEqual(800, service.AddMoney(300, player));
        }

        [TestMethod]
        public void TestMethodRemoveMoney()
        {
            Service service = new Service();
            Player player = new Player();
            player.Money = 600;
            Assert.AreEqual(450, service.RemoveMoney(150, player));
        }


        List<Skill> skills = new List<Skill>
        {
            new Skill
            {
                Id = 1,
                Description = "C#",
                Level = 10
            }, new Skill
            {
                Id = 2,
                Description = "PHP",
                Level = 50
            }, new Skill
            {
                Id = 3,
                Description = "SQL",
                Level = 50
            }

        };
        List<Skill> skillsTrain = new List<Skill>
        {
            new Skill
            {
                Id = 1,
                Description = "C#",
                Level = 5
            }, new Skill
            {
                Id = 5,
                Description = "HTML",
                Level = 15
            }, new Skill
            {
                Id = 3,
                Description = "SQL",
                Level = 52
            }

        };

        [TestMethod]
        public void TestMethodImproveDev()
        {
            Service service = new Service();
            Developer dev = new Developer();
            School school = new School();
            Training training = new Training();
            dev.DevSkills = skills;
            training.ImprovedSkills = skillsTrain;
            school.Training = training;
            Assert.AreEqual(1, service.ImproveDev(dev, school));

        }

        [TestMethod]
        public void TestMethodEndProject()
        {
            Service service = new Service();
            Player player = new Player();
            Developer dev = new Developer();
            Project project = new Project();
            player.Money = 1000;
            project.Reward = 250;
            project.Developers.Add(dev);
            project.IsDone = true;
            Assert.AreEqual(1250, service.EndProject(project, player));
            Assert.AreEqual(false, player.Projects.Contains(project));
        }

        [TestMethod]
        public void TestMehodFailProjectOK()
        {
            Player player = new Player();
            Service service = new Service();
            Project project = new Project();
            service.ChooseProject(project, player);
            Assert.AreEqual(true, service.FailProject(project, player));
        }

        [TestMethod]
        public void TestMehodFailProjectKO()
        {
            Player player = new Player();
            Service service = new Service();
            Project project = new Project();
            service.ChooseProject(project, player);
            Assert.AreEqual(false, service.FailProject(project, player));

        }

        [TestMethod]
        public void TestMehodAffectDev()
        {
            Service service = new Service();
            Developer dev = new Developer();
            Project project = new Project();
            Assert.AreEqual(1, service.AffectDev(dev, project));
        }


        //List<Player> turnPlayers = new List<Player>
        //{
        //    new Player
        //    {
        //        Id = 1,
        //        Name = "david"

        //    },
        //    new Player
        //    {
        //        Id = 2,
        //        Name = "hubert"
        //    },
        //     new Player
        //     {
        //        Id = 3,
        //        Name = "frank"
        //    },
        //      new Player
        //    {
        //        Id = 4,
        //        Name = "benoit"
        //    }
        //};

        
        //[TestMethod]
        //public void TestMehodPlayerTurn()
        //{
        //    Service service = new Service();
        //    Game game = new Game();
        //    List<int> ints = new List<int>();
        //    game.ConnectedPlayers = turnPlayers;
        //    Assert.AreEqual(ints, service.PlayerTurn(game));
        //}

        List<Game> games = new List<Game>
        {
             new Game
            {
                NbMaxTurn = 30,
                NbTurn = 30
            },
            new Game
            {
                NbMaxTurn = 45,
                NbTurn = 30
            }
        };

        [TestMethod]
        public void TestMehodGenerateProject()
        {
            Service service = new Service();
            Assert.IsNotNull(service.GenerateProject());
        }

        [TestMethod]

        public void TestMethodGenerateDev()
        {
            Service service = new Service();
            Assert.IsNotNull(service.GenerateDeveloper());
        }

        [TestMethod]
        public void TestMethodGenerateSchool()
        {
            Service service = new Service();
            Assert.IsNotNull(service.GenerateSchool());
        }
        [TestMethod]
        public void TestDevCost()
        {
            Service s = new Service();
            Player p = new Player();
            Developer dev = new Developer();
            dev.Name = "micgi";
            dev.Salary = 10;
            p.Developers.Add(dev);

            dev.Name = "Charly";
            dev.Salary = 10;
            p.Developers.Add(dev);

            dev.Name = "baptiste";
            dev.Salary = 10;
            p.Developers.Add(dev);

            dev.Name = "Benjamin";
            dev.Salary = 10;
            p.Developers.Add(dev);

            Assert.AreEqual(40,s.DevCost(p));
        }
    }
}
