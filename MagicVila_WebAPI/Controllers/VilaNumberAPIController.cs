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
    
    [Route("api/vilaNumberAPI")]
    [ApiController]
    public class VilaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;     
        private readonly IMapper _mapper;
        private readonly IVilaNumberRepository _dbVilaNumber;
        private readonly IVilaRepository _dbVila;

        public VilaNumberAPIController(IVilaNumberRepository dbVilaNumber, IMapper mapper, IVilaRepository dbVila)
        {
            _dbVilaNumber = dbVilaNumber;
            _dbVila = dbVila;
            _mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable <APIResponse>>> GetVilaNumbers()
        {
            try
            {
                IEnumerable<VilaNumber> vilaNumberList = await _dbVilaNumber.GetAllAsync(includeProperties: "Vila");
                _response.Result = _mapper.Map<List<VilaNumberDTO>>(vilaNumberList);
                _response.StatusCodes = HttpStatusCode.OK;
                
            }

            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages= new List<string> { ex.Message };
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name ="GetVilaNumber")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilaNumber(int vilaId)
        {
            try
            {
                if (vilaId == 0)
                {

                    return BadRequest();
                }
                var vilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == vilaId, includeProperties: "Vila");
                if (vilaNumber == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VilaNumberDTO>(vilaNumber);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilaNumber([FromBody] VilaNumberCreateDTO createNumberDTO)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            try
            {
                if (await _dbVilaNumber.GetAsync(u => u.VilaNo == createNumberDTO.VilaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa number already exist");

                    return BadRequest(ModelState);
                }
                if(await _dbVila.GetAsync(u =>u.Id == createNumberDTO.VilaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "invalid vila ID");

                    return BadRequest(ModelState);
                }
                if (createNumberDTO == null)
                {
                    return NotFound();

                }
                
                VilaNumber vilaNumber = _mapper.Map<VilaNumber>(createNumberDTO);

                await _dbVilaNumber.CreateAsync(vilaNumber);

                _response.Result = vilaNumber;
                _response.StatusCodes = HttpStatusCode.Created;
                await _dbVilaNumber.SaveAsync();
                return CreatedAtRoute("GetVila", new { id = vilaNumber.VilaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }


            return _response;
           
        }

        [HttpDelete("{id:int}", Name = "DeleteVilaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVilaNumber(int vilaId)
        {
            try
            {
                if (vilaId == 0)
                {
                    return BadRequest();
                }
                var vilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == vilaId);
                if (vilaNumber == null)
                {
                    return NotFound();
                }
                await _dbVilaNumber.RemoveAsync(vilaNumber);
                await _dbVilaNumber.SaveAsync();
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

        [HttpPut("{id:int}", Name ="UpdateVilaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVilaNumber(int id, [FromBody]VilaNumberUpdateDTO updateNumberDTO)
        {
            try
            {
                if (updateNumberDTO == null || updateNumberDTO.VilaNo != id)
                {
                    return BadRequest();
                }
                if (await _dbVila.GetAsync(u => u.Id == updateNumberDTO.VilaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "invalid vila ID");

                    return BadRequest(ModelState);
                }

                VilaNumber modelNumber = _mapper.Map<VilaNumber>(updateNumberDTO);


                await _dbVilaNumber.UpdateAsync(modelNumber);


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

        [HttpPatch("{id:int}", Name = "PatchPartialVilaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> PatchPartialVilaNumber(int vilaId, [FromBody]JsonPatchDocument<VilaNumberUpdateDTO> PatchUpdateDTO)
        {
            try
            {
                if (PatchUpdateDTO == null | vilaId == 0)
                {
                    return BadRequest();
                }

                var vilaNumberPatch = await _dbVilaNumber.GetAsync(u => u.VilaNo == vilaId, tracked: false);
                if (vilaNumberPatch == null)
                {
                    return BadRequest();
                }
                VilaNumberUpdateDTO vilaNumberUpdateDTO = _mapper.Map<VilaNumberUpdateDTO>(vilaNumberPatch);


                PatchUpdateDTO.ApplyTo(vilaNumberUpdateDTO);

                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                VilaNumber modelNumber = _mapper.Map<VilaNumber>(vilaNumberUpdateDTO);
                await _dbVilaNumber.UpdateAsync(modelNumber);
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
