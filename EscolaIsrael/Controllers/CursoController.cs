using System.Linq;
using System.Threading.Tasks;
using EscolaIsrael.Data;
using EscolaIsrael.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaIsrael.Controllers
{
    public class CursoController : Controller
    {
        private readonly EscolaContext _context;
        public CursoController(EscolaContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos.OrderBy(c => c.NomeDoCurso).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeDoCurso", "Descricao", "Valor")] Curso curso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(curso);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(curso);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.CursoID == id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("CursoID, NomeDoCurso, Descricao, Valor")] Curso curso)
        {
            if (id != curso.CursoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.CursoID))
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
            return View(curso);
        }

        private bool CursoExists(long? id)
        {
            return _context.Cursos.Any(e => e.CursoID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.CursoID == id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.CursoID == id);

            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.CursoID == id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}