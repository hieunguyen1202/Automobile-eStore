using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutomobileLibrary.DataAccess;
using AutomobileLibrary.Repository;
namespace AutomobileWebApp.Controllers
{
    [Route("Cars")]
    public class CarsController : Controller
    {
        ICarRepository carRepository = null;
        public CarsController() => carRepository = new CarRepository();
        // GET: CarsController
        [Route("Index")]
        public ActionResult Index()
        {
            var carList = carRepository.GetCars();
            return View(carList);
        }
        [Route("Details")]
        // GET: CarsController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carRepository.GetCarByID(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            TempData["carId"] = id;

            return View(car);
        }

        [Route("Create")]
        // GET: CarsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carRepository.InsertCar(car);
                    ViewBag.Message = "Add new a car successfully";
                }
                if (ViewBag.Message != null)
                {
                    TempData["SuccessMessage"] = ViewBag.Message;
                }
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(car);
        }
        [HttpGet]
        [Route("Edit/{id}")]
        // GET: CarsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carRepository.GetCarByID(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                if (id != car.CarId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    carRepository.UpdateCar(car);
                    ViewBag.Message = "Update a car successfully";
                }

                if (ViewBag.Message != null)
                {
                    TempData["SuccessMessage"] = ViewBag.Message;
                }

                return RedirectToAction(nameof(Edit));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(id);
            }
        }
        [Route("Delete/{id}")]
        // GET: CarsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carRepository.GetCarByID(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                carRepository.DeleteCar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
