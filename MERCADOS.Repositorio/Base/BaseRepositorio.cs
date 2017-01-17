using MERCADOS.IRepositorio;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MERCADOS.Repositorio
{
    public class BaseRepositorio<T> : IBaseRepositorio<T> where T : class
    {
        private readonly IContext _context;

        protected IContext Context
        {
            get { return _context; }
        }

        public BaseRepositorio(IContext context)
        {
            _context = context;
        }

        public IQueryable<T> Listar(Expression<Func<T, bool>> filter = null, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            return _context.Listar(filter, pageIndex, pageSize);
        }

        public int Contar(Expression<Func<T, bool>> filter = null)
        {
            return _context.Contar(filter);
        }

        public T ListarUno(Expression<Func<T, bool>> predicate)
        {
            return _context.ListarUno(predicate);
        }

        public void Insertar(T entity)
        {
            _context.Insertar(entity);
        }

        public void ActualizarParcial(T entity, params string[] noChangedPropertyNames)
        {
            _context.ActualizarParcial(entity, noChangedPropertyNames);
        }

        public void Actualizar(T entity)
        {
            _context.Actualizar(entity);
        }

        public void Eliminar(T entity)
        {
            _context.Eliminar(entity);
        }
    }
}
