﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtBlogArtikelApi.Models;

namespace JwtBlogArtikelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly BlogArtiklenDbContext _context;

        public BookmarksController(BlogArtiklenDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookmarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookmark>>> GetBookmarks()
        {
            return await _context.Bookmarks.ToListAsync();
        }

        // GET: api/Bookmarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookmark>> GetBookmark(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            return bookmark;
        }

        // GET: api/User/{id}/Bookmarks/
        [HttpGet("User/{id}/Bookmark/")]
        public async Task<ActionResult<IEnumerable<Bookmark>>> UserBookmarks(int id)
        {
            var bookmarks = await _context.Bookmarks
                .Where(s => s.UserId == id)
                .ToListAsync();

            if (bookmarks == null)
            {
                return NotFound();
            }

            return bookmarks;
        }


        // PUT: api/Bookmarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmark(int id, Bookmark bookmark)
        {
            if (id != bookmark.UserId)
            {
                return BadRequest();
            }

            _context.Entry(bookmark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkExists(id))
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

        // POST: api/Bookmarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookmark>> PostBookmark(Bookmark bookmark)
        {
            _context.Bookmarks.Add(bookmark);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookmarkExists(bookmark.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookmark", new { id = bookmark.UserId }, bookmark);
        }

        // DELETE: api/Bookmarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkExists(int id)
        {
            return _context.Bookmarks.Any(e => e.UserId == id);
        }
    }
}
