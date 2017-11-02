using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            SkiRunRepository skiRunRepository = new SkiRunRepository();

            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;
                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            ConsoleView.DisplayAllSkiRuns(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            ConsoleView.DisplaySkiRunDetail(skiRuns);
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            ConsoleView.DisplayDeleteRecord(skiRuns);
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            ConsoleView.DisplayAddRecord(skiRuns);
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            ConsoleView.DisplayUpdateRecord(skiRuns);
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            ConsoleView.DisplayVerticalQuery(skiRuns);
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = ConsoleView.DisplayClosingValidation(skiRuns);
                            break;
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
