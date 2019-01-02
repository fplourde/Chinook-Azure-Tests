using System.Collections.Generic;
using System.Threading.Tasks;
using Albums.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Albums.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<List<Album>> Get()
        {
            var db = new ChinookDatabase();

            var result = await db.Albums.ToListAsync();
            return result;
        }
    }
}