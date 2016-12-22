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
    public class EventController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: /Event/
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: /Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventa = db.Events.Find(id);
            if (eventa == null)
            {
                return HttpNotFound();
            }
            var Rezultati = from b in db.People
                            select new
                            {
                                b.Ime,
                                b.PersonID,
                                b.Prezime,
                                b.Username,

                                Checked = ((from ab in db.PersonsToEvents
                                            where (ab.EventID == id) & (ab.PersonID == b.PersonID)
                                            select ab).Count() > 0)
                            };

            var MyViewModel = new EventViewModel();
            MyViewModel.EventID = id.Value;
            MyViewModel.Naziv = eventa.Naziv;
            MyViewModel.Opis = eventa.Opis;
            MyViewModel.Grad = eventa.Grad;
            MyViewModel.Adresa = eventa.Adresa;
            MyViewModel.Datum = eventa.Datum;


            var MyPeopleList = new List<CheckPersonViewModel>();
            foreach (var item in Rezultati)
            {
                MyPeopleList.Add(new CheckPersonViewModel { Id = item.PersonID, Ime = item.Ime, Prezime = item.Prezime, Username = item.Username, Checked = item.Checked });
            }
            MyViewModel.Persons = MyPeopleList;
            return View(MyViewModel);
        }

        
        // GET: /Event/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
                return View();
            else return RedirectToAction("Index");
        }

        // POST: /Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EventID,Naziv,Grad,Adresa,Opis,Datum")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }
        
        // GET: /Event/Edit/5
        public ActionResult Edit(int? id)
        {
            //if(Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Event eventa = db.Events.Find(id);
                if (eventa == null)
                {
                    return HttpNotFound();
                }
                var Rezultati = from b in db.People
                                select new
                                {
                                    b.Ime,
                                    b.PersonID,
                                    b.Prezime,
                                    b.Username,

                                    Checked = ((from ab in db.PersonsToEvents
                                                where (ab.EventID == id) & (ab.PersonID == b.PersonID)
                                                select ab).Count() > 0)
                                };

                var MyViewModel = new EventViewModel();
                MyViewModel.EventID = id.Value;
                MyViewModel.Naziv = eventa.Naziv;
                MyViewModel.Opis = eventa.Opis;
                MyViewModel.Grad = eventa.Grad;
                MyViewModel.Adresa = eventa.Adresa;
                MyViewModel.Datum = eventa.Datum;


                var MyPeopleList = new List<CheckPersonViewModel>();
                foreach (var item in Rezultati)
                {
                    MyPeopleList.Add(new CheckPersonViewModel { Id = item.PersonID, Ime = item.Ime, Prezime=item.Prezime, Username=item.Username, Checked = item.Checked });
                }
                MyViewModel.Persons = MyPeopleList;
                return View(MyViewModel);


                //else RedirectToAction("Index","Event");
            }
        }

        // POST: /Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventViewModel eventa)
        {
            if (ModelState.IsValid)
            {
                var MyEvent = db.Events.Find(eventa.EventID);
                MyEvent.Naziv = eventa.Naziv;
                MyEvent.Opis = eventa.Opis;
                MyEvent.Adresa = eventa.Adresa;
                MyEvent.Grad = eventa.Grad;
                MyEvent.Datum = eventa.Datum;
                
                foreach (var item in db.PersonsToEvents)
                {
                    if (item.EventID == eventa.EventID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in eventa.Persons)
                {
                    if (item.Checked)
                        db.PersonsToEvents.Add(new PersonToEvent() { EventID = eventa.EventID, PersonID = item.Id });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventa);
        }

        // GET: /Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event= db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);

        }

        // POST: /Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
