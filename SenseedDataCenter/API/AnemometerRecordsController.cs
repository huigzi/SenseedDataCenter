using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenseedDataCenter.Models;

namespace SenseedDataCenter.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnemometerRecordsController : ControllerBase
    {
        private readonly SenseedDataCenterContext _context;

        public AnemometerRecordsController(SenseedDataCenterContext context)
        {
            _context = context;
        }

        // GET: api/AnemometerRecords
        [HttpGet]
        public IEnumerable<AnemometerRecord> GetAnemometerRecord()
        {
            return _context.AnemometerRecords;
        }

        // GET: api/AnemometerRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnemometerRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var anemometerRecord = await _context.AnemometerRecords.FindAsync(id);

            if (anemometerRecord == null)
            {
                return NotFound();
            }

            return Ok(anemometerRecord);
        }

        // PUT: api/AnemometerRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnemometerRecord([FromRoute] int id, [FromBody] AnemometerRecord anemometerRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anemometerRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(anemometerRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnemometerRecordExists(id))
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

        // POST: api/AnemometerRecords
        [HttpPost]
        public async Task<IActionResult> PostAnemometerRecord([FromBody] AnemometerRecord anemometerRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AnemometerRecords.Add(anemometerRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnemometerRecord", new { id = anemometerRecord.Id }, anemometerRecord);
        }


        private bool AnemometerRecordExists(int id)
        {
            return _context.AnemometerRecords.Any(e => e.Id == id);
        }
    }
}