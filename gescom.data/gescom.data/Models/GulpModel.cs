﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace gescom.data.Models
{
    public static class GulpHelpers
    {


        // item selon son identifiant id.
        public static GulpItem Get(long id)
        {
            var result = new GulpItem();
            foreach (var item in GetList().Where(item => item.Id == id)) result = item;
            return result;
        }

     /*   public static void InitAll()
        {
            var liste = ArticleHelpers.GetList();
            var repository = new GulpRepository();
            foreach (var item in liste)
            {
                if(item.Id != 9184)
                {
                    var g = new GulpItem() { Id = item.Id };
                    repository.Add(g);
                }
            }
            repository.Save();
        } */

        public static List<GulpItem> GetList()
        {
            var cart = new GulpCart();
            return cart.Gulps;
        }

        public static bool Update(GulpModel model)
        {
            var repository = new GulpRepository();
            return repository.Update(model);
        }


    }

    public class GulpCart : IEnumerable<GulpItem>
    {
        public GulpCart()
        {
            Gulps = new List<GulpItem>();
            var repository = new GulpRepository();
            var count = repository.Count();
            if (count == 0) return;
            foreach (var element in repository.Gulps())
            {

                Gulps.Add(element);
            }
        }

        public List<GulpItem> Gulps { get; set; }

        public IEnumerator<GulpItem> GetEnumerator()
        {
            return Gulps.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public partial class GulpItem
    {
        public void Copy(GulpItem item)
        {
            C1 = item.C1;
            C2 = item.C2;
            C3 = item.C3;
            C4 = item.C4;
            Q1 = item.Q1;
            Q2 = item.Q2;
            Q3 = item.Q3;
            Q4 = item.Q4;
            U2 = item.U2;
            U3 = item.U3;
            U4 = item.U4;
            P2 = item.P2;
            P3 = item.P3;
            D1 = item.D1;
            D2 = item.D2;
            D3 = item.D3;
            D4 = item.D4;
            L1 = item.L1;
            L2 = item.L2;
            L3 = item.L3;
            L4 = item.L4;
        }
    }

    public class GulpModel : GulpItem
    {


    }

    public class GulpRepository : IData
    {
        private readonly DataGescomDataContext _context = new DataGescomDataContext();

        public int Count()
        {
            return _context.GulpItems.Count();
        }

        public bool Save()
        {
            try
            {
                _context.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Add(GulpItem item)
        {
            _context.GulpItems.InsertOnSubmit(item);
        }


        public void Delete(GulpItem item)
        {
            _context.GulpItems.DeleteOnSubmit(item);
        }

        public GulpItem Get(long id)
        {
            return _context.GulpItems.SingleOrDefault(d => d.Id == id);
        }

        public IQueryable<GulpItem> Gulps()
        {
            return _context.GulpItems;
        }

        public bool Update(GulpModel model)
        {
            var item = Get(model.Id);
            item.Copy(model);
            try
            {
                _context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}