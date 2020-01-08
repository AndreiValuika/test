﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using BulbaCourses.DiscountAggregator.Logic;
using BulbaCourses.DiscountAggregator.Logic.Models;
using BulbaCourses.DiscountAggregator.Web.App_Start;
using FluentValidation;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(BulbaCourses.DiscountAggregator.Web.Startup))]

namespace BulbaCourses.DiscountAggregator.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseNinjectMiddleware(() => ConfigureValidation(config)).UseNinjectWebApi(config);


        }

        private IKernel ConfigureValidation(HttpConfiguration config)
        {
            var kernel = new StandardKernel(new LogicModule());
            // Web API configuration and services
            FluentValidationModelValidatorProvider.Configure(config,
                cfg => cfg.ValidatorFactory = new NinjectValidationFactory(kernel));

            //IValidator<Course>
            AssemblyScanner.FindValidatorsInAssemblyContaining<Course>()
                .ForEach(result => kernel.Bind(result.InterfaceType)
                    .To(result.ValidatorType));


            return kernel;
        }
    }
}
