using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SImpleWebsite.Data;
using SImpleWebsite.Models;
using System.Diagnostics;

namespace SImpleWebsite.Controllers
{
 
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly DataContext context;

        public SongsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try 
            {
                var songs = await this.context.Songs.ToListAsync();
                return this.Ok(songs);
            }
            catch (Exception ex) 
            {
                return this.BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            try
            {
                this.context.Add(song);

                await this.context.SaveChangesAsync();
                return this.Ok(song);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song song)
        {
            try
            {
                var e = await this.context.Songs.FindAsync(id);
                if (e == null)
                {
                    return this.NotFound();
                }

                e.Title = song.Title;
                e.Description = song.Description;
                await this.context.SaveChangesAsync();
                return this.Ok(e);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var e = await this.context.Songs.FindAsync(id);
                if (e == null)
                {
                    return this.NotFound();
                }

                this.context.Songs.Remove(e);
                await this.context.SaveChangesAsync();
                return this.Ok(e);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}