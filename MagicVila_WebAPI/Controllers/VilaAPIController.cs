using MagicVila_WebAPI.Models;
using MagicVila_WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MagicVila_WebAPI.Repository.IRepository;
using System.Net;

namespace MagicVila_WebAPI.Controllers
{
    
    [Route("api/vilaAPI")]
    [ApiController]
    public class VilaAPIController : ControllerBase
    {
        protected APIResponse _response;     
        private readonly IMapper _mapper;
        private readonly IVilaRepository _dbVila;

        public VilaAPIController(IVilaRepository dbVila, IMapper mapper)
        {
            _dbVila = dbVila;
            _mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable <APIResponse>>> GetVilas()
        {
            try
            {
                IEnumerable<Vila> vilaList = await _dbVila.GetAllAsync();
                _response.Result = _mapper.Map<List<VilaDTO>>(vilaList);
                _response.StatusCodes = HttpStatusCode.OK;
                
            }

            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages= new List<string> { ex.Message };
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name ="GetVila")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVila(int id)
        {
            try
            {
                if (id == 0)
                {

                    return BadRequest();
                }
                var vila = await _dbVila.GetAsync(u => u.Id == id);
                if (vila == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VilaDTO>(vila);
                _response.StatusCodes = HttpStatusCode.OK;
                
            }

            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVila([FromBody] VilaCreateDTO createDTO)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            try
            {
                if (await _dbVila.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already exist");

                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return NotFound();

                }
                /*if(vilaDTO.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }*/
                Vila vila = _mapper.Map<Vila>(createDTO);

                await _dbVila.CreateAsync(vila);

                _response.Result = vila;
                _response.StatusCodes = HttpStatusCode.Created;
                await _dbVila.SaveAsync();
                return CreatedAtRoute("GetVila", new { id = vila.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }


            return _response;
           
        }

        [HttpDelete("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVila(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var vila = await _dbVila.GetAsync(u => u.Id == id);
                if (vila == null)
                {
                    return NotFound();
                }
                await _dbVila.RemoveAsync(vila);
                await _dbVila.SaveAsync();
                _response.StatusCodes = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name ="UpdateVila")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVila(int id, [FromBody]VilaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.Id != id)
                {
                    return BadRequest();
                }

                Vila model = _mapper.Map<Vila>(updateDTO);


                await _dbVila.UpdateAsync(model);


                _response.StatusCodes = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;

            
        }

        [HttpPatch("{id:int}", Name = "PatchPartialVila")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> PatchPartialVila(int id, [FromBody]JsonPatchDocument<VilaUpdateDTO> PatchUpdateDTO)
        {
            try
            {
                if (PatchUpdateDTO == null | id == 0)
                {
                    return BadRequest();
                }

                var vilaPatch = await _dbVila.GetAsync(u => u.Id == id, tracked: false);
                if (vilaPatch == null)
                {
                    return BadRequest();
                }
                VilaUpdateDTO vilaUpdateDTO = _mapper.Map<VilaUpdateDTO>(vilaPatch);


                PatchUpdateDTO.ApplyTo(vilaUpdateDTO);

                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                Vila model = _mapper.Map<Vila>(vilaUpdateDTO);
                await _dbVila.UpdateAsync(model);
                _response.StatusCodes = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;

        }
    }
}
