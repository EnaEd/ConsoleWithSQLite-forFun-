using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel.DataAnnotations;
using SQLite;
using MaxLengthAttribute = SQLite.MaxLengthAttribute;

namespace TimerProject.Models.SQLModels
{
    
    public class Project
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        [MaxLength(30), NotNull]
        public string Name { get; set; }

        
    }
}
