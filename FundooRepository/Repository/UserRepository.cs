// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "UserRepository.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using StackExchange.Redis;

    /// <summary>
    /// UserRepository class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The user
        /// </summary>
        private readonly IMongoCollection<RegisterModel> User;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            User = database.GetCollection<RegisterModel>("User");
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Register Successful</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<RegisterModel> Register(RegisterModel user)
        {
            try
            {
                var ifExist = await this.User.AsQueryable().SingleOrDefaultAsync(e => e.email == user.email);
                if (ifExist == null)
                {
                    await this.User.InsertOneAsync(user);
                    return user;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Login Successful</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                
                var emailExist = await this.User.AsQueryable().Where(x => (x.email == login.email)).FirstOrDefaultAsync();
                if (emailExist != null)
                {
                    emailExist = await this.User.AsQueryable().Where(x => x.password == login.password).FirstOrDefaultAsync();
                    if (emailExist != null)
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "First Name", emailExist.firstName);
                        database.StringSet(key: "Last Name", emailExist.lastName);
                        database.StringSet(key: "Email", emailExist.email);
                        database.StringSet(key: "UserId", emailExist.UserID);
                                   
                        return emailExist;
                    }

                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Reset Link send to Your Email</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<bool> ForgetPassword(string email)
        {
            try
            {
                var emailExist = await this.User.AsQueryable().Where(x => x.email == email).FirstOrDefaultAsync();
                if (emailExist != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(email);
                    mail.Subject = "Reset Password for FundooNotes";
                    this.SendMSMQ();
                    mail.Body = this.ReceiveMSMQ();

                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends the MSMQ.
        /// </summary>
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

        /// <summary>
        /// Receives the MSMQ.
        /// </summary>
        /// <returns>Message</returns>
        public string ReceiveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Fundoo");
            var receivemessage = msgqueue.Receive();
            receivemessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemessage.Body.ToString();
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="newpassword">The newpassword.</param>
        /// <returns>Reset Password Successful</returns>
        /// <exception cref="System.Exception">System Exception Message</exception>
        public async Task<RegisterModel> ResetPassword(ResetModel password)
        {
            try
            {
                var emailExist = await this.User.AsQueryable().Where(x => x.email == password.Email).FirstOrDefaultAsync();
                if (emailExist != null)
                {
                        await User.UpdateOneAsync(x => x.email == password.Email,
                            Builders<RegisterModel>.Update.Set(x => x.password, password.Password));
                        return emailExist;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Unique token</returns>
        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                      { new Claim(ClaimTypes.Email, email) }),
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
