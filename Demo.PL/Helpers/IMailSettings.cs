

using Demo.DAL.Common.Email;

namespace Demo.PL.Helpers
{
    public interface IMailSettings
    {
        public void sendEmail(Email email);
    }
}
