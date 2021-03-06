﻿using System;
using System.Web.Mvc;
using MaterialCMS.Entities.Messaging;
using MaterialCMS.Helpers;
using MaterialCMS.Messages;
using MaterialCMS.Website.Binders;
using Ninject;

namespace MaterialCMS.Web.Areas.Admin.ModelBinders
{
    public class MessageTemplateOverrideModelBinder : MessageTemplateModelBinder
    {
        public MessageTemplateOverrideModelBinder(IKernel kernel)
            : base(kernel)
        {
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var type = GetTypeByName(controllerContext);

            bindingContext.ModelMetadata =
                ModelMetadataProviders.Current.GetMetadataForType(
                    () => CreateModel(controllerContext, bindingContext, type), type);

            var messageTemplate = base.BindModel(controllerContext, bindingContext) as MessageTemplate;

            return messageTemplate;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var type = GetTypeByName(controllerContext);
            return Activator.CreateInstance(type);
        }

        private static Type GetTypeByName(ControllerContext controllerContext)
        {
            var valueFromContext = GetValueFromContext(controllerContext, "TemplateType");
            return TypeHelper.GetTypeByName(valueFromContext);
        }
    }
}