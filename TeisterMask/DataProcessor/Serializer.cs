namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .Where(x => x.Tasks.Any())
                .ToArray()
                .Select(x => new ExportProjectsDto
                {
                    ProjectName = x.Name,
                    TasksCount = x.Tasks.Count,
                    HasEndDate = x.DueDate == null ? "No" : "Yes",
                    Tasks = x.Tasks.ToArray().Select(x => new ExportTasksDto
                    {
                        Name = x.Name,
                        Label = x.LabelType.ToString()
                    })
                    .OrderBy(x => x.Name).ToArray()
                })
                .OrderByDescending(x => x.TasksCount).ThenBy(x => x.ProjectName).ToArray();
            var result = ExamHelper.ExamHelper.FromDTOToStringXML<ExportProjectsDto[]>(projects, "Projects");
            return result;
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees
                .Where(x => x.EmployeesTasks.Any(s => s.Task.OpenDate >= date))
                .ToList()
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Where(t => t.Task.OpenDate >= date)
                    .ToList()
                    .OrderByDescending(s => s.Task.DueDate)
                    .ThenBy(s => s.Task.Name)
                    .Select(t => new
                    {
                        TaskName = t.Task.Name,
                        OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString(),
                    }).ToList()
                })
                .OrderByDescending(x => x.Tasks.Count)
                .ThenBy(y => y.Username)
                .Take(10)
                .ToList();

            string result = ExamHelper.ExamHelper.FromDTOToJSONString(employees);
            return result;
        }
    }
}