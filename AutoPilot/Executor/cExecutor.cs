using AutoPilot.Actions;
using System.Collections.ObjectModel;

namespace AutoPilot.Executor
{
    public class cExecutor
    {
        ObservableCollection<Action> Actions;
        private ExcelHandler lExcelHandler = new ExcelHandler();
        private JsonHandler lJsonHandler = new JsonHandler();

        public async void run(ObservableCollection<FilePaths> pPathsList)
        {
            foreach (var Paths in pPathsList)
            {
                Actions = lJsonHandler.ReadData(Paths.JsonFilePath);

                if (Paths.ExcelFilePath == null)
                {
                    foreach (var action in Actions)
                    {
                        if (action is Delay delayAction)
                        {
                            await delayAction.Execute();
                        }
                        else
                        {
                            action.Execute();
                        }
                    }
                }
                else
                {
                    int usedRows = lExcelHandler.GetNumberOfUsedRows(Paths.ExcelFilePath);
                    for (int i = 1 + 1; i < usedRows + 1; i++)
                    {
                        foreach (var action in Actions)
                        {
                            if (action is Delay delayAction)
                            {
                                await delayAction.Execute();
                            }
                            else if (action is DataInput)
                            {
                                DataInput lDataInput = (DataInput)action;
                                lDataInput.ExcelPath = Paths.ExcelFilePath;
                                lDataInput.Row = i;
                                lDataInput.Execute();
                            }
                            else
                            {
                                action.Execute();
                            }
                        }
                    }

                }
            }
        }

        public async void run(ObservableCollection<Action> pActions)
        {
            foreach (var action in pActions)
            {
                if (action is Delay delayAction)
                {
                    await delayAction.Execute();
                }
                else
                {
                    action.Execute();
                }
            }
        }
    }
}
