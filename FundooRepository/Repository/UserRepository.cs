using FundooModels;
using FundooRepository.Interface;
using MongoDB.Driver;
using System;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Experimental.System.Messaging;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<RegisterModel> _User;
        private IConfiguration configuration;
       public UserRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _User = database.GetCollection<RegisterModel>("User");
        }
        public async Task<string> Register(RegisterModel user)
        {
            try
            {
                var ifExist = await _User.AsQueryable<RegisterModel>().SingleOrDefaultAsync(e => e.Email == user.Email);
                if (ifExist == null)
                {
                    await _User.InsertOneAsync(user);
                    return "Register Successful";
                }
                return "Email Already Exist";
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Login(LoginModel logindetails)
        {
            try
            {
                var EmailExist = await _User.AsQueryable<RegisterModel>().Where(x => (x.Email == logindetails.Email)).FirstOrDefaultAsync();
                if (EmailExist != null)
                {
                    var PasswordExist = await _User.AsQueryable<RegisterModel>().Where(x => x.Password == logindetails.Password).FirstOrDefaultAsync();
                    if (PasswordExist != null)
                    {
                        return "Login Successful";
                    }
                    return "Enter valid Password";
                }
                return "Enter valid Email";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> ForgetPassword(ForgetModel MYEmail)
        {
            
            try
            {
                var EmailExist = await _User.AsQueryable<RegisterModel>().Where(x => x.Email == MYEmail.Email).FirstOrDefaultAsync();
                if (EmailExist != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(MYEmail.Email);
                    mail.Subject = "Reset Password for FundooNotes";
                    SendMSMQ();
                    mail.Body = ReceiveMSMQ();

                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                    return "Reset Link send to Your Email";
                }
                return "Email does not exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\Fundoo"))
            {
                msgqueue = new MessageQueue(@".\Private$\Fundoo");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\Fundoo");
            }
            msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "This is Password reset link. Reset Link =>";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }
        public string ReceiveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Fundoo");
            var receivemessage = msgqueue.Receive();
            receivemessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemessage.Body.ToString();
        }

        public async Task<string> ResetPassword(ResetModel newpassword)
        {
            try
            {
                var EmailExist = await _User.AsQueryable().Where(x => x.Email == newpassword.Email).FirstOrDefaultAsync();
                if (EmailExist != null)
                {
                        EmailExist.Password = newpassword.Password;
                        await _User.UpdateOneAsync(x => x.Email == newpassword.Email,
                            Builders<RegisterModel>.Update.Set(x => x.Password, newpassword.Password));
                        return "Reset Password Successful";
                }
                return "Enter valid Email";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Email, email)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
