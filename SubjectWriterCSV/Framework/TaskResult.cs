using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectWriterCSV.Framework
{
    public class TaskResult
    {
        private bool _success { get; set; }
        public bool Success { get { return this._success; } }

        private List<string> _messages { get; set; }
        public string Messages { get { return String.Join(",", _messages); } }


        public TaskResult()
        {
            this._success = true;
            this._messages = new List<string>();
        }

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }

        public void AddErrorMessage(string message)
        {
            _success = false;
            _messages.Add(message);
        }
    }

    public class TaskResult<T> : TaskResult
    {
        public T Data { get; set; }
    }
}
