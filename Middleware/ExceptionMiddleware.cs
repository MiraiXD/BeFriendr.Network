using System.Net;
using BeFriendr.Network.UserProfiles.Exceptions;

namespace BeFriendr.Network.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            
            catch (RelationshipExceptions.Create.UserDoesNotExistException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.Error.WriteLine($"Could not find user with userName: {e.UserName} while creating a relationship");
                await context.Response.WriteAsync("Something went wrong");
            }
            catch(RelationshipExceptions.SetStatus.ActionNotAllowed e)
            {
                 context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.Error.WriteLine($"Cannot set status {e.Status} for relationship with status {e.Relationship.Status}");
                await context.Response.WriteAsync("Something went wrong");
            }
            catch(RelationshipExceptions.SetStatus.UnauthorizedReceiverException e)
            {
                 context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.Error.WriteLine($"User {e.LoggedInUserName} is not the receiver of this relationship");
                await context.Response.WriteAsync("Something went wrong");
            }
            catch(RelationshipExceptions.SetStatus.UnauthorizedSenderException e)
            {
                 context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.Error.WriteLine($"User {e.LoggedInUserName} is not the sender of this relationship");
                await context.Response.WriteAsync("Something went wrong");
            }
            catch (RelationshipExceptions.SetStatus.NotFoundException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.Error.WriteLine($"Relationship with sender userName: {e.SenderUserName} and receiver userName: {e.ReceiverUserName} has not been found");
                await context.Response.WriteAsync("Something went wrong");
            }
            catch (RelationshipExceptions.SetStatus.IncorrectStatusValueException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.Error.WriteLine("Incorrect status value: " + e.Status);
                await context.Response.WriteAsync("Something went wrong");
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Console.Error.WriteLine(e.Message);
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}