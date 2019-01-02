using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<List<Playlist>> Get()
        {
            var db = new ChinookDatabase();

            var result = await db.Playlists.ToListAsync();
            return result;
        }
    }
}