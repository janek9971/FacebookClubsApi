using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using ParserModel.Repositories;

namespace API.Controllers
{
    [Produces("text/plain")]
    [Route("api/FbClubsAPI")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public class ParserController
    {
        private readonly  IParserRootRepository _parserRoot;

        public ParserController(IParserRootRepository parserRoot)
        {
            _parserRoot = parserRoot;
        }

        [HttpPost]
        [Route("Parser")]
        [ProducesResponseType(200, Type = typeof(string))]
        public string ParseWeb() =>
            _parserRoot.Parse();
    }
}
