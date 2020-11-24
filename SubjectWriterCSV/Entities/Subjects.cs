using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectWriterCSV.Entities
{
    public class Subjects
    {
        public int Id { get; set; }
        [Required]
        public int Credits { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string SchoolRoom { get; set; }
        [Required, MaxLength(50)]
        public string Schedule { get; set; }
        [Required, MaxLength(50)]
        public string Quater { get; set; }
        [Required, MaxLength(1)]
        public string MinLiteralForApprove { get; set; }
    }
}
