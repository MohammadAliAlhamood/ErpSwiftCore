using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging; 
using System.Net; 
namespace ErpSwiftCore.Application
{
    /// <summary>
    /// أبستراكت بيس هندلر يأخذ Request من نوع IRequest<APIResponseDto> ويُرجع APIResponseDto.
    /// يحتوي على منطق موحّد للتعامل مع try–catch وبناء الـAPIResponseDto.
    /// </summary>
    public abstract class BaseHandler<TRequest> 
        : IRequestHandler<TRequest, APIResponseDto> where TRequest 
        : IRequest<APIResponseDto>
    {
        private readonly ILogger<BaseHandler<TRequest>> _logger;
        private ILogger<DeleteCompanyCommand> logger;
        protected BaseHandler(ILogger<BaseHandler<TRequest>> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// في الهانْدلرز الملموسة، نكتب المنطق الفعلي هنا في هذه الدالة.
        /// إذا نجح كل شيء، نحول ما يعيده إلى كائن APIResponseDto بواسطة CreateSuccessResponse().
        /// إذا فشل لأي سبب (مثلاً في الوصول إلى قاعدة البيانات أو في منطق الدومين)،
        /// يُرمى استثناء ونلتقطه في Handle().
        /// </summary>
        protected abstract Task<object?> HandleRequestAsync(TRequest request, CancellationToken cancellationToken);
        public async Task<APIResponseDto> Handle(TRequest request, CancellationToken cancellationToken)
        {
            APIResponseDto response = new ();
            try
            {
                // 1. ننفّذ المنطق الفعلي (توليد النتيجة أو تنفيذ الأمر)
                var result = await HandleRequestAsync(request, cancellationToken);

                // 2. إذا نجح المنطق، نحضّر استجابة النجاح
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = result;
                return response;
            }
            catch (ValidationException fvEx)
            {
                // استثناءات الفلوينت فاليشن: نرجع 400 Bad Request مع تفاصيل الأخطاء
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;

                foreach (var failure in fvEx.Errors)
                {
                    response.ErrorMessages.Add(failure.ErrorMessage);
                }

                // نسجّل الأخطاء في اللوج من دون إفشاء التفاصيل الحسّاسة
                _logger.LogWarning("Validation failed for {RequestType}: {Errors}",
                    typeof(TRequest).Name,
                    string.Join("; ", fvEx.Errors));

                return response;
            }
            catch (DomainNotFoundException nfEx)
            {
                // مثال: استثناء من الدومين يفيد أن المورد المطلوب غير موجود
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessages.Add(nfEx.Message);

                _logger.LogInformation("Resource not found in {RequestType}: {Message}",
                    typeof(TRequest).Name,
                    nfEx.Message);

                return response;
            }
            catch (DomainException dEx)
            {
                // مثال: أي استثناء عام من طبقة الدومين للدلالة على فشل منطقي
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add(dEx.Message);

                _logger.LogWarning("Domain exception in {RequestType}: {Message}",
                    typeof(TRequest).Name,
                    dEx.Message);

                return response;
            }
            catch (Exception ex)
            {
                // أي خطأ غير متوقع (قاعدة بيانات، خادم، إلخ)
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.IsSuccess = false;
                response.ErrorMessages.Add("حدث خطأ داخلي في الخادم. حاول مرة أخرى لاحقًا.");

                // نُسجّل التفاصيل كاملة في اللوج لأغراض التشخيص (لاتعرَض للمستخدم النهائي)
                _logger.LogError(ex, "Unhandled exception in {RequestType}", typeof(TRequest).Name);

                return response;
            }
        }
    }

    #region ──────────── أمثلة على استثناءات دومين قياسية ────────────

    /// <summary>
    /// يمكنك تعديل أو إزالة هذه الاستثناءات بما يتوافق مع تطبيقك.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class DomainNotFoundException : DomainException
    {
        public DomainNotFoundException(string message) : base(message) { }
    }

    #endregion
}