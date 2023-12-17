using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common;


public enum ActionResponseStatusCode
{
    [Display(Name = "Success")]
    Success = 200,

    [Display(Name = "ServerError")]
    ServerError = 500,

    [Display(Name = "BadRequest")]
    BadRequest = 400,

    [Display(Name = "NotFound")]
    NotFound = 404,

    [Display(Name = "UnAuthorized")]
    UnAuthorized = 401,

    [Display(Name = "Forbidden")]
    Forbidden = 403,

    [Display(Name = "Redirect")]
    Redirect = 302,

    [Display(Name = "Redirect Permanently")]
    RedirectPermanently = 301,
}

public interface IActionResponse
{
    public bool IsSuccess { get; set; }
    public ActionResponseStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}
public interface IActionResponse<TData> : IActionResponse
{
    public TData Data { get; set; }
}

public class ActionResponse : IActionResponse
{
    public ActionResponse(ActionResponseStatusCode statusCode)
    {
        IsSuccess = statusCode switch
        {
            ActionResponseStatusCode.Success => true,
            _ => false
        };
        StatusCode = statusCode;
        //Message = StatusCode.GetDisplayName();
    }
    public ActionResponse() : this(ActionResponseStatusCode.Success)
    {
    }
    public ActionResponse(ActionResponseStatusCode statusCode, string message) : this(statusCode) => Message = message;
    public ActionResponse(ActionResponseStatusCode statusCode, string message, params object[] messagePlaceHolder) : this(statusCode, string.Format(message, messagePlaceHolder))
    {
    }

    public static IActionResponse Success() => new ActionResponse(ActionResponseStatusCode.Success);
    public static IActionResponse Success(string message) => new ActionResponse(ActionResponseStatusCode.Success, message);

    public static IActionResponse Fail(ActionResponseStatusCode status) => new ActionResponse(status);
    public static IActionResponse Fail(ActionResponseStatusCode status, string message) => new ActionResponse(status, message);
    public static IActionResponse Fail(ActionResponseStatusCode status, string message, params object[] messagePlaceHolder) => new ActionResponse(status, message, messagePlaceHolder);

    public bool IsSuccess { get; set; }
    public ActionResponseStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}
public class ActionResponse<TData> : ActionResponse, IActionResponse<TData>
{
    public ActionResponse()
    { }
    public ActionResponse(ActionResponseStatusCode statusCode) : base(statusCode)
    {
    }
    public ActionResponse(ActionResponseStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
    public ActionResponse(ActionResponseStatusCode statusCode, string message, params object?[] messagePlaceHolder) : base(statusCode, message, messagePlaceHolder)
    {
    }

    public ActionResponse(TData data) : base() => Data = data;
    public ActionResponse(ActionResponseStatusCode statusCode, TData data) : base(statusCode) => Data = data;
    public ActionResponse(ActionResponseStatusCode statusCode, TData data, string message) : base(statusCode, message) => Data = data;
    public ActionResponse(ActionResponseStatusCode statusCode, TData data, string message, params object[] messagePlaceHolder) : base(statusCode, message, messagePlaceHolder) => Data = data;


    public static new IActionResponse<TData> Success() => new ActionResponse<TData>(ActionResponseStatusCode.Success);
    public static IActionResponse<TData> Success(TData data) => new ActionResponse<TData>(ActionResponseStatusCode.Success, data: data);

    public static new IActionResponse<TData> Fail(ActionResponseStatusCode status) => new ActionResponse<TData>(status);
    public static new IActionResponse<TData> Fail(ActionResponseStatusCode status, string message, params object?[] messagePlaceHolder) => new ActionResponse<TData>(status, message, messagePlaceHolder);

    public TData Data { get; set; } = default!;
}