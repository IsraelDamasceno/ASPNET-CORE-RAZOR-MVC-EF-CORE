using System.Linq;
using System.Threading.Tasks;
using EscolaIsrael.Data;
using EscolaIsrael.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaIsrael.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly EscolaContext _context;
        public FuncionarioController(EscolaContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.OrderBy(c => c.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome", "Cargo", "Endereço")] Funcionario funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.FuncionarioID == id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("FuncionarioID, Nome, Cargo, Endereço")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioID))
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
            return View(funcionario);
        }

        private bool FuncionarioExists(long? id)
        {
            return _context.Funcionarios.Any(e => e.FuncionarioID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.FuncionarioID == id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.FuncionarioID == id);

            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.FuncionarioID == id);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}