using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiPartners.Models;
using WiPartners.Repository;

namespace WiPartners.Controllers
{
    public class PersonController : Controller
    {
        PersonRepository person = new PersonRepository();
        PartnerPersonalData partner = new PartnerPersonalData();
        List<PartnerPersonalData> partners = new List<PartnerPersonalData>();

        // GET: Person
        public ActionResult Index()
        {

            try
            {
                partners = person.ReadData()
                               .Select(u => new PartnerPersonalData
                               {
                                   ID = u.ID,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Adress = u.Adress,
                                   PartnerNumber = u.PartnerNumber,
                                   CroatianPIN = u.CroatianPIN,
                                   PartnerTypeId = u.PartnerTypeId,
                                   CreatedAtUtc = u.CreatedAtUtc,
                                   CreateByUser = u.CreateByUser,
                                   IsForeign = u.IsForeign,
                                   ExternalCode = u.ExternalCode,
                                   Gender = u.Gender

                               }).ToList();

                if (partners.Count() == 0)
                {
                    return RedirectToAction("NewPartner");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound");
                throw;

            }


            return View(new PartnerPersonalData() { partners = partners });
        }

        public ActionResult NewPartner()
        {


            return View();
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult NewPartner(PartnerPersonalData newPartner)
        {

            newPartner.CreatedAtUtc = partner.CreatedAtUtc;

            try
            {
                if (ModelState.IsValid)
                {
                    person.CreateNewPartner(newPartner);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound");
                throw;
            }

            return View(newPartner);
        }

        public ActionResult NotFound()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
