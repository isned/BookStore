using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BooksController : Controller


    {
        private readonly BookStoreDbContext _context;
        public BooksController(BookStoreDbContext context)
        {
            _context = context;                   
        }                

        // GET: BooksController
        public ActionResult Index()
        {
            var books = _context.Books.ToList(); // Charger tous les livres depuis la base de données
            return View(books); // Passer la liste des livres à la vue
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Language,ISBN,DatePublished,Price,Author,ImageUrl")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _context.Books.Find(id); // Trouver le livre par son ID dans la base de données
            if (book == null)
            {
                return NotFound(); // Retourner une vue NotFound si le livre n'est pas trouvé
            }
            return View(book); // Passer le livre à la vue d'édition
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(book); // Mettre à jour les informations du livre dans la base de données
                _context.SaveChanges(); // Enregistrer les modifications
                return RedirectToAction(nameof(Index)); // Rediriger vers la liste des livres
            }
            return View(book);
        }


        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Find(id); // Trouver le livre par son ID dans la base de données
            if (book == null)
            {
                return NotFound(); // Retourner une vue NotFound si le livre n'est pas trouvé
            }
            return View(book); // Passer le livre à la vue de confirmation de suppression
        }

        // POST: BooksController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book); // Supprimer le livre de la base de données
            _context.SaveChanges(); // Enregistrer les modifications
            return RedirectToAction(nameof(Index)); // Rediriger vers la liste des livres
        }


        // POST: BooksController/Delete/5
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Delete(int id, IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }*/
    }
}