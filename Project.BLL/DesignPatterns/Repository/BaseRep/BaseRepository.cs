﻿using Project.BLL.DesignPatterns.Repository.IRep;
using Project.BLL.DesignPatterns.Singleton;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.Repository.BaseRep
{
    public class BaseRepository<T>:IRepository<T> where T:BaseEntity
    {
        MyContext _db;
        public BaseRepository()
        {
            _db = DBTool.DBInsantance;
        }

        void Save()
        {
            _db.SaveChanges();
        }
        public void Add(T item)
        {
            _db.Set<T>().Add(item);
            Save();
        }

        public void AddRange(List<T> list)
        {
            _db.Set<T>().AddRange(list);
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Any(exp);
        }

        public void Delete(T item)
        {
            item.Status = ENTITIES.Models.Enums.DataStatus.Deleted;
            item.DeletedDate = DateTime.Now;
            Save();

        }

        public void DeleteRange(List<T> list)
        {
            foreach (T item in list)
            {
                Delete(item);
            }
        }

        public void Destroy(T item)
        {
            _db.Set<T>().Remove(item);
            Save();
        }

        public void DestroyRange(List<T> list)
        {
            _db.Set<T>().RemoveRange(list);
            Save();
        }

        public T Find(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            return Where(x => x.Status != ENTITIES.Models.Enums.DataStatus.Deleted);
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetFirstData()
        {
            return _db.Set<T>().OrderBy(x => x.CreatedDate).FirstOrDefault();
        }

        public T GetLastData()
        {
            return _db.Set<T>().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }

        public List<T> GetModifieds()
        {
            return Where(x => x.Status == ENTITIES.Models.Enums.DataStatus.Updated);
        }

        public List<T> GetPassives()
        {
            return Where(x => x.Status == ENTITIES.Models.Enums.DataStatus.Deleted);
        }

        public object Select(Expression<Func<T, object>> exp)
        {
            return _db.Set<T>().Select(exp).ToList();
        }

        public void Update(T item)
        {

            T toBeUpdated = Find(item.ID);
            item.Status = ENTITIES.Models.Enums.DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            Save();
        }

        public void UpdateRange(List<T> list)
        {
            foreach (T item in list)
            {
                Update(item);
            }
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp).ToList();
        }


    }
}
