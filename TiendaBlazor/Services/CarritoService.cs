using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaBlazor.Data;

namespace BlazorWeb.Services
{
    public class CarritoService
    {
        private readonly ApplicationDbContext _context;

        public CarritoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Carrito>> GetAllCarritosAsync(int userId)
        {
            return await _context.Carritos
                                 .Include(c => c.Producto)
                                 .Where(c => c.UsuarioId == userId)
                                 .ToListAsync();
        }

        public async Task<Carrito> GetCarritoByIdAsync(int id)
        {
            return await _context.Carritos
                                 .Include(c => c.Producto)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCarritoAsync(Carrito carrito)
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarritoAsync(Carrito carrito)
        {
            _context.Carritos.Update(carrito);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarritoAsync(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
                await _context.SaveChangesAsync();
            }
        }
    }
}
