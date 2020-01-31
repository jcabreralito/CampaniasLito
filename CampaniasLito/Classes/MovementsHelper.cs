using CampaniasLito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampaniasLito.Classes
{
    public class MovementsHelper : IDisposable
    {
        private static CampaniasLitoContext db = new CampaniasLitoContext();

        public void Dispose()
        {
            db.Dispose();
        }
    }
}