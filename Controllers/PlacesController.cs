using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DYV.Services.Places;
using DYV.Models;
using DYV.Models.PlacesViewModels;
using DYV.Models.PlaceViewModels;
using DYV.Services.Practices;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DYV.Controllers
{
    [Authorize(Roles = "Client, Subscriber, Admin")]
    public class PlacesController : Controller
    {
        private readonly IPlacesService _placesService;
        private readonly IPracticesService _practicesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlacesController(IPlacesService placesService, IPracticesService practicesService, UserManager<ApplicationUser> userManager)
        {
            _placesService = placesService;
            _practicesService = practicesService;
            _userManager = userManager;
        }

        // GET: /Places/
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _placesService.GetPlacesListViewModelForCurrentUser());
        }

        // GET: /Places/Create/
        [Authorize(Roles = "Client, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        //POST: /Places/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Create([Bind("Name,Address1,City,County,Country,PostCode,Latitude,Longitude")] AddPlaceViewModel placeModel)
        {
            if (ModelState.IsValid)
            {
                await _placesService.AddNewPlace(placeModel);
                return RedirectToAction("Index");
            }

            return View(placeModel);
        }

        // GET : /Places/Share/
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Share()
        {
            return View(await _placesService.GetSharePlacesViewModel());
        }

        //POST : /Places/Share/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Share([Bind("SelectedPracticeId, Practices")] SharePlacesViewModel sharePlacesViewModel)
        {
            if (ModelState.IsValid)
            {
                await _placesService.AddSharePlaces(sharePlacesViewModel);
                return RedirectToAction("Index");
            }

            return View(sharePlacesViewModel);
        }

        //GET : Places/Unshare/Id
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Unshare(int? Id)
        {
            if (!Id.HasValue)
            {
                return NotFound();
            }

            return View(await _placesService.GetUnshareViewModel(Id.Value));
        }

        //POST : /Places/Unshare
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Unshare(int id)
        {
            await _practicesService.Unshare(id);
            return RedirectToAction("Index");
        }

        //GET: /Places/Details/5
        [Authorize(Roles = ("Client, Subscriber, Admin"))]
        [Route("Places/Details/{id?}/{returnController=Places}")]
        public async Task<IActionResult> Details(int? id, string returnController)
        {
            if (!id.HasValue || id == null)
                return NotFound();

            PlaceDetailsViewModel place;

            if (User.IsInRole("Admin"))
                place = await _placesService.GetPlaceByIdForAdmin(id.Value, returnController);
            else
                place = await _placesService.GetPlaceById(id.Value, returnController);

            if (place == null)
                return NotFound();

            return View(place);
        }

        //GET: /Places/Edit/5
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue || id == null)
                return NotFound();

            var place = await _placesService.GetEditPlaceViewModelById(id.Value);

            if (place == null)
                return NotFound();

            return View(place);
        }

        //POST: /Places/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Edit(int? id, EditPlaceViewModel placeModel)
        {
            if (!id.HasValue || id.Value != placeModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _placesService.UpdatePlace(placeModel);
                return RedirectToAction("Details", new { id = placeModel.Id });
            }

            ModelState.AddModelError(string.Empty, "Please fill in required fields.");
            return View(placeModel);
        }

        //GET: /Places/Delete/5
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            return View(await _placesService.GetDeletePlaceViewModelById(id.Value));
        }

        //POST: /Places/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client, Admin")]
        public async Task<IActionResult> Delete(int? id, bool notUsed)
        {
            if (!id.HasValue)
                return NotFound();
                                    
            await _placesService.DeletePlace(id.Value);

            return RedirectToAction("Index");
        }
    }
}
