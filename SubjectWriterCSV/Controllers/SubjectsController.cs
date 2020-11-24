using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubjectWriterCSV.Entities;
using SubjectWriterCSV.Services;
using SubjectWriterCSV.ViewModel.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectWriterCSV.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectsRepository _subjectsRepository;

        public SubjectsController(ISubjectsRepository subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Subjects = await _subjectsRepository.GetAllAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            Response.Headers.Add("Content-Disposition", $"attachment; filename=Materias-{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}.csv");
            var export = await _subjectsRepository.ExportAllAsync();

            return File(export.Data, "text/csv");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddViewModel();

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Subjects subject)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var model = new AddViewModel();
             
            await _subjectsRepository.AddAsync(subject);

            return RedirectToAction(nameof(this.Index));
        }
    }
}
