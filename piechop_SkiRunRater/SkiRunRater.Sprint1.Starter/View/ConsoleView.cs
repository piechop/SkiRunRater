using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    public static class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        /// <summary>
        /// method to display the manager menu and get the user's choice
        /// </summary>
        /// <returns></returns>
        public static AppEnum.ManagerAction GetUserActionChoice()
        {
            AppEnum.ManagerAction userActionChoice = AppEnum.ManagerAction.None;
            //
            // set a string variable with a length equal to the horizontal margin and filled with spaces
            //
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            //
            // set up display area
            //
            DisplayReset();

            //
            // display the menu
            //
            DisplayMessage("Ski Manager Menu");
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "1. Display All Ski Runs Information" + Environment.NewLine +
                leftTab + "2. Display a Ski Run Detail" + Environment.NewLine +
                leftTab + "3. Delete a Ski Run" + Environment.NewLine +
                leftTab + "4. Add a Ski Run" + Environment.NewLine +
                leftTab + "5. Update a Ski Run" + Environment.NewLine +
                leftTab + "6. Query Ski Runs by Vertical" + Environment.NewLine +
                leftTab + "E. Exit" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number/letter for the menu choice.");
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userActionChoice = AppEnum.ManagerAction.ListAllSkiRuns;
                    break;
                case '2':
                    userActionChoice = AppEnum.ManagerAction.DisplaySkiRunDetail;
                    break;
                case '3':
                    userActionChoice = AppEnum.ManagerAction.DeleteSkiRun;
                    break;
                case '4':
                    userActionChoice = AppEnum.ManagerAction.AddSkiRun;
                    break;
                case '5':
                    userActionChoice = AppEnum.ManagerAction.UpdateSkiRun;
                    break;
                case '6':
                    userActionChoice = AppEnum.ManagerAction.QuerySkiRunsByVertical;
                    break;
                case 'E':
                case 'e':
                    userActionChoice = AppEnum.ManagerAction.Quit;
                    break;
                default:
                    Console.WriteLine(
                        "It appears you have selected an incorrect choice." + Environment.NewLine +
                        "Press any key to try again or the ESC key to exit.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userActionChoice = AppEnum.ManagerAction.Quit;
                    }
                    break;
            }

            return userActionChoice;
        }

        /// <summary>
        /// method to display all ski run info
        /// </summary>
        public static void DisplayAllSkiRuns(List<SkiRun> skiRuns)
        {
            DisplayReset();

            DisplayMessage("All of the existing ski runs are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));
            //columnHeader.Append("Vertical in Feet".PadRight(5));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in skiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));
                //skiRunInfo.Append(skiRun.Vertical.ToString().PadRight(5));

                DisplayMessage(skiRunInfo.ToString());
            }
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }


        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            DisplayReset();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }

        public static void DisplaySkiRunDetail(List<SkiRun> runs)
        {
            DisplayAllSkiRuns(runs);
            Console.WriteLine("\nEnter the ski run ID for more details: \n");
            int ID;
            if (int.TryParse(Console.ReadLine(), out ID))
            {
                SkiRun run = CheckRunExists(ID, runs);

                if (run != null)
                {
                    DisplayReset();
                    DisplayMessage("Ski Run Detail");
                    Console.WriteLine("\n\tID: " + run.ID);
                    Console.WriteLine("\tSki Run Name: " + run.Name);
                    Console.WriteLine("\tVertical (feet): " + run.Vertical);
                    DisplayContinuePrompt();
                }
                else
                {
                    DisplaySkiRunNotFound(ID.ToString(), AppEnum.ManagerAction.None, runs);
                }

            }
            else
            {
                DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
            }
        }

        public static void DisplayVerticalQuery(List<SkiRun> runs)
        {
            int minVertical;
            DisplayReset();
            Console.WriteLine("Enter the minimum vertical:\n");
            if (int.TryParse(Console.ReadLine(), out minVertical))
            {
                int maxVertical;
                DisplayReset();
                Console.WriteLine("Enter the maximum vertical:\n");
                if (int.TryParse(Console.ReadLine(), out maxVertical))
                {
                    DisplayReset();
                    Console.WriteLine("Ski Runs between " + minVertical + " and " + maxVertical + " feet:\n");
                    Console.WriteLine("ID\tSki Run");
                    foreach(SkiRun run in runs)
                    {
                        if(run.Vertical>=minVertical&&run.Vertical<=maxVertical)
                        {
                            Console.WriteLine(run.ID + "\t" + run.Name);
                        }
                    }
                    DisplayContinuePrompt();
                }
                else
                {
                    DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
                }
            }
            else
            {
                DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
            }
        }

        public static bool DisplayClosingValidation(List<SkiRun> runs)
        {
            bool isActive = true;
            DisplayReset();
            Console.WriteLine("Are you sure you want to exit? (y/n)");
            string input = Console.ReadLine();
            if (getYesValidation(input, AppEnum.ManagerAction.None,runs))
            {
                isActive = false;
            }

            return isActive;
        }

        private static void DisplayInvalidInput(AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("That is not a valid input. Please try again.");
            DisplayContinuePrompt();
            //RedirectMenu(redirect,runs);
        }

        public static SkiRun DisplayAddRecord(List<SkiRun> runs)
        {
            SkiRun record = new SkiRun();
            
            if(DisplayEnterID(record, AppEnum.ManagerAction.None,runs))
            {
                if(DisplayEnterName(record, AppEnum.ManagerAction.AddSkiRun, runs))
                {
                    if(DisplayEnterVertical(record, AppEnum.ManagerAction.AddSkiRun, runs))
                    {
                        if (ValidateNewRecord(record, AppEnum.ManagerAction.AddSkiRun, runs))
                        {
                            //runs.Add(record);
                            //DisplayAllSkiRuns(runs);
                            //DisplayContinuePrompt();
                        }
                        else
                        {
                            record = null;
                        }
                    }
                    else
                    {
                        record = null;
                    }
                }
                else
                {
                    record = null;
                }
            }
            else
            {
                record = null;
            }

            return record;
        }

        private static bool DisplayEnterName(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            bool valid = false;
            Console.WriteLine("Enter the Ski Run's name:");
            string name = Console.ReadLine();

            if (name.Length == 0)
            {
                DisplayNameError(redirect,runs);
            }
            else
            {
                record.Name = name;
                valid = true;
            }

            return valid;
        }

        private static void DisplayNameError(AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("Name is too short. Please try again.");
            DisplayContinuePrompt();
            //RedirectMenu(redirect,runs);
        }

        private static SkiRun IDRecordExists(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("ID number "+record.ID + " already exists.\nWould you like to modify this record? (y/n)");

            if (getYesValidation(Console.ReadLine(), redirect,runs))
            {
                DisplayUpdateRecordDetails(record, redirect,runs);
            }
            return record;
        }

        private static bool DisplayEnterID(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            bool valid = false;
            Console.WriteLine("Enter the ID of the Ski Run:");
            int ID;
            if (int.TryParse(Console.ReadLine(), out ID))
            {
                SkiRun run = CheckRunExists(ID,runs);

                if (run == null)
                {
                    record.ID = ID;
                    valid = true;
                }
                else
                {
                    record = IDRecordExists(run, redirect,runs);
                }
                
            }
            else
            {
                DisplayInvalidInput(redirect,runs);
            }
            return valid;
        }

        private static SkiRun CheckRunExists(int ID, List<SkiRun> runs)
        {
            SkiRun run = null;

            foreach(SkiRun record in runs)
            {
                if(record.ID==ID)
                {
                    run = record;
                    break;
                }
            }

            return run;
        }

        private static bool DisplayEnterVertical(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            bool valid = false;
            Console.WriteLine("Enter " + record.Name + "'s vertical in feet:");
            int vertical;
            if (int.TryParse(Console.ReadLine(), out vertical))
            {
                record.Vertical = vertical;
                valid = true;
            }
            else
            {
                DisplayInvalidInput(redirect,runs);
            }
            return valid;
        }

        private static bool ValidateNewRecord(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            bool isValid = false;

            DisplayReset();
            Console.WriteLine("Is this correct? (y/n)");
            Console.WriteLine("ID: " + record.ID);
            Console.WriteLine("Name: " + record.Name);
            Console.WriteLine("Vertical: " + record.Vertical);

            string input = Console.ReadLine();
            if (getYesValidation(input, redirect,runs))
            {
                isValid = true;
            }
            return isValid;
        }

        private static bool getYesValidation(string input, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            bool isYes = false;
            switch (input)
            {
                case "y":
                    isYes = true;
                    break;
                case "n":
                    //RedirectMenu(AppEnum.ManagerAction.None,runs);
                    break;
                default:
                    DisplayInvalidInput(redirect,runs);
                    break;
            }
            return isYes;
        }

        public static int DisplayDeleteRecord(List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("Enter the ID of the record you wish to delete.");
            int ID = -1;
            if (int.TryParse(Console.ReadLine(), out ID))
            {
                SkiRun record = CheckRunExists(ID,runs);

                if (record != null)
                {
                    ID = DisplayDeleteValidation(record, runs);
                }
                else
                {
                    DisplaySkiRunNotFound(ID.ToString(), AppEnum.ManagerAction.DeleteSkiRun, runs);
                    ID = -1;
                }
            }
            else
            {
                DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
            }
            return ID;
        }

        private static int DisplayDeleteValidation(SkiRun record,List<SkiRun> runs)
        {
            int ID = -1;
            DisplayReset();
            Console.WriteLine("Delete this record? (y/n)");
            Console.WriteLine("ID: " + record.ID);
            Console.WriteLine("Ski Run: " + record.Name);
            Console.WriteLine("Vertical: " + record.Vertical);

            if (getYesValidation(Console.ReadLine(), AppEnum.ManagerAction.DeleteSkiRun,runs))
            {
                ID = record.ID;
                //runs.Remove(record);
                DisplayDeleteSuccess(runs);
            }

            return ID;
        }

        private static void DisplayDeleteSuccess(List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("Deletion successful.");
            DisplayContinuePrompt();
            //RedirectMenu(AppEnum.ManagerAction.None,runs);
        }

        public static SkiRun DisplayUpdateRecord(List<SkiRun> runs)
        {
            SkiRun record = null;
            DisplayReset();
            Console.WriteLine("Whose record would you like to update?");
            int ID;
            if (int.TryParse(Console.ReadLine(), out ID))
            {
                record = CheckRunExists(ID,runs);

                if (record != null)
                {
                    DisplayUpdateRecordDetails(record, AppEnum.ManagerAction.UpdateSkiRun, runs);
                }
                else
                {
                    DisplaySkiRunNotFound(ID.ToString(), AppEnum.ManagerAction.UpdateSkiRun, runs);
                }
            }
            else
            {
                DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
            }
            return record;
        }

        private static void DisplayUpdateRecordDetails(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();

            Console.WriteLine("Updating Record:");
            Console.WriteLine("ID: "+record.ID);
            Console.WriteLine("Name: "+record.Name);
            Console.WriteLine("Vertical: "+record.Vertical);

            Console.WriteLine("\nWhat would you like to update?");
            //Console.WriteLine("1. ID");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Vertical");

            int userChoice;

            if (int.TryParse(Console.ReadLine(), out userChoice))
            {
                switch(userChoice)
                {
                    //case 1:
                        //UpdateID(record, AppEnum.ManagerAction.UpdateSkiRun,runs);
                        //break;
                    case 1:
                        UpdateName(record, AppEnum.ManagerAction.UpdateSkiRun, runs);
                        break;
                    case 2:
                        UpdateVertical(record, AppEnum.ManagerAction.UpdateSkiRun, runs); 
                        break;
                    default:
                        DisplayInvalidInput(AppEnum.ManagerAction.UpdateSkiRun, runs);
                        break;
                }
            }
            else
            {
                DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
            }
        }

        private static void UpdateID(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            int oldID = record.ID;
            DisplayEnterID(record, redirect,runs);

            if (ValidateNewRecord(record, redirect,runs))
            {
                //DisplayAllSkiRuns(runs);
                //DisplayContinuePrompt();
            }
            else
            {
                record.ID = oldID;
            }
        }

        private static void UpdateName(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            string oldName = record.Name;
            DisplayEnterName(record, redirect,runs);

            if (ValidateNewRecord(record, redirect,runs))
            {
                //DisplayAllSkiRuns(runs);
                //DisplayContinuePrompt();
            }
            else
            {
                record.Name = oldName;
            }
        }

        private static void UpdateVertical(SkiRun record, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            int oldVertical = record.Vertical;
            DisplayEnterVertical(record, redirect,runs);

            if (ValidateNewRecord(record, redirect,runs))
            {
                //DisplayAllSkiRuns(runs);
                //DisplayContinuePrompt();
            }
            else
            {
                record.Vertical = oldVertical;
            }
        }

        private static SkiRun CheckRunRepeat(int ID,List<SkiRun> runs)
        {
            SkiRun record = null;
            foreach (SkiRun run in runs)
            {
                if (run.ID == ID)
                {
                    record = run;
                    break;
                }
            }

            return record;
        }

        private static void DisplaySkiRunNotFound(string name, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine(name + " was not found. Please try a different Ski Run.");
            DisplayContinuePrompt();
            //RedirectMenu(redirect,runs);
        }

        private static void DisplayUnrecognizedKeyMenu(AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("That is not a recognized menu. Please try again.");
            DisplayContinuePrompt();
            //RedirectMenu(redirect,runs);
        }

        //private static void RedirectMenu(AppEnum.ManagerAction redirect, List<SkiRun> runs)
        //{
        //    switch (redirect)
        //    {
        //        case AppEnum.ManagerAction.ListAllSkiRuns:
        //            DisplayAllSkiRuns(runs);
        //            break;
        //        case AppEnum.ManagerAction.AddSkiRun:
        //            DisplayAddRecord(runs);
        //            break;
        //        case AppEnum.ManagerAction.DeleteSkiRun:
        //            DisplayDeleteRecord(runs);
        //            break;
        //        case AppEnum.ManagerAction.UpdateSkiRun:
        //            DisplayUpdateRecord(runs);
        //            break;
        //        case AppEnum.ManagerAction.Quit:
        //            DisplayClosingValidation(runs);
        //            break;
        //        case AppEnum.ManagerAction.None:
        //            GetUserActionChoice();
        //            break;
        //        default:
        //            DisplayUnrecognizedKeyMenu(redirect, runs);
        //            break;
        //    }
        //}


        #endregion
    }
}
