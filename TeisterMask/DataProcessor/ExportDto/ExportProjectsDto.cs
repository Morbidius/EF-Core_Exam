using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectsDto
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        public string ProjectName { get; set; }
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ExportTasksDto[] Tasks { get; set; }
    }
    [XmlType("Task")]
    public class ExportTasksDto
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }
}
