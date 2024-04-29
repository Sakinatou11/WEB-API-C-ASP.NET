﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvisController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public AvisController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Avis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvis()
        {
            var avis = await _context.Avis.ToListAsync();
            return avis;
        }

        // GET: api/Avis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Avis>> GetAvis(Guid id)
        {
            var avis = await _context.Avis.FindAsync(id);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }

        // PUT: api/Avis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvis(Guid id, Avis avis)
        {
            if (id != avis.Id)
            {
                return BadRequest();
            }

            _context.Entry(avis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvisExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Avis
        [HttpPost]
        public async Task<ActionResult<Avis>> PostAvis(Avis avis)
        {
            _context.Avis.Add(avis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvis", new { id = avis.Id }, avis);
        }

        // DELETE: api/Avis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvis(Guid id)
        {
            var avis = await _context.Avis.FindAsync(id);
            if (avis == null)
            {
                return NotFound();
            }

            _context.Avis.Remove(avis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvisExists(Guid id)
        {
            return _context.Avis.Any(e => e.Id == id);
        }
    }
}
