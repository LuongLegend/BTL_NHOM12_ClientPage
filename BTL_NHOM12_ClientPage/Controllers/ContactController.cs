using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class ContactController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        Random random = new Random();
        // GET: Contact
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        bool checkDublicateKey(string id)
        {
            var a = from b in db.Contacts where b.contact_ID == id select b;
            return a.Count() > 0;
        }
        void insertToContact(string id, string cusName, string phone, string address, string note, string time)
        {
            Contact a = new Contact();
            a.contact_ID = id;
            a.username = cusName;
            a.phone = phone;
            a.note = note;
            a.address = address;
            a.status_Contact = "pending";
            a.create_date = Convert.ToDateTime(time);
            db.Contacts.InsertOnSubmit(a);
            db.SubmitChanges();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            string cusName = f["inputName"];
            string cusPhone = f["inputPhone"];
            string cusNote = f["inputNote"];
            string cusAddress = f["inputAddress"];
            string contactID = "CT" + RandomString(6);
            do
            {
                contactID = "CT" + RandomString(6);
            } while (checkDublicateKey(contactID));
            string time = DateTime.Now.ToString("dd/MM/yyyy");
            insertToContact(contactID, cusName, cusPhone, cusAddress, cusNote, time);
            ViewBag.sent = "true";
            return View();
        }
    }
}