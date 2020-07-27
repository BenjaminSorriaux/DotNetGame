using DotNetGame.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Linq;
using System.Reflection;
using System.IO;

namespace DotNetGame.Server.Services
{
    public class Service
    {

        public ObservableCollection<Project> Projects { get; set; }
        public ObservableCollection<Player> Players { get; set; }
        public ObservableCollection<Developer> Developers { get; set; }


        #region Les méthodes
        /// <summary>
        /// Add a dev to the company
        /// the dev will be removed from the list
        /// </summary>
        /// <param name="dev"></param>
        /// <returns>number of player's devs</returns>
        public int BuyDev(Developer dev, Player player)
        {

            if (!(player.Developers.Contains(dev)))
            {
                player.Developers.Add(dev);
            }

            return player.Developers.Count;
        }

        /// <summary>
        /// remove a dev
        /// </summary>
        /// <param name="dev"></param>
        /// <returns>number of player's devs</returns>
        public int FireDev(Developer dev, Player player)
        {

            if (player.Developers.Contains(dev))
            {
                player.Developers.Remove(dev);
            }
            return player.Developers.Count;
        }

        /// <summary>
        /// add a chosen project
        /// then removed from the list 
        /// </summary>
        /// <param name="project"></param>
        /// <returns>number of player's projects</returns>
        public int ChooseProject(Project project, Player player)
        {
            player.Projects.Add(project);

            return player.Projects.Count;
        }

        /// <summary>
        /// remove or cancel a project
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Le nombre de projets en cours du joueur</returns>
        public int CancelProject(Project project, Player player)
        {

            if (player.Projects.Contains(project))
            {
                player.Projects.Remove(project);
            }
            return player.Projects.Count;
        }

        /// <summary>
        /// end a project
        /// </summary>
        /// <param name="project"></param>
        /// <returns>the amont of money earned</returns>
        public decimal EndProject(Project project, Player player)
        {

            foreach (Developer developer in project.Developers)
            {
                developer.IsWorking = false;
            }
            if (project.RemainingDays == 0 && project.IsDone)
            {

                AddMoney(project.Reward, player);
            }

            player.Projects.Remove(project);
            return player.Money;
        }

        /// <summary>
        /// add money 
        /// </summary>
        /// <param name="income"></param>
        /// <returns>income</returns>
        public decimal AddMoney(decimal income, Player player) => player.Money += income;

        public decimal RemoveMoney(decimal loss, Player player) => player.Money -= loss;

        /// <summary>
        /// Method to send a dev at a school.
        /// During a fixed number of turns, the dev will be unusable.
        /// He will improve some skills.
        /// </summary>
        /// <param name="developer"></param>
        /// <param name="school"></param>
        /// <returns>Number of skills improved</returns>
        public int ImproveDev(Developer developer, School school)
        {
            int timer = 0;
            int numberSkillImproved = 0;
            developer.IsLearning = true;

            while (school.Training.TimeTraining > timer)
            {
                timer++;
            }

            foreach (var (skill, skillDev) in school.Training.ImprovedSkills.SelectMany(skill => developer.DevSkills.Where(skillDev => skillDev.Id == skill.Id && skill.Level > skillDev.Level).Select(skillDev => (skill, skillDev))))
            {
                skillDev.Level = skill.Level;
                numberSkillImproved++;
            }

            //foreach (Skill skill in school.Training.ImprovedSkills)
            //{
            //    foreach (Skill skillDev in developer.DevSkills)
            //    {
            //        if (skillDev.Id == skill.Id && skill.Level > skillDev.Level)
            //        {
            //            skillDev.Level = skill.Level;
            //            numberSkillImproved++;
            //        }
            //    }

            //}

            developer.IsLearning = false;
            return numberSkillImproved;

        }

        /// <summary>
        /// Method who add a developer to a project
        /// </summary>
        /// <param name="developer"></param>
        /// <param name="project"></param>
        /// <returns>Return the number of developers on a project</returns>
        public int AffectDev(Developer developer, Project project)
        {
            developer.IsWorking = true;
            project.Developers.Add(developer);
            return project.Developers.Count;
        }

        /// <summary>
        /// a project can fail with a random percentage
        /// </summary>
        /// <param name="project"></param>
        /// <returns>success of fail</returns>
        public bool FailProject(Project project, Player player)
        {
            Random rand = new Random();
            bool success = true;
            if (project.RemainingDays >= 0 && rand.Next(0, 100) >= 80)
            {
                success = false;
                CancelProject(project, player);
            }
            return success;
        }

        public int PlayerTurn(Game game, Player player)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            var turn = game.NbTurn;
            bool isPlaying = false;
            game.ConnectedPlayers.Count();
            if (isPlaying == true)
            {
                rand.Next();
                turn++;
            }

            return turn;

            //var turns = game.NbTurns;

            //Random rand = new Random((int)DateTime.Now.Ticks);
            //do
            //{
            //    rand.Next();

            //}
            //while ();

            //return turns;
        }

        private DeserializeData GetDeserializedData()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataFolder, "GameFile.json");
            return JsonConvert.DeserializeObject<DeserializeData>(File.ReadAllText(filePath));
        }

        public List<Project> GenerateProject()
        {
            return GetDeserializedData().Projects;
        }

        public List<Developer> GenerateDeveloper()
        {
            return GetDeserializedData().Developers;
        }

        public List<School> GenerateSchool()
        {
            return GetDeserializedData().Schools;
        }


        public decimal DevCost(Player player) => player.Developers.Sum(p => p.Salary);

        #endregion
    }
}
