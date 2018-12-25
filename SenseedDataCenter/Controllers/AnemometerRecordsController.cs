using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenseedDataCenter.Models;

namespace SenseedDataCenter.Controllers
{
    public class AnemometerRecordsController : Controller
    {
        private readonly SenseedDataCenterContext _context;

        public AnemometerRecordsController(SenseedDataCenterContext context)
        {
            _context = context;
        }

        // GET: AnemometerRecords
        [Authorize]
        public async Task<IActionResult> Index(DateTime? startTime, DateTime? stopTime, string id, int pageIndex = 1)
        {
            int pageSize = 20;

            var records = from record in _context.AnemometerRecords
                where record.ProductId == id
                select record;

            if (startTime != null)
            {
                records = records.Where(s => s.RecordDate >= startTime);
            }

            if (stopTime != null)
            {
                records = records.Where(s => s.RecordDate <= stopTime);
            }

            var pageCount = records.Count() % pageSize == 0
                ? records.Count() / pageSize
                : records.Count() / pageSize + 1;

            var resultRecords = records.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.ProductId = id;

            return View(await resultRecords.ToListAsync());
        }

        // GET: AnemometerRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anemometerRecord = await _context.AnemometerRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anemometerRecord == null)
            {
                return NotFound();
            }

            return View(anemometerRecord);
        }

        public async Task<IActionResult> DownLoads(DateTime? startTime, DateTime? stopTime, string id)
        {
            var records = from record in _context.AnemometerRecords
                where record.ProductId == id
                select record;

            if (startTime != null)
            {
                records = records.Where(s => s.RecordDate >= startTime);
            }

            if (stopTime != null)
            {
                records = records.Where(s => s.RecordDate <= stopTime);
            }

            var tempRecords = await records.ToListAsync();

            using (var memoryStream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(memoryStream);

                foreach (var record in tempRecords)
                {
                    streamWriter.WriteLine($"N2S: " + record.N2S + "m/s E2W: " + record.E2W + "m/s Velcoity: " +
                                           record.Velocity +
                                           "m/s Direction: " + record.Direction + "° SoundVelocity: " +
                                           record.SoundVelocity + "m/s " + record.RecordDate);
                    streamWriter.Flush();
                }

                return File(memoryStream.GetBuffer(), "text/plain", "record.txt");
            }
        }

        // GET: AnemometerRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnemometerRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,E2W,N2S,Velocity,Direction,SoundVelocity,ProductId")] AnemometerRecord anemometerRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anemometerRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anemometerRecord);
        }

        // GET: AnemometerRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anemometerRecord = await _context.AnemometerRecords.FindAsync(id);
            if (anemometerRecord == null)
            {
                return NotFound();
            }
            return View(anemometerRecord);
        }

        // POST: AnemometerRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,E2W,N2S,Velocity,Direction,SoundVelocity")] AnemometerRecord anemometerRecord)
        {
            if (id != anemometerRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anemometerRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnemometerRecordExists(anemometerRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anemometerRecord);
        }

        // GET: AnemometerRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anemometerRecord = await _context.AnemometerRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anemometerRecord == null)
            {
                return NotFound();
            }

            return View(anemometerRecord);
        }

        // POST: AnemometerRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anemometerRecord = await _context.AnemometerRecords.FindAsync(id);
            _context.AnemometerRecords.Remove(anemometerRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnemometerRecordExists(int id)
        {
            return _context.AnemometerRecords.Any(e => e.Id == id);
        }

    }
}
