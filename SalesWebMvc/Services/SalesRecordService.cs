using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //essa consulta não é executada pela simples deinição dela
            //essa declaração vai pegar esse SalesRecord que é do tipo DBset e construir um objeto
            //result do tipo IQueryable, e em cima desse obj agora vou poder acrescentar outros
            //detalhes da minha consulta: 
            var result = from obj in _context.SalesRecord select obj;
            //detalhe 1:
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            //detalhe 2:
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //executando a minha consulta
            return await result
                .Include(x => x.Seller) //fazendo o join das tabelas de vendedor
                .Include(x => x.Seller.Department) //fazendo o join tabela de departamentos
                .OrderByDescending(x => x.Date) //ordenando por data decrescente
                .ToListAsync();
        }
    }
}
