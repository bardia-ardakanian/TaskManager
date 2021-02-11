using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Handler
    {
        Admin admin;
        public Handler(Admin admin)
        {
            this.admin = admin;
        }

        public void Process(String input)
        {
            String[] command = input.Split(' ');
            if (command[0].ToLower() == "exit") System.Environment.Exit(1);
            if (command[0] != "db")
            {
                Console.WriteLine("\"" + command[0] + "\" is not recognized as an internal or external command, " +
                    "operable program or batch file.");
                return;
            }

            switch (command[1])
            {
                case "add":
                    {
                        if (command[2].Equals("-u"))                                                        //add user
                        {
                            if (command.Length == 4)
                            {
                                admin.AddUser(command[3]);
                            }
                            else
                            {
                                Console.WriteLine("\ndb: Invalid input!\n" +
                                                    "usage: \"db add -u <parameter>\"\n");
                            }
                        }
                        else if (command[2].Equals("-t"))
                        {
                            if (command.Length == 4)
                            {
                                admin.AddTask(command[3]);
                            }
                            else
                            {
                                Console.WriteLine("\ndb: Invalid input!\n" +
                                                    "\"usage: db add -t <parameter>\"\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\ndb: wrong usage of \"add\" command. See \"db --help\"\n");
                        }

                        break;
                    }

                case "rm":
                    {
                        if (command[2].Equals("-u"))                                                        //add task
                        {
                            if (command.Length == 4)
                            {
                                admin.RemoveUser(command[3]);
                            }
                            else
                            {
                                Console.WriteLine("\ndb: Invalid input!\n" +
                                                    "usage: \"db rm -u <parameter>\"\n");
                            }
                        }
                        else if (command[2].Equals("-t"))
                        {
                            if (command.Length == 4)
                            {
                                admin.RemoveTask(command[3]);
                            }
                            else
                            {
                                Console.WriteLine("\ndb: Invalid input!\n" +
                                                    "\"usage: db rm -t <parameter>\"\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\ndb: wrong usage of \"rm\" command. See \"db --help\"\n");
                        }

                        break;
                    }

                case "assign":
                    {
                        if (command.Length == 4)
                        {
                            admin.AssignTask(command[2], command[3]);
                        }
                        else
                        {
                            Console.WriteLine("\ndb: Invalid input!\n" +
                                                    "usage: \"db assign <username> <task title>\"\n");
                        }

                        break;
                    }

                case "expel":
                    {
                        if (command.Length == 4)
                        {
                            admin.ExpelTask(command[2], command[3]);
                        }
                        else
                        {
                            Console.WriteLine("\ndb: Invalid input!\n" +
                                                    "usage: \"db expel <username> <task title>\"\n");
                        }

                        break;
                    }

                case "display":
                    {
                        if (command.Length == 3)
                        {
                            if (command[2].Equals("-u"))
                            {
                                admin.DisplayUsers();
                            }
                            else if (command[2].Equals("-t"))
                            {
                                admin.DisplayTasks();
                            }
                            else if (command[2].Equals("-m"))
                            {
                                admin.DisplayMaps();
                            }
                            else
                            {
                                Console.WriteLine("\ndb: nothing specific to clear\n" +
                                                    "hint: maybe you wanted to say 'db display -u'/" +
                                                    "'db display -t'/'db display -m'?\"\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\ndb: wrong usage of \"display\" command. See \"db --help\"\n");
                        }

                        break;
                    }

                case "drop":
                    {
                        if (command.Length == 3)
                        {
                            if (command[2].Equals("-u"))
                            {
                                admin.DropUsers();
                            }
                            else if(command[2].Equals("-t"))
                            {
                                admin.DropTasks();
                            }
                            else if (command[2].Equals("-m"))
                            {
                                admin.DropMaps();
                            }
                            else
                            {
                                Console.WriteLine("\ndb: nothing specific to clear\n" +
                                                    "hint: maybe you wanted to say 'db clear -u'/" +
                                                    "'db clear -t'/'db clear -m'?\"\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\ndb: wrong usage of \"clear\" command. See \"db --help\"\n");
                        }

                        break;
                    }

                case "--help":
                    {
                        Console.WriteLine(Help());
                        break;
                    }
            }

        }

        private String Help()
        {
            return "\nThese are common db commands used in various situations: \n\n" +
            "add a user/task:\n      add -u <username>\n      add -t <title>\n\n" +
            "remove a user/task:\n      rm -u <username>\n      rm -t <title>\n\n" +
            "assign a task:\n      assign <username> <task>\n\n" +
            "expel a task:\n      expel <username> <task>\n\n" +
            "display table:\n      display -u (users)\n      display -t (tasks)\n      display -m (maps)\n\n" +
            "drop table:\n      drop -u (users)\n      drop -t (tasks)\n      drop -m (maps)\n";
        }
    }
}
