using System;
using Kaizen.Core.Exceptions.User;
using NUnit.Framework;

namespace Core.Test.Exceptions
{
    [TestFixture]
    public class CoreExceptionsTest
    {
        [Test]
        public void ThrowIncorrectPasswordException()
        {
            try
            {
                throw new IncorrectPassword();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<IncorrectPassword>(exception, "Exception must be type of IncorrectPassword.");

                IncorrectPassword incorrectPassword = exception as IncorrectPassword;
                Assert.AreEqual(400, incorrectPassword.StatusCode);
                Assert.AreEqual("Contraseña incorrecta, intentelo de nuevo", incorrectPassword.Message);
            }
        }

        [Test]
        public void ThrowUserDoesNotExists()
        {
            try
            {
                throw new UserDoesNotExists();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<UserDoesNotExists>(exception, "Exception must be type of UserDoesNotExits.");

                UserDoesNotExists userDoesNotExists = exception as UserDoesNotExists;
                Assert.AreEqual(400, userDoesNotExists.StatusCode);
                Assert.AreEqual("Usuario no registrado", userDoesNotExists.Message);
            }
        }

        [Test]
        public void ThrowUserNotCreate()
        {
            try
            {
                throw new UserNotCreate();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<UserNotCreate>(exception, "Exception must be type of UserNotCreate.");

                UserNotCreate userNotCreate = exception as UserNotCreate;
                Assert.AreEqual(400, userNotCreate.StatusCode);
                Assert.AreEqual("Usuario no creado", userNotCreate.Message);
            }
        }
    }
}
