using System.Text.Json;
namespace platformservice.middlewares
{
  public class ExceptionHandlerMiddleware
    {
       private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next=next;
        }

        public async Task  InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode= StatusCodes.Status500InternalServerError;
                context.Response.ContentType="application/json";
                var response= new {message=ex.Message, description="An unexpected error occured . Please contect the adiminstrator."};
                var jsonResponse= JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}