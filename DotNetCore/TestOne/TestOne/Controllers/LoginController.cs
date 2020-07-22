using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public LoginController()
        { }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public string Login(string userName = "", string passWord = "")
        {
            return "登录成功";
        }

    }
}
