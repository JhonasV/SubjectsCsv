using SubjectWriterCSV.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectWriterCSV.ViewModel.Subjects
{
    public class IndexViewModel
    {
        public TaskResult<List<SubjectWriterCSV.Entities.Subjects>> Subjects { get; set; }
    }
}
