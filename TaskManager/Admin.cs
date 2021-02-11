using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Admin
    {
        public void AddUser(string username)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                if (context.Users.Any(e => e.Username == username))
                {
                    Console.WriteLine("\nfatal: " + username + " already exist\n");
                    return;
                }

                var user = new Domain.User()
                {
                    Username = username
                };

                context.Users.Add(user);
                context.SaveChanges();

                Console.WriteLine("\nuser added successfully\n");
            }
        }

        public void RemoveUser(string username)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                //check if user is present
                Domain.User user = GetUser(username);
                if (user == null)
                {
                    Console.WriteLine("\nfatal: " + username + " not found!\n");
                    return;
                }

                TurnOffValidation();
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Deleted;

                //remove assigned tasks
                while (context.Maps.Any(e => e.ID == user.ID))
                {
                    Domain.Map assigned = context.Maps.Single(e => e.ID == user.ID);
                    context.Maps.Attach(assigned);
                    context.Entry(assigned).State = EntityState.Deleted;
                    context.SaveChanges();
                }

                TurnOnValidation();
                context.SaveChanges();

                Console.WriteLine("\nuser removed successfully\n");
            }
        }

        public void AddTask(string title)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                if (context.Tasks.Any(e => e.Title == title))
                {
                    Console.WriteLine("\nfatal: " + title + " already exist\n");
                    return;
                }

                DateTime localDate = DateTime.Now;
                var task = new Domain.Task()
                {
                    Title = title,
                    GenerationTimeStamp = localDate
                };

                context.Tasks.Add(task);
                context.SaveChanges();

                Console.WriteLine("\ntask added successfully\n");
            }
        }

        public void RemoveTask(string title)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                //check if task is present
                Domain.Task task = GetTask(title);
                if (task == null)
                {
                    Console.WriteLine("\nfatal: " + title + " not found!\n");
                    return;
                }

                TurnOffValidation();
                context.Tasks.Attach(task);
                context.Entry(task).State = EntityState.Deleted;

                //remove assigned tasks
                while (context.Maps.Any(e => e.Title == title))
                {
                    Domain.Map assigned = context.Maps.Single(e => e.Title == title);
                    context.Maps.Attach(assigned);
                    context.Entry(assigned).State = EntityState.Deleted;
                    context.SaveChanges();
                }

                TurnOnValidation();
                context.SaveChanges();

                Console.WriteLine("\ntask removed successfully\n");
            }
        }

        public void AssignTask(string username, string title)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                Domain.User user = GetUser(username);
                Domain.Task task = GetTask(title);
                //if either task or user arent present do nothing
                if (user == null || task == null) {
                    Console.WriteLine("\nfatal: either user or task is not present\n");
                    return;
                }

                var Assign = new Domain.Map()
                {
                    ID = user.ID,
                    Title = task.Title
                };

                //if task has already been assigned
                if (context.Maps.Any(e => e.ID == Assign.ID && e.Title == title)) return;
                context.Maps.Add(Assign);
                context.SaveChanges();

                Console.WriteLine("\ntask assigned successfully\n");
            }
        }

        public void ExpelTask(string username, string title)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                Domain.Map assigned = GetAssignedTask(username, title);
                if (assigned == null)
                {
                    Console.WriteLine("\nfatal: map{" + username + ":" + title + "} not found!\n");
                    return;
                }

                TurnOffValidation();
                context.Maps.Attach(assigned);
                context.Entry(assigned).State = EntityState.Deleted;
                TurnOnValidation();
                context.SaveChanges();

                Console.WriteLine("\nassigned task expelled successfully\n");
            }
        }

        public void DisplayUsers()
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                Console.WriteLine("\n+-------------------------------------------------------------------+" +
                                  "\n|                              USERS                                |" + 
                                  "\n+-------------------------------------------------------------------+" +
                                  "\n|            USERNAME            |                ID                |" +
                                  "\n+-------------------------------------------------------------------+");
                                
                foreach (Domain.User user in context.Users)
                {
                    String data = String.Format("| {0} | {1,-32} |", user.Username.Substring(0, 30), user.ID.ToString());
                    Console.WriteLine(data + 
                        "\n+-------------------------------------------------------------------+");
                }

                Console.WriteLine();
            }
        }

        public void DisplayTasks()
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                Console.WriteLine("\n+-------------------------------------------------------------------+" +
                                  "\n|                              TASKS                                |" + 
                                  "\n+-------------------------------------------------------------------+" +
                                  "\n|             TITLE              |               DATE               |" +
                                  "\n+-------------------------------------------------------------------+");

                foreach (Domain.Task task in context.Tasks)
                {
                    String data = String.Format("| {0} | {1,-32} |", task.Title.Substring(0, 30), task.GenerationTimeStamp.ToString());
                    Console.WriteLine(data +
                        "\n+-------------------------------------------------------------------+");
                }

                Console.WriteLine();
            }
        }

        public void DisplayMaps()
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                Console.WriteLine("\n+-------------------------------------------------------------------+" +
                                  "\n|                              MAPS                                 |" +
                                  "\n+-------------------------------------------------------------------+" +
                                  "\n|             TITLE              |                ID                |" +
                                  "\n+-------------------------------------------------------------------+");

                foreach (Domain.Map map in context.Maps)
                {
                    String data = String.Format("| {0} | {1,-32} |", map.Title.Substring(0, 30), map.ID.ToString());
                    Console.WriteLine(data +
                        "\n+-------------------------------------------------------------------+");
                }

                Console.WriteLine();
            }
        }

        public void DropUsers()
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [USER]");
                context.SaveChanges();

                Console.WriteLine("\nusers cleared successfully\n");
            }
        }

        public void DropTasks()
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [TASK]");
                context.SaveChanges();

                Console.WriteLine("\ntasks cleared successfully\n");
            }
        }

        public void DropMaps()
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Map]");
                context.SaveChanges();

                Console.WriteLine("\nmaps cleared successfully\n");
            }
        }

        private Domain.User GetUser(string username)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                //check if user exists
                if (!context.Users.Any(e => e.Username == username)) return null;

                return context.Users.Single(e => e.Username == username);
            }
        }

        private Domain.Task GetTask(string title)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                //check if task exists
                if (!context.Tasks.Any(e => e.Title == title)) return null;
                return context.Tasks.Single(e => e.Title == title);
            }
        }

        private Domain.Map GetAssignedTask(string username, string title)
        {
            using (var context = new Domain.TaskManagerDbContext())
            {
                Domain.User user = GetUser(username);
                Domain.Task task = GetTask(title);

                if (user == null || task == null) return null;
                else return context.Maps.Single(e => e.ID == user.ID && e.Title == task.Title);
            }
        }

        private void TurnOffValidation()
        {
            using (var localDb = new Domain.TaskManagerDbContext())
            {
                bool oldValidateOnSaveEnabled = localDb.Configuration.ValidateOnSaveEnabled;

                try
                {
                    localDb.Configuration.ValidateOnSaveEnabled = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void TurnOnValidation()
        {
            using (var localDb = new Domain.TaskManagerDbContext())
            {
                bool oldValidateOnSaveEnabled = localDb.Configuration.ValidateOnSaveEnabled;

                try
                {
                    localDb.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
