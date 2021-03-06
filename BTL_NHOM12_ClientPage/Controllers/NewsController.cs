﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class NewsController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        // GET: News
        public ActionResult Index()
        {
            var z = from a in db.News select a;
            var zl = from a in db.News where a.new_ID == Request["idgiday"] select a;
            var cate = from a in db.Categories select a;
            ViewBag.cates = cate;
            ViewBag.news = z;
            ViewData["news"] = z;
            return View(zl);
        }
    }
}