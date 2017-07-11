using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using APIWorkshop.Models;
using APIWorkshop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIWorkshop.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var boards = BoardsRepository.Boards;
            return Ok(boards);
        }

        [HttpGet("{id}", Name = "GetBoard")]
        public IActionResult Get(int id)
        {
            try
            {
                var board = BoardsRepository.Boards.FirstOrDefault(b => b.Id == id);
                if (board == null)
                {
                    return NotFound();
                }

                return Ok(board);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Board board)
        {
            try
            {
                BoardsRepository.Boards.Add(board);
                var uri = Url.Link("GetBoard", new { id = board.Id });
                return Created(uri,board);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Board modelBoard)
        {
            try
            {
                var board = BoardsRepository.Boards.FirstOrDefault(b => b.Id == modelBoard.Id);
                if (board == null)
                {
                    return NotFound();
                }
                board.Name = modelBoard.Name ?? board.Name;
                board.Description = modelBoard.Description ?? board.Description;

                return Ok(board);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var board = BoardsRepository.Boards.FirstOrDefault(b => b.Id == id);
                if (board == null)
                {
                    return NotFound();
                }
                BoardsRepository.Boards.Remove(board);                
                return Ok(board);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
