using InforceShortener.Data.Models;

namespace InforceShortener.Data
{
    public class Repository<Model> where Model : class, IModel
    {
        protected readonly DataContext Context;

        public Repository(DataContext context)
        {
            Context = context;
        }

        public IQueryable<Model> GetAll()
        {
            return Context.Set<Model>().AsQueryable();
        }

        public Model GetById(int id)
        {
            return Context.Set<Model>().Find(id);
        }

        public void Insert(Model entity)
        {
            if (entity.Id != default)
            {
                throw new ArgumentException("Use default value for id property");
            }

            Context.Set<Model>().Add(entity);
        }

        public void Update(Model entity)
        {
            if (entity.Id == default)
            {
                throw new ArgumentException("Can not use default id value");
            }

            var oldEntity = GetById(entity.Id);

            Context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(int id)
        {
            var model = GetById(id);

            if (model == null)
            {
                throw new ArgumentException("Model with this id was not found");
            }

            Context.Set<Model>().Remove(model);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}

