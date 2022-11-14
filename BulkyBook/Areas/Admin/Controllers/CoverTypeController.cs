using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                return View(coverType);
            }

            var paramer = new DynamicParameters();
            paramer.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, paramer);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var paramer = new DynamicParameters();
                paramer.Add("@Name",coverType.Name);
                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, paramer);
                }
                else
                {
                    paramer.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, paramer);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            // callingusing Stored Procedure
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll,null);
            return Json(new { data=allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var paramer = new DynamicParameters();
            paramer.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get,paramer);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleteing" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, paramer);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete was Successful" });

        }
        #endregion
    }
}