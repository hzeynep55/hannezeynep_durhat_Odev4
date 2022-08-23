﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace Homework4Base
{
    public class ResponseGiudAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Response-Giud", Guid.NewGuid().ToString());
        }
    }
}
