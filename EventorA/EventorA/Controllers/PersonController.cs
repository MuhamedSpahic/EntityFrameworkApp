using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventorA.Models;

namespace EventorA.Controllers
{
    public class PersonController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: /Person/
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: /Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            var Rezultati = from b in db.Events
                            select new
                            {
                                b.Naziv,
                                b.EventID,
                                b.Grad,
                                b.Adresa,
                                b.Opis,
                                b.Datum,
                                Checked = ((from ab in db.PersonsToEvents
                                            where (ab.PersonID == id) & (ab.EventID == b.EventID)
                                            select ab).Count() > 0)
                            };

            var MyViewModel = new PersonViewModel();
            MyViewModel.PersonID = id.Value;
            MyViewModel.Ime = person.Ime;
            MyViewModel.Prezime = person.Prezime;
            MyViewModel.Pasword = person.Pasword;

            var MyEventList = new List<CheckEventViewModel>();
            foreach (var item in Rezultati)
            {
                MyEventList.Add(new CheckEventViewModel { Id = item.EventID, Naziv = item.Naziv, Checked = item.Checked });
            }
            MyViewModel.Eventi = MyEventList;
            return View(MyViewModel);

        }

        // GET: /Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PersonID,Ime,Prezime,Pasword")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: /Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
             var Rezultati= from b in db.Events
                           select new{
                               b.Naziv,
                               b.EventID,
                               b.Grad,
                               b.Adresa,
                               b.Opis,
                               b.Datum,
                               Checked =((from ab in db.PersonsToEvents 
                                              where (ab.PersonID==id) & (ab.EventID==b.EventID)
                                              select ab).Count()>0)
        };

             var MyViewModel = new PersonViewModel();
             MyViewModel.PersonID = id.Value;
             MyViewModel.Ime = person.Ime;
             MyViewModel.Prezime = person.Prezime;
             MyViewModel.Pasword = person.Pasword;

             var MyEventList = new List<CheckEventViewModel>();
             foreach (var item in Rezultati)
             {
                 MyEventList.Add(new CheckEventViewModel { Id = item.EventID,Naziv=item.Naziv, Checked=item.Checked });
             }
             MyViewModel.Eventi = MyEventList;
            return View(MyViewModel);
        }

        // POST: /Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                var MyPerson = db.People.Find(person.PersonID);
                MyPerson.Ime = person.Ime;
                MyPerson.Prezime = person.Prezime;
                MyPerson.Pasword = person.Pasword;

                foreach (var item in db.PersonsToEvents)
                {
                    if(item.PersonID==person.PersonID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted; 
                    }
                }
                foreach (var item in person.Eventi)
                {
                    if (item.Checked)
                        db.PersonsToEvents.Add(new PersonToEvent() { PersonID = person.PersonID, EventID = item.Id });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View(person);
        }

        // GET: /Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: /Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
