using Microsoft.EntityFrameworkCore;
using SubjectWriterCSV.Data;
using SubjectWriterCSV.Entities;
using SubjectWriterCSV.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectWriterCSV.Services
{

    public interface ISubjectsRepository
    {
        Task<TaskResult<List<Subjects>>> GetAllAsync();
        Task<TaskResult<bool>> AddAsync(Subjects subject);
        Task<TaskResult<Byte[]>> ExportAllAsync();
    }

    public class SubjectsRepository : ISubjectsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubjectsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<TaskResult<bool>> AddAsync(Subjects subject)
        {
            var result = new TaskResult<bool>();

            await _applicationDbContext.Subjects.AddAsync(subject);

            result.Data = await _applicationDbContext.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<TaskResult<byte[]>> ExportAllAsync()
        {
            var result = new TaskResult<byte[]>();

            var sb = new StringBuilder();

            var subjects = await this.GetAllAsync();
            sb.AppendFormat($"Id, Materia, Creditos, Cuatrimestre, Aula, Horario, Nota Minima para aprobar {Environment.NewLine}");
            foreach (Subjects item in subjects.Data)
            {
                sb.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6} {7}", item.Id, item.Name, item.Credits, item.Quater, item.SchoolRoom, item.Schedule, item.MinLiteralForApprove, Environment.NewLine);
            }

            result.Data = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return result;
        }

        public async Task<TaskResult<List<Subjects>>> GetAllAsync()
        {
            var result = new TaskResult<List<Subjects>>();

            result.Data = await _applicationDbContext.Subjects.ToListAsync();

            return result;
        }
    }
}
