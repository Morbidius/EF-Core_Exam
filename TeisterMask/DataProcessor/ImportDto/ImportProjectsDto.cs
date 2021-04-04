using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportProjectsDto
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
       
        [Required]
        public string OpenDate { get; set; }
        
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public List<ImportProjectTasksDto> Tasks { get; set; }
    }

    [XmlType("Task")]
    public class ImportProjectTasksDto
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        public string OpenDate { get; set; }
        
        [Required]
        public string DueDate { get; set; }

        [DataType(nameof(ExecutionType))]
        [Required]
        public int ExecutionType { get; set; }

        [DataType(nameof(LabelType))]
        [Required]
        public int LabelType { get; set; }
    }

}
