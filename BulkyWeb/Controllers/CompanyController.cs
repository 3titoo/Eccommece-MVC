using BulkyWeb.Models;
using BulkyWeb.Repositry;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CompanyController : Controller
    {
        public Irepo<Company> _repo;
        public CompanyController(Irepo<Company> repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var companies = _repo.GetAll();
            return View(companies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(company);
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Error creating company";
            return View(company);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var company = _repo.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
        [HttpPost]
        public IActionResult Edit(int id, Company company)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(company);
                TempData["success"] = "Company updated successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Error updating company";
            return View(company);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var company = _repo.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
        [HttpPost]
        public IActionResult Delete(Company company)
        {
            if (company == null)
            {
                return NotFound();
            }
            _repo.Remove(company.Id);
            TempData["success"] = "Company deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
