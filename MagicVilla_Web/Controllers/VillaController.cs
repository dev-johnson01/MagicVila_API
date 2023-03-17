﻿using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VilaDTO> list = new();

            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VilaDTO>>(Convert.ToString(response.Result));
            }   
            return View(list);
        }
        public async Task<IActionResult> CreateVilla()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VilaCreateDTO model)
        {
            var response = await _villaService.CreateAsync<APIResponse>(model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateVilla(int villaID)
        {

            var response = await _villaService.GetAsync<APIResponse>(villaID);
            if (response != null && response.IsSuccess)
            {
                VilaDTO model = JsonConvert.DeserializeObject<VilaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VilaUpdateDTO>(model));

            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VilaUpdateDTO model)
        {
            var response = await _villaService.UpdateAsync<APIResponse>(model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int villaID)
        {

            var response = await _villaService.GetAsync<APIResponse>(villaID);
            if (response != null && response.IsSuccess)
            {
                VilaDTO model = JsonConvert.DeserializeObject<VilaDTO>(Convert.ToString(response.Result));
                return View(model);

            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VilaDTO model)
        {
            var response = await _villaService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(model);
        }
    }
}
