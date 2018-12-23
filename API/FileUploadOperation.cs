using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() != "apiloadbillboardspost") return;
            foreach (var parameter in operation.Parameters.ToList())
            {
                if (parameter.Name == "ContentType") operation.Parameters.Remove(parameter);
                if (parameter.Name == "ContentDisposition") operation.Parameters.Remove(parameter);
                if (parameter.Name == "Headers") operation.Parameters.Remove(parameter);
                if (parameter.Name == "Length") operation.Parameters.Remove(parameter);
                if (parameter.Name == "Name") operation.Parameters.Remove(parameter);
                if (parameter.Name == "FileName") operation.Parameters.Remove(parameter);
            }
            //operation.Parameters.Clear();
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "uploadedFile",
                In = "formData",
                Description = "Upload File",
                Required = true,
                Type = "file"
            });
            operation.Consumes.Add("multipart/form-data");
        }
    }
}
