using Microsoft.EntityFrameworkCore;
using Toyota.Application.Interfaces.Business;
using Toyota.Application.Interfaces.Data;
using Toyota.Application.Mapping;
using Toyota.Business.Common;
using Toyota.Data.Context.Toyota;
using Toyota.Entities.Auth;
using Toyota.Entities.User;
using Toyota.Shared.Entities.Common;
using System.Data;
using Toyota.Shared.Utilities;

namespace Toyota.Business.Auth
{
    public class AuthBusiness : ToyotaBaseBusiness, IAuthBusiness
    {
        #region Fields

        private readonly IToyotaRepository<Application.Domain.User> _userRepository;
        private readonly IToyotaDbContext _context;

        #endregion

        #region Ctor

        public AuthBusiness(
            IToyotaRepository<Application.Domain.User> userRepository,
            IToyotaDbContext context
            ) : base()
        {
            _userRepository = userRepository;
            _context = context;
        }
        #endregion

        #region Methods

        public async Task<ServiceResponse<LoginResponseUserEntity>> Login(LoginRequestEntity request)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var user = await _userRepository.GetList().FirstOrDefaultAsync(x => x.Username == request.Username && x.IsActive);

                if (user == null)
                {
                    return new ServiceResponse<LoginResponseUserEntity>(Constants.ErrorCode, "Kullanıcı adı veya şifre yanlış, lütfen tekrar deneyiniz.");
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return new ServiceResponse<LoginResponseUserEntity>(Constants.ErrorCode, "Kullanıcı adı veya şifre yanlış, lütfen tekrar deneyiniz.");
                }

                await tx.CommitAsync();
                var result = user.ToLoginResponseUserEntityModel();
                return new ServiceResponse<LoginResponseUserEntity>(result);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new ServiceResponse<LoginResponseUserEntity>(ex);
            }
        }

        #endregion
    }
}
