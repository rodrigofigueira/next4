using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repository
{
    public class HashSecurityAPIRepository : IHashSecurityAPIRepository
    {
        private DataContext _context;

        public HashSecurityAPIRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var _hashSecurity = await _context.HashSecurityAPIs.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.HashSecurityAPIs.Remove(_hashSecurity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0 ? true : false;
        }

        public async Task<HashSecurityAPI> GetById(int id)
        {
            return await _context.HashSecurityAPIs.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<HashSecurityAPI> Post(HashSecurityAPI hashSecurity)
        {
            _context.HashSecurityAPIs.Add(hashSecurity);
            await _context.SaveChangesAsync();
            return hashSecurity;

        }

        public async Task<bool> Update(HashSecurityAPI hashSecurity)
        {
            HashSecurityAPI _hashSecurity = await _context.HashSecurityAPIs
                                        .Where(u => u.Id == hashSecurity.Id).FirstOrDefaultAsync();

            _context.Entry(_hashSecurity).CurrentValues.SetValues(hashSecurity);

            //todo: se o objeto passado for idêntico ao do banco o SaveChanges retorna 0
            //e consequentemente false
            int atualizou = await _context.SaveChangesAsync();
            return atualizou > 0 ? true : false;
        }

    }
}
