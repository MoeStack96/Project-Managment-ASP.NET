using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.Areas.Identity.Data;
using Project_management_system.Models;

namespace Project_management_system.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetProjectList(string? status)
        {
            IQueryable<tbl_Project> query = _context.tbl_Project.OrderByDescending(x => x.Id);

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.ProjectStatus == status);
            }

            var projects = query.ToList();

            if (projects.Any())
            {
                return Json(projects);
            }
            else
            {
                return Json(new { success = false, message = "No projects found." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(tbl_Project model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                                       .ToDictionary(
                                           kvp => kvp.Key,
                                           kvp => kvp.Value.Errors.First().ErrorMessage
                                       );

                return Json(new { success = false, errors });
            }

            _context.tbl_Project.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public JsonResult GetProjectDetails(int id)
        {
            var project = _context.tbl_Project.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return Json(new { success = false, message = "Project not found." });
            }

            return Json(new
            {
                success = true,
                projectId = project.Id,
                projectName = project.Name,
                clientName = project.ClientName,
                description = project.Description,
                startDate = project.StartDate.ToString("yyyy-MM-dd"),
                endDate = project.EndDate.ToString("yyyy-MM-dd"),
                budget = project.Budget,
                ProjectStatus = project.ProjectStatus,
            });
        }

        [HttpPost]
        public JsonResult UpdateProject(tbl_Project model)
        {
            if (ModelState.IsValid)
            {
                var project = _context.tbl_Project.FirstOrDefault(p => p.Id == model.Id);
                if (project == null)
                {
                    return Json(new { success = false, message = "Project not found." });
                }

                project.Name = model.Name;
                project.ClientName = model.ClientName;
                project.Description = model.Description;
                project.StartDate = model.StartDate;
                project.EndDate = model.EndDate;
                project.Budget = model.Budget;
                project.ProjectStatus = model.ProjectStatus;

                _context.SaveChanges();

                return Json(new { success = true });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();

            return Json(new { success = false, message = string.Join(", ", errors) });
        }

        [HttpPost]
        public JsonResult DeleteProject(int id)
        {
            try
            {
                var project = _context.tbl_Project.FirstOrDefault(p => p.Id == id);
                if (project == null)
                {
                    return Json(new { success = false, message = "Project not found." });
                }
                _context.tbl_Project.Remove(project);
                _context.SaveChanges(); 
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred while deleting the project{ex.Message}." });
            }
        }
    }
}
