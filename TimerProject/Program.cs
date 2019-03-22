using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerProject.Models.SQLModels;
using TimerProject.Services;

namespace TimerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLService sQLService = new SQLService();
            sQLService.CreateDataBase();
            sQLService.CreateTables();
            sQLService.InsertProjectData(null);
            foreach (Project item in sQLService.GetProject())
            {
                Console.WriteLine($"Projects: {item.Id}{item.Name}");
                sQLService.InsertTimeWorkData(new Models.SQLModels.TimeWork() { DateStart=DateTime.Now.ToString(),
                                                                                DateEnd =DateTime.Now.AddHours(8).ToString(),
                                                                                IdProject=item.Id});
                sQLService.InsertTimeWorkData(new Models.SQLModels.TimeWork()
                {
                    DateStart = DateTime.Now.AddDays(-1).ToString(),
                    DateEnd = DateTime.Now.AddDays(-1).AddHours(8).ToString(),
                    IdProject = item.Id
                });
                Console.WriteLine(sQLService.GetHours(item));
            }
            
            Console.ReadLine();

           
        }
    }
}
