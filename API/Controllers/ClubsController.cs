using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParserModel.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly ConfigureConnections _bookService;

        public ClubsController(ConfigureConnections bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Events>> Get()
        {
            return _bookService.Get();
        }

        //[HttpGet("{id:length(24)}", Name = "GetBook")]
        //public ActionResult<Events> Get(string id)
        //{
        //    var book = _bookService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        [HttpPost]
        public ActionResult<IEnumerable<Events>> Create(IEnumerable<Events> book,string name)
        {
            var enumerable = book.ToList();
            _bookService.Create(enumerable,name);

            return enumerable;
        }

        //[HttpPost]
        //public ActionResult CreateCollection(string name)
        //{
        //    _bookService.CreateCollection(name);

        //    return Ok();
        //}

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Events bookIn)
        //{
        //    var book = _bookService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bookService.Update(id, bookIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var book = _bookService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bookService.Remove(book.Id);

        //    return NoContent();
        //}
    }
}