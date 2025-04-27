using System;
using System.Threading.Tasks;
using AutoMapper;
using CampRating.Common;
using CampRating.Services;
using CampRating.Services.Interfaces;
using CampRating.Web.ViewModels.Camp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampRating.Web.Controllers;

public class CampController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ICampService _campService;
    private readonly IReviewService _reviewService;

    public CampController(IMapper mapper, ICampService campService,  IReviewService reviewService)
    {
        this._mapper = mapper;
        this._campService = campService;
        this._reviewService = reviewService;
    }

    [HttpGet]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public IActionResult Create()
    {
        return View(new CampFormModel());
    }

    [HttpPost]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public async Task<IActionResult> Create(CampFormModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await this._campService.CreateAsync(model);
            return RedirectToAction("All", "Camp");
        }
        catch (Exception ex)
        {
            this.ModelState.AddModelError("", $"Something went wrong: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult All()
    {
        var viewModel = new AllCampsViewModel
        {
            Camps = this._campService.GetAll<CampInListViewModel>()
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var campViewModel = this._campService.GetById<CampDetailsViewModel>(id);

        if (campViewModel == null)
        {
            return NotFound();
        }

        campViewModel.AverageRating = await this._reviewService.GetAverageRatingByCampIdAsync(id);
        campViewModel.ReviewsCount = await this._reviewService.GetReviewsCountByCampIdAsync(id);

        return View(campViewModel);
    }

    [HttpGet]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public IActionResult Edit(string id)
    {
        var campModel = this._campService.GetById<CampFormModel>(id);
    
        if (campModel == null)
        {
            return NotFound();
        }

        ViewData["CampId"] = id;
    
        return View(campModel);
    }

    [HttpPost]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public async Task<IActionResult> Edit(string id, CampFormModel model)
    {
    
        if (!this.ModelState.IsValid)
        {
            ViewData["CampId"] = id;
            return View(model);
        }

        try
        {
            await this._campService.UpdateAsync(model, id);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            this.ModelState.AddModelError("", $"Something went wrong: {ex.Message}");
            ViewData["CampId"] = id;
            return View(model);
        }
    }
    [HttpPost]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }

        try
        {
            await this._campService.DeleteAsync(id);
            return RedirectToAction(nameof(All));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error deleting camp: {ex.Message}";
            return RedirectToAction(nameof(All));
        }
    }
}