﻿using System;
using Sap.SmartAccounting.Core.Dapper;
using Sap.SmartAccounting.Mvc.Entities;
using Sap.SmartAccounting.Mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sap.SmartAccounting.Mvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IRepository _repo = new Repository();

        // GET: Payment
        public ActionResult Index()
        {
            var model = new MatchingModels.PaymentListDto();

            var list = _repo.All<Payment>().FindAll(x => x.IsActive);

            if (list.Count > 0)
            {
                var mapper = PaymentDto.ConfigMapper().CreateMapper();
                var payments = mapper.Map<IEnumerable<PaymentDto>>(list.AsEnumerable()).ToList();

                model.Payments = payments;
            }

            return View(model);
        }

        // GET: Payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = new Payment()
                    {
                        B1Id = string.Empty,
                        CompanyCode = string.Empty,
                        CompanyName = model.Company,
                        BankCode = string.Empty,
                        BankName = model.Bank,
                        Amount = model.Amount,
                        ReceiveDate = model.ReceiveDate,
                        Reference = model.Reference,
                        AccountId = 0,
                        AccountCode = string.Empty,
                        AccountName = model.Account,
                        Status = PaymentStatusEnum.ToBePost,
                        CreateTime = DateTime.Now,
                        IsActive = true
                    };

                    _repo.Insert(payment);

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Warn", ex.Message);
                }
            }

            return View();
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}