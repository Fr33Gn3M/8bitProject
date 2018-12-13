using System;
using System.Web.Mvc;
using Common;

namespace Computing.Controllers
{
    public class BusinessController : Controller
    {
        // GET: Business
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult geneSXS()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.geneSXS();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult setFCQSZT()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.setFCQSZT();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult setTDQSZT()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.setTDQSZT();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult setTdSfxz()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.setTdSfxz();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult setTdExQSZT()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.setTdExQSZT();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult deleteQLR()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.deleteQLR();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult insertQLR()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.insertQLR();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
        public JsonResult setQLRQLLX()
        {
            var result = new DAL.JsonResult() { };
            try
            {
                DataIntegrateHelper.setQLRQLLX();
                result.Status = 1000;
            }
            catch (Exception ex)
            {
                result.Status = 9999;
                result.Error = ex.Message;
            }
            return CustomsJsonResult.GetJsonResult(result);
        }
    }
}