using EscolaIsrael.Data;
using EscolaIsrael.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaIsrael.Controllers
{
    public class AlunoController : Controller
    {
        private readonly EscolaContext _context;
        public AlunoController(EscolaContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos.OrderBy(c => c.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome", "NomeDaMae", "Endereço")] Aluno aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(aluno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(aluno);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var aluno = await _context.Alunos.SingleOrDefaultAsync(m => m.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("AlunoID, Nome, NomeDaMae, Endereço")] Aluno aluno)
        {
            if (id != aluno.AlunoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoID))
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
            return View(aluno);
        }

        private bool AlunoExists(long? id)
        {
            return _context.Alunos.Any(e => e.AlunoID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aluno = await _context.Alunos.SingleOrDefaultAsync(m => m.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aluno = await _context.Alunos.SingleOrDefaultAsync(m => m.AlunoID == id);

            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var aluno = await _context.Alunos.SingleOrDefaultAsync(m => m.AlunoID == id);
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}