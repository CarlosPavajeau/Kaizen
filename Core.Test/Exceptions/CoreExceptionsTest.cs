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
                throw new IncorrectPasswordException();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<IncorrectPasswordException>(exception, "Exception must be type of IncorrectPassword.");

                IncorrectPasswordException incorrectPassword = exception as IncorrectPasswordException;
                Assert.AreEqual(400, incorrectPassword.StatusCode);
                Assert.AreEqual("Contraseña incorrecta, intentelo de nuevo", incorrectPassword.Message);
            }
        }

        [Test]
        public void ThrowUserDoesNotExists()
        {
            try
            {
                throw new UserDoesNotExistsException();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<UserDoesNotExistsException>(exception, "Exception must be type of UserDoesNotExits.");

                UserDoesNotExistsException userDoesNotExists = exception as UserDoesNotExistsException;
                Assert.AreEqual(400, userDoesNotExists.StatusCode);
                Assert.AreEqual("Usuario no registrado", userDoesNotExists.Message);
            }
        }

        [Test]
        public void ThrowUserNotCreate()
        {
            try
            {
                throw new UserNotCreateException();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<UserNotCreateException>(exception, "Exception must be type of UserNotCreate.");

                UserNotCreateException userNotCreate = exception as UserNotCreateException;
                Assert.AreEqual(400, userNotCreate.StatusCode);
                Assert.AreEqual("Usuario no creado", userNotCreate.Message);
            }
        }
    }
}
